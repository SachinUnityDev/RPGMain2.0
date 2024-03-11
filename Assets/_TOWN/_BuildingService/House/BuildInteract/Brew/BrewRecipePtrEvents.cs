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

        [Header("Color")]
        [SerializeField] Color colorN;
        [SerializeField] Color colorNA;
        void Start()
        {
            InvService.Instance.OnItemRemovedFrmComm += OnItemRemoved;
        }

        void OnItemRemoved(Iitems item)
        {
            ChkIngredQtyNupdate();
        }
        public void InitBrewRecipe(ItemDataWithQty ingredReq)
        {
            this.ingredReq = ingredReq;  
            FillSlot(); 
            ChkIngredQtyNupdate();
        }
        
 
        public bool ChkIngredQtyNupdate()
        {
            int quantity =
                   InvService.Instance.invMainModel.GetItemNosInCommInv(ingredReq.itemData);
            quantity +=
                InvService.Instance.invMainModel.GetItemNosInStashInv(ingredReq.itemData);

            if (quantity >= ingredReq.quantity)
            {
                hasSufficientIngred = true;
                imgIngred.color = colorN;
            }
            else
            {
                hasSufficientIngred = false;
                imgIngred.color = colorNA;
            }

            return hasSufficientIngred; 
        }

        public void UpdateIngredSlotStatus()
        {
            // subtract
            // set color etc 
            for (int i = 0; i < ingredReq.quantity; i++)
            {
                InvService.Instance.invMainModel.RemoveItemFrmCommInv(ingredReq.itemData);
            }
            ChkIngredQtyNupdate();
               // InvService.Instance.invMainModel.RemoveItemFrmStashInv(ingredReq.itemData);
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

