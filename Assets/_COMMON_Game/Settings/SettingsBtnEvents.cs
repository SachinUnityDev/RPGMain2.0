using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;


namespace Common
{
    public class SettingsBtnEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public bool isPlankClicked;

        [SerializeField] Button button;
        

        [Header("Line Anim related")]
        [SerializeField] Image LineImg;

        [Header("Pop up Anim related")]
        float animTime = 0.15f;
        float scaleValue = 1.25f;

        [Header("Color Change related")]
        [SerializeField] TextMeshProUGUI textContent;
        [SerializeField] Color colorNormal; 
        [SerializeField] Color colorHL;
        void AnimScaleUp()
        {
            transform.DOScale(scaleValue, animTime);
            textContent.color = colorHL;
        }

        void AnimScaleDown()
        {
            LineImg.DOFade(0, 0.05f);
            transform.DOScale(1f, animTime);
            textContent.color = colorNormal;
        }
        public void SetClickedState()
        {
            ChgOtherPlankStatus();
            AnimScaleUp(); 
            isPlankClicked = true;
        }
        public void SetUnclickedState()
        {
            AnimScaleDown();
            isPlankClicked = false;
        }

        bool ChkOtherClickedStatus()
        {
            Transform parentTrans = transform.parent;
            foreach (Transform child in parentTrans)
            {
                if (child.gameObject == this.gameObject) continue;
                SettingsBtnEvents settingsBtnEvent = child.GetComponent<SettingsBtnEvents>();
                if (settingsBtnEvent.isPlankClicked)
                {
                    return true;
                }
            }
            return false;
        }

        void ChgOtherPlankStatus()
        {
            //get parent .. loop thru all the scripts see if any oneelse is clicked
            Transform parentTrans = transform.parent;
            foreach (Transform child in parentTrans)
            {
                if (child.gameObject == this.gameObject) continue;
                SettingsBtnEvents settingsBtnEvent = child.GetComponent<SettingsBtnEvents>();
                if (settingsBtnEvent.isPlankClicked)
                {
                    settingsBtnEvent.SetUnclickedState();
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!isPlankClicked)
            {
                SetClickedState(); 
            }
            else
            {
                SetUnclickedState();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!isPlankClicked)
                AnimScaleUp();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!isPlankClicked)
                AnimScaleDown();   // no connection to clicked or unclicked state 

        }
        void Awake()
        {
            isPlankClicked = false;
          
            button = GetComponent<Button>();
            LineImg = GetComponent<Image>();
            textContent = button.GetComponentInChildren<TextMeshProUGUI>();
            textContent.color = colorNormal;
            AnimScaleDown();
        }

    }
}


