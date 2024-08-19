using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Common;
using TMPro;
using UnityEngine.UI;
using Interactables;

namespace Combat
{
    public class PerkBtnPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] InvSkillViewMain skillViewMain;
        [SerializeField] CharController charController;
        [SerializeField] SkillController1 skillController;
        SkillViewSO skillViewSO;
        [SerializeField] PerkData perkData = new PerkData();

        [SerializeField] Transform BGPipe1; 
        [SerializeField] Transform BGPipe2;

        [SerializeField] Color colorClickable;
        [SerializeField] Color colorClicked;
        [SerializeField] Color colorUnClickable;

        [Header("Perk Info Panel")]
        [SerializeField] Transform perkHoveredTrans;

        SkillModel skillModel; 
        
    
        private void Start()
        {
            InvService.Instance.OnCharSelectInvPanel += (CharModel c)=>OnCharSelect();
            SkillService.Instance.OnPerkStateChg += OnPerkStateChg;
        }
        private void OnDisable()
        {
            InvService.Instance.OnCharSelectInvPanel -= (CharModel c) => OnCharSelect();
            SkillService.Instance.OnPerkStateChg -= OnPerkStateChg;
        }
        void OnPerkStateChg(PerkData _perkData)
        {
            if(perkData.perkName == _perkData.perkName)
            {
                perkData.state =_perkData.state;
                SetPerkBtnState(_perkData.state);
            }
        }
        #region INIT
        public void Init(PerkData _perkData, InvSkillViewMain _skillViewMain, SkillModel skillModel)
        {
            perkData = _perkData;
            skillViewMain = _skillViewMain;
            this.skillModel = skillModel; 
            skillViewSO = skillViewMain.skillViewSO;
            SetPerkBtnState(perkData.state);
          
            OnCharSelect();

            // fill in name and btn image
            transform.GetComponentInChildren<TextMeshProUGUI>().text
                                                     = perkData.perkName.ToString().CreateSpace();
            SetPerkBtnState(perkData.state);
            BGPipe1 = skillViewMain.rightSkillView.BGPipe1; 
            BGPipe2 = skillViewMain.rightSkillView.BGPipe2;
        }

        void OnCharSelect()
        {
            this.charController = InvService.Instance.charSelectController;
            skillController = charController.skillController;
        }
        #endregion

        void UpdatePerkData(PerkData _perkData)
        {
            perkData = _perkData; 
        }
        
        #region PIPES RELATIONS
        void ShowPipeRelations()
        {
            PerkData _perkData =
                        skillController.GetPerkData(perkData.perkName); 
            UpdatePerkData(_perkData);

            if (_perkData.perkType == PerkType.A1 || _perkData.perkType == PerkType.B1)
            {
                ShowLevel1N2();
                ShowLevel2N3();
            }

            if (_perkData.perkType == PerkType.A2 || _perkData.perkType == PerkType.B2)
            {
                ShowLevel2N3(); 
            }
        }
        void ShowLevel1N2()
        {
            for (int i = 0; i < 2; i++)
            {
                if (perkData.pipeRel[i] == 0)
                {
                    BGPipe1.GetChild(i).gameObject.SetActive(false);
                }
                else //1,2 or 3
                {
                    BGPipe1.GetChild(i).gameObject.SetActive(true);
                    if (perkData.pipeRel[i] == 1 || perkData.pipeRel[i] == 2)
                    {
                        if (i == 0)
                            BGPipe1.GetChild(i).GetComponent<Image>().sprite =
                                    skillViewSO.GetPerkPipe(perkData.perkType, PerkType.A2, 1);
                        else
                            BGPipe1.GetChild(i).GetComponent<Image>().sprite =
                               skillViewSO.GetPerkPipe(perkData.perkType, PerkType.B2, 1);
                    }
                    else
                    {
                        if (i == 0)
                            BGPipe1.GetChild(i).GetComponent<Image>().sprite =
                                    skillViewSO.GetPerkPipe(perkData.perkType, PerkType.A2, 3);
                        else
                            BGPipe1.GetChild(i).GetComponent<Image>().sprite =
                               skillViewSO.GetPerkPipe(perkData.perkType, PerkType.B2, 3);
                    }
                }
            }
        }
        void ShowLevel2N3()
        {
            if (perkData.perkType == PerkType.A2 || perkData.perkType == PerkType.B2)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (perkData.pipeRel[i] == 0)
                    {
                        BGPipe2.GetChild(i).gameObject.SetActive(false);
                    }
                    else //1,2 or 3
                    {
                        BGPipe2.GetChild(i).gameObject.SetActive(true);
                        if (perkData.pipeRel[i] == 1 || perkData.pipeRel[i] == 2)
                        {
                            if (i == 0)
                                BGPipe2.GetChild(i).GetComponent<Image>().sprite =
                                        skillViewSO.GetPerkPipe(perkData.perkType, PerkType.A3, 1);
                            else
                                BGPipe2.GetChild(i).GetComponent<Image>().sprite =
                                   skillViewSO.GetPerkPipe(perkData.perkType, PerkType.B3, 1);
                        }
                        else
                        {
                            if (i == 0)
                                BGPipe2.GetChild(i).GetComponent<Image>().sprite =
                                        skillViewSO.GetPerkPipe(perkData.perkType, PerkType.A3, 3);
                            else
                                BGPipe2.GetChild(i).GetComponent<Image>().sprite =
                                   skillViewSO.GetPerkPipe(perkData.perkType, PerkType.B3, 3);
                        }
                    }
                }
            }
        }
        void HidePipeRelations()
        {
            foreach (Transform pipeTrans in BGPipe1)
            {
                pipeTrans.gameObject.SetActive(false);
            }
            foreach (Transform pipeTrans in BGPipe2)
            {
                pipeTrans.gameObject.SetActive(false);
            }

        }

        #endregion
        public void OnPointerClick(PointerEventData eventData)
        {
            if(skillController.charController.charModel.skillPts > 0)
                if (skillViewMain.isPerkClickAvail)// chks whether the skill pts + btn is pressed 
                {
                    if(skillController.IsPerkClickable(skillModel.skillName, perkData.perkName))
                        if(perkData.state== PerkSelectState.Clickable)
                        {
                            skillController.OnPerkClicked(perkData);                   
                            skillViewMain.rightSkillView.FillSkillScroll(skillModel);
                        }
                }
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            this.charController = InvService.Instance.charSelectController;
            skillController = charController.skillController;
            

            if (skillModel != null)
            {
                SkillService.Instance.On_SkillHovered(skillModel.charName, skillModel.skillName);
            }
            else
            {
                Debug.LogError("Skill Model is null " + skillController); return;
            }
            skillModel = SkillService.Instance.skillModelHovered;

            ShowPipeRelations();
            ShowPerkHoveredPanel();
        }
        public void OnPointerExit(PointerEventData eventData)
        {
          //  if(perkData.state != PerkSelectState.Clicked)
            HidePipeRelations();
            perkHoveredTrans.gameObject.SetActive(false);
        }
        void SetPerkBtnState(PerkSelectState _state)
        {
            switch (_state)
            {
                case PerkSelectState.Clickable:
                    transform.GetComponent<Image>().sprite = skillViewSO.perkBtnNormal;
                    transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = colorClickable;
                    break;
                case PerkSelectState.Clicked:
                    transform.GetComponent<Image>().sprite = skillViewSO.perkBtnSelect;
                    transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = colorClicked;
                    break;
                case PerkSelectState.UnClickable:
                    transform.GetComponent<Image>().sprite = skillViewSO.perkBtnUnSelectable;
                    transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = colorUnClickable;
                    break;
                default:
                    break;
            }
        }

        void ShowPerkHoveredPanel()
        {
            // PERK hovered 
            PerkBase perkBase =
                     skillController.GetPerkBase(perkData.skillName, perkData.perkName);
            perkBase.PerkHovered();

            // LOCATE trans 
            Transform rightSkillTrans = transform.parent.parent; 
            RightSkillView rightSkillView = rightSkillTrans.GetComponent<RightSkillView>();          
            perkHoveredTrans = rightSkillView.perkInfoPanelTrans;     
            // INIT Info Panel
            perkHoveredTrans.GetComponent<PerkViewInfoPanel>()
                                        .Init(perkData, perkBase, skillController);
            perkHoveredTrans.gameObject.SetActive(true);
        }




    }
}