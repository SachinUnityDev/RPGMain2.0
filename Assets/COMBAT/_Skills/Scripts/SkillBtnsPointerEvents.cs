using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Common;
using TMPro;
using System.Linq;
using System;

namespace Combat
{
    public class SkillBtnsPointerEvents : MonoBehaviour
                            , IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        const float waitTime = 0.1f;
        [SerializeField] int expBeyondLineNo = 3;
        [SerializeField] Vector2 skillCardBaseDim = new Vector2(328f, 325f);
        public SkillHexSO skillHexSO;
        //public GameObject skillCardPrefab; 

        [SerializeField] GameObject skillCard;
        [SerializeField] int index = -1;

        public SkillModel skillModel; 
        public SkillSelectState skillState;
        public SkillNames prevSkillHovered;

        [SerializeField] List<string> alliesDesc = new List<string>();
        [SerializeField] List<string> enemyDesc = new List<string>();
        [SerializeField] List<string> bothAllyNEnemy = new List<string>();
        [SerializeField] List<string> attackTypeLs = new List<string>();
        [SerializeField] List<string> damageTypeLS = new List<string>();
        [SerializeField] List<string> sunStrReturn = new List<string>();
        [SerializeField] List<string> finalDesc = new List<string>();

        [Header("SkillState Display")]
        [SerializeField] TextMeshProUGUI skillStateTxt;

        [Header("SKillView")]
        SkillView skillView;

        void Start()
        {
            prevSkillHovered = SkillNames.None;
        }

        public void InitSkillBtns(SkillView skillView)
        {
            this.skillView = skillView;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (skillModel.skillName == SkillNames.None) return;
            ShowSkillCard();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            skillView.index = -1;
            StartCoroutine(Wait());
        }
        
        public void ShowSkillCard()
        {
            skillCard.SetActive(true);
            skillView.pointerOnSkillIcon = true;
            SkillDataSO skillSO = SkillService.Instance
                       .GetSkillSO(CombatService.Instance.currCharClicked.charModel.charName);
            index = gameObject.transform.GetSiblingIndex();
            skillView.index = index;
            skillModel = SkillService.Instance.skillModelHovered;
            // UPDATE SKILL SERVICE 
            if (skillSO != null && index < skillSO.allSkills.Count)
                SkillService.Instance.On_SkillHovered(CombatService.Instance.currCharClicked.charModel.charName,
                            skillSO.allSkills[index].skillName);
            else { Debug.Log("Skill SO is null " + index); return; }

            // SkillService.Instance.currCharName = CombatService.Instance.currCharClicked.charModel.charName;





            //float htSkillIcon = gameObject.GetComponent<RectTransform>().rect.height;          

            //  SortSkillCardData();
            PopulateSkillCard();
        }

        public void PopulateSkillCard()
        {
            // Populate heading
            Transform skillBtnHovered = gameObject.transform;

            Transform Heading = skillCard.transform.GetChild(0).GetChild(1).GetChild(1);
            Heading.GetComponent<TextMeshProUGUI>().text = SkillService.Instance.currSkillHovered.ToString().CreateSpace();

            Transform Desc = skillCard.transform.GetChild(0).GetChild(2);
            ToggleTxt(Desc);

            //float htOfTxt = skillCard.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<RectTransform>().rect.height;
            int lines = skillModel.descLines.Count;
            if (lines > expBeyondLineNo)
            {
                int incr = lines - expBeyondLineNo;
                skillCard.GetComponent<RectTransform>().sizeDelta
                    = new Vector2(skillCardBaseDim.x, skillCardBaseDim.y + incr * 40f);

            }
            else
            {
                skillCard.GetComponent<RectTransform>().sizeDelta
                  = new Vector2(skillCardBaseDim.x, skillCardBaseDim.y);
            }
            float skillBtnHt = skillBtnHovered.GetComponent<RectTransform>().rect.height;
            float htSkillcard = skillCard.GetComponent<RectTransform>().rect.height;

            float xPos = skillBtnHovered.localPosition.x;
            float yPos = (skillBtnHt + htSkillcard / 3);
            Vector3 pos = new Vector3(xPos, yPos, 1);
            skillCard.GetComponent<RectTransform>().anchoredPosition = pos;

            for (int i = 0; i < skillModel.descLines.Count; i++)
            {
                Desc.transform.GetChild(i).gameObject.SetActive(true);
                Desc.transform.GetChild(i).gameObject.GetComponent<TextMeshProUGUI>().text
                                                                     = skillModel.descLines[i];

            }
            // populate round and stamina Info
            FillHexesInSkillCard();
            FillSkillCardLowerValues();
            FillSkillCardUpperCorners();
            PopulateSkillState();
        }

