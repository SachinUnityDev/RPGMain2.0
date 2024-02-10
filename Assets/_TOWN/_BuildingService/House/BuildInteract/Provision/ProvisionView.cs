using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;
using UnityEngine.UI;
using DG.Tweening;
using Common;

namespace Town
{
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
            selectIndex = index; 
            potionName = _potionName;
            MoveArrow();
            OnAddImg2Slot(provisonOpts[selectIndex]);
        }

        void MoveArrow()
        {
            Vector3 pos = 
            optContainer.GetChild(selectIndex).GetComponent<RectTransform>().position;
            arrowTrans.DOMoveY(pos.y, 0.1f); 
        }

        public void OnAddImg2Slot(Iitems item)
        {
            slotTrans.GetComponent<HouseProvSlotView>().Init(item); 
        }

        void OnAddItem2ProvisionSlot()
        {
            Debug.Log("Add to provision slot");
            ItemData itemData = new ItemData(ItemType.Potions, (int)potionName);
            Iitems item = ItemService.Instance.GetNewItem(itemData); 
            if (item != null)
            {
                InvService.Instance.invMainModel.EquipItem2PotionActInv(item, 2); 
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
            if (provisonOpts.Count == 0)
            {
                Iitems item1 =
                        ItemService.Instance.itemFactory.GetNewItem(ItemType.Potions,  (int)PotionNames.HealthPotion);
                Iitems item2 =
                         ItemService.Instance.itemFactory.GetNewItem(ItemType.Potions, (int)PotionNames.StaminaPotion);
                Iitems item3 =
                         ItemService.Instance.itemFactory.GetNewItem(ItemType.Potions, (int)PotionNames.FortitudePotion);

                provisonOpts.AddRange(new List<Iitems>() { item1, item2, item3 }); 

            }
            OnSelect(PotionNames.HealthPotion, 0);
        }
    }
}