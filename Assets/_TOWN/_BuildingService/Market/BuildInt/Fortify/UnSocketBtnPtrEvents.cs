using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
namespace Town
{
    public class UnSocketBtnPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] Sprite btnN;
        [SerializeField] Sprite btnHL;
        [SerializeField] Sprite btnDisabled;
        [SerializeField] Transform costDisplay; 

        Image btnImg; 

        [Header("Global Var")]
        [SerializeField] CharNames charSelect;
        [SerializeField] ArmorModel armorModel;
        [SerializeField] ItemModel itemModel;
        [SerializeField] UnSocketView unSocketView;

        [SerializeField] int COST_BRONZE_UNSOCKET_DIV = 10;
        [SerializeField] int COST_BRONZE_UNSOCKET_SUPPORT = 8;

        [SerializeField] bool isDisabled = false;
        int divGemCount = 0;
        int supportGemCount = 0;
        Currency currReq; 

        public void InitUnSocketBtnPtrEvents(CharNames charSelect
                        , ArmorModel armorModel, UnSocketView unSocketView)
        {
            this.charSelect = charSelect;
            this.armorModel = armorModel;
            CharController charController = CharService.Instance.GetAbbasController(charSelect); 
            ItemController itemController = charController.itemController;
            itemModel = itemController.itemModel;
            this.unSocketView = unSocketView;
            SetSocketCount();
            SetCurrReq();

            if (itemModel.IsUnSocketable() ||
                !EcoServices.Instance.HasMoney(PocketType.Inv, currReq))
                unSocketView.StatusUpdate(UnsocketStatus.UnSocketed); 
            else
                unSocketView.StatusUpdate(UnsocketStatus.Socketed);
            SetBtnState(PocketType.Inv);
        }
     
        void SetCurrReq()
        {
            int bronze2Sub = divGemCount * COST_BRONZE_UNSOCKET_DIV 
                                + supportGemCount * COST_BRONZE_UNSOCKET_SUPPORT;
            currReq = new Currency(0, bronze2Sub).RationaliseCurrency();

            costDisplay.GetChild(1).GetComponent<DisplayCurrency>().Display(currReq); 
        }
        void SetSocketCount()
        {
            for (int i = 0; i < 2; i++)
            {
                if (itemModel.divItemsSocketed[i] != null)
                {
                    divGemCount++;
                }
            }
            if (itemModel.supportItemSocketed != null)
                supportGemCount++;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (itemModel.IsUnSocketable())
                return;
            UnSocketGems();
            btnImg.sprite = btnN;
        }
        void UnSocketGems()
        {  
            EcoServices.Instance.DebitPlayerInvThenStash(currReq);
            
            itemModel.divItemsSocketed[0] = null;
            itemModel.divItemsSocketed[1] = null;
            itemModel.supportItemSocketed = null;
            unSocketView.FillCharPlanks(); 
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            if (isDisabled)
                return;
            btnImg.sprite = btnHL; 

        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (isDisabled)
                return;
            btnImg.sprite = btnN;
        }
        void SetBtnState(PocketType pocketType)
        {            
            if(itemModel.IsUnSocketable() || 
                !EcoServices.Instance.HasMoney(pocketType, currReq))
            {
                isDisabled= true;
                btnImg.sprite = btnDisabled; 
            }
            else
            {
                isDisabled= false;
            }
        }

        private void Awake()
        {
            btnImg = transform.GetChild(0).GetComponent<Image>();
            btnImg.sprite = btnN;

            EcoServices.Instance.OnPocketSelected += SetBtnState; 
        }
    }
}