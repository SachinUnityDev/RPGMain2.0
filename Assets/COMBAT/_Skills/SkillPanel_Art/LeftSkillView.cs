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

        [Header("Skill Button")]
        [SerializeField] Button skillPlusBtn;

        [Header("Inv Skill Main Panel")]
        public InvSkillViewMain skillViewMain;

        [Header("NTBR")]
        [SerializeField] Transform charNameTrans;
        [SerializeField] Transform iconContainerTrans;
        [SerializeField] Transform skillPtsTrans; 


        private void Start()
        {
            charNameTrans = transform.GetChild(0); 
            iconContainerTrans = transform.GetChild(1);
            InvService.Instance.OnCharSelectInvPanel += PopulateLeftSkillPanel;
            skillPlusBtn.onClick.AddListener(OnSkillPlusBtnPressed); 
        }
            
        void OnSkillPlusBtnPressed()
        {


        }
        void PopulateLeftSkillPanel(CharModel charModel)
        {
            charController = InvService.Instance.charSelectController;
            selectSkillController = charController.GetComponent<SkillController1>();
            skillDataSO =
                    SkillService.Instance.GetSkillSO(charController.charModel.charName);

            charNameTrans.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                                                                charModel.charNameStr;

            transform.GetComponent<Image>().sprite = skillDataSO.leftInvSkillPanelBG;


            PopulateTheMainSkills();
            PopulateTheUtilitySkills();
            PopulateTheCampingSkillsAndUzu();    
        }
        void PopulateTheMainSkills()
        {
            Transform mainSkillTrans = iconContainerTrans.GetChild(0).GetChild(1);
            for (int i = 0; i < 4; i++)
            {        
                SkillNames skillName = skillDataSO.allSkills[i].skillName; 
                mainSkillTrans.GetChild(i).GetComponent<InvSkillBtnPtrEvents>().Init(skillDataSO, skillName, this); 
            }

        }
        void PopulateTheUtilitySkills()
        {
            Transform utilitySkillTrans = iconContainerTrans.GetChild(1).GetChild(1);
            SkillNames skillname = skillDataSO.allSkills.Find(t => t.skillType == SkillTypeCombat.Move).skillName;
                      

            utilitySkillTrans.GetChild(0).GetComponent<InvSkillBtnPtrEvents>().Init(skillDataSO, skillname, this);
            skillname = skillDataSO.allSkills.Find(t => t.skillType == SkillTypeCombat.Patience).skillName;


            utilitySkillTrans.GetChild(1).GetComponent<InvSkillBtnPtrEvents>().Init(skillDataSO, skillname, this);
            int i = skillDataSO.allSkills.FindIndex(t => t.skillType == SkillTypeCombat.Weapon); // to prevent null error 

            if (i != -1)
            {
                utilitySkillTrans.parent.GetChild(0).GetChild(1).gameObject.SetActive(true);// weapon heading
                utilitySkillTrans.GetChild(2).gameObject.SetActive(true);             
                utilitySkillTrans.GetChild(2).GetComponent<InvSkillBtnPtrEvents>().Init(skillDataSO
                                                                , skillDataSO.allSkills[i].skillName, this);
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
                campSkillTrans.GetChild(2).GetComponent<InvSkillBtnPtrEvents>().Init(skillDataSO
                    , skillDataSO.allSkills[i].skillName, this);
            }
            else
            {
                campSkillTrans.parent.GetChild(0).GetChild(1).gameObject.SetActive(false);// Uzu heading 
                campSkillTrans.GetChild(2).gameObject.SetActive(false);
            }
      
            //SkillData skillData = skillDataSO.allSkills.Find(t => t.skillType == SkillTypeCombat.Uzu);
            //if(skillData != null)
            //{
            //    campSkillTrans.GetChild(2).gameObject.SetActive(true);
            //    campSkillTrans.GetChild(2).GetComponent<Image>().sprite = skillData.skillIconSprite;
            //}
            //else
            //{
            //    campSkillTrans.GetChild(2).gameObject.SetActive(false);
            //}
        }
        #region CLICKED UNSKILLED STATE

        // if one is clicked .. unclick all others
        public void UnClickAllSkillState()
        {
            foreach(var child in transform.GetComponentsInChildren<InvSkillBtnPtrEvents>())
            {
                child.SetUnClick(); 
            }
        }


        #endregion 
        void PopulateTheSkillPoints()
        {

        }
    }
}