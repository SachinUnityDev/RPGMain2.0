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

    public class CodexViewController : MonoBehaviour, IPanel
    {
        [SerializeField] int TOTAL_PAGE = 20; 
        [SerializeField] GameObject contentGO;

        CodexSO codexSO;
        [SerializeField] int currPageIndex =1;
        [SerializeField] HeaderData currHeaderData;
        [SerializeField] SubHeaderData currSubHeaderData;

        [SerializeField] Image img;
        [SerializeField] TextMeshProUGUI pageStr;
        [SerializeField] Button leftBtn;
        [SerializeField] Button rightBtn;
    
        public void PopulatePageNImg(CodexSpriteData spriteData)
        {
            img.sprite = spriteData.pageSprite;
            DisplayPage(spriteData.pageNum); 
        }
        CodexSpriteData SetPage(int pageNum)
        {
            currPageIndex = pageNum;
            foreach (HeaderData headerData in codexSO.allHeaderData)
            {
                int i = headerData.allHeaderSprites.FindIndex(t => t.pageNum == pageNum); 
                if (i != -1)
                {
                    currHeaderData = headerData;
                    currSubHeaderData = null;
                    return headerData.allHeaderSprites[i]; 
                }
                foreach (SubHeaderData sub in headerData.allSubHeaders)
                {
                    int j = sub.allSubHeaderSprites.FindIndex(t => t.pageNum == pageNum);
                    if (j!= -1)
                    {
                        currHeaderData = headerData;
                        currSubHeaderData = sub;
                        return sub.allSubHeaderSprites[j]; 
                    }
                }
            }
            return null;            
        }
        public void DisplayPage(int pageNum)
        {           
            pageStr.text = "" + pageNum + "/" + TOTAL_PAGE;
        }
        void OnRightScrollBtnPressed()
        {          
            if (currPageIndex >= TOTAL_PAGE )
            {
                currPageIndex = TOTAL_PAGE;
            }
            else
            {
                currPageIndex++;
                PopulatePageNImg(SetPage(currPageIndex));
            }
        }

        void OnLeftScrollBtnPressed()
        {
            if (currPageIndex <= 1)
            {
                currPageIndex = 1;
            }
            else
            {
                currPageIndex--;

                PopulatePageNImg(SetPage(currPageIndex));
            }
        }
        public void UnClickOthers()
        {
            foreach (Transform child in contentGO.transform)
            {
                HeaderNamePtrEvents headerNamePtrEvents =
                            child.GetComponent<HeaderNamePtrEvents>(); 
                if (headerNamePtrEvents.isClicked)
                {
                    headerNamePtrEvents.OnUnClicked();
                }
            }
        }
        void PopulateCodexHeaders()
        {
            codexSO = CodexService.Instance.codexSO; 
            int j = 0; 
            foreach (Transform child in contentGO.transform)  // looping thru headers
            {
                CodexHeader codexHeader = codexSO.allHeaderData[j].codexHeader;                 
                child.GetComponent<HeaderNamePtrEvents>().Init(codexHeader, this);
                j++; 
            }
        }
        public void Init()
        {
            
        }



        public void Load()
        {
            UIControlServiceGeneral.Instance.TogglePanel(this.gameObject, true);
            PopulateCodexHeaders();
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(this.gameObject, false);
        }

        void Start()
        {
            UnLoad();
            leftBtn.onClick.AddListener(OnLeftScrollBtnPressed);
            rightBtn.onClick.AddListener(OnRightScrollBtnPressed);

        }


    }
}
