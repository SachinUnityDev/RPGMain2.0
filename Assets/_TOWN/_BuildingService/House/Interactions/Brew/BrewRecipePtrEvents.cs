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

        [SerializeField] IngredData netIngred;
        [SerializeField] int quantity;
        [SerializeField] Image imgIngred;
        [SerializeField] TextMeshProUGUI txtTrans;

        private void Awake()
        {
            imgIngred = transform.GetChild(0).GetComponent<Image>();
            txtTrans = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        }
        public void InitBrewRecipe(IngredData netIngred, int quantity)
        {
            this.netIngred = netIngred;  
            this.quantity = quantity;
            FillSlot(); 
        }
        
        void FillSlot()
        {       
            Sprite itemSprite = GetSprite(netIngred.ItemData);
            imgIngred.sprite = itemSprite;
            txtTrans.text = quantity.ToString();
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
        public void SubtractIngredFrmSlot(IngredData subIngred)
        {



        }
    }
}

//if(quantity== 0)
//{
//    if (associatedPlus != null)
//     associatedPlus.gameObject.SetActive(false);
//    gameObject.SetActive(false);
//    return;                 
//}
//if (associatedPlus != null)
//    associatedPlus.gameObject.SetActive(true);
//gameObject.SetActive(true);