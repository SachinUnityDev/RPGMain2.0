using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;


namespace Interactables
{
    public class ItemActionPtrController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public ItemActions itemActions;
        [SerializeField] Color colorN;
        [SerializeField] Color colorHL;
        [SerializeField] Color colorUnClickable;
        IComInvActions iComInvActions; 

        public bool isClickable = true; 
        public void Init(ItemActions itemActions, IComInvActions iComInvAction) 
        {
            this.itemActions = itemActions;
            this.iComInvActions = iComInvAction;    
        }

        public void ResetItemAction()
        {
            itemActions = ItemActions.None;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("CLICKED" + itemActions);
            switch (itemActions)
            {
                case ItemActions.None:
                    break;
                case ItemActions.Equipable:
                    iComInvActions.Equip(); 
                    break;
                case ItemActions.Consumable:
                    iComInvActions.Consume();
                    break;
                case ItemActions.Disposable:
                    iComInvActions.Dispose();
                    break;
                case ItemActions.Sellable:
                    iComInvActions.Dispose();
                    break;
                case ItemActions.Readable:
                    iComInvActions.Read();
                    break;
                case ItemActions.Enchantable:
                    iComInvActions.Enchant();
                    break;
                case ItemActions.Rechargeable:
                    iComInvActions.RechargeGem();
                    break;
                case ItemActions.Socketable:
                    iComInvActions.Socket();
                    break;
                default:
                    break;
            }
            InvService.Instance.invViewController.CloseRightClickOpts();
            ResetItemAction(); 
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(isClickable)
                transform.GetChild(0).GetComponent<TextMeshProUGUI>().color
                            = colorHL; 
            else
                transform.GetChild(0).GetComponent<TextMeshProUGUI>().color
                           = colorUnClickable;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (isClickable)
                transform.GetChild(0).GetComponent<TextMeshProUGUI>().color
                            = colorN;
            else
                transform.GetChild(0).GetComponent<TextMeshProUGUI>().color
                           = colorUnClickable;
        }

        void Start()
        {
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().color
             = colorN;
        }
    }
}
