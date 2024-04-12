using Combat;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Common
{
    public class BestiarySkillView : MonoBehaviour
    {

        const int skillBtnCount = 8;

        public Sprite LockedSkillIconSprite;
        public Sprite NASkillIconSprite;
        [SerializeField] Transform currTransHovered;



        [Header("SKILL CARD PARAMS")]
        public SkillHexSO skillHexSO;
        public bool pointerOnSkillCard = false;
        public bool pointerOnSkillIcon = false;


        BestiaryViewController bestiaryViewController; 


        [SerializeField] GameObject SkillPanel;
        [SerializeField] GameObject textPrefab;
        [SerializeField] Vector2 posOffset;
        public int index;
        GameObject SkillCard;

        void Start()
        {
            index = -1;

            //CombatEventService.Instance.OnCharClicked += SetSkillsPanel;
            //CombatEventService.Instance.OnCharClicked += (CharController c) => FillSkillClickedState(-1);
            //CombatEventService.Instance.OnEOT += () => FillSkillClickedState(-1);
            //CombatEventService.Instance.OnCombatInit +=
            //(CombatState startState, LandscapeNames landscape, EnemyPackName enemyPackName) => InitSkillBtns();
           // InitSkillBtns();
        }

        private void OnDisable()
        {
            //CombatEventService.Instance.OnCharClicked -= SetSkillsPanel;
            //CombatEventService.Instance.OnCharClicked -= (CharController c) => FillSkillClickedState(-1);
            //CombatEventService.Instance.OnEOT -= () => FillSkillClickedState(-1);
            //CombatEventService.Instance.OnCombatInit -=
            //    (CombatState startState, LandscapeNames landscape, EnemyPackName enemyPackName) => InitSkillBtns();
        }
        public void InitSkillBtns(CharModel charModel, BestiaryViewController bestiaryViewController)
        {
            // first ally in party set
            if (charModel == null)
                Debug.LogError(" No charModel provided"); 

            this.bestiaryViewController = bestiaryViewController;
            SetSkillsPanel(charModel); 
           
        }

  
        //public void UnClickAllSkills()
        //{
        //    foreach (Transform child in transform)
        //    {
        //        child.GetComponent<SkillBtnViewCombat>().SetUnClick();
        //    }
        //}


       
        //public void FillSkillClickedState(int index)  // -1 index => all skills frames are cleared
        //{
        //    //SkillService.Instance.ClearPrevSkillData();
        //    GridService.Instance.ClearOldTargetsOnGrid();
        //    UnClickAllSkills();

        //    CharNames currCharName = CombatService.Instance.currCharClicked.charModel.charName;
        //    int currClickedCharID = CombatService.Instance.currCharClicked.charModel.charID;
        //    foreach (SkillDataSO skillSO in SkillService.Instance.allSkillDataSO)
        //    {
        //        if (skillSO.charName == currCharName)
        //        {
        //            for (int i = 0; i < skillSO.allSkills.Count; i++)
        //            {
        //                if (skillSO.allSkills[i].skillUnLockStatus == 1)
        //                {
        //                    if (skillSO.allSkills[i].skillType == SkillTypeCombat.Retaliate) // Skipping retaliate skill from 
        //                        continue;
        //                    Transform skillIconTranform = transform.GetChild(i);
        //                    skillIconTranform.GetComponent<Image>().sprite
        //                                                        = skillSO.allSkills[i].skillIconSprite;
        //                    SkillNames skillName = skillSO.allSkills[i].skillName;

        //                    SkillModel skillModel = SkillService.Instance.GetSkillModel(currClickedCharID, skillName);
        //                    SkillBtnViewCombat skillBtn = skillIconTranform.GetComponent<SkillBtnViewCombat>();
        //                    if (skillModel == null)
        //                    {
        //                        Debug.Log("SkillMModel missing" + skillName);
        //                        return;
        //                    }
        //                    //if(i != index)
        //                    //{

        //                    //    if (skillModel.GetSkillState() == SkillSelectState.Clicked)
        //                    //            skillController.UpdateSkillState(skillModel);
        //                    //}
        //                    if (i == index)
        //                    {
        //                        if (skillModel.GetSkillState() == SkillSelectState.Clickable)
        //                            skillModel.SetSkillState(SkillSelectState.Clicked);
        //                        Change2ClickedFrame(skillIconTranform);
        //                    }
        //                    skillBtn.skillModel = skillModel;
        //                    skillBtn.RefreshIconAsPerState();
        //                }
        //            }
        //        }
        //    }
        //}
        //public void Change2ClickedFrame(Transform skillBtnTransform)
        //{
        //    skillBtnTransform.GetComponent<SkillBtnViewCombat>().SetClicked();
        //    // skillBtnTransform.GetChild(1).GetComponent<RectTransform>().localScale = Vector3.one * 1.25f;  
        //}

        public void SetSkillsPanel(CharModel charModel)
        {

            CharController charController = CharService.Instance.GetCharCtrlWithCharID(charModel.charID); 
            SkillDataSO skillSO = SkillService.Instance.GetSkillSO(charModel.charName);
            int j = 0;
            foreach (SkillData skillData in skillSO.allSkills)
            {
                if (skillData.skillType == SkillTypeCombat.Retaliate) // Skipping retaliate skill from 
                    continue;
                BestiarySkillViewPtrEvents skillBtn = transform.GetChild(j).GetComponent<BestiarySkillViewPtrEvents>();

               // skillController.UpdateSkillState(skillModel);
                skillBtn.RefreshIconAsPerState();
                skillBtn.SkillBtnInit(skillSO, skillData, this);
                j++;
            }
            if (skillSO.passiveSkills.Count > 0)
            {
                foreach (PassiveSkillData pSKillData in skillSO.passiveSkills)
                {
                    BestiarySkillViewPtrEvents skillBtn = transform.GetChild(j).GetComponent<BestiarySkillViewPtrEvents>();
                    skillBtn.PSkillBtnInit(skillSO, pSKillData, this);
                    j++;
                }
            }
            for (int i = j; i < transform.childCount; i++)
            {
                BestiarySkillViewPtrEvents skillBtn = transform.GetChild(i).GetComponent<BestiarySkillViewPtrEvents>();
                skillBtn.SkillBtnInit(null, null, this);
            }
        }
    }
}