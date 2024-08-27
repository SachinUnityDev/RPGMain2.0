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

        public Sprite LockedSkillIconSprite;
        public Sprite NASkillIconSprite;
        [SerializeField] Transform currTransHovered;


        [Header("SKILL CARD PARAMS")]
        public SkillHexSO skillHexSO;
        public bool pointerOnSkillCard =false;
        public bool pointerOnSkillIcon = false; 


        [SerializeField] GameObject SkillPanel; 
        [SerializeField] GameObject textPrefab;
        [SerializeField] Vector2 posOffset;
        public int index;
        GameObject SkillCard; 
      
        public SkillController1 skillController;
        

#endregion

        void Start()
        {
            index = -1; 
     
            CombatEventService.Instance.OnCharClicked += SetSkillsPanel;
            CombatEventService.Instance.OnCharClicked += (CharController c)=> FillSkillClickedState(-1);
            CombatEventService.Instance.OnEOT += () => FillSkillClickedState(-1);
            CombatEventService.Instance.OnCombatInit += 
            (CombatState startState, LandscapeNames landscapeName, EnemyPackName enemyPackName) =>InitSkillBtns();
           // InitSkillBtns();
        }

        private void OnDisable()
        {
            CombatEventService.Instance.OnCharClicked -= SetSkillsPanel;
            CombatEventService.Instance.OnCharClicked -= (CharController c) => FillSkillClickedState(-1);
            CombatEventService.Instance.OnEOT -= () => FillSkillClickedState(-1);
            CombatEventService.Instance.OnCombatInit -= 
                (CombatState startState, LandscapeNames landscapeName, EnemyPackName enemyPackName) => InitSkillBtns();            
        }
        void InitSkillBtns()
        {
            // first ally in party set
            SetSkillsPanel(CharService.Instance.allCharsInPartyLocked[0]); 
        } 

        public void SkillBtnPressed(int index)
        {
        //    GameObject btn = EventSystem.current.currentSelectedGameObject;
        //    int index = btn.transform.GetSiblingIndex();
            // outliers
            if (CombatService.Instance.currCharOnTurn.charModel.charMode == CharMode.Enemy) return;
            if (CombatService.Instance.currCharClicked.charModel.charID != CombatService.Instance.currCharOnTurn.charModel.charID)
                return; 
          
            SkillDataSO skillSO = SkillService.Instance
                        .GetSkillSO(CombatService.Instance.currCharOnTurn.charModel.charName);

            if (skillSO != null)
            {
                if (index < skillSO.allSkills.Count) // -1 retaliate skill correction
                    SkillService.Instance.currSkillName = skillSO.allSkills[index].skillName;
                else
                    return;
            }
            else
            {
                return;
            }
            SkillModel skillModel = SkillService.Instance.currSkillController.GetSkillModel(SkillService.Instance.currSkillName);
  
            SkillService.Instance.currSkillModel = skillModel; 

            if (SkillService.Instance.currSkillController != null)
            {
                SkillService.Instance.ClearPrevSkillData();
            }
            FillSkillClickedState(index);        
            SkillService.Instance.On_SkillSelected
                (CombatService.Instance.currCharOnTurn.charModel.charName, SkillService.Instance.currSkillName);
        }

        public void UnClickAllSkills()
        {
            foreach (Transform child in transform)
            {              
                child.GetComponent<SkillBtnViewCombat>().SetUnClick();
            }
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
            GridService.Instance.ClearOldTargetsOnGrid();
            UnClickAllSkills();
          
            CharNames currCharName = CombatService.Instance.currCharClicked.charModel.charName;
            int currClickedCharID = CombatService.Instance.currCharClicked.charModel.charID;
            foreach (SkillDataSO skillSO in SkillService.Instance.allSkillDataSO)
            {
                if (skillSO.charName == currCharName)
                {
                    for (int i = 0; i < skillSO.allSkills.Count; i++)
                    {
                        if (skillSO.allSkills[i].skillUnLockStatus == 1)
                        {
                            if (skillSO.allSkills[i].skillType == SkillTypeCombat.Retaliate) // Skipping retaliate skill from 
                                continue;
                            Transform skillIconTranform = transform.GetChild(i);
                            skillIconTranform.GetComponent<Image>().sprite
                                                                = skillSO.allSkills[i].skillIconSprite;
                            SkillNames skillName = skillSO.allSkills[i].skillName;

                            SkillModel skillModel = SkillService.Instance.GetSkillModel(currClickedCharID, skillName);
                            SkillBtnViewCombat skillBtn = skillIconTranform.GetComponent<SkillBtnViewCombat>();
                            if (skillModel == null)
                            {
                                Debug.Log("SkillMModel missing" + skillName);
                                return;
                            }
                            //if(i != index)
                            //{
                              
                            //    if (skillModel.GetSkillState() == SkillSelectState.Clicked)
                            //            skillController.UpdateSkillState(skillModel);
                            //}
                            if(i== index)
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
            skillBtnTransform.GetComponent<SkillBtnViewCombat>().SetClicked();
           // skillBtnTransform.GetChild(1).GetComponent<RectTransform>().localScale = Vector3.one * 1.25f;  
        }

        public void SetSkillsPanel(CharController charController)
        {
            if (charController == null) return; 
            CharNames charName = charController.charModel.charName;

            SkillController1 skillController = charController.skillController;
            SkillDataSO skillSO = SkillService.Instance.GetSkillSO(charName);
            int j = 0; 
            foreach ( SkillModel skillModel in skillController.charSkillModel.allSkillModels)
            {
                if (skillModel.skillType == SkillTypeCombat.Retaliate) // Skipping retaliate skill from 
                    continue;
                SkillBtnViewCombat skillBtn = transform.GetChild(j).GetComponent<SkillBtnViewCombat>();
                
                skillController.UpdateSkillState(skillModel);
                skillBtn.RefreshIconAsPerState();
                skillBtn.SkillBtnInit(skillSO, skillModel, this);
                j++; 
            }
            if (skillSO.passiveSkills.Count > 0)
            {
                foreach (PassiveSkillData pSKillData in skillSO.passiveSkills)
                {
                    SkillBtnViewCombat skillBtn = transform.GetChild(j).GetComponent<SkillBtnViewCombat>();
                    skillBtn.PSkillBtnInit(skillSO, pSKillData, this); 
                    j++;
                }
            }
            for (int i = j; i < transform.childCount; i++)
            {
                SkillBtnViewCombat skillBtn = transform.GetChild(i).GetComponent<SkillBtnViewCombat>();
                skillBtn.SkillBtnInit(null, null, this); 
            }
        }
    }
}