        public void PopulateSkillState()
        {
            // grab the UI and show the state
            skillStateTxt.text = skillModel.GetSkillState().ToString();

        }
        public void FillSkillCardLowerValues()
        {

            skillCard.transform.GetChild(1).GetChild(2).GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text
                = skillModel.staminaReq.ToString();
            skillCard.transform.GetChild(1).GetChild(3).GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text
               = skillModel.skillLvl.ToString();

            string str = "";
            if (skillModel.cd <= 0)
                str = "No cd";
            else
                str = $"{skillModel.cd} Rd";

            skillCard.transform.GetChild(0).GetChild(3).GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text
                = str;

        }
        public void FillSkillCardUpperCorners()
        {
            // get attack type from skillmodel
            // get respective sprite and background from skillHexSO
            // zero down on the images and allocate 
            AttackType attackType = skillModel.attackType;
            Sprite atSprite = skillHexSO.allAttacksSprites.Find(t => t.attackType == attackType).attackTypeSprite;
            Sprite bGSprite = skillHexSO.allAttacksSprites.Find(t => t.attackType == attackType).attackTypeBG;

            skillCard.transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = bGSprite;
            skillCard.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Image>().sprite = atSprite;

            SkillInclination skillIncli = skillModel.skillInclination;
            Sprite SLSprite = skillHexSO.allSkillIncli.Find(t => t.SkillIncliType == skillIncli).SkillIncliSprite;
            Sprite bgSpriteSIncli = skillHexSO.allSkillIncli.Find(t => t.SkillIncliType == skillIncli).SkillIncliBG;
            skillCard.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = bgSpriteSIncli;
            skillCard.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>().sprite = SLSprite;
        }

        public void FillHexesInSkillCard()
        {
            Sprite sprite1 = null, sprite2 = null, sprite3 = null;
            //Debug.Log("Skillcard data " + skillCardData); 

            if (skillModel.allPerkHexes.Count == 0) return;
            foreach (var ls in skillModel.allPerkHexes)
            {
                List<PerkType> SCperkChain = skillModel.perkChain.OrderBy(e => e).ToList();
                List<PerkType> LSPerkChain = ls.perkChain.OrderBy(e => e).ToList();

                if (SCperkChain.Count == 0)  // none case 
                {
                    sprite1 = skillHexSO.allHexes.Find(t => t.hexName
                            == skillModel.allPerkHexes[0].hexNames[0]).hexSprite;
                    sprite2 = skillHexSO.allHexes.Find(t => t.hexName
                          == skillModel.allPerkHexes[0].hexNames[1]).hexSprite;
                    sprite3 = skillHexSO.allHexes.Find(t => t.hexName
                          == skillModel.allPerkHexes[0].hexNames[2]).hexSprite;
                }
                else if (SCperkChain.SequenceEqual(LSPerkChain))
                {
                    sprite1 = skillHexSO.allHexes.Find(t => t.hexName == ls.hexNames[0]).hexSprite;
                    sprite2 = skillHexSO.allHexes.Find(t => t.hexName == ls.hexNames[1]).hexSprite;
                    sprite3 = skillHexSO.allHexes.Find(t => t.hexName == ls.hexNames[2]).hexSprite;
                }

                Transform hexParent = skillCard.transform.GetChild(0);
                hexParent.GetChild(0).GetChild(0).GetComponent<Image>().sprite = sprite1;
                hexParent.GetChild(0).GetChild(1).GetComponent<Image>().sprite = sprite2;
                hexParent.GetChild(0).GetChild(2).GetComponent<Image>().sprite = sprite3;

            }
        }

