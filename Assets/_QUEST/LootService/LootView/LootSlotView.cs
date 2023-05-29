using Interactables;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace Quest
{
    public class LootSlotView : MonoBehaviour, IPointerClickHandler
    {
        public int slotID;
        [SerializeField] Image itemImg;
        [SerializeField] Image itemBG;
        [SerializeField] Transform itemFrame;

        private void Awake()
        {
            slotID = transform.GetSiblingIndex();    
            itemImg = transform.GetChild(0).GetChild(0).GetComponent<Image>();  
            itemBG = transform.GetComponent<Image>();
            itemFrame = transform.GetChild(2);
        }
        public void InitSlot(ItemDataWithQty itemDataWithQty)
        {
            if(itemDataWithQty == null)
            {
                transform.gameObject.SetActive(false);
                return; 
            }
            transform.gameObject.SetActive(true);
            itemImg.gameObject.SetActive(true);
            itemImg.sprite = GetSprite(itemDataWithQty.ItemData.itemName, itemDataWithQty.ItemData.itemType);
            itemBG.sprite = GetBGSprite(itemDataWithQty.ItemData); 

        }

        public void ClearSlot()
        {   
            transform.GetComponent<Image>().sprite = InvService.Instance.InvSO.emptySlot;
        }
    
  
        Sprite GetSprite(int itemName, ItemType itemType)
        {
            Sprite sprite = InvService.Instance.InvSO.GetSprite(itemName, itemType);
            if (sprite != null)
                return sprite;
            else
                Debug.Log("SPRITE NOT FOUND");
            return null;
        }
        Sprite GetBGSprite(ItemData itemData)
        {
            Sprite sprite = InvService.Instance.InvSO.GetBGSprite(itemData);
            if (sprite != null)
                return sprite;
            else
                Debug.Log("BG SPRITE NOT FOUND");
            return null;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
           
        }
    }
}