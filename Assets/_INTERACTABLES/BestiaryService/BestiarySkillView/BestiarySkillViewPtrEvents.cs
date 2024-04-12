using Combat;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Common
{
    public class BestiarySkillViewPtrEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler  
    {
        [SerializeField] BestiarySkillView skillView;
        

        public bool IsClicked;

        [Header("Skill card Dimensions")]
        [SerializeField] int expBeyondLineNo = 3;

        [Header("Skill lvl")]
        [SerializeField] Transform skillLvlTrans;

        [Header("Skill Card positioning")]
        [SerializeField] Vector3 offset = new Vector3(0, 150, 0);

        [Header("Skill HEX SO TBR")] // contain skill card SO 
        public SkillHexSO skillHexSO;

        [SerializeField] int index = -1;

        public SkillSelectState skillState;
        public SkillNames prevSkillHovered;

        [Header("Key Skill ref SET UP ON INIT")]
        public SkillData skillData;
        public SkillDataSO skillDataSO;

        [Header("Global Var")]
        [SerializeField] GameObject skillCardGO;
        [SerializeField] Transform skillPtsTrans;
        public SkillNames skillName;
        public PassiveSkillName passiveSkillName;
        void Start()
        {
            IsClicked = false;
            prevSkillHovered = SkillNames.None;
            passiveSkillName = PassiveSkillName.None;
            skillName = SkillNames.None;
          //  SkillService.Instance.OnSkillUsed += OnSkillUsed;

        }
        //private void OnDisable()
        //{
        //  //  SkillService.Instance.OnSkillUsed -= OnSkillUsed;
        //}
        //void OnSkillUsed(SkillEventData skillEventData)
        //{
        //    SetUnClick();
        //}

        #region  POINTER EVENTS
        //public void OnPointerClick(PointerEventData eventData)
        //{
        //    if (eventData.button == 0)
        //    {
        //        if (CombatService.Instance.combatState == CombatState.INTactics) return;
        //        if (skillModel == null) return;
        //        if (skillModel.skillUnLockStatus == 0 || skillModel.skillUnLockStatus == -1) return;
        //        if (skillModel.GetSkillState() == SkillSelectState.Clickable ||
        //            skillModel.GetSkillState() == SkillSelectState.Clicked)
        //        {
        //            if (!IsClicked)
        //            {
        //                SetClicked();
        //               // skillView.SkillBtnPressed(transform.GetSiblingIndex());
        //            }
        //        }
        //    }
        //    if ((int)eventData.button == 1)
        //    {
        //        SetUnClick();
        //    }
        //}

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (skillData != null)
                ShowSkillcardINBestiary();
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            HideSkillCard();
        }
        #endregion

        void ClearData()
        {
            passiveSkillName = PassiveSkillName.None;
            skillName = SkillNames.None;
            skillDataSO = null;
            skillCardGO = null;

        }
        public void PSkillBtnInit(SkillDataSO skillDataSO, PassiveSkillData passiveSkillData, BestiarySkillView skillView)
        {
            ClearData();
            Image skillImg = transform.GetComponent<Image>();
            this.skillDataSO = skillDataSO;
            this.skillView = skillView;

            skillLvlTrans.gameObject.SetActive(false);
            if (passiveSkillData == null)
            {
                skillImg.sprite = skillView.NASkillIconSprite;
                skillName = SkillNames.None;
                return;
            }
            passiveSkillName = passiveSkillData.passiveSkillName;
            skillImg.sprite = passiveSkillData.passiveSprite;
            skillCardGO = PassiveSkillService.Instance.pSkillCardGO;
        }

        public void SkillBtnInit(SkillDataSO _skillDataSO, SkillData skillData, BestiarySkillView skillView)
        {
            ClearData();
            Image skillImg = transform.GetComponent<Image>();
            this.skillData = skillData;
            skillDataSO = _skillDataSO;
            this.skillView = skillView;
            if (skillData == null)
            {
                skillImg.sprite = skillView.NASkillIconSprite;
                skillName = SkillNames.None;
                skillLvlTrans.gameObject.SetActive(false);
                return;
            }

            skillName = skillData.skillName;

            index = _skillDataSO.allSkills.FindIndex(t => t.skillName == skillName);
            if (skillData.skillUnLockStatus == 1)
            {
                skillImg.sprite = skillDataSO.allSkills[index].skillIconSprite;
            }
            if (skillData.skillUnLockStatus == 0)
            {
                skillImg.sprite = skillView.LockedSkillIconSprite;
            }
            if (skillData.skillUnLockStatus == 1)
            {
                skillImg.sprite = skillDataSO.allSkills[index].skillIconSprite;
            }
            if (skillData.skillUnLockStatus == -1)
            {
                skillImg.sprite = skillView.NASkillIconSprite;
            }
            int skilllvlInt = (int)skillData.skillLvl;  // skillmodel req here for this

            skillCardGO = SkillService.Instance.skillCardGO;
            DsplySkillLvl();
        }
        void HideSkillCard()
        {
            if (skillCardGO != null)
            {
                skillCardGO.gameObject.SetActive(false);
            }
        }
        void ShowSkillcardINBestiary()
        {
            
            if (skillCardGO == null)
                return;
            CharModel charModel = BestiaryService.Instance.currbestiaryModel; 
            if(charModel == null) return;
            CharNames charName = charModel.charName;
            CharController charClicked = BestiaryService.Instance.GetCharControllerWithName(charName);

            skillDataSO = SkillService.Instance.GetSkillSO(charName);
            if (passiveSkillName != PassiveSkillName.None)
            {
                PassiveSkillService.Instance.On_PSkillHovered(passiveSkillName, charClicked);
            }
            else if (skillDataSO != null)
            {
               SkillService.Instance.On_SkillHovered(skillDataSO.charName, skillName);
            }
            else
            {
                Debug.Log("Skill SO is null"); return;
            }
            PosNShowSkillCard();
        }

        void DsplySkillLvl()
        {
            skillLvlTrans.gameObject.SetActive(false);
            if (skillData == null)
                return;

            if (skillData.skillUnLockStatus == 1)
            {
                int skillLvl = (int)skillData.skillLvl;
                SkillTypeCombat skillType = skillData.skillType;

                switch (skillType)
                {
                    case SkillTypeCombat.None:
                        break;
                    case SkillTypeCombat.Skill1:
                        skillLvlTrans.gameObject.SetActive(true);
                        skillLvlTrans.GetComponentInChildren<TextMeshProUGUI>().text = skillData.skillLvl.ToString();
                        break;
                    case SkillTypeCombat.Skill2:
                        skillLvlTrans.gameObject.SetActive(true);
                        skillLvlTrans.GetComponentInChildren<TextMeshProUGUI>().text = skillData.skillLvl.ToString();
                        break;
                    case SkillTypeCombat.Skill3:
                        skillLvlTrans.gameObject.SetActive(true);
                        skillLvlTrans.GetComponentInChildren<TextMeshProUGUI>().text = skillData.skillLvl.ToString();
                        break;
                    case SkillTypeCombat.Ulti:
                        skillLvlTrans.gameObject.SetActive(true);
                        skillLvlTrans.GetComponentInChildren<TextMeshProUGUI>().text = skillData.skillLvl.ToString();
                        break;
                    case SkillTypeCombat.Patience:
                        skillLvlTrans.gameObject.SetActive(false);
                        break;
                    case SkillTypeCombat.Move:
                        skillLvlTrans.gameObject.SetActive(false);
                        break;
                    case SkillTypeCombat.Weapon:
                        break;
                    case SkillTypeCombat.Uzu:
                        skillLvlTrans.gameObject.SetActive(true);
                        skillLvlTrans.GetComponentInChildren<TextMeshProUGUI>().text = skillData.skillLvl.ToString();
                        break;
                    case SkillTypeCombat.Retaliate:
                        break;
                    case SkillTypeCombat.Passive:
                        break;
                    default:
                        break;
                }

            }
        }

        void PosNShowSkillCard()
        {
            float width = skillCardGO.GetComponent<RectTransform>().rect.width;
            float height = skillCardGO.GetComponent<RectTransform>().rect.height;

            skillCardGO.SetActive(true);
            skillCardGO.transform.SetParent(transform);
            //int incrVal = skillCardGO.GetComponent<SkillCardView>().GetIncVal();
            GameObject Canvas = GameObject.FindWithTag("Canvas");
            Canvas canvasObj = Canvas.GetComponent<Canvas>();
            Vector3 offSetFinal = (offset + new Vector3(0, height/2, 0)) * canvasObj.scaleFactor;
            RectTransform skillCardRect = skillCardGO.GetComponent<RectTransform>();
            skillCardRect.localPosition = Vector2.zero;
            Vector3 pos = skillCardRect.position + offSetFinal + new Vector3(0f, 40f, 0f);
            skillCardRect.pivot = new Vector2(0.5f, 0f);
            skillCardRect.anchorMin = new Vector2(0.5f, 0f);
            skillCardRect.anchorMax = new Vector2(0.5f, 0f);
            skillCardRect.DOMove(pos, 0.001f);

        }

        public void RefreshIconAsPerState()
        {
            if (skillData == null) return;
            skillState = SkillSelectState.Clickable;
            // ChangeSkillFrame(skillState);
            if (skillState == SkillSelectState.Clicked || skillState == SkillSelectState.Clickable)
            {
                gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else
            {
                gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
        }
    
    }
}