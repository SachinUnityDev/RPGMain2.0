using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Common;
using TMPro;

namespace Interactables
{
    public class LockBtnPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] CompanionViewController companionViewController;

        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite spriteLit;
        [SerializeField] Image img;
        [SerializeField] TextMeshProUGUI raceTxt;
        [SerializeField] bool isClicked;

        public void Init(CompanionViewController companionViewController)
        {
            this.companionViewController = companionViewController;
            img = transform.GetChild(0).GetComponent<Image>();
            img.sprite = spriteN;
            raceTxt = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            raceTxt.text = "Lock race";
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            isClicked = !isClicked;
            if (isClicked)
            {
                img.sprite = spriteLit;
                
            }
            else
            {
                img.sprite = spriteN;
            }
            companionViewController.ToggleLock(isClicked);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            img.sprite = spriteLit;
            raceTxt.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            raceTxt.gameObject.SetActive(false);
            if (!isClicked)
                img.sprite = spriteN;
        }

        void Start()
        {

        }

    }

}


