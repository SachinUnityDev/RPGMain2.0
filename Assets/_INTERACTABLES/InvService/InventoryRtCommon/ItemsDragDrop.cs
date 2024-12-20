using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Common;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

namespace Interactables
{
    public class ItemsDragDrop : MonoBehaviour,  IBeginDragHandler
                                , IEndDragHandler, IDragHandler,  IPointerEnterHandler, IPointerExitHandler        
    {
        #region Drag and Drop Decalaration 
        [Header("Pointer related")]
        RectTransform rectTransform;
        [SerializeField] GameObject canvasGO;
        [SerializeField] CanvasGroup canvasGroup;

        [Header("Reference for the Public")]
        public iSlotable iSlotable; // talk to daddy
        public Iitems itemDragged;
        public Transform slotParent;

        GameObject clone;

        [Header("Item Card related")]
        [SerializeField] GameObject itemCardGO;
        [SerializeField] Vector3 offset;
        [SerializeField] List<GameObject> hovered = new List<GameObject>();
        #endregion

        private void OnEnable()
        {
            canvasGO = ItemService.Instance.canvasGO;
            itemCardGO = ItemService.Instance.itemCardGO;
        }

        void Start()
        {
            iSlotable = transform.parent.parent.GetComponent<iSlotable>();
            if (iSlotable == null)
                Debug.Log("ERROR Item Slot Controller Not found"+ gameObject.name);
            if (gameObject.GetComponent<CanvasGroup>() == null)
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            else
                canvasGroup = gameObject.GetComponent<CanvasGroup>();

        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (iSlotable.slotType == SlotType.GewgawsActiveInv || iSlotable.slotType == SlotType.PotionsActiveInv
            || iSlotable.slotType == SlotType.ProvActiveInv 
            || iSlotable.slotType == SlotType.TrophySelectSlot || iSlotable.slotType == SlotType.TrophyScrollSlot)
                return;
            hovered = eventData.hovered;
            if(iSlotable.ItemsInSlot.Count != 0)
            {
                //if (ItemService.Instance.itemCardGO == null)
                //{
                //   // ItemService.Instance.itemCardGO = Instantiate(ItemService.Instance.itemCardPrefab);
                //}

                Sequence seq = DOTween.Sequence();
                seq
                    .AppendCallback(()=> ItemService.Instance.itemCardGO.gameObject.SetActive(false))
                    .AppendCallback(() => PosItemCard())
                    .AppendInterval(0.5f)
                    .AppendCallback(() => ShowItemCard())
                    ;                 
                seq.Play();
            }
        }
    
        public void OnPointerExit(PointerEventData eventData)
        {
            if (iSlotable.slotType == SlotType.GewgawsActiveInv || iSlotable.slotType == SlotType.PotionsActiveInv
                || iSlotable.slotType == SlotType.ProvActiveInv || iSlotable.slotType == SlotType.TrophySelectSlot
            || iSlotable.slotType == SlotType.TrophyScrollSlot || iSlotable.slotType == SlotType.PotionActInCombat)
                return; // eliminating drag and drop on active slot of Potion n Gewgaw

            if(ItemService.Instance.itemCardGO != null)
            ItemService.Instance.itemCardGO.SetActive(false);
            if (InvService.Instance.invRightViewController.rightClickOpts.GetComponent<RightClickOpts>().isHovered)
                return; 

            ItemSlotController itemSlotController = iSlotable as ItemSlotController;
          
            Sequence closeSeq = DOTween.Sequence();
            closeSeq
                    .PrependInterval(0.1f)  
                    .AppendCallback(() => CloseRightClickOpts(itemSlotController))
                    ;
            if (itemSlotController != null)
                closeSeq.Play();
        }
    
        void CloseRightClickOpts(ItemSlotController itemSlotController)
        {
            if (!InvService.Instance.invRightViewController.rightClickOpts.GetComponent<RightClickOpts>().isHovered)
            {
                itemSlotController.CloseRightClickOpts();
                Debug.Log("CLOSED on ITEM DRAG"); 
            }
        }

        void ShowItemCard()
        {
            if(hovered.Any(t=>t == gameObject))
            itemCardGO.GetComponent<ItemCardView>().ShowItemCard(iSlotable.ItemsInSlot[0]);             
        }
        void PosItemCard()
        {
             canvasGO = GameObject.FindGameObjectWithTag("Canvas");

            itemCardGO = ItemService.Instance.itemCardGO;
            itemCardGO.transform.SetParent(canvasGO.transform);

            int index = itemCardGO.transform.parent.childCount - 1;
            itemCardGO.transform.SetSiblingIndex(index);
            RectTransform invXLRect = itemCardGO.GetComponent<RectTransform>();

            invXLRect.pivot = new Vector2(0.5f, 0.5f);
            invXLRect.localScale = Vector3.one;

            //if (iSlotable.slotType == SlotType.TradeScrollSlot)
            //    PosTradeScrollSlot();
             if (iSlotable.slotType == SlotType.PotionActInCombat)
                PosItemCardInCombat();
            else
                PosItemCardInInv();
        }

        void PosItemCardInCombat()
        {
            float width = itemCardGO.GetComponent<RectTransform>().rect.width;
            float height = itemCardGO.GetComponent<RectTransform>().rect.height;

            Canvas canvasObj = canvasGO.GetComponent<Canvas>();
            // get slot index based on slot index adjust the offset
            Transform slotTrans = transform.parent.parent;
            
            Vector3 offsetFinal;

            offset = new Vector3(0, (height/2+100), 0);
            offsetFinal = (offset) * canvasObj.scaleFactor;
            
            Vector3 pos = transform.position + offsetFinal;

            Sequence seq = DOTween.Sequence();
            seq
                .Append(itemCardGO.transform.DOMove(pos, 0.1f))
                .Append(itemCardGO.transform.GetComponent<Image>().DOFade(1.0f, 0.3f))
                ;
            //itemCardGO.SetActive(true);
            seq.Play();
        }
        void PosTradeScrollSlot()
        {
            float width = itemCardGO.GetComponent<RectTransform>().rect.width;
            float height = itemCardGO.GetComponent<RectTransform>().rect.height;

            Canvas canvasObj = canvasGO.GetComponent<Canvas>();
            // get slot index based on slot index adjust the offset
            Transform slotTrans = transform.parent.parent;

            Vector3 offsetFinal;

            offset = new Vector3(150, (height / 2), 0);
            offsetFinal = (offset) * canvasObj.scaleFactor;

            Vector3 pos = transform.position + offsetFinal;

            Sequence seq = DOTween.Sequence();
            seq
                .Append(itemCardGO.transform.DOMove(pos, 0.1f))
                .Append(itemCardGO.transform.GetComponent<Image>().DOFade(1.0f, 0.3f))
                ;
            itemCardGO.SetActive(true);
            seq.Play();
        }
        void PosItemCardInInv()
        {
            float width = itemCardGO.GetComponent<RectTransform>().rect.width;
            float height = itemCardGO.GetComponent<RectTransform>().rect.height;

            //if (GameService.Instance.gameModel.gameState == GameState.InTown)
            //    canvas = GameObject.FindWithTag("TownCanvas");
            //if (GameService.Instance.gameModel.gameState == GameState.InQuest)
            //    canvas = GameObject.FindWithTag("QuestCanvas");

            Canvas canvasObj = canvasGO.GetComponent<Canvas>();
            // get slot index based on slot index adjust the offset
           Transform slotTrans = transform.parent.parent;
            int slotIndex = slotTrans.GetSiblingIndex() % 6;
            Vector3 offsetFinal; 
            if(slotIndex <= 2)
            {
                offset = new Vector3(100, 70,0);
                offsetFinal = (offset + new Vector3(width / 2, -(height / 2)+100f, 0)) * canvasObj.scaleFactor;
            }
            else
            {
                offset = new Vector3(-100, 70,0);
                offsetFinal = (offset + new Vector3(-width / 2, -(height / 2)+100f, 0)) * canvasObj.scaleFactor;
            }

            Vector3 pos = transform.position + offsetFinal;

            Sequence seq = DOTween.Sequence();
            seq
                .Append(itemCardGO.transform.DOMove(pos, 0.01f))
                .Append(itemCardGO.transform.GetComponent<Image>().DOFade(1.0f, 0.2f))
                ;
               // itemCardGO.SetActive(true);
            seq.Play(); 
        }


        #region DRAG N DROP POINTER METHODS 
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (iSlotable.slotType == SlotType.ProvActiveInv || iSlotable.slotType == SlotType.TrophySelectSlot
                || iSlotable.slotType == SlotType.TrophyScrollSlot)
                return;
            iSlotable.CloseRightClickOpts();
            rectTransform = GetComponent<RectTransform>();
            slotParent = transform.parent;

            canvasGO = GetComponentInParent<Canvas>().gameObject;

            if (canvasGO == null)
                Debug.LogError(" Canva sis null"); 
            transform.SetParent(canvasGO.transform);  // to keep draged object on top 
            transform.SetAsLastSibling();   
            if (iSlotable.ItemsInSlot.Count <= 0)            
            {
                CreateEmptyClone(); 
                
            }else
            {
                CreateClone();
            }
            itemDragged = iSlotable.ItemsInSlot[0];
            iSlotable.RemoveItem();           
            canvasGroup.blocksRaycasts = false;
        }

