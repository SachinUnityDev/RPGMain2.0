using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;
using UnityEngine.UI;
using DG.Tweening;
using Common;
using System;

namespace Town
{
    // Provision View is for the Potion Select Panel in the House
    public class ProvisionView : MonoBehaviour, IPanel
    {
        public int selectIndex;
        public PotionNames potionName;

        [Header(" to be ref")]
        [SerializeField] Button closeBtn;
        [SerializeField] Button tickBtn;

        [SerializeField] Transform optContainer;
        [SerializeField] Transform arrowTrans;
        [SerializeField] List<Iitems> provisonOpts = new List<Iitems>();

        [SerializeField] Transform slotTrans; 
        private void Awake()
        {
            tickBtn.onClick.AddListener(OnAddItem2ProvisionSlot);
        }
        private void Start()
        {
            closeBtn.onClick.AddListener(UnLoad);
            
        }
        private void OnEnable()
        {
            SetCurrentPotionInPanel4Abbas();
        }
        void SetCurrentPotionInPanel4Abbas()
        {
            // get char Select
            CharController charController = CharService.Instance.GetAllyController(CharNames.Abbas);
            
            if(charController.charModel.provisionItems.Count > 0)
            potionName = (PotionNames)charController.charModel.provisionItems[0].itemDataQty.itemData.itemName;

            int i = 0;
            switch (potionName)
            {
                case PotionNames.None:
                    i = 0;
                    Debug.LogError("PotionNames.None"); 
                    break;
                case PotionNames.HealthPotion:
                    i = 0;  break;
                case PotionNames.StaminaPotion:
                    i = 1; break;                    
                case PotionNames.FortitudePotion:
                    i = 2; break;                 
                default:
                    i = 0; break;
            }
            OnSelect(potionName, i); 
        }

        public void OnSelect(PotionNames _potionName, int index)
        {
            foreach (Transform child in optContainer)
            {
                ProvisionOptionsPtrEvents opts = child.GetComponent<ProvisionOptionsPtrEvents>(); 
                if(opts.potionName != _potionName)
                {
                    opts.OnDeSelect(true);
                }                
            }
            if (provisonOpts.Count == 0)
            {
                ProvisionPopulate();
            }
            selectIndex = index; 
            potionName = _potionName;
            MoveArrow();
            OnAddImg2Slot(provisonOpts[selectIndex]);
            UpdateInCharModel4Abbas();
        }

        void MoveArrow()
        {
            Vector3 pos = 
            optContainer.GetChild(selectIndex).GetComponent<RectTransform>().position;
            arrowTrans.DOMoveY(pos.y, 0.1f); 
        }

         void OnAddImg2Slot(Iitems item)
        {
            slotTrans.GetComponent<HouseProvSlotView>().Init(item); 
        }

        void UpdateInCharModel4Abbas() // for Abbas only
        {
            CharController charController = CharService.Instance.GetAllyController(CharNames.Abbas);

            if (charController.charModel.provisionItems.Count > 0)
               charController.charModel.provisionItems[0].itemDataQty.itemData.itemName = (int)potionName;
        }
        void OnAddItem2ProvisionSlot()
        {
            CharController charController = CharService.Instance.GetAllyController(CharNames.Abbas);
            ItemData itemData = new ItemData(ItemType.Potions, (int)potionName);
            Iitems item = ItemService.Instance.GetNewItem(itemData); 
            if (item != null)
            {
                InvService.Instance.invMainModel.EquipItem2PotionProvSlot(item, charController); 
            }
            UnLoad();
        }

        public void Load()
        {
            
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
        }

        public void Init()
        {
            ProvisionPopulate();
            SetCurrentPotionInPanel4Abbas();
        }
        void ProvisionPopulate()
        {
            if (provisonOpts.Count == 0)
            {
                Iitems item1 =
                        ItemService.Instance.itemFactory.GetNewItem(ItemType.Potions, (int)PotionNames.HealthPotion);
                Iitems item2 =
                         ItemService.Instance.itemFactory.GetNewItem(ItemType.Potions, (int)PotionNames.StaminaPotion);
                Iitems item3 =
                         ItemService.Instance.itemFactory.GetNewItem(ItemType.Potions, (int)PotionNames.FortitudePotion);

                provisonOpts.AddRange(new List<Iitems>() { item1, item2, item3 });
            }
        }
    }
}