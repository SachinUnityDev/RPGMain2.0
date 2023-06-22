using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace Quest
{
    public class ToolSlotView : MonoBehaviour, IPointerClickHandler
    {

        ToolView toolView;

        [Header("Trans references")]
        public int slotID;
        [SerializeField] Image itemImg;
        [SerializeField] Image itemBG;
        [SerializeField] Transform itemFrame;
      

        [SerializeField] Iitems item;
        [SerializeField] bool isSelected;

        private void Awake()
        {
            slotID = transform.GetSiblingIndex();
            itemImg = transform.GetChild(0).GetComponentInChildren<Image>();
            itemBG = transform.GetComponent<Image>();
            itemFrame = transform.GetChild(2);          
            // deSelect State...
            isSelected = false;
            itemFrame.gameObject.SetActive(false);
        }
        public void InitSlot(Iitems item, ToolView toolView)
        {
            if (item == null)
            {
                transform.gameObject.SetActive(false);
                return;
            }
            this.toolView = toolView;
            this.item = item;
            transform.gameObject.SetActive(true);
            itemImg.gameObject.SetActive(true);
            itemImg.sprite = GetSprite(item.itemName, item.itemType);
            itemBG.sprite = GetBGSprite(item);
            
            if (transform.GetSiblingIndex() == 0)
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
            item = null;
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
            toolView.OnSlotSelected(item);
        }
        public void OnDeSelected()
        {
            isSelected = false; 
            itemFrame.gameObject.SetActive(false);
            toolView.OnSlotDeSelected(item);
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!isSelected)
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