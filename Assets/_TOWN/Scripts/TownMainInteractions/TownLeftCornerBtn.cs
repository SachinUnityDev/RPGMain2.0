using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
using TMPro; 


namespace Town
{
    public class TownLeftCornerBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] GameObject centerBtn;
        LeftBtnsController leftBtnsController;
        [SerializeField] string btnNameStr = "";
        [Header("NOT TO BE REF")]
        [SerializeField] TextMeshProUGUI nametxt; 

        public void OnPointerEnter(PointerEventData eventData)
        {
            nametxt.gameObject.SetActive(true);
            nametxt.text = btnNameStr;
            Debug.Log("Enter a side btn"); 
            leftBtnsController.isOpen = true;
            transform.DOScale(1.25f, 0.1f);
            leftBtnsController.OpenBtns();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            nametxt.gameObject.SetActive(false);
            Debug.Log("Exit a side btn");
            leftBtnsController.isOpen = false;
            transform.DOScale(1f, 0.1f);
            leftBtnsController.CloseBtns();
        }


        void Start()
        {
            centerBtn = transform.parent.GetChild(2).gameObject;
            leftBtnsController = centerBtn.GetComponent<LeftBtnsController>();
            nametxt = transform.parent.GetChild(3).GetComponent<TextMeshProUGUI>();         
            nametxt.gameObject.SetActive(false);
        }
    }
}