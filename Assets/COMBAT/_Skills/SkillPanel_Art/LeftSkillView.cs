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
            PopulateTheMainSkills();
            PopulateTheUtilitySkills();
            PopulateTheCampingSkills();
            // get skill SO from the skillService
            // get skillController
            // skillModelData
        }
        void PopulateTheMainSkills()
        {
            Transform mainSkillTrans = iconContainerTrans.GetChild(0).GetChild(1);
            for (int i = 0; i < 4; i++)
            {
                mainSkillTrans.GetChild(i).GetComponent<Image>().sprite =
                                        skillDataSO.allSkills[i].skillIconSprite;
                SkillData skillData = skillDataSO.allSkills[i];
                //int lvl = (int)(selectSkillController.GetSkillModelData(skillData.skillName).perkLvl); 
                //mainSkillTrans.GetChild(i).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = 
                //                lvl.ToString(); 

                mainSkillTrans.GetChild(i).GetComponent<InvSkillBtnPtrEvents>().Init(skillDataSO, skillData); 
            }

        }
        void PopulateTheUtilitySkills()
        {
            Transform utilitySkillTrans = iconContainerTrans.GetChild(1).GetChild(1);
            SkillData skillData = skillDataSO.allSkills.Find(t => t.skillType == SkillTypeCombat.Move);

            utilitySkillTrans.GetChild(0).GetComponent<Image>().sprite =
                                                       skillData.skillIconSprite;

            utilitySkillTrans.GetChild(0).GetComponent<InvSkillBtnPtrEvents>().Init(skillDataSO, skillData); 

             skillData = skillDataSO.allSkills.Find(t => t.skillType == SkillTypeCombat.Patience);
             utilitySkillTrans.GetChild(1).GetComponent<Image>().sprite =
                                                       skillData.skillIconSprite;

            utilitySkillTrans.GetChild(0).GetComponent<InvSkillBtnPtrEvents>().Init(skillDataSO, skillData);

            skillData = skillDataSO.allSkills.Find(t => t.skillType == SkillTypeCombat.Weapon);
            if(skillData != null)
            {
                utilitySkillTrans.GetChild(2).gameObject.SetActive(true); 
                utilitySkillTrans.GetChild(2).GetComponent<Image>().sprite = skillData.skillIconSprite;

                utilitySkillTrans.GetChild(0).GetComponent<InvSkillBtnPtrEvents>().Init(skillDataSO, skillData);
            }                
            else
                utilitySkillTrans.GetChild(2).gameObject.SetActive(false);           


        }
        void PopulateTheCampingSkills()
        {
            Transform campSkillTrans = iconContainerTrans.GetChild(2).GetChild(1);
            SkillData skillData = skillDataSO.allSkills.Find(t => t.skillType == SkillTypeCombat.Uzu);
            if(skillData != null)
            {
                campSkillTrans.GetChild(2).gameObject.SetActive(true);
                campSkillTrans.GetChild(2).GetComponent<Image>().sprite = skillData.skillIconSprite;
            }
            else
            {
                campSkillTrans.GetChild(2).gameObject.SetActive(false);
            }



        }
        void PopulateTheSkillPoints()
        {

        }
    }
}