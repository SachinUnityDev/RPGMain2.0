using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Intro
{
    public class Pg1OptsPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        QuickStartPg1View pg1View; 

        SpriteOpts spriteOpts;

        [Header(" To be filled")]
        [SerializeField] string unLockedTxt;
        [SerializeField] int quickStartOpt; 
        Image img; 
        public void Init( SpriteOpts spriteOpts, QuickStartPg1View pg1View)
        {
            this.spriteOpts= spriteOpts;
            this.pg1View= pg1View;  
            
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
                    CharController charController = CharService.Instance.GetAbbasController(CharNames.Abbas);
                    charController.charModel.classType = ClassType.Skirmisher;
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