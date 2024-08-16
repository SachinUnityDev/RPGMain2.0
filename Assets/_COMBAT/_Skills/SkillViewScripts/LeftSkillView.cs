using Combat;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

namespace Common
{
    public class LeftSkillView : MonoBehaviour
    {
        CharController charController; 
        SkillController1 selectSkillController;
        SkillDataSO skillDataSO;

        [Header("Inv Skill Main Panel")]
        public InvSkillViewMain skillViewMain;

        [Header("NTBR")]
        [SerializeField] Transform charNameTrans;
        [SerializeField] Transform iconContainerTrans;
        [SerializeField] SkillPtsView skillPtsView; 


        private void Start()
        { 
            InvService.Instance.OnCharSelectInvPanel += FillLeftSkillPanel;
            // Get ABBAS
            CharModel charModel = CharService.Instance.GetAllyController(CharNames.Abbas).charModel;
            FillLeftSkillPanel(charModel);
            CharService.Instance.OnSkillPtsChg += On_SkillPtsChg;
        }
        private void OnDisable()
        {
            InvService.Instance.OnCharSelectInvPanel -= FillLeftSkillPanel;
            CharService.Instance.OnSkillPtsChg -= On_SkillPtsChg;
        }


        void On_SkillPtsChg(CharController charController, int skillval)
        {
            if(charController != InvService.Instance.charSelectController) return;
            skillPtsView.Init(this);    
        }
        #region POPULATE SKILL BTNS and Panel Content
        void FillLeftSkillPanel(CharModel charModel)
        {
            
            charController = InvService.Instance.charSelectController;
         //   if (charController == null) return;
            selectSkillController = charController.GetComponent<SkillController1>();
            skillDataSO =
                    SkillService.Instance.GetSkillSO(charController.charModel.charName);

            transform.GetComponent<Image>().sprite = skillDataSO.leftInvSkillPanelBG;

            PopulateTheMainSkills();
            PopulateTheUtilitySkills();
            PopulateTheCampingSkillsAndUzu();    
            skillPtsView.Init(this);
        }
        void PopulateTheMainSkills()
        {
            iconContainerTrans = transform.GetChild(1);
            Transform mainSkillTrans = iconContainerTrans.GetChild(0).GetChild(1);
            for (int i = 0; i < 4; i++)
            {        
                SkillNames skillName = skillDataSO.allSkills[i].skillName; 
                mainSkillTrans.GetChild(i).GetComponent<InvSkillBtnPtrEvents>()
                                        .Init(skillDataSO, skillName, this, true); 
            }
        }
        void PopulateTheUtilitySkills()
        {
            Transform utilitySkillTrans = iconContainerTrans.GetChild(1).GetChild(1);
            SkillNames skillname = skillDataSO.allSkills.Find(t => t.skillType == SkillTypeCombat.Move).skillName;
                      

            utilitySkillTrans.GetChild(0).GetComponent<InvSkillBtnPtrEvents>()
                                            .Init(skillDataSO, skillname, this, false);
            skillname = skillDataSO.allSkills.Find(t => t.skillType == SkillTypeCombat.Patience).skillName;


            utilitySkillTrans.GetChild(1).GetComponent<InvSkillBtnPtrEvents>()
                                            .Init(skillDataSO, skillname, this, false);
            int i = skillDataSO.allSkills.FindIndex(t => t.skillType == SkillTypeCombat.Weapon); // to prevent null error 

            if (i != -1)
            {
                utilitySkillTrans.parent.GetChild(0).GetChild(1).gameObject.SetActive(true);// weapon heading
                utilitySkillTrans.GetChild(2).gameObject.SetActive(true);             
                utilitySkillTrans.GetChild(2).GetComponent<InvSkillBtnPtrEvents>()
                    .Init(skillDataSO, skillDataSO.allSkills[i].skillName, this, false);
            }
            else
            {
                utilitySkillTrans.parent.GetChild(0).GetChild(1).gameObject.SetActive(false); // weapon heading
                utilitySkillTrans.GetChild(2).gameObject.SetActive(false);
            }                
        }
        void PopulateTheCampingSkillsAndUzu()
        {

            Transform campSkillTrans = iconContainerTrans.GetChild(2).GetChild(1);
            int i = skillDataSO.allSkills.FindIndex(t => t.skillType == SkillTypeCombat.Uzu);

            if (i != -1)
            {
                campSkillTrans.parent.GetChild(0).GetChild(1).gameObject.SetActive(true);
                campSkillTrans.GetChild(2).gameObject.SetActive(true);
                campSkillTrans.GetChild(2).GetComponent<InvSkillBtnPtrEvents>()
                    .Init(skillDataSO, skillDataSO.allSkills[i].skillName, this, false);
            }
            else
            {
                campSkillTrans.parent.GetChild(0).GetChild(1).gameObject.SetActive(false);// Uzu heading 
                campSkillTrans.GetChild(2).gameObject.SetActive(false);
            }
        }

        #endregion

        #region CLICKED UNSKILLED STATE
        public void UnClickAllSkillState()
        {
            foreach(var child in transform.GetComponentsInChildren<InvSkillBtnPtrEvents>())
            {
                child.SetUnClick(); 
            }
        }
        #endregion 
    
    }
}