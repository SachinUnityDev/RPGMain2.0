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

    public class RightSkillView : MonoBehaviour
    {
        CharController charController;
        SkillController1 selectSkillController;

        [Header("Pipe View Main")]
        public Transform BGPipe1; 
        public Transform BGPipe2;


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
        [SerializeField]int index = -1;
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

    
        }

        void OnLeftBtnPressed()
        {
            if (Time.time - prevLeftClick < 0.3f) return;
    
            if (index <= 0)
            {
                index = 0;
            }
            else
            {
                --index;
                skillViewMain.On_SkillSelectedInPanel(scrollList[index]);
            }
            prevLeftClick = Time.time;
        }
        void OnRightBtnPressed()
        {
            if (Time.time - prevRightClick < 0.3f) return;  
   
            if (index >= scrollList.Count-1)
            {                
                index = scrollList.Count-1;
            }
            else
            {
                ++index;
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
                perkBtnContainer.GetChild(i).gameObject.SetActive(true);    
                perkBtnContainer.GetChild(i).GetComponent<PerkBtnPtrEvents>().Init(perkData, skillViewMain);               

            }
            if(allPerkData.Count <= 2)
            {
              BGPipe1.gameObject.SetActive(false);
                BGPipe2.gameObject.SetActive(false);    
            }
            else if(allPerkData.Count <=4)
            {                
                BGPipe2.gameObject.SetActive(false);
            }
            else
            {
                BGPipe1.gameObject.SetActive(true);
                BGPipe2.gameObject.SetActive(true);
            }
            for (int j = allPerkData.Count; j < 6; j++)
            {
                perkBtnContainer.GetChild(j).gameObject.SetActive(false);


            }
        }

    

        void PopulatePerksPanel(SkillNames skillName)
        {
            // get all perk data from skillController1

        }

    }
}