using Common;
using DG.Tweening;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
namespace Combat
{
    public class PortView : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("TBR")]
        [SerializeField] TextMeshProUGUI nametxt;
        [SerializeField] ExpDetailedView expDetailedView;
        [SerializeField] Image bgPortImg;
        [SerializeField] Image charPortImg;
        [SerializeField] Image frameImg; 

        

        [Header(" Lvl up Bird")]
        [SerializeField] Image lvlUpBird;


        [Header(" Lvl Bar TBR")]
        [SerializeField] Image lvlbarImg;
        [SerializeField] OnLvlBarHover onLvlBarHover; 
        [SerializeField] LvlNExpSO lvlNExpSO;

        [Header(" Char lvl Text")]
        [SerializeField] TextMeshProUGUI lvlTxt; 


        CombatEndView combatEndView; 

        [SerializeField] AllCharSO allCharSO; 
        [Header("Global var")]
        [SerializeField] CharModel charModel;
        [SerializeField]int sharedExp;
        [SerializeField] int manualExp;
        [SerializeField] int firstBloodExp;
        [SerializeField] int killsNSavesExp; 
        public void InitPortView(CharModel charModel, int sharedExp, CombatEndView combatEndView)
        {
            this.charModel = charModel;
            this.combatEndView= combatEndView;
            onLvlBarHover.InitOnLvlBarHover(charModel);
            FillPort(sharedExp);            
            ResetLvlUpBirds();
            CalcFirstBloodExp();
            CalcKillsNSavesExp();
            FillLvlExpBar(sharedExp + killsNSavesExp + firstBloodExp);
        }
        void CalcKillsNSavesExp()
        {
            CombatModel combatModel = CombatEventService.Instance.combatModel;
            float savesExp = combatModel.GetSavesExp(charModel.charID);     
            float killsExp = combatModel.GetKillsExp(charModel.charID);
            killsNSavesExp = sharedExp*(int)(savesExp + killsExp);            
        }
        void CalcFirstBloodExp()
        {
            if(charModel.charID == combatEndView.firstBloodChar.charModel.charID)
            {
                firstBloodExp = 6 * charModel.charLvl;
            }
            else
            {
                firstBloodExp = 0;
            }
        }
        void ResetLvlUpBirds()
        {
            Sequence seq = DOTween.Sequence();
            seq                
                .AppendCallback(() => lvlUpBird.gameObject.SetActive(false))
                .Append(lvlUpBird.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0f,-1000f), 0.4f, true))                
                ;
            seq.Play();
        }   
        void FillLvlExpBar(int expAdded)
        {           
            int deltaExp = lvlNExpSO.GetdeltaExpPts4Lvl(charModel.charLvl);
            int thresholdExp = lvlNExpSO.GetThresholdExpPts4Lvl(charModel.charLvl);
            lvlbarImg.DOFillAmount(((float)charModel.mainExp -thresholdExp) / deltaExp,0.1f);
            int currExp = charModel.mainExp + expAdded; 
            if (!charModel.LvlUpCharChk(currExp))
            {
                charModel.LvlNExpUpdate(currExp);
                float val = (float)(currExp - thresholdExp) / deltaExp;
                Sequence seq = DOTween.Sequence();
                seq
                    .AppendInterval(1f)
                    .Append(lvlbarImg.DOFillAmount(val, 0.4f))
                    ;
                seq.Play();
            }
            else
            {
                 charModel.LvlNExpUpdate(currExp);
                 deltaExp = lvlNExpSO.GetdeltaExpPts4Lvl(charModel.charLvl);
                 thresholdExp = lvlNExpSO.GetThresholdExpPts4Lvl(charModel.charLvl);

                float val1 = (float)(currExp - thresholdExp) / deltaExp;
                // exp gain in combat
                Sequence seq = DOTween.Sequence();

                seq
                    .AppendInterval(1f)
                    .Append(lvlbarImg.DOFillAmount(1, 0.4f))
                    .Append(lvlbarImg.DOFillAmount(0, 0.1f))
                    .AppendInterval(0.4f)
                    .AppendCallback(() => lvlUpBird.gameObject.SetActive(true))
                    .Append(lvlUpBird.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0f, -98f), 0.4f, true))
                    .Append(lvlbarImg.DOFillAmount(val1, 0.4f))
                    ;
                seq.Play(); 
            }
            lvlTxt.text = charModel.charLvl.ToString();
            onLvlBarHover.InitOnLvlBarHover(charModel);
        }
        void FillPort(int sharedExp)
        {
            
            CharacterSO charSO = CharService.Instance.allCharSO.GetCharSO(charModel.charName);
            CharComplimentarySO charCompSO = CharService.Instance.charComplimentarySO;

            ClassType classType = charModel.classType; 
            nametxt.text = classType.ToString().CreateSpace();
            bgPortImg.gameObject.SetActive(true);

            if (charModel.stateOfChar == StateOfChar.UnLocked)
            {                
                charPortImg.sprite = charSO.bpPortraitUnLocked;
                frameImg.sprite = charCompSO.frameAvail;
                lvlbarImg.sprite  = charCompSO.lvlBarAvail;
                Sprite BGUnClicked = charCompSO.BGAvailUnClicked;
                Sprite BGClicked = charCompSO.BGAvailClicked;

                bgPortImg.sprite = BGUnClicked;

                this.sharedExp = sharedExp;
            }
            else if(charModel.stateOfChar == StateOfChar.Fled ||
                charModel.stateOfChar == StateOfChar.Dead)// fled and dead
            {
                charPortImg.sprite = charSO.bpPortraitUnAvail;
                frameImg.sprite = charCompSO.frameUnavail;
                bgPortImg.sprite = charCompSO.BGUnavail;
                // SIDE BARS LVL
                lvlbarImg.sprite =  charCompSO.lvlbarUnAvail;
                this.sharedExp = 0;                
            }
            expDetailedView.InitExp(charModel, sharedExp, firstBloodExp, killsNSavesExp);

            //transform.GetChild(3).GetComponent<TextMeshProUGUI>().text
            //                                    = charModel.classType.ToString().CreateSpace();

        }
        public void AddManualExp()
        {
            manualExp = CombatService.Instance.GetManualExp();
            expDetailedView.AddManualExpDsply(manualExp);
            combatEndView.OnManualExpAwarded();
            FillLvlExpBar(manualExp);
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (CanAwardManualExp())
            {
                AddManualExp();
                combatEndView.manualExpRewarded = true;
            }
        }
        bool CanAwardManualExp()
        {
            if (charModel.charName == CharNames.Abbas) return false;            
            if (!combatEndView.manualExpBtnPressed) return false;
            if (combatEndView.manualExpRewarded) return false;
            return true; 
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(charModel.stateOfChar == StateOfChar.UnLocked)
            if (CanAwardManualExp())
            {
                bgPortImg.sprite = allCharSO.bgPortClicked;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (charModel.stateOfChar == StateOfChar.UnLocked)
                bgPortImg.sprite = allCharSO.bgPortUnClicked;
           
        }
    }
}