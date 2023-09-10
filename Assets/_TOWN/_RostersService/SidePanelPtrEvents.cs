using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

namespace Common
{
    public class SidePanelPtrEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] Sprite spriteHL;
        [SerializeField] Sprite spriteN;

        [SerializeField] Image img;
        [SerializeField] int index;
        public string headerStr = ""; 
        public string descStr = "";
        [Header("NTBR")]
        [SerializeField] GameObject descPlank;
        [SerializeField] TextMeshProUGUI headertxt; 

        RosterSO rosterSO;

        void OnEnable()
        {
            RosterService.Instance.OnRosterScrollCharSelect -= PrintPlanks;        
            RosterService.Instance.OnRosterScrollCharSelect += PrintPlanks;
        }
        void OnDisable()
        {
            RosterService.Instance.OnRosterScrollCharSelect -= PrintPlanks;
        }
        public void SidePlankInit()
        {
            img = gameObject.GetComponentInChildren<Image>();
            img.sprite = spriteN;
            descPlank = transform.GetChild(2).gameObject;
            descPlank.SetActive(false);
            index = transform.GetSiblingIndex();
            rosterSO = RosterService.Instance.rosterSO;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            img.sprite = spriteHL;
            descPlank.SetActive(true);
            PrintPlanks(RosterService.Instance.scrollSelectCharModel);
        }
        void PrintPlanks(CharModel charModel)
        {
            if (index == 2) return;
            
            if (index == 0)
            {
                PopulateAvailDesc(RosterService.Instance.scrollSelectCharModel);
            }
            if (index == 1)
            {
                PopulateFameDesc(RosterService.Instance.scrollSelectCharModel);
            }

            descPlank.GetComponentInChildren<TextMeshProUGUI>().text = descStr;
            transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = headerStr;
        }


        void PopulateAvailDesc(CharModel selectCharModel)
        {
            AvailOfChar availOfChar = selectCharModel.availOfChar;
            headerStr = GetHeaderStr(availOfChar);
            descStr = GetAvailStr(availOfChar);
          
        }
        void PopulateFameDesc(CharModel selectCharModel)
        {
            FameBehavior fameBehavior = selectCharModel.fameBehavior;

            headerStr = fameBehavior.ToString();
            descStr = FameService.Instance.fameSO.GetFameBehaviorStr(fameBehavior).ToString();
          
        }
        string GetHeaderStr(AvailOfChar availOfChar)
        {
            string str = RosterService.Instance.rosterSO
                        .allAvailStateStr.Find(t => t.availOfChar == availOfChar)
                        .availStateHeaderStr;
            return str; 
        }
        string GetAvailStr(AvailOfChar availOfChar)
        {
            string str =   rosterSO.allAvailStateStr.Find(t => t.availOfChar == availOfChar).availStateDescStr;
            if (str == null)
                str = ""; 
            return str; 
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            descPlank.SetActive(false);
            img.sprite = spriteN;
        }

       

    }



}