        public void RefreshIconAsPerState()
        {
            skillState = skillModel.GetSkillState();
            // ChangeSkillFrame(skillState);
            switch (skillState)
            {
                case SkillSelectState.Clickable:
                    gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    //ChangeSkillFrame(SkillSelectState.Clickable);
                    break;
                case SkillSelectState.UnClickable_InCd:
                    gameObject.GetComponent<Image>().color = new Color(0.5f, 1, 1, 0.8f);

                    break;
                case SkillSelectState.UnClickable_NoTargets:
                    gameObject.GetComponent<Image>().color = new Color(0.6f, 1, 1, 0.8f);

                    break;
                case SkillSelectState.UnClickable_NoStamina:
                    gameObject.GetComponent<Image>().color = new Color(0.9f, 1, 1, 0.8f);

                    break;
                case SkillSelectState.Unclickable_notOnCastPos:
                    gameObject.GetComponent<Image>().color = new Color(0.2f, 1, 1, 0.8f);

                    break;
                case SkillSelectState.Unclickable_passiveSkills:
                    gameObject.GetComponent<Image>().color = new Color(0.3f, 1, 1, 0.8f);

                    break;
                case SkillSelectState.Unclickable_notCharsTurn:
                    gameObject.GetComponent<Image>().color = new Color(1f, 1, 1, 0.9f);

                    break;
                case SkillSelectState.Clicked:
                    // change frame from skillSO
                    // ChangeSkillFrame(SkillSelectState.Clicked);
                    break;


                default:
                    break;
            }


        }

        #region HELPER Methods
        IEnumerator Wait()
        {
            yield return new WaitForSeconds(waitTime);
            if (!skillView.pointerOnSkillCard && skillView.index == -1)
                skillCard.SetActive(false);
        }

