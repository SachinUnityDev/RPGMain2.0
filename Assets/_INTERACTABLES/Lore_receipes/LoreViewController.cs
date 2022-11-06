using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Common;
using Combat;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

namespace Interactables
{

    public class LoreViewController : MonoBehaviour, IPanel
    {
        const int SIZE_INCR_PER_100 = 60;
        [Header("Main Lore Panel")]

        [SerializeField] GameObject loreSelectPanel; 
        [SerializeField] GameObject loreBtnParent;

        [Header("LORE CONTENT PAGE")]
        [SerializeField] GameObject loreContainer; // main Container

        [SerializeField] TextMeshProUGUI LoreNameTxt;
        [SerializeField] TextMeshProUGUI subLoreNametxt;
        [SerializeField] Button leftBtn;
        [SerializeField] Button rightBtn;
        [SerializeField] TextMeshProUGUI mainLoreTxt;

        [SerializeField] GameObject maintxtContent;
        [SerializeField] Button refreshBtn; // go back to the main panel 


        [Header("Global Var")]
        [SerializeField] LoreData currLoredata; // once lore Selection is clicked Populate this 
        [SerializeField] int index; 
        void Start()
        {
            leftBtn.onClick.AddListener(OnLeftBtnPressed);
            rightBtn.onClick.AddListener(OnRightBtnPressed);
            refreshBtn.onClick.AddListener(OnRefreshBtnPressed); 
        }
        public void OnLoreSelectBtnPressed(LoreNames loreName)
        {
            loreSelectPanel.SetActive(false);
            loreContainer.SetActive(true);
            PopulateLoreScroll(loreName);
        }
        void OnRefreshBtnPressed()
        {         
            PopulateLoreSelection();
        }
        void PopulateLoreScroll(LoreNames loreName)
        {
            currLoredata = LoreService.Instance.GetLoreData(loreName);
            LoreNameTxt.text = LoreService.Instance.GetLoreString(loreName);           
            index = 0;
            PopulateSubLore(); 
        }

        void PopulateSubLore()
        {
            if (currLoredata == null) return; 
            subLoreNametxt.text = currLoredata.allSubData[index].subLoreNames.ToString();
            string mainStr = currLoredata.allSubData[index].Loretxt;
            SetContentSize(mainStr);
            mainLoreTxt.text = mainStr;           
        }
        void OnLeftBtnPressed()
        {
            if (index <= 0)
            {
                index = 0;
            }
            else
            {
                index--; PopulateSubLore(); 
            }
        }
        void OnRightBtnPressed()
        {
            int count = currLoredata.allSubData.Count; 
            if (index >= count - 1)
            {
                index = count - 1;
            }
            else
            {
                index++; PopulateSubLore();
            }
        }

        void SetContentSize(string mainTxtSize)
        {
            // for 200 words 1200 height
            string[] strSplt = mainTxtSize.Split(' ');
            int wordLen = strSplt.Length;
            float height = wordLen * 5;
            RectTransform rect = maintxtContent.GetComponent<RectTransform>();
            float prevWidth = rect.sizeDelta.x; 
            
            rect.sizeDelta = new Vector2(prevWidth, height); 

        }

        void PopulateLoreSelection()
        {
            loreSelectPanel.SetActive(true);
            loreContainer.SetActive(false);
            int i = 1; 
            foreach (Transform child in loreBtnParent.transform)
            {
                LoreNames loreName = (LoreNames)i; 
                child.GetComponent<LoreBtnEvents>().Init(loreName);
                i++; 
            }
   
        }

    

        public void Load()
        {
          

        }

        public void UnLoad()
        {

        }

        public void Init()
        {
            PopulateLoreSelection();
          
        }
    }
}
