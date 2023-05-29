using Interactables;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace Quest
{
    public class LootSlotView : MonoBehaviour, IPointerClickHandler
    {
        LootView lootView; 

        [Header("Trans references")]
        public int slotID;
        [SerializeField] Image itemImg;
        [SerializeField] Image itemBG;
        [SerializeField] Transform itemFrame;

        [SerializeField] ItemDataWithQty itemDataWithQty;
        [SerializeField] bool isSelected;

        private void Awake()
        {
            slotID = transform.GetSiblingIndex();    
            itemImg = transform.GetChild(0).GetChild(0).GetComponent<Image>();  
            itemBG = transform.GetComponent<Image>();
            itemFrame = transform.GetChild(2);
            // deSelect State...
            isSelected = false;
            itemFrame.gameObject.SetActive(false);
        }
        public void InitSlot(ItemDataWithQty itemDataWithQty, LootView lootView)
        {
            if(itemDataWithQty == null)
            {
                transform.gameObject.SetActive(false);
                return; 
            }
            this.lootView= lootView;
            this.itemDataWithQty = itemDataWithQty;
            transform.gameObject.SetActive(true);
            itemImg.gameObject.SetActive(true);
            itemImg.sprite = GetSprite(itemDataWithQty.ItemData.itemName, itemDataWithQty.ItemData.itemType);
            itemBG.sprite = GetBGSprite(itemDataWithQty.ItemData);
            if (lootView.IsItemSelected(itemDataWithQty))
            {
                isSelected = true;
                itemFrame.gameObject.SetActive(true);
            }
            else
            {
                isSelected = false;
                itemFrame.gameObject.SetActive(false);
            }
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

        public void OnSelected()
        {
            isSelected = true;
            itemFrame.gameObject.SetActive(true);
            lootView.OnSlotSelected(itemDataWithQty);
        }
        public void OnDeSelected()
        {
            isSelected= false;
            itemFrame.gameObject.SetActive(false);
            lootView.OnSlotDeSelected(itemDataWithQty);
        }
        public void OnPointerClick(PointerEventData eventData)
        {
           if(!isSelected)
            {
                OnSelected();
            }
            else
            {
                OnDeSelected();
            }
        }
    }
}