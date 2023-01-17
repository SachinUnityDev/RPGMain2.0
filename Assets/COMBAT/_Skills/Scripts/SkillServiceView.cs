using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events; 
using DG.Tweening;
using TMPro;
using Common;

namespace Combat
{
    //[System.Serializable]
    //public class SkillCardData
    //{
    //    public SkillModel skillModel;

    //}

    public class SkillServiceView : MonoSingletonGeneric<SkillServiceView>
    {
#region Declarations
        // algo for sprite toggles. 
        GameObject skillPanel;
        const int skillBtnCount = 8; 

      //  [SerializeField] List<Button> SkillBtns = new List<Button>();
        [SerializeField] Button optionsBtn;
        [SerializeField] Button fleeBtn;
        [SerializeField] Sprite LockedSkillIconSprite;
        [SerializeField] Sprite NASkillIconSprite; 
        [SerializeField] Transform currTransHovered;

        [Header("DRAG REFERENCE")]
        [SerializeField] PerkSelectionController perkSelectionController;


        [Header("SKILL CARD PARAMS")]
        public SkillHexSO skillHexSO;
        public bool pointerOnSkillCard =false;
        public bool pointerOnSkillIcon = false; 

        //[SerializeField] List<GameObject> allySkillCards = new List<GameObject>();
        //[SerializeField] List<GameObject> enemySkillCards = new List<GameObject>();
        [SerializeField] GameObject SkillPanel; 
       // [SerializeField] GameObject currSkillCard;
        [SerializeField] GameObject textPrefab;
        [SerializeField] Vector2 posOffset;
        public int index;
        GameObject SkillCard; 
      
        public SkillController1 skillController;
        //public SkillBase skillBase;

#endregion

        void Start()
        {
            index = -1; 
            optionsBtn.onClick.AddListener(OnOptionBtnPressed);

            skillPanel = GameObject.FindGameObjectWithTag("SkillPanel");

            //CombatEventService.Instance.OnSOTactics +=
            //   () => SetSkillsPanel(CombatService.Instance.defaultChar.charModel.charName);
            CombatEventService.Instance.OnSOT +=
              () => SetSkillsPanel(CombatService.Instance.currCharOnTurn.charModel.charID);

            CombatEventService.Instance.OnCharClicked += 
                ()=>SetSkillsPanel(CombatService.Instance.currCharClicked.charModel.charID);
            CombatEventService.Instance.OnCharClicked +=
                                                 () => PopulateSkillClickedState(-1);
            CombatEventService.Instance.OnEOT += () => PopulateSkillClickedState(-1);

        }

       

        public void OnOptionBtnPressed()
        {
            perkSelectionController.On_OptionBtnPressed(); 
        }
        
        public void OnFleeBtnPressed()
        {

        }

        public void SkillBtnPressed()
        {
            GameObject btn = EventSystem.current.currentSelectedGameObject;
            int index = btn.transform.GetSiblingIndex();
            // outliers
            if (CombatService.Instance.currCharOnTurn.charModel.charMode == CharMode.Enemy) return;
            if (CombatService.Instance.currCharClicked.charModel.charID != CombatService.Instance.currCharOnTurn.charModel.charID)
                return; 

            SkillDataSO skillSO = SkillService.Instance
                        .GetSkillSO(CombatService.Instance.currCharOnTurn.charModel.charName);

            if (skillSO != null)
            {
                if (index < skillSO.allSkills.Count)
                    SkillService.Instance.currSkillName = skillSO.allSkills[index].skillName;
                else
                    return;
            }
            else
                return; 
         
            if (SkillService.Instance.currSkillController != null)
            {
                SkillService.Instance.ClearPrevSkillData();
            }
            PopulateSkillClickedState(index);        
            SkillService.Instance.On_SkillSelected
                (CombatService.Instance.currCharOnTurn.charModel.charName);
        }
      
        public SkillSelectState UpdateSkillState(SkillModel _skillModel)
        {
  
           // Debug.Log("SKILL NAME " + _skillModel.skillName + "TARGETS" + _skillModel.targetPos.Count);
            if (CombatService.Instance.combatState == CombatState.INTactics)
            {
                return SkillSelectState.UnClickable_InTactics;
            }            
            if (CombatService.Instance.currCharClicked != CombatService.Instance.currCharOnTurn)
            {                        
                return SkillSelectState.Unclickable_notCharsTurn; 
            }
            else if (IfInCoolDown(_skillModel))      // only char on turn will get here 
            {              
                return SkillSelectState.UnClickable_InCd; 
            }
            else if (IsNotOnCastPos(_skillModel))     // not on cast pos 
            {
                
                return SkillSelectState.Unclickable_notOnCastPos; 
            }
            else if (NoTargetsInRange(_skillModel))
            {               
                 return SkillSelectState.UnClickable_NoTargets; 
            }
            else if (HasNoStamina(_skillModel))
            {                
                return SkillSelectState.UnClickable_NoStamina; 
            }
            else if (_skillModel.skillInclination == SkillInclination.Passive)
            {               
                return SkillSelectState.Unclickable_passiveSkills; 
            }
            else
            {              
                return SkillSelectState.Clickable; 
            }
        }