       void CreateEmptyClone()
        {
            GameObject cloneGO = InvService.Instance.InvSO.itemImgPrefab;
            clone = Instantiate(cloneGO);
            clone.transform.SetParent(slotParent);
            RectTransform cloneRect = clone.GetComponent<RectTransform>();
            cloneRect.anchoredPosition = Vector3.zero;
            cloneRect.localScale = Vector3.one;
            //clone.GetComponent<CanvasGroup>().blocksRaycasts = false;
            SetAnchorsMaxStretch(cloneRect);
        }
        private void SetAnchorsMaxStretch(RectTransform rectTransformClone)
        {
            // Set anchors to cover the entire parent container (0, 0, 1, 1)
            rectTransformClone.anchorMin = Vector2.zero;
            rectTransformClone.anchorMax = Vector2.one;

            // Reset the size to zero (optional, depending on your requirements)
            rectTransformClone.sizeDelta = Vector2.zero;
            rectTransformClone.anchoredPosition = Vector2.zero;

            // Optional: Set the pivot to control the reference point during scaling
            rectTransformClone.pivot = new Vector2(0.5f, 0.5f);
        }
        void CreateClone() // clone should have raycast block while transfer
        {
            clone = Instantiate(this.gameObject);
            clone.transform.SetParent(slotParent);
            RectTransform cloneRect = clone.GetComponent<RectTransform>();
            cloneRect.localPosition= Vector3.zero;
            cloneRect.anchoredPosition = Vector3.zero;
            cloneRect.localScale = Vector3.one;
            //clone.GetComponent<CanvasGroup>().blocksRaycasts = false; 
            SetAnchorsMaxStretch(cloneRect);

        }

