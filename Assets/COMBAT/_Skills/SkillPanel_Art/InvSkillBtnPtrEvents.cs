using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Combat;
using TMPro;
using System.Linq;
using System;
using Interactables;
using DG.Tweening;

namespace Common
{
    public class InvSkillBtnPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public bool IsClicked; 

        [Header("Skill card Dimensions")]
        [SerializeField] int expBeyondLineNo = 3;
        [SerializeField] Vector2 skillCardBaseDim = new Vector2(328f, 325f);

        [Header("Skill Card positioning")]
        [SerializeField] Vector3 offset = new Vector3(148, 60,0);

        [Header("Skill HEX SO")] // contain skill card SO 
        public SkillHexSO skillHexSO;

       // [SerializeField] GameObject skillCard;
        [SerializeField] int index = -1;

        public SkillSelectState skillState;
        public SkillNames prevSkillHovered;

        [SerializeField] List<string> alliesDesc = new List<string>();
        [SerializeField] List<string> enemyDesc = new List<string>();
        [SerializeField] List<string> bothAllyNEnemy = new List<string>();
        [SerializeField] List<string> attackTypeLs = new List<string>();
        [SerializeField] List<string> damageTypeLS = new List<string>();
        [SerializeField] List<string> sunStrReturn = new List<string>();
        [SerializeField] List<string> finalDesc = new List<string>();


        [Header("Key Skill ref SET UP ON INIT")]
        public SkillModel skillModel;
        public SkillDataSO skillDataSO;

        [Header("Global Var")]
        [SerializeField] GameObject skillCardGO;        
        [SerializeField] Transform skillPtsTrans;
        public SkillNames skillName;
        LeftSkillView leftSkillView; 

        void Start()
        {
            IsClicked = false;
            prevSkillHovered = SkillNames.None;
          //  skillCard = skillHexSO.skillCardPrefab;
           // skillPtsTrans = transform.GetChild(2);
        }
        #region  POINTER EVENTS
        public void OnPointerClick(PointerEventData eventData)
        {
            IsClicked = !IsClicked;             
            
            if (IsClicked)
            {
                leftSkillView.UnClickAllSkillState();       // add frame
                IsClicked = true;
                transform.GetChild(1).GetComponent<Image>().sprite = skillHexSO.skillIconFrameHL; 
                ShowSkillcardInInv();
            }
            else
            {
                SetUnClick(); 
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ShowSkillcardInInv();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
           // if(!IsClicked)
                HideSkillCard(); 
        }
        #endregion

        public void SetUnClick()
        {
            IsClicked = false;
            transform.GetChild(1).GetComponent<Image>().sprite = skillHexSO.SkillNormalFrame;
          //  HideSkillCard();
        }

        public void Init(SkillDataSO _skillDataSO, SkillNames _skillName, LeftSkillView leftSkillView)
        {
            skillDataSO = _skillDataSO;
            skillName = _skillName;
            index = _skillDataSO.allSkills.FindIndex(t => t.skillName == _skillName); 
            
            SkillController1 skillController = InvService.Instance.charSelectController.skillController; 
            skillModel = skillController.GetSkillModel(skillName);

            transform.GetComponent<Image>().sprite =
                                    skillDataSO.allSkills[index].skillIconSprite;

            if (skillModel == null) return;
            int skilllvlInt = (int)skillModel.skillLvl;  // skillmodel req here for this
            this.leftSkillView = leftSkillView;

            skillCardGO = SkillService.Instance.skillCardGO;
            //if (skillPtsTrans != null)
            //    skillPtsTrans.GetComponent<TextMeshProUGUI>().text = skilllvlInt.ToString();
        }

        void HideSkillCard()
        { 
            skillCardGO.gameObject.SetActive(false);
        }
        void ShowSkillcardInInv()
        {
            if (GameService.Instance.gameModel.gameState != GameState.InTown)
                return; 

           // skillCard.SetActive(true);
           // SkillServiceView.Instance.pointerOnSkillIcon = true;  // to be checked
            skillDataSO = SkillService.Instance
                                    .GetSkillSO(InvService.Instance.charSelectController.charModel.charName); 
                       
            //index = gameObject.transform.GetSiblingIndex();
            //SkillServiceView.Instance.index = index;
            //UPDATE SKILL SERVICE
            if (skillDataSO != null)
            {
                SkillService.Instance.On_SkillHovered(skillDataSO.charName,skillName);
            }
            else
            {
                Debug.Log("Skill SO is null "); return;
            }
            skillModel = SkillService.Instance.skillModelHovered;
            PosSkillCard(); 

        }

        void PosSkillCard()
        {
            float width = skillCardGO.GetComponent<RectTransform>().rect.width;
            float height = skillCardGO.GetComponent<RectTransform>().rect.height;
            GameObject Canvas = GameObject.FindWithTag("MainCanvas");
            Canvas canvasObj = Canvas.GetComponent<Canvas>();
            Vector3 offSetFinal = (offset + new Vector3(width / 2, -height / 2, 0))*canvasObj.scaleFactor; 
            Vector3 pos = transform.position + offSetFinal;
            skillCardGO.GetComponent<Transform>().DOMove(pos, 0.1f); 
            skillCardGO.SetActive(true); 
        }

    }
}