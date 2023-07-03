using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

namespace Common
{
    public interface iHelp
    {
        HelpName GetHelpName(); 
    }

    public class HelpView : MonoBehaviour, IPanel
    {
        [Header("UI TBR ")]
        [SerializeField] TextMeshProUGUI headingTxt;
        [SerializeField] TextMeshProUGUI helpStr;
        [SerializeField] GameObject pageBtn;
        [SerializeField] TextMeshProUGUI pageNum; 

        //[Space(5)]
        [Header("Global var")]
        float prevRightClick =0f;
        [SerializeField]int index;
        [SerializeField] int maxIndex;
        [SerializeField] HelpSO helpSO;
        [SerializeField] HelpName helpName;

        HelpController helpController; 

        void Start()
        {
            
        }
        public void InitHelp(HelpName helpName, HelpSO helpSO, HelpController helpController)
        {
            this.helpSO = helpSO;    
            this.helpName = helpName; 
            this.helpController = helpController;   
            index = 0;
            maxIndex = helpSO.helpStrs.Count;
            Load(); 
            FillPage();
            pageBtn.GetComponent<HelpScrollBtnPtrEvents>().InitScrollBtn(this);
        }


        void FillPage()
        {
            headingTxt.text = helpSO.headingTxt;
            helpStr.text = helpSO.helpStrs[index];
            // page number
            pageNum.text = "";
                //$"{index + 1}" + "/" + $"{maxIndex}";     
            if(maxIndex> 1)
            {
                pageBtn.gameObject.SetActive(true);
            }
            else
            {
                pageBtn.gameObject.SetActive(false);
            }
        }

        public void OnPageTurnBtnPressed()
        {
            if (Time.time - prevRightClick < 0.3f) return;
            if (index == maxIndex - 1)
            {
                index = 0;
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
            helpController.RemoveHelpView();
        }

        public void Init()
        {
           
        }
    }
}