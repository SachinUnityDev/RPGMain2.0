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


namespace Common
{
    public class InvSkillBtnPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Const")]
        [SerializeField] int expBeyondLineNo = 3;
        [SerializeField] Vector2 skillCardBaseDim = new Vector2(328f, 325f);
        public SkillHexSO skillHexSO;
        

        [SerializeField] GameObject skillCard;
        //[SerializeField] int index = -1;


        public SkillCardData skillCardData;
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
        public SkillData skillData;
        public SkillDataSO skillDataSO; 

        void Start()
        {
            prevSkillHovered = SkillNames.None;
            skillCard = skillHexSO.skillCardPrefab;
        }
        #region  POINTER EVENTS
        public void OnPointerClick(PointerEventData eventData)
        {
          
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            // populate the skillCard Data as per the skills 
            //if (skillCardData.skillModel.skillName == SkillNames.None) return;
            ShowSkillCard();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
           
        }
        #endregion

        public void Init(SkillDataSO _skillDataSO, SkillData _skillData)
        {
            skillDataSO = _skillDataSO; 
            skillData = _skillData;
        }
        void ShowSkillCard()
        {
            skillCard.SetActive(true);
           // SkillServiceView.Instance.pointerOnSkillIcon = true;  // to be checked
            //SkillDataSO skillSO = SkillService.Instance
            //           .GetSkillSO(CombatService.Instance.currCharClicked.charModel.charName);
            //index = gameObject.transform.GetSiblingIndex();
            //SkillServiceView.Instance.index = index;
            // UPDATE SKILL SERVICE 
            if (skillDataSO!= null && skillData != null)
            {
                SkillService.Instance.On_SkillHovered(skillDataSO.charName,
                                                            skillData.skillName);
            }                
            else 
            {
                Debug.Log("Skill SO is null "); return; 
            }
            skillCardData = SkillService.Instance.skillCardData;
        }



    }
}