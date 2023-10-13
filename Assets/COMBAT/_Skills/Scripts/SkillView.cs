using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
using Common;

namespace Combat
{
    public class SkillView: MonoBehaviour
    {
#region Declarations
        const int skillBtnCount = 8;

        [SerializeField] Sprite LockedSkillIconSprite;
        [SerializeField] Sprite NASkillIconSprite;
        [SerializeField] Transform currTransHovered;


        [Header("SKILL CARD PARAMS")]
        public SkillHexSO skillHexSO;
        public bool pointerOnSkillCard =false;
        public bool pointerOnSkillIcon = false; 


        [SerializeField] GameObject SkillPanel; 
       // [SerializeField] GameObject currSkillCard;
        [SerializeField] GameObject textPrefab;
        [SerializeField] Vector2 posOffset;
        public int index;
        GameObject SkillCard; 
      
        public SkillController1 skillController;
        //public SkillBase skillBase;

#endregion

        void OnEnable()
        {
            index = -1; 
     
            //CombatEventService.Instance.OnSOTactics +=
            //   () => SetSkillsPanel(CombatService.Instance.defaultChar.charModel.charName);
            CombatEventService.Instance.OnCharOnTurnSet +=
              (CharController charController) => SetSkillsPanel(charController.charModel.charID);

            CombatEventService.Instance.OnCharClicked += 
                ()=>SetSkillsPanel(CombatService.Instance.currCharClicked.charModel.charID);
            CombatEventService.Instance.OnCharClicked +=
                                                 () => FillSkillClickedState(-1);
            CombatEventService.Instance.OnEOT += () => FillSkillClickedState(-1);
            CombatEventService.Instance.OnSOTactics += InitSkillBtns;
        }

        private void OnDisable()
        {
            CombatEventService.Instance.OnSOT -=
            () => SetSkillsPanel(CombatService.Instance.currCharOnTurn.charModel.charID);
            CombatEventService.Instance.OnCharClicked -=
               () => SetSkillsPanel(CombatService.Instance.currCharClicked.charModel.charID);
            CombatEventService.Instance.OnCharClicked -=
                                                 () => FillSkillClickedState(-1);
            CombatEventService.Instance.OnEOT -= () => FillSkillClickedState(-1);
            CombatEventService.Instance.OnSOTactics -= InitSkillBtns;
        }
        void InitSkillBtns()
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<SkillBtnsPointerEvents>().InitSkillBtns(this); 
            }
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

            SkillModel skillModel = SkillService.Instance.currSkillController.GetSkillModel(SkillService.Instance.currSkillName);
            if (skillModel.GetSkillState() != SkillSelectState.Clickable)
                return;

            SkillService.Instance.currSkillModel = skillModel; 

