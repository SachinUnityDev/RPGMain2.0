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
        [SerializeField] TextMeshProUGUI qty; 

        [SerializeField] ItemBaseWithQty itemBaseWithQty;
        [SerializeField] bool isSelected;

        private void Awake()
        {
            slotID = transform.GetSiblingIndex();    
            itemImg = transform.GetChild(0).GetChild(0).GetComponent<Image>();  
            itemBG = transform.GetComponent<Image>();
            itemFrame = transform.GetChild(2);
            qty = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();    
            // deSelect State...
            isSelected = false;
            itemFrame.gameObject.SetActive(false);
        }
        public void InitSlot(ItemBaseWithQty itemBaseWithQty, LootView lootView)
        {
            if(itemBaseWithQty == null)
            {
                transform.gameObject.SetActive(false);
                return; 
            }
            this.lootView= lootView;
            this.itemBaseWithQty = itemBaseWithQty;
            transform.gameObject.SetActive(true);
            itemImg.gameObject.SetActive(true);

            Debug.Log("ITEM>>>" + itemBaseWithQty.item.itemName); 

            itemImg.sprite = GetSprite(itemBaseWithQty.item.itemName, itemBaseWithQty.item.itemType);
            itemBG.sprite = GetBGSprite(itemBaseWithQty.item);
            if (itemBaseWithQty.qty > 1)
            {
                qty.transform.parent.gameObject.SetActive(true);
                qty.text = itemBaseWithQty.qty.ToString();
            }
            else
            {
                qty.transform.parent.gameObject.SetActive(false);
                qty.text = ""; 
            }

            if (lootView.IsItemSelected(itemBaseWithQty))
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
            itemBaseWithQty= null;
            isSelected = false;
            itemFrame.gameObject.SetActive(false);
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
        Sprite GetBGSprite(Iitems item)
        {
            Sprite sprite = InvService.Instance.InvSO.GetBGSprite(item);
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
            lootView.OnSlotSelected(itemBaseWithQty);
        }
        public void OnDeSelected()
        {
            isSelected= false;
            itemFrame.gameObject.SetActive(false);
            lootView.OnSlotDeSelected(itemBaseWithQty);
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