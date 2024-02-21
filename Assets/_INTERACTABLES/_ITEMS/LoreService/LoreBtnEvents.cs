using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
namespace Interactables
{
    public class LoreBtnEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] LoreBookNames loreName;
        [SerializeField] Sprite loreHL;
        [SerializeField] Sprite loreN;
        [SerializeField] Sprite loreLocked;

        [SerializeField] bool isLocked = false; 
        public void OnPointerClick(PointerEventData eventData)
        {

            LoreService.Instance.loreViewController.OnLoreSelectBtnPressed(loreName);
            // set active false 
            // open Lore

        }

        private void Start()
        {
            
        }

        void SetSpriteInit(LoreBookNames loreName)
        {
            loreHL = LoreService.Instance.loreSO.loreUnLockedHL;
            loreN = LoreService.Instance.loreSO.loreUnLockedN;
            loreLocked = LoreService.Instance.loreSO.loreLockImg;
            LoreData loreData = LoreService.Instance.GetLoreData(loreName);
            isLocked = loreData.isLocked; 
        }
        public void Init(LoreBookNames _loreName)
        {
            loreName = _loreName;
         
            SetSpriteInit(loreName);           
            PopulateNameStringsNBG();
    
        }
        void PopulateNameStringsNBG()
        {
            string loreNameStr = LoreService.Instance.GetLoreString(loreName);
           

            if (isLocked)
            {
                transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
                transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Locked";
                gameObject.GetComponent<Image>().sprite = loreLocked;

            }
            else
            {
                string[] loreNameSplit = loreNameStr.Split(' ');
                transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = loreNameSplit[0];
                transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = loreNameSplit[1];
                transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = loreNameSplit[2];
                gameObject.GetComponent<Image>().sprite = loreN;
            }
        }
    

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!isLocked)
            {
                gameObject.GetComponent<Image>().sprite = loreHL; 
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!isLocked)
            {
                gameObject.GetComponent<Image>().sprite = loreN;
            }
        }
    }

}

