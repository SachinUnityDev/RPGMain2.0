using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

namespace Common
{
    public class SubHeaderPtrevents : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] HeaderData headerData; 
        [SerializeField] SubHeaderData subHeaderData;
        [SerializeField] CodexSO codexSO;
        [SerializeField] int headerIndex =-1;
        [SerializeField] int subIndex =0;

        [SerializeField] CodexViewController codexViewController;
        [SerializeField] Image img; 

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClicked();

        }

        public void Init(HeaderData headerData, SubHeaderData subHeaderData
            , CodexViewController codexViewController)
        {

            this.headerData = headerData;
            this.subHeaderData = subHeaderData;
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text
                 = subHeaderData.codexSubHeader.ToString().CreateSpace();
            this.codexViewController = codexViewController; 


        }

        void UnClickOthers()
        {
            foreach (Transform child in transform.parent)
            {
                if(child != this.transform)
                child.GetComponent<SubHeaderPtrevents>().OnUnClicked(); 
            }
        }
        public void OnClicked()
        {
            UnClickOthers(); 
            codexViewController.PopulatePageNImg(subHeaderData.allSubHeaderSprites[0]);
            img.sprite = codexSO.spriteSubHL; 
        }

        public void OnUnClicked()
        {
            img.sprite = codexSO.spriteSubN;
        }


        void Start()
        {
            codexSO = CodexService.Instance.codexSO;
            img = GetComponent<Image>();
            subIndex = transform.GetSiblingIndex();
            img.sprite = codexSO.spriteSubN;
        }


    }
}

