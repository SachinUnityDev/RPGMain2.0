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

        private void OnEnable()
        {
            GetItemRef();
        }
        void GetItemRef()
        {
            slotID = transform.GetSiblingIndex();
            itemImg = transform.GetChild(0).GetComponentInChildren<Image>();
            itemBG = transform.GetComponent<Image>();
            itemFrame = transform.GetChild(2);
            // deSelect State...
            isSelected = false;
            itemFrame.gameObject.SetActive(false);
        }

        public void InitSlot(ToolNames toolName, Iitems item, ToolView toolView)
        {
             GetItemRef();
            if (toolName == ToolNames.None)
            {
                transform.gameObject.SetActive(false); return; 
            }            
            this.toolView = toolView;
            this.item = item;

            transform.gameObject.SetActive(true);
            itemImg.gameObject.SetActive(true);

            itemImg.sprite = GetSprite(toolName);
            itemBG.sprite = GetBGSprite(item);
            if (item == null)
            {
                
                 SetInactive(toolName);
                return;
            }      
                   
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

        public void SetInactive(ToolNames toolName)
        {
            transform.GetComponent<Image>().sprite = InvService.Instance.InvSO.emptySlot;
            item = null;
            isSelected = false;

            //itemFrame.gameObject.SetActive(false);
        }


        Sprite GetSprite(ToolNames toolName)
        {
            Sprite sprite = InvService.Instance.InvSO.GetSprite((int)toolName, ItemType.Tools);
            if (sprite != null)
                return sprite;
            else
                Debug.Log("SPRITE NOT FOUND");
            return null;
        }
        Sprite GetBGSprite(Iitems item)
        {
            if(item == null) return InvService.Instance.InvSO.emptySlot;
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
            if(item == null) return;
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