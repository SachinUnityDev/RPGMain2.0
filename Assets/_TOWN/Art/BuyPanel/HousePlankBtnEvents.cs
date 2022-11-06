using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening; 

namespace Common
{
    public class HousePlankBtnEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] Image plankBG;
        public bool isPlankClicked;

        [SerializeField] Button tickButton;

        public void SetUnclickedState()
        {

            isPlankClicked = false; 
            plankBG.DOFade(0, 0.05f);
            tickButton.gameObject.SetActive(false);

        }

        void OnTickButtonPressed()
        {
            Debug.Log("Purchase made");


        }

        void ShowTick()
        {
            if (isPlankClicked)  // Less money to purchase condition to be added later 
            {
                tickButton.gameObject.SetActive(true);
            }
        }



        bool ChkOtherClickedStatus()
        {
            Transform parentTrans = transform.parent;
            foreach (Transform child in parentTrans)
            {
                if (child.gameObject == this.gameObject) continue;
                HousePlankBtnEvents HPBEvent = child.GetComponent<HousePlankBtnEvents>();
                if (HPBEvent.isPlankClicked)
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
                HousePlankBtnEvents HPBEvent = child.GetComponent<HousePlankBtnEvents>(); 
                if (HPBEvent.isPlankClicked)
                {   
                    HPBEvent.SetUnclickedState();
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!isPlankClicked)
            {
                ChgOtherPlankStatus(); 
                plankBG.DOFade(1, 0.05f);
                isPlankClicked = true;
                ShowTick();
            }
            else
            {
                SetUnclickedState();
            }

        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(!ChkOtherClickedStatus())
                plankBG.DOFade(1, 0.05f); 
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(!isPlankClicked)
                plankBG.DOFade(0, 0.05f);
        }
        void Start()
        {
            isPlankClicked = false;
            plankBG = GetComponent<Image>();
            tickButton = GetComponentInChildren<Button>();
            tickButton.gameObject.SetActive(false);
            tickButton.onClick.AddListener(OnTickButtonPressed);
        }

    }


}




