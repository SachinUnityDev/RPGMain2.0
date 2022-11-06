using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
using System.Linq;
using UnityEngine.UI;
using System;


namespace Common
{
    public class HeaderNamePtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] CodexHeader codexHeader;
        [SerializeField] CodexViewController codexViewController;
        [SerializeField] CodexSO codexSO; 

        [SerializeField] HeaderData headerData;        
        [SerializeField] Transform subContainer;

        public bool isClicked = false;
        [SerializeField] float moveDistance = 0f;
        Transform contentGO;
        [SerializeField] float startY = 0;
        RectTransform rect;

        Image img; 
        public void Init(CodexHeader codexHeader, CodexViewController codexViewController)
        {
            this.codexViewController = codexViewController;
            this.codexHeader = codexHeader;
            // loop thru all the children and populate the subheader Name 

            int i= 0;
            headerData = CodexService.Instance.codexSO.allHeaderData.Find(t => t.codexHeader == codexHeader);
        
            
            subContainer = transform.GetChild(1);
            if (subContainer.childCount == 0) return;
            if (headerData == null) return; 
           
            foreach (Transform child in subContainer)  // looping thru headers
            {
                if (headerData.allSubHeaders.Count > i)
                {
                 
                    child.GetComponent<SubHeaderPtrevents>()
                        .Init(headerData, headerData.allSubHeaders[i], codexViewController);
                    i++;
                }
                else break;              
            }

            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text
                = headerData.codexHeader.ToString().CreateSpace();

        }
        public void OnClicked()
        {
            img.sprite = codexSO.spriteHeaderHL; 

            Expand();
            subContainer.DOScaleY(1, 0.1f);
            isClicked = true;
            
            if(headerData.allHeaderSprites != null)
            {
                if (headerData.allHeaderSprites != null)
                {
                    codexViewController.PopulatePageNImg(headerData.allHeaderSprites[0]);                    
                }         
                else
                    Debug.Log("Error Image null");
            }
            else
            {
                transform.GetChild(0).GetComponent<SubHeaderPtrevents>().OnClicked();  
            } 
        }

        public void OnUnClicked()
        {
            img.sprite = codexSO.spriteHeaderN;

            Contract();
            subContainer.DOScaleY(0, 0.1f);
            isClicked = false;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!isClicked)
            {
                codexViewController.UnClickOthers();
                OnClicked(); 
            }
            else
            {
                OnUnClicked();
            }
            
        }

        void Contract()
        {
            contentGO = transform.parent;
       
            Vector2 currSize = rect.sizeDelta;
            rect.sizeDelta = currSize - new Vector2(0f,  moveDistance); 

            int allChildCount = contentGO.childCount;
            int headerIndex = transform.GetSiblingIndex();
            for (int i = headerIndex + 1; i < allChildCount; i++)
            {              
                contentGO.GetChild(i).GetComponent<HeaderNamePtrEvents>().MoveUp();
            }
        }

        public void MoveDown(float dist)
        {
            transform.DOLocalMoveY(startY-dist, 0.1f); 
        }
        public void MoveUp()
        {
            transform.DOLocalMoveY(startY, 0.1f);
        }
        void Expand()
        {

            int count = subContainer.childCount;
            moveDistance = count * 70f;

            Vector2 currSize = rect.sizeDelta;
            rect.sizeDelta = currSize + new Vector2(0f, moveDistance);
            int allChildCount = contentGO.childCount;
            int headerIndex = transform.GetSiblingIndex();

            for (int i = headerIndex + 1; i < allChildCount; i++)
            {              
                contentGO.GetChild(i).GetComponent<HeaderNamePtrEvents>().MoveDown(moveDistance);
            }
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            if(!isClicked)
            img.sprite = codexSO.spriteHeaderHover;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(!isClicked)
            img.sprite = codexSO.spriteHeaderN;
        }

        void Start()
        {
            img = GetComponent<Image>();
            codexSO = CodexService.Instance.codexSO;
            img.sprite = codexSO.spriteHeaderN; 
            subContainer.DOScaleY(0, 0.1f);
            contentGO = transform.parent;
            startY = transform.localPosition.y;
             rect = contentGO.GetComponent<RectTransform>();
        }
    }
}
