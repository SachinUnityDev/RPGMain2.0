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

        [SerializeField] ItemDataWithQty ingredReq;
        [SerializeField] int quantity;
        [SerializeField] Image imgIngred;
        [SerializeField] TextMeshProUGUI txtTrans;

        public bool hasSufficientIngred; 

      
        public void InitBrewRecipe(ItemDataWithQty ingredReq, int quantity)
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
        public void SubtractIngredFrmSlot(ItemDataWithQty subIngred)
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
            Sprite itemSprite = GetSprite(ingredReq.itemData);
            Debug.Log("Ingred " + (IngredNames)ingredReq.itemData.itemName);
            imgIngred = transform.GetChild(0).GetComponent<Image>();
            txtTrans = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
            imgIngred.sprite = itemSprite;
            txtTrans.text = ingredReq.quantity.ToString();
        }
        Sprite GetSprite(ItemData itemData)
        {
            Sprite sprite = InvService.Instance.InvSO.GetSprite(itemData.itemName, itemData.itemType);
            if (sprite != null)
                return sprite;
            else
                Debug.Log("SPRITE NOT FOUND");
            return null;
        }
    
    }
}

