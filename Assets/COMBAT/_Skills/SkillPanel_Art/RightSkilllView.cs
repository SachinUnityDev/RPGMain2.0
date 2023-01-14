using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using Interactables;
using TMPro;
using UnityEngine.UI;
using System.Linq;

namespace Common
{

    public class RightSkilllView : MonoBehaviour
    {
        CharController charController;
        SkillController1 selectSkillController;

        
        [SerializeField] SkillDataSO skillDataSO;

        [Header("SKill View SO to be ref")]
        [SerializeField] SkillViewSO skillViewSO;

        [Header("Inv Skill Main Panel")]
        public InvSkillViewMain skillViewMain;

        [Header("Back ground Image Char ref")]
        [SerializeField] Image CharBGImg;

        [Header("SkillScroll related")]
        float prevLeftClick = 0f; 
        float prevRightClick = 0f;
        int index = -1;
        [SerializeField] Button leftBtn;
        [SerializeField] Button rightBtn;
        [SerializeField] Transform skillScrollTrans;
        [SerializeField] Transform perkBtnContainer;

        [SerializeField] List<SkillModel> scrollList = new List<SkillModel>();  
        private void Start()
        {
            
            InvService.Instance.OnCharSelectInvPanel += PopulateRightSkillPanel;
            leftBtn.onClick.AddListener(OnLeftBtnPressed);
            rightBtn.onClick.AddListener(OnRightBtnPressed);
            skillViewMain.OnSkillSelectedInPanel += PopulateSkillScroll; 


        }
        void PopulateRightSkillPanel(CharModel charModel)
        {
            charController = InvService.Instance.charSelectController;
            selectSkillController = charController.skillController;
            skillDataSO =
                    SkillService.Instance.GetSkillSO(charController.charModel.charName);

            CharBGImg.sprite = skillDataSO.rightInvSkillPanelBG;
            scrollList.Clear(); 
            for (int i = 0; i < 4; i++)
            {
                scrollList.Add(selectSkillController.allSkillModels[i]); 
            }
            if(charController.charModel.charName == CharNames.Abbas_Skirmisher)
            {
                SkillModel skillModel = selectSkillController.allSkillModels
                                        .Find(t => t.skillType == SkillTypeCombat.Uzu); 
                scrollList.Add(skillModel);
            }

            PopulateSkillScroll(scrollList[0]); 

            //PopulateTheMainSkills();
            //PopulateTheUtilitySkills();
            //PopulateTheCampingSkillsAndUzu();
            //// get skill SO from the skillService
            // get skillController
            // skillModelData
        }

        void OnLeftBtnPressed()
        {
            if (Time.time - prevLeftClick < 0.3f) return;
            --index;
            if (index < 0)
            {
                index = 0;
            }
            else
            {
                skillViewMain.On_SkillSelectedInPanel(scrollList[index]);
            }
            prevLeftClick = Time.time;
        }
        void OnRightBtnPressed()
        {
            if (Time.time - prevRightClick < 0.3f) return;  
            ++index;
            if (index >= scrollList.Count)
            {                
                index = scrollList.Count;
            }
            else
            {
                skillViewMain.On_SkillSelectedInPanel(scrollList[index]);          
            }
            prevRightClick = Time.time;
        }

        void PopulateSkillScroll(SkillModel skillModel)
        {

            if(skillModel != null)
            {
                skillScrollTrans.GetChild(0).GetComponent<TextMeshProUGUI>().text
                                            = skillModel.skillName.ToString();
            }
            // get skillPerkData and Print PerkPanel 
            List<PerkData> allPerkData = selectSkillController.GetSkillPerkData(skillModel.skillName);
            int i = 0;
            if (allPerkData == null) return;
            foreach (PerkData perkData in allPerkData)
            {
                PerkType perkType = perkData.perkType;
                i = (int)perkType - 1;

                perkBtnContainer.GetChild(i).GetComponent<PerkBtnPtrEvents>().Init(perkData, skillViewMain);               

            }


            //for (int i = 0; i < perkBtnContainer.childCount; i++)
            //{
            //    PerkType perkType = (PerkType)(i + 1);
            //    PerkData skillPerkData = allPerkData.Find(t => t.perkType == perkType);
            //    if (skillPerkData != null)
            //    {
            //        perkBtnContainer.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text
            //                                = skillPerkData.perkName.ToString();
            //        SetPerkBtnImage(perkBtnContainer.GetChild(i), skillPerkData.state);
            //    }
            //}


            //RosterService.Instance.On_ScrollSelectCharModel(unLockedChars[index]);
            //if (RosterService.Instance.scrollSelectCharModel.charName == CharNames.Abbas_Skirmisher)
            //{
            //    index++;
            //    RosterService.Instance.scrollSelectCharModel = unLockedChars[index];
            //}
            //Debug.Log("RosterService" + RosterService.Instance.scrollSelectCharModel.charName);
            //PopulatePortrait();
            //PopulateSidePlank();
        }

    

        void PopulatePerksPanel(SkillNames skillName)
        {
            // get all perk data from skillController1

        }

    }
}