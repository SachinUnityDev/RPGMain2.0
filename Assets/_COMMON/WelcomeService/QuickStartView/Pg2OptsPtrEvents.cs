using Common;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace Intro
{
    public class Pg2OptsPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        QuickStartPg2View pg2View;
        QuickStartView quickStartView; 

        SpriteOpts spriteOpts;

        [Header(" To be filled")]
        [SerializeField] string unLockedTxt;
        [SerializeField] JobNames jobName; 
        Image img;
        public void Init(SpriteOpts spriteOpts, QuickStartPg2View pg2View, QuickStartView quickStartView)
        {
            this.spriteOpts = spriteOpts;
            this.pg2View = pg2View;
            this.quickStartView = quickStartView;

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
                GameService.Instance.currGameModel.jobSelect = jobName;
                IntroServices.Instance.LoadNext(); 
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
                pg2View.descTxt.text = unLockedTxt;
            else
                pg2View.descTxt.text = "Unavailable for the demo";

            pg2View.descTxt.gameObject.SetActive(true);
        }
        void HideTxt()
        {
            pg2View.descTxt.gameObject.SetActive(false);
        }
    }
}