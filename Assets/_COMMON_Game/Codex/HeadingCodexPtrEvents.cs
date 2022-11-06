using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
namespace Common
{
    public class HeadingCodexPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite spriteExp;
        public bool isClicked = false; 
                
        [SerializeField] CodexViewController codexViewController;
        public CodexHeader codexHeader; 
        public int headerIndex =0;

        [SerializeField] bool isHeadingOpen = false;
        [SerializeField] CodexSO codexSO;


        [SerializeField] List<GameObject> allSubHeaderBtn = new List<GameObject>();
        [SerializeField] GameObject contentGO; 
        [SerializeField] Button headerBtn;
        Transform subHeader;
        Vector2 addedSize; 

        public void Init(CodexHeader codexHeader, CodexViewController codexViewController)
        {
            this.codexHeader = codexHeader;
            this.codexViewController = codexViewController;
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                                                            codexHeader.ToString();
            codexSO = CodexService.Instance.codexSO;
            spriteN = codexSO.headingArrowN;
            headerIndex = codexSO.allHeaderData.FindIndex(t => t.codexHeader == codexHeader);
        }
        void OnHeaderBtnPressed()
        {
            if (!isHeadingOpen)
            {
               // codexViewController.OnHeaderBtnClicked(index); // updation 
                PopulateSubHeaders();
                PopulateHeaderDisplay(); 
                isHeadingOpen = true;
            }
            else
            {
                Contract();
                isHeadingOpen = false;
            }
        }

        void PopulateHeaderDisplay()
        {




        }
        void PopulateSubHeaders()
        {

            HeaderData headerData = codexSO.allHeaderData[headerIndex];
            subHeader = transform.GetChild(1);
            if (headerData.allSubHeaders.Count == 0) return;
            foreach (SubHeaderData sub in headerData.allSubHeaders)
            {

                GameObject btnGO = Instantiate(codexSO.subHeaderBtnPrefab);
                btnGO.transform.SetParent(transform.GetChild(1));
                RectTransform rt = btnGO.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(140, 30);

               // btnGO.GetComponent<SubHeaderPtrevents>().Init(sub.codexSubHeader, this); 

                allSubHeaderBtn.Add(btnGO);
            }
            allSubHeaderBtn[0].GetComponent<SubHeaderPtrevents>().OnClicked();
            Expand();
        }

        void Expand()
        {
            RectTransform rt = subHeader.GetComponent<RectTransform>();
            Vector2 currSize = rt.sizeDelta;
            int count = allSubHeaderBtn.Count;
             addedSize = new Vector2(0, count * 60f);
            Vector2 expandSize = currSize + addedSize;
            rt.sizeDelta = expandSize;

            RectTransform headerRT = contentGO.GetComponent<RectTransform>();
            Vector2 currheaderSize = headerRT.sizeDelta;
            Vector2 newHeaderSize = addedSize + currheaderSize;
            headerRT.sizeDelta = newHeaderSize;

            Transform parent = transform.parent; 
            int allChildCount = parent.childCount;
            for (int i = headerIndex+1; i < allChildCount; i++)
            {
                float currY = parent.GetChild(i).localPosition.y; 
                parent.GetChild(i).DOLocalMoveY(currY-addedSize.y, 0.1f); 
            }
          
        }

        void Contract()
        {
            Transform parent = transform.parent;
            int allChildCount = parent.childCount;
            for (int i = headerIndex + 1; i < allChildCount; i++)
            {
                float currY = parent.GetChild(i).localPosition.y;
                parent.GetChild(i).DOLocalMoveY(currY, 0.1f);
            }
        }


        void Start()
        {
            headerIndex = transform.GetSiblingIndex();
            headerBtn = transform.GetComponent<Button>();
            headerBtn.onClick.AddListener(OnHeaderBtnPressed);
            contentGO = transform.parent.gameObject;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
          
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
          
        }

        public void OnPointerExit(PointerEventData eventData)
        {
          
        }
    }
}

