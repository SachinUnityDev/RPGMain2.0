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

        [Header("Perk Info Panel")]
        public Transform perkInfoPanelTrans; // used by Perk btn info panel for info transfer

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
        private void Awake()
        {
            perkInfoPanelTrans = transform.GetChild(4); 
           
            leftBtn.onClick.AddListener(OnLeftBtnPressed);
            rightBtn.onClick.AddListener(OnRightBtnPressed);        
        }
        private void OnEnable()
        {
            SkillService.Instance.OnSkillSelectInInv += PopulateSkillScroll;
            InvService.Instance.OnCharSelectInvPanel += PopulateRightSkillPanel;
        }
        private void OnDisable()
        {
            SkillService.Instance.OnSkillSelectInInv -= PopulateSkillScroll;
            InvService.Instance.OnCharSelectInvPanel -= PopulateRightSkillPanel;
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
            if(charController.charModel.charName == CharNames.Abbas)
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
                PopulateSkillScroll(scrollList[index]);
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
               PopulateSkillScroll(scrollList[index]);          
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
            if (allPerkData == null)
            {
                for (int j = 0; j < 6; j++)
                {
                    perkBtnContainer.GetChild(j).gameObject.SetActive(false);
                    BGPipe1.gameObject.SetActive(false);
                    BGPipe2.gameObject.SetActive(false);
                }
                return; 
            }
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

    }
}