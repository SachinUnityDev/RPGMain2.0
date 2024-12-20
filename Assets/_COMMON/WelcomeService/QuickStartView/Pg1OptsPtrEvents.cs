using Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Intro
{
    public class Pg1OptsPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        QuickStartPg1View pg1View;
        QuickStartView quickStartView; 
        SpriteOpts spriteOpts;

        [Header(" Title txt")]
        [SerializeField] TextMeshProUGUI titleTxt; 

        [Header(" To be filled")]
        [SerializeField] string unLockedTxt;
        [SerializeField] int quickStartOpt;
        [SerializeField] ClassType classType; 


        Image img; 
        public void Init( SpriteOpts spriteOpts, QuickStartPg1View pg1View, QuickStartView quickStartView)
        {
            titleTxt = GetComponentInChildren<TextMeshProUGUI>();
            this.spriteOpts= spriteOpts;
            this.pg1View= pg1View;  
            this.quickStartView= quickStartView;
            img = GetComponent<Image>();
            this.spriteOpts = spriteOpts;
            if (spriteOpts.isUnlocked)
            {
                img.sprite = spriteOpts.spriteN;
            }
            else
            {
                img.sprite = spriteOpts.spriteNA;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (spriteOpts.isUnlocked)
            {
                if(quickStartOpt == 1)
                {
                    quickStartView.ShowPg2(); 
                    GameService.Instance.currGameModel.abbasClassType = classType;   
                }
            }
            else
            {
                img.sprite = spriteOpts.spriteNA;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (spriteOpts.isUnlocked)
            {
                img.sprite = spriteOpts.spriteHL;
            }
            else
            {
                img.sprite = spriteOpts.spriteNA;
            }
            ShowTxt();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (spriteOpts.isUnlocked)
            {
                img.sprite = spriteOpts.spriteN;
            }
            else
            {
                img.sprite = spriteOpts.spriteNA;
            }
            HideTxt();
        }
        void ShowTxt()
        {
            if (spriteOpts.isUnlocked)
                pg1View.descTxt.text = unLockedTxt; 
            else
                pg1View.descTxt.text = "Unavailable for the demo";

            pg1View.descTxt.gameObject.SetActive(true); 
        }
        void HideTxt()
        {
            pg1View.descTxt.gameObject.SetActive(false);
        }
    }
}