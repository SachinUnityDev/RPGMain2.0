using Common;
using DG.Tweening;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace Combat
{
    public class SkillBtnViewCombat : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
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
        public SkillModel skillModel;
        public SkillDataSO skillDataSO;

        [Header("Global Var")]
        [SerializeField] GameObject skillCardGO;
        [SerializeField] Transform skillPtsTrans;
        public SkillNames skillName;
        SkillView skillView;
        public PassiveSkillName passiveSkillName; 
        void Start()
        {
            IsClicked = false;
            prevSkillHovered = SkillNames.None;
            passiveSkillName = PassiveSkillName.None;
            skillName= SkillNames.None;
            SkillService.Instance.OnSkillUsed += OnSkillUsed; 

        }
        private void OnDisable()
        {
            SkillService.Instance.OnSkillUsed -= OnSkillUsed;
        }
        void OnSkillUsed(SkillEventData skillEventData)
        {
            SetUnClick();
        }

        #region  POINTER EVENTS
        public void OnPointerClick(PointerEventData eventData)
        {
            if (CombatService.Instance.combatState == CombatState.INTactics) return;
            if (skillModel == null) return;
            if(skillModel.skillUnLockStatus== 0 || skillModel.skillUnLockStatus == -1) return;
            if (skillModel.GetSkillState() == SkillSelectState.Clickable ||
                skillModel.GetSkillState() == SkillSelectState.Clicked)
            {
                if (!IsClicked)
                {
                    SetClicked();
                    skillView.SkillBtnPressed(transform.GetSiblingIndex());
                }
                else
                {
                    SetUnClick();
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(skillModel != null)
                 ShowSkillcardInCombat();
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            HideSkillCard();
        }
        #endregion

        public void SetUnClick()
        {
            IsClicked = false;
            if (skillModel != null)
                skillModel.SetPrevSkillState();    
            transform.GetChild(1).GetComponent<Image>().sprite = skillHexSO.SkillNormalFrame;
        }
        public void SetClicked()
        {
            if (skillModel == null) return;            
            skillView.UnClickAllSkills();
            if (IsEnemy()) return;
            IsClicked = true;
            skillModel.SetSkillState(SkillSelectState.Clicked);
            transform.GetChild(1).GetComponent<Image>().sprite = skillHexSO.SkillSelectFrame;
        }
        bool IsEnemy()
        {
            CharController charController = CombatService.Instance.currCharClicked; 
            if (charController)
                if (charController.charModel.charMode == CharMode.Enemy)
                    return true; 
            return false;
        }
        void ClearData()
        {
            passiveSkillName = PassiveSkillName.None;
            skillName = SkillNames.None;
            skillDataSO = null;
            skillCardGO= null;
            
        }
        public void PSkillBtnInit(SkillDataSO skillDataSO, PassiveSkillData passiveSkillData,SkillView skillView)
        {
            ClearData();
            Image skillImg = transform.GetComponent<Image>();            
            this.skillDataSO = skillDataSO;
            this.skillView = skillView;

           // skillView.skillController.UpdateAllSkillState(CombatService.Instance.currCharClicked);
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

        public void SkillBtnInit(SkillDataSO _skillDataSO, SkillModel skillModel , SkillView skillView)
        {
            ClearData();
            Image skillImg = transform.GetComponent<Image>();
            this.skillModel = skillModel;
            skillDataSO = _skillDataSO;
            this.skillView = skillView;
            if (skillModel == null)
            {
                skillImg.sprite = skillView.NASkillIconSprite;
                skillName = SkillNames.None;
                skillLvlTrans.gameObject.SetActive(false);
                return;
            }
            
            skillName = skillModel.skillName;
            
            index = _skillDataSO.allSkills.FindIndex(t => t.skillName == skillName);
            if (skillModel.skillUnLockStatus == 1)
            {
                skillImg.sprite = skillDataSO.allSkills[index].skillIconSprite;
            }
            if (skillModel.skillUnLockStatus == 0)
            {
                skillImg.sprite = skillView.LockedSkillIconSprite;
            }
            if (skillModel.skillUnLockStatus == 1)
            {
                skillImg.sprite = skillDataSO.allSkills[index].skillIconSprite;
            }
            if (skillModel.skillUnLockStatus == -1)
            {
                skillImg.sprite = skillView.NASkillIconSprite;
            }
            int skilllvlInt = (int)skillModel.skillLvl;  // skillmodel req here for this
       
            skillCardGO = SkillService.Instance.skillCardGO;
            DsplySkillLvl();
           
        }

        void HideSkillCard()
        {
            if(skillCardGO != null)
            {
                skillCardGO.gameObject.SetActive(false);              
            }            
        }
        void ShowSkillcardInCombat()
        {
            if (GameService.Instance.gameModel.gameState != GameState.InCombat)
                return;
            if (skillCardGO == null)
                return;
            CharController charClicked = CombatService.Instance.currCharClicked;
            CharNames charName = charClicked.charModel.charName; 
             
            skillDataSO = SkillService.Instance.GetSkillSO(charName);
            if(passiveSkillName != PassiveSkillName.None)
            {
                PassiveSkillService.Instance.On_PSkillHovered(passiveSkillName, charClicked); 

            }else if (skillDataSO != null)
            {
                SkillService.Instance.skillModelHovered = skillModel;
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
            if (skillModel == null)
                return;
           
            if (skillModel.skillUnLockStatus == 1)
            {
                int skillLvl = (int)skillModel.skillLvl;
                SkillTypeCombat skillType = skillModel.skillType;

                switch (skillType)
                {
                    case SkillTypeCombat.None:
                        break;
                    case SkillTypeCombat.Skill1:
                        skillLvlTrans.gameObject.SetActive(true);
                        skillLvlTrans.GetComponentInChildren<TextMeshProUGUI>().text = skillModel.skillLvl.ToString(); 
                        break;
                    case SkillTypeCombat.Skill2:
                        skillLvlTrans.gameObject.SetActive(true);
                        skillLvlTrans.GetComponentInChildren<TextMeshProUGUI>().text = skillModel.skillLvl.ToString();
                        break;
                    case SkillTypeCombat.Skill3:
                        skillLvlTrans.gameObject.SetActive(true);
                        skillLvlTrans.GetComponentInChildren<TextMeshProUGUI>().text = skillModel.skillLvl.ToString();
                        break;
                    case SkillTypeCombat.Ulti:
                        skillLvlTrans.gameObject.SetActive(true);
                        skillLvlTrans.GetComponentInChildren<TextMeshProUGUI>().text = skillModel.skillLvl.ToString();
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
                        skillLvlTrans.GetComponentInChildren<TextMeshProUGUI>().text = skillModel.skillLvl.ToString();
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
            Vector3 offSetFinal = (offset + new Vector3(-width / 2, (0), 0))* canvasObj.scaleFactor;
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
            if (skillModel == null) return;
            skillState = skillModel.GetSkillState();
            // ChangeSkillFrame(skillState);
            if(skillState == SkillSelectState.Clicked || skillState == SkillSelectState.Clickable)
            {
                gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else
            {
                gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }       
        }

        #region HELPER Methods
        //IEnumerator Wait()
        //{
        //    yield return new WaitForSeconds(2);
        //    if (!skillView.pointerOnSkillCard && skillView.index == -1)
        //        skillCardGO.SetActive(false);
        //}

        //void ToggleTxt(Transform transform)
        //{
        //    foreach (Transform child in transform)
        //    {
        //        child.gameObject.SetActive(false);
        //    }
        //}

        #endregion


    }
}

//switch (skillState)
//{
//    case SkillSelectState.Clickable:
//        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
//        //ChangeSkillFrame(SkillSelectState.Clickable);
//        break;
//    case SkillSelectState.UnClickable_InCd:
//        gameObject.GetComponent<Image>().color = new Color(0.5f, 1, 1, 0.8f);

//        break;
//    case SkillSelectState.UnClickable_NoTargets:
//        gameObject.GetComponent<Image>().color = new Color(0.6f, 1, 1, 0.8f);

//        break;
//    case SkillSelectState.UnClickable_NoStamina:
//        gameObject.GetComponent<Image>().color = new Color(0.9f, 1, 1, 0.8f);

//        break;
//    case SkillSelectState.Unclickable_notOnCastPos:
//        gameObject.GetComponent<Image>().color = new Color(0.2f, 1, 1, 0.8f);

//        break;
//    case SkillSelectState.Unclickable_passiveSkills:
//        gameObject.GetComponent<Image>().color = new Color(0.3f, 1, 1, 0.8f);

//        break;
//    case SkillSelectState.Unclickable_notCharsTurn:
//        gameObject.GetComponent<Image>().color = new Color(1f, 1, 1, 0.9f);

//        break;
//    case SkillSelectState.Clicked:
//        // change frame from skillSO
//        // ChangeSkillFrame(SkillSelectState.Clicked);
//        break;


//    default:
//        break;
//}

