using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;


namespace Town
{
    public class TownRightCornerBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] GameObject centerBtn;
        RightBtnController rightBtnsController;
        [SerializeField] string nameStr;

        [Header("NO TO BE REFER")]
        [SerializeField] TextMeshProUGUI nameTxt; 

        public void OnPointerEnter(PointerEventData eventData)
        {
            nameTxt.gameObject.SetActive(true);
            nameTxt.text = nameStr; 
            Debug.Log("Enter a side btn");
            rightBtnsController.isOpen = true;
            transform.DOScale(1.25f, 0.1f); 
            rightBtnsController.OpenBtns();
        }

        public void OnPointerExit(PointerEventData eventData)
        {

            nameTxt.gameObject.SetActive(false);
            Debug.Log("Exit a side btn");
            rightBtnsController.isOpen = false;
            transform.DOScale(1f, 0.1f);
            rightBtnsController.CloseBtns();
        }


        void Start()
        {
            centerBtn = transform.parent.GetChild(2).gameObject;
            rightBtnsController = centerBtn.GetComponent<RightBtnController>();

          
            nameTxt = transform.parent.GetChild(3).GetComponent<TextMeshProUGUI>();
            nameTxt.gameObject.SetActive(false);
        }

    }


}