        void ToggleTxt(Transform transform)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }

        #endregion

        public void OnPointerClick(PointerEventData eventData)
        {

            if (eventData.button == PointerEventData.InputButton.Left)
                skillView.SkillBtnPressed();
            //if (eventData.button == PointerEventData.InputButton.Left)
            //    ChangeSkillFrame(SkillSelectState.Clickable); 
            //if (skillState == SkillSelectState.Clickable)
            //{
            //    SkillServiceView.Instance.SkillBtnPressed();             
            //    skillState = SkillSelectState.Clicked;
            //    skillCardData.skillModel.SetSkillState(skillState);               
            //}
            //else if(skillState == SkillSelectState.Clicked)
            //{               
            //    skillState = SkillSelectState.Clickable;
            //    skillCardData.skillModel.SetSkillState(skillState);               
            //}
            // ChangeSkillFrame(skillState);
        }
        public void ChangeSkillFrame(SkillSelectState _skillState)  // call from  skill Controller
        {
            foreach (Transform child in transform.parent)
            {
                child.GetChild(1).GetComponent<Image>().sprite = skillHexSO.SkillNormalFrame;
                child.GetChild(1).GetComponent<RectTransform>().localScale = Vector3.one;

            }
            if (_skillState == SkillSelectState.Clicked)
            {
                // transform.GetChild(1).GetComponent<Image>().sprite = skillHexSO.SkillSelectFrame;
                transform.GetChild(1).GetComponent<RectTransform>().localScale = Vector3.one * 1.25f;
            }

        }

        #region  ACTION METHODS

        void SortSkillCardData()
        {
            if (prevSkillHovered == SkillService.Instance.currSkillHovered) return;
            Debug.Log("Inside the SKILL SORT ");
            attackTypeLs = skillModel.descLines?.Where(t => t.Contains(AttackType.Ranged.ToString())).ToList();

            bothAllyNEnemy.Clear();
            bothAllyNEnemy = skillModel.descLines?.Where(t => t.Contains("<style=Enemy>") && t.Contains("<style=Allies>")).ToList();

            enemyDesc.Clear();
            enemyDesc = skillModel.descLines?.Where(t => t.Contains("<style=Enemy>")).Except(bothAllyNEnemy).ToList();

            alliesDesc.Clear();
            alliesDesc = skillModel.descLines?.Where(t => t.Contains("<style=Allies>")).Except(bothAllyNEnemy).ToList();

            //alliesDesc = SumUpLS(alliesDesc);
            //enemyDesc = SumUpLS(enemyDesc);
            //bothAllyNEnemy = SumUpLS(bothAllyNEnemy);

            finalDesc.Clear();
            finalDesc.AddRange(alliesDesc);
            finalDesc.AddRange(enemyDesc);
            finalDesc.AddRange(bothAllyNEnemy);
            finalDesc.Distinct().ToList();

            skillModel.descLines = finalDesc;
        }
        List<string> SumUpLS(List<string> stringLS)
        {
            sunStrReturn = new List<string>();

            List<string> damageTypeLS = SortbyDamageType(stringLS);
            List<string> attributesLS = SortByAttributes(stringLS);
            List<string> statesLS = SortByStates(stringLS);
            List<string> miscLS = stringLS.Except(damageTypeLS).Except(attributesLS).Except(statesLS).ToList();

            sunStrReturn.AddRange(damageTypeLS);
            sunStrReturn.AddRange(attributesLS);
            sunStrReturn.AddRange(statesLS);
            sunStrReturn.AddRange(miscLS);


            return sunStrReturn;
        }
        List<string> SortbyDamageType(List<string> stringLS)
        {
            List<string> strReturn = new List<string>();

            for (int i = 1; i < Enum.GetNames(typeof(DamageType)).Length; i++)
            {
                string str = ((DamageType)i).ToString();

                strReturn.AddRange(stringLS.Where(t => t.Contains(str)).ToList());
            }
            return strReturn;
        }

        List<string> SortByAttributes(List<string> stringLS)
        {
            List<string> strReturn = new List<string>();

            for (int i = 1; i < Enum.GetNames(typeof(AttribName)).Length; i++)
            {
                string str = ((AttribName)i).ToString();

                strReturn.AddRange(stringLS.Where(t => t.Contains(str)).ToList());
            }
            return strReturn;

        }
        List<string> SortByStates(List<string> stringLS)
        {
            List<string> strReturn = new List<string>();

            for (int i = 1; i < Enum.GetNames(typeof(CharStateName)).Length; i++)
            {
                string str = ((CharStateName)i).ToString();

                strReturn.AddRange(stringLS.Where(t => t.Contains(str)).ToList());
            }
            return strReturn;
        }

        #endregion
    }


}
// loop thru all the icons on charClicked and refresh
//public void GetSkillState(GameObject SkillIconObj)
//{
//    //return skillModel.skillSelState; 

//    ////int index2 = SkillIconObj.transform.GetSiblingIndex();
//    //CharController currSelChar = CombatService.Instance.currCharClicked;

//    //SkillDataSO skillSO = SkillService.Instance.GetSkillSO(currSelChar.charModel.charName);
//    //SkillNames skillName = skillSO.allSkills[index2].skillName;

//    // skillState = SkillService.Instance.GetSkillState(skillName); 
//}
////public void SetIconSkillState(SkillSelectState _skillState)
////{
////    skillState = _skillState; 
////}