        public void OnDrag(PointerEventData eventData)
        {
            if (iSlotable.slotType == SlotType.ProvActiveInv || iSlotable.slotType == SlotType.TrophySelectSlot
                || iSlotable.slotType == SlotType.TrophyScrollSlot)
                return;
            if (GameService.Instance.currGameModel.gameScene == GameScene.TOWN
                || GameService.Instance.currGameModel.gameScene == GameScene.QUEST)
                canvasGO = GameObject.FindGameObjectWithTag("Canvas");
             
            rectTransform.anchoredPosition += eventData.delta/canvasGO.GetComponent<Canvas>().scaleFactor;           
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (iSlotable.slotType == SlotType.ProvActiveInv || iSlotable.slotType == SlotType.TrophySelectSlot
            || iSlotable.slotType == SlotType.TrophyScrollSlot)
                return;
            canvasGroup.blocksRaycasts = true;            
            GameObject draggedGO = eventData.pointerDrag;

            if (draggedGO.transform.parent?.GetComponent<IDropHandler>() == null
                && draggedGO.transform.parent.parent?.GetComponent<IDropHandler>() == null)
            {               
                InvService.Instance.On_DragResult(false, this);
            }
            else
            {
                if (clone.GetComponent<CanvasGroup>() != null)
                {
                    clone.GetComponent<CanvasGroup>().blocksRaycasts = true;
                }
            }
        }
    
        #endregion
    }



}

