using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Town
{
    public class BrewReadySlotPtrEvents : MonoBehaviour
    {
        public int readyQuantity;

        [Header("Slot images and txt")]
        [SerializeField] Image imgIngred;
        [SerializeField] Image imgTxtBG;
        [SerializeField] TextMeshProUGUI quantityTxt;

        private void Awake()
        {
            imgIngred = transform.GetChild(0).GetComponent<Image>();
            imgTxtBG = transform.GetChild(1).GetComponent<Image>(); 
            quantityTxt = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();

        }


        public void Add2Slot(AlcoholSO alcoholSO)
        {
            SlotEnable(); 
            readyQuantity++;    
            Sprite itemSprite = alcoholSO.iconSprite;
            imgIngred.sprite = itemSprite;
            quantityTxt.text = readyQuantity.ToString();            
        }
        
        public bool OnConsume(Iitems item)
        {
            AlcoholBase alcoholBase = item as AlcoholBase;
            if (alcoholBase != null)
            {
                alcoholBase.OnDrink(); 
            }
            readyQuantity--;
            if(readyQuantity == 0)
            {
                EmptySlot();
                return false;
            }
            else
            {
                quantityTxt.text = readyQuantity.ToString();                
                return true;
            }            
        }
        void SlotEnable()
        {
            imgIngred.gameObject.SetActive(true);
            imgTxtBG.gameObject.SetActive(true);
            quantityTxt.gameObject.SetActive(true);
        }

        void EmptySlot()
        {
            imgIngred.gameObject.SetActive(false);
            imgTxtBG.gameObject.SetActive(false);
            quantityTxt.gameObject.SetActive(false);
        }
    }
}