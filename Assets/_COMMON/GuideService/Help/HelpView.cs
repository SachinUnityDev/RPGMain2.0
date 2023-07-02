using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

namespace Common
{
    public class HelpView : MonoBehaviour, IPanel
    {
        [Header("UI TBR ")]
        [SerializeField] TextMeshProUGUI headingTxt;
        [SerializeField] TextMeshProUGUI helpStr;
        [SerializeField] Button pageBtn;
        [SerializeField] TextMeshProUGUI pageNum; 

        //[Space(5)]
        [Header("Global var")]
        float prevRightClick =0f;
        [SerializeField]int index;
        [SerializeField] int maxIndex;
        [SerializeField] HelpSO helpSO;
        [SerializeField] HelpName helpName;
        void Start()
        {
            pageBtn.onClick.AddListener(OnPageTurnBtnPressed);
        }
        public void InitHelp(HelpName helpName, HelpSO helpSO)
        {
            this.helpSO = helpSO;    
            this.helpName = helpName; 
            index = 0;
            maxIndex = helpSO.helpStrs.Count;
            Load(); 
            FillPage();
        }


        void FillPage()
        {
            headingTxt.text = helpSO.headingTxt;
            helpStr.text = helpSO.helpStrs[index];
            // page number
            pageNum.text = "";
                //$"{index + 1}" + "/" + $"{maxIndex}"; 
        }

        void OnPageTurnBtnPressed()
        {
            if (Time.time - prevRightClick < 0.3f) return;
            if (index == 0)
            {
                index = maxIndex - 1;
                FillPage();
            }
            else
            {
                ++index; FillPage();
            }
            prevRightClick = Time.time;
        }

        public void Load()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, true);
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
        }

        public void Init()
        {
           
        }
    }
}