        public bool HasNoStamina(SkillModel _skillModel)
        {
            StatData staminaData = CharService.Instance.GetCharCtrlWithCharID(_skillModel.charID).GetStat(StatsName.stamina);
            float stamina = staminaData.currValue; 

            if (stamina < _skillModel.staminaReq)
            {
                return true; 
            }
            return false; 
        }    

        bool NoTargetsInRange(SkillModel _skillModel)
        {          

            if(_skillModel.skillType == SkillTypeCombat.Move || _skillModel.attackType == AttackType.Remote)
            {
              // Checks only target Pos as skill is used on empty tile 
                if (_skillModel.targetPos.Count != 0) return false;
                else return true; 

            }
            else
            {  // get dyna from target pos
                if (SkillService.Instance.GetTargetInRange(_skillModel) == null)
                {
                   // Debug.Log("return null targets due to no DYNA");     
                    return true;
                } 
                if (SkillService.Instance.GetTargetInRange(_skillModel).Count == 0)
                {
                   /// Debug.Log("return ZERO targets due to no DYNA");
                    return true;
                }
                else
                {
                   // Debug.Log("return HAS targets");
                    return false;
                }
            }         
        }
        
        bool IsNotOnCastPos(SkillModel _skillModel)
        {
            GameObject charGO = CharService.Instance.GetCharGOWithName(_skillModel.charName, _skillModel.charID);

            int pos = GridService.Instance.GetDyna4GO(charGO).currentPos;
           // Debug.Log("Position in" + pos);
            
            return !(_skillModel.castPos.Any(t=>t == pos));
        }
        
        bool IfInCoolDown(SkillModel _skillModel)
        {
            if (_skillModel.cd == -5) return false;
            if (_skillModel.lastUsedInRound == -5) return false; 
            int rdDiff =CombatService.Instance.currentRound - _skillModel.lastUsedInRound;
           // Debug.Log("CD diff " + rdDiff); 
            if (rdDiff <= _skillModel.cd)
            {
                return true; 
            }
            return false; 
        }

        public void UpdateSkillBtntxt(CharNames _charName,SkillNames _skillName, int posOnSkillPanel)
        {



            //SkillModel skillModel = SkillService.Instance.GetSkillModel(_charName, _skillName); 
          
            //int cdGap = CombatService.Instance.currentRound - skillModel.lastUsedInRound;
            //string displayTxt = "";
            //if (cdGap <= 0)
            //    displayTxt = "";
            //else
            //    displayTxt = cdGap.ToString();
            //skillPanel.transform.GetChild(posOnSkillPanel).GetChild(0).GetComponent<TextMeshProUGUI>().text
            //    = displayTxt;
        }

        public void PopulateSkillClickedState(int index)  // -1 index => all skills frames are cleared
        {
            //SkillService.Instance.ClearPrevSkillData();
            GridService.Instance.ClearOldTargets();
            foreach (Transform child in skillPanel.transform)
            {
                child.GetChild(1).GetComponent<Image>().sprite = skillHexSO.SkillNormalFrame;
                child.GetChild(1).GetComponent<RectTransform>().localScale = Vector3.one;
            }

            CharNames currCharName = CombatService.Instance.currCharClicked.charModel.charName;
            int currClickedCharID = CombatService.Instance.currCharClicked.charModel.charID;
            foreach (SkillDataSO skillSO in SkillService.Instance.allCharSkillSO)
            {
                if (skillSO.charName == currCharName)
                {
                    for (int i = 0; i < skillSO.allSkills.Count; i++)
                    {
                        if (skillSO.allSkills[i].skillUnLockStatus == 1)
                        {
                            Transform skillIconTranform = skillPanel.transform.GetChild(i);
                            skillIconTranform.GetComponent<Image>().sprite
                                                                = skillSO.allSkills[i].skillIconSprite;
                            SkillNames skillName = skillSO.allSkills[i].skillName;

                            SkillModel skillModel = SkillService.Instance.GetSkillModel(currClickedCharID, skillName);
                            SkillBtnsPointerEvents skillBtn = skillIconTranform.GetComponent<SkillBtnsPointerEvents>();
                            if (skillModel == null)
                            {
                                Debug.Log("SkillMModel missing" + skillName);
                                return;
                            }
                                

                            if(i != index)
                            {
                                if (skillModel.GetSkillState() == SkillSelectState.Clicked)
                                    skillModel.SetSkillState(SkillSelectState.Clickable);
                            }
                            else
                            {
                                if (skillModel.GetSkillState() == SkillSelectState.Clickable)
                                    skillModel.SetSkillState(SkillSelectState.Clicked);
                                    Change2ClickedFrame(skillIconTranform); 
                            }
                            skillBtn.skillCardData = skillModel;
                            skillBtn.RefreshIconAsPerState();
                        }
                    }
                }
            }
        }


        public void Change2ClickedFrame(Transform skillBtnTransform)
        {
            skillBtnTransform.GetChild(1).GetComponent<Image>().sprite = skillHexSO.SkillSelectFrame;
            skillBtnTransform.GetChild(1).GetComponent<RectTransform>().localScale = Vector3.one * 1.25f;  
        }

        public void SetSkillsPanel(int  _charID)
        {
            CharController charController = CharService.Instance.GetCharCtrlWithCharID(_charID);
            if (charController == null) return; 
            CharNames charName = charController.charModel.charName; 

            foreach (SkillDataSO skillSO in SkillService.Instance.allCharSkillSO)
            {
                if (skillSO.charName == charName)
                {
                    for (int i = 0; i < skillSO.allSkills.Count; i++)
                    {
                        if (skillSO.allSkills[i].skillUnLockStatus == 1)
                        {                         
                            Transform skillIconTranform = skillPanel.transform.GetChild(i);
                            skillIconTranform.GetComponent<Image>().sprite
                                                                = skillSO.allSkills[i].skillIconSprite;
                            SkillNames skillName = skillSO.allSkills[i].skillName;

                            SkillModel skillModel = SkillService.Instance.GetSkillModel(_charID, skillName);
                            if (skillModel == null)
                                Debug.Log("SkillMModel missing" + skillName);
                            //else if(skillModel.skillName == SkillNames.CleansingWater)
                            //{
                            //   // Debug.Log("TARGET POS COUNT " + skillModel.targetPos.Count);
                            //}

                            SkillSelectState skillSelState = UpdateSkillState(skillModel); //<= updates the skillState in SkillModel
                            skillModel.SetSkillState(skillSelState);

                            SkillBtnsPointerEvents skillBtn = skillIconTranform.GetComponent<SkillBtnsPointerEvents>();

                            // Debug.Log("SkillModel Updates" + skillModel.GetSkillState()); 
                            skillBtn.skillCardData = skillModel;

                            skillBtn.RefreshIconAsPerState();

                        }
                        else if(skillSO.allSkills[i].skillUnLockStatus == 0)
                        {
                            //skillPanel.transform.GetChild(i).gameObject.SetActive(true);
                            skillPanel.transform.GetChild(i).GetComponent<Image>().sprite = LockedSkillIconSprite;
                        }else
                        {
                            skillPanel.transform.GetChild(i).GetComponent<Image>().sprite = NASkillIconSprite;
                        }                            
                    }
                    // to make the extra button as not available 
                    for (int i = skillSO.allSkills.Count; i < skillBtnCount; i++)
                    {
                        skillPanel.transform.GetChild(i).GetComponent<Image>().sprite = NASkillIconSprite;
                    }
                }
            }
        }
    }
}
//for (int i = 0; i < 8; i++)
//{
//    Button btn = skillPanel.transform.GetChild(i).GetComponent<Button>();
//    if (btn != null)
//    {
//        SkillBtns.Add(btn);
//       // btn.onClick.AddListener(SkillBtnPressed);                        
//    }
//}    


//public void UpdateSkillIconTxt(SkillNames skillName, int cdGap)
//{
//    // loop thru all the skill Icons.. if it matches the SkillName update txt


//    string displayTxt = "";
//    if (cdGap <= 0)
//        displayTxt = "";
//    else
//        displayTxt = cdGap.ToString(); 


//    for (int i = 0; i < skillBtnCount ; i++)
//    {
//        SkillDataSO skillSO = SkillService.Instance
//                .GetSkillSO(CombatService.Instance.currentCharSelected.charModel.charName);
//        if (skillSO != null && skillSO.allSkills[i].skillUnLockStatus !=-1) // Skill is not NA
//        {
//            Debug.Log("SKILL Index" + i); 
//           if(skillName == skillSO.allSkills[i].skillName)
//            {                       

//                skillPanel.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text
//                                                            = displayTxt; 
//            }
//        }
//    }
//}