            if (SkillService.Instance.currSkillController != null)
            {
                SkillService.Instance.ClearPrevSkillData();
            }
            FillSkillClickedState(index);        
            SkillService.Instance.On_SkillSelected
                (CombatService.Instance.currCharOnTurn.charModel.charName, SkillService.Instance.currSkillName);
        }
      
       

        public SkillSelectState UpdateSkillState(SkillModel _skillModel)
        {
  
           // Debug.Log("SKILL NAME " + _skillModel.skillName + "TARGETS" + _skillModel.targetPos.Count);
            if (CombatService.Instance.combatState == CombatState.INTactics)
            {
                _skillModel.SetSkillState(SkillSelectState.UnClickable_InTactics); 
                return SkillSelectState.UnClickable_InTactics;
            }            
            if (CombatService.Instance.currCharClicked != CombatService.Instance.currCharOnTurn)
            {
                _skillModel.SetSkillState(SkillSelectState.Unclickable_notCharsTurn);
                return SkillSelectState.Unclickable_notCharsTurn; 
            }
            else if (HasNoChkActionPts())
            {
                _skillModel.SetSkillState(SkillSelectState.UnClickable_NoActionPts);
                return SkillSelectState.UnClickable_NoActionPts;
            }
            else if (IfInCoolDown(_skillModel))      // only char on turn will get here 
            {
                _skillModel.SetSkillState(SkillSelectState.UnClickable_InCd);
                return SkillSelectState.UnClickable_InCd; 
            }
            else if (IsNotOnCastPos(_skillModel))     // not on cast pos 
            {
                _skillModel.SetSkillState(SkillSelectState.Unclickable_notOnCastPos);
                return SkillSelectState.Unclickable_notOnCastPos; 
            }
            else if (NoTargetsInRange(_skillModel))
            {
                _skillModel.SetSkillState(SkillSelectState.UnClickable_NoTargets);
                return SkillSelectState.UnClickable_NoTargets; 
            }
            else if (HasNoStamina(_skillModel))
            {
                _skillModel.SetSkillState(SkillSelectState.UnClickable_NoStamina);
                return SkillSelectState.UnClickable_NoStamina; 
            }
            else if (_skillModel.skillInclination == SkillInclination.Passive)  // as enemies only // more like traits
            {
                _skillModel.SetSkillState(SkillSelectState.Unclickable_passiveSkills);
                return SkillSelectState.Unclickable_passiveSkills; 
            }
            else
            {
                _skillModel.SetSkillState(SkillSelectState.Clickable); 
                return SkillSelectState.Clickable;
            }
        }

        bool HasNoChkActionPts()
        {
            CharController charController = CombatService.Instance.currCharOnTurn;
            if (charController.charModel.charMode == CharMode.Enemy)
                return false; 
            CombatController combatController = charController.GetComponent<CombatController>();

            if (combatController.actionPts > 0)
                return false;
            return true; 
        }

        public bool HasNoStamina(SkillModel _skillModel)
        {
            StatData staminaData = CharService.Instance.GetCharCtrlWithCharID(_skillModel.charID).GetStat(StatName.stamina);
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
            if (rdDiff >=_skillModel.cd)
            {
                return false; 
            }
            return true; 
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

        public void FillSkillClickedState(int index)  // -1 index => all skills frames are cleared
        {
            //SkillService.Instance.ClearPrevSkillData();
            GridService.Instance.ClearOldTargets();
            foreach (Transform child in transform)
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
                            Transform skillIconTranform = transform.GetChild(i);
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
                            skillBtn.skillModel = skillModel;
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


        void UpdateAllSkillBtnState(CharController charController)
        {
            // get char controller 
            // 
            if (charController == null) return;
            SkillController1 skillController = charController.skillController;

            for (int i = 0; i < skillController.allSkillModels.Count; i++)
            {
                if (skillController.allSkillModels[i].GetSkillState()== SkillSelectState.Clickable)
                {
                    // image set active false       
                }
                else
                {
                    // image set active true .. change color
                }
            }

            //for (SkillModel skillmodel in skillController.allSkillModels)
            //{
            //    if(skillmodel.GetSkillState() == SkillSelectState.Clickable)
            //    {

            //    }
            //}

           

        }

        public void SetSkillsPanel(int  _charID)
        {
            CharController charController = CharService.Instance.GetCharCtrlWithCharID(_charID);
            if (charController == null) return; 
            CharNames charName = charController.charModel.charName;
            // SET ACTION POINTS 


            if(GameService.Instance.gameModel.gameState == GameState.InCombat)
            {
                CombatController combatController =
                                charController.GetComponent<CombatController>();
                if(combatController!= null && charController.charModel.charMode == CharMode.Ally) 
                    combatController.SetActionPts();
            }
           
            foreach (SkillDataSO skillSO in SkillService.Instance.allCharSkillSO)
            {
                if (skillSO.charName == charName)
                {
                    for (int i = 0; i < skillSO.allSkills.Count; i++)
                    {
                        
                        if (skillSO.allSkills[i].skillUnLockStatus == 1)
                        {                         
                            Transform skillIconTranform = transform.GetChild(i);
                            skillIconTranform.GetComponent<Image>().sprite
                                                                = skillSO.allSkills[i].skillIconSprite;
                            SkillNames skillName = skillSO.allSkills[i].skillName;

                            SkillModel skillModel = SkillService.Instance.GetSkillModel(_charID, skillName);
                            if (skillModel == null)
                                Debug.Log("SkillMModel missing" + skillName);

                            skillModel.SetSkillState(SkillSelectState.Clickable); // setting clicable here if unlcickable due to any reasons will be reset in update

                            UpdateSkillState(skillModel); //<= updates the skillState in SkillModel
                           // skillModel.SetSkillState(skillSelState);

                            SkillBtnsPointerEvents skillBtn = skillIconTranform.GetComponent<SkillBtnsPointerEvents>();

                            // Debug.Log("SkillModel Updates" + skillModel.GetSkillState()); 
                            skillBtn.skillModel = skillModel;

                            skillBtn.RefreshIconAsPerState();

                        }
                        else if(skillSO.allSkills[i].skillUnLockStatus == 0)
                        {
                            //skillPanel.transform.GetChild(i).gameObject.SetActive(true);
                            // setting clicable here if unlcickable due to any reasons will be reset in update
                            SkillNames skillName = skillSO.allSkills[i].skillName;
                            SkillModel skillModel = SkillService.Instance.GetSkillModel(_charID, skillName);
                            skillModel.SetSkillState(SkillSelectState.UnClickable_Locked);
                            transform.GetChild(i).GetComponent<Image>().sprite = LockedSkillIconSprite;
                        }else
                        {
                            transform.GetChild(i).GetComponent<Image>().sprite = NASkillIconSprite;
                        }                            
                    }
                    // to make the extra button as not available 
                    for (int i = skillSO.allSkills.Count; i < skillBtnCount; i++)
                    {
                        transform.GetChild(i).GetComponent<Image>().sprite = NASkillIconSprite;
                    }
                }
            }
        }
    }
}
