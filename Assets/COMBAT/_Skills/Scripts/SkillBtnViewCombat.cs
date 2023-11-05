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
        [SerializeField] Vector3 offset = new Vector3(0, 250, 0);

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
        void Awake()
        {
            IsClicked = false;
            prevSkillHovered = SkillNames.None;
        }
        #region  POINTER EVENTS
        public void OnPointerClick(PointerEventData eventData)
        {
            IsClicked = !IsClicked;

            if (IsClicked)
            {
                if (skillModel.GetSkillState() != SkillSelectState.Clickable)
                    return; 
                IsClicked = true;
                transform.GetChild(1).GetComponent<Image>().sprite = skillHexSO.skillIconFrameHL;
                ShowSkillcardInCombat();
                skillView.SkillBtnPressed(transform.GetSiblingIndex());
            }
            else
            {
                SetUnClick();
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
            transform.GetChild(1).GetComponent<Image>().sprite = skillHexSO.SkillNormalFrame;
        }
        public void SkillBtnInit(SkillDataSO _skillDataSO, SkillModel skillModel , SkillView skillView)
        {
            Image skillImg = transform.GetComponent<Image>();
            this.skillModel = skillModel;
            skillDataSO = _skillDataSO;
            this.skillView = skillView;
            if (skillModel == null)
            {
                skillImg.sprite = skillView.NASkillIconSprite;
                skillName = SkillNames.None; 
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
           
            CharNames charName = CombatService.Instance.currCharClicked.charModel.charName; 
             
            skillDataSO = SkillService.Instance.GetSkillSO(charName);
            SkillService.Instance.skillModelHovered = skillModel;
            if (skillDataSO != null)
            {
                SkillService.Instance.On_SkillHovered(skillDataSO.charName, skillName);
            }
            else
            {
                Debug.Log("Skill SO is null "); return;
            }
            PosNShowSkillCard();
        }

        void DsplySkillLvl()
        {
            if (skillModel == null)
                return;
            if (skillModel.skillUnLockStatus == 1)
            {
                int skillLvl = (int)skillModel.skillLvl;
                SkillTypeCombat skillType = skillModel.skillType;

                skillLvlTrans.gameObject.SetActive(false);

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
            int incrVal = skillCardGO.GetComponent<SkillCardView>().GetIncVal();
            GameObject Canvas = GameObject.FindWithTag("Canvas");
            Canvas canvasObj = Canvas.GetComponent<Canvas>();
            Vector3 offSetFinal = (offset + new Vector3(-width / 2, (height*2/3), 0)) * canvasObj.scaleFactor;
            Vector3 pos = transform.position + offSetFinal;
            skillCardGO.GetComponent<Transform>().DOMove(pos, 0.001f);
            
        }

        public void RefreshIconAsPerState()
        {
            if (skillModel == null) return;
            skillState = skillModel.GetSkillState();
            // ChangeSkillFrame(skillState);
            switch (skillState)
            {
                case SkillSelectState.Clickable:
                    gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    //ChangeSkillFrame(SkillSelectState.Clickable);
                    break;
                case SkillSelectState.UnClickable_InCd:
                    gameObject.GetComponent<Image>().color = new Color(0.5f, 1, 1, 0.8f);

                    break;
                case SkillSelectState.UnClickable_NoTargets:
                    gameObject.GetComponent<Image>().color = new Color(0.6f, 1, 1, 0.8f);

                    break;
                case SkillSelectState.UnClickable_NoStamina:
                    gameObject.GetComponent<Image>().color = new Color(0.9f, 1, 1, 0.8f);

                    break;
                case SkillSelectState.Unclickable_notOnCastPos:
                    gameObject.GetComponent<Image>().color = new Color(0.2f, 1, 1, 0.8f);

                    break;
                case SkillSelectState.Unclickable_passiveSkills:
                    gameObject.GetComponent<Image>().color = new Color(0.3f, 1, 1, 0.8f);

                    break;
                case SkillSelectState.Unclickable_notCharsTurn:
                    gameObject.GetComponent<Image>().color = new Color(1f, 1, 1, 0.9f);

                    break;
                case SkillSelectState.Clicked:
                    // change frame from skillSO
                    // ChangeSkillFrame(SkillSelectState.Clicked);
                    break;


                default:
                    break;
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