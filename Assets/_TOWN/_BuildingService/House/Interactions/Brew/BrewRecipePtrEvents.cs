using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

namespace Town
{
    public class BrewRecipePtrEvents : MonoBehaviour
    {
        [SerializeField] Transform associatedPlus; 

        [SerializeField] IngredData ingredReq;
        [SerializeField] int quantity;
        [SerializeField] Image imgIngred;
        [SerializeField] TextMeshProUGUI txtTrans;

        public bool hasSufficientIngred; 

        private void Awake()
        {
            imgIngred = transform.GetChild(0).GetComponent<Image>();
            txtTrans = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        }
        public void InitBrewRecipe(IngredData ingredReq, int quantity)
        {
            this.ingredReq = ingredReq;  
            this.quantity = quantity;
            FillSlot(); 
        }
        
        void SetQuantityStatus()
        {
            if(quantity > ingredReq.quantity)
            {
                hasSufficientIngred= true;
            }
            else
            {
                hasSufficientIngred= false;
            }
        }
        public void SubtractIngredFrmSlot(IngredData subIngred)
        {



        }

        public void DisableSlot()
        {
            if (associatedPlus != null)
                associatedPlus.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
        public void EnableSlot()
        {
            if (associatedPlus != null)
                associatedPlus.gameObject.SetActive(true);
            gameObject.SetActive(true);
        }
        void FillSlot()
        {             
            Sprite itemSprite = GetSprite(ingredReq.ItemData);
            imgIngred.sprite = itemSprite;
            txtTrans.text = ingredReq.quantity.ToString();
        }
        Sprite GetSprite(ItemData itemData)
        {
            Sprite sprite = InvService.Instance.InvSO.GetSprite(itemData.ItemName, itemData.itemType);
            if (sprite != null)
                return sprite;
            else
                Debug.Log("SPRITE NOT FOUND");
            return null;
        }
    
    }
}

