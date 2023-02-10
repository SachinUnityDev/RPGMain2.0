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
        [SerializeField] Button tickBtn;
        [SerializeField] Transform optContainer;
        [SerializeField] Transform arrowTrans;
        List<Iitems> provisonOpts; 
        private void Awake()
        {
            tickBtn.onClick.AddListener(OnAdd2ProvisionSlot);                 

        }
        private void Start()
        {
            provisonOpts = new List<Iitems>();
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
        }

        void MoveArrow()
        {
            Vector3 pos = 
            optContainer.GetChild(selectIndex).GetComponent<RectTransform>().position;
            arrowTrans.DOMoveY(pos.y, 0.1f); 
        }

        public void OnAdd2ProvisionSlot()
        {
            //add to abbas provision slot here
        }

        public void Load()
        {
            
        }

        public void UnLoad()
        {

        }

        public void Init()
        {
            if (provisonOpts.Count == 0)
            {
                Iitems item1 =
                ItemService.Instance.itemFactory.GetNewPotionItem(PotionNames.HealthPotion);
                Iitems item2 =
                ItemService.Instance.itemFactory.GetNewPotionItem(PotionNames.StaminaPotion);
                Iitems item3 =
                    ItemService.Instance.itemFactory.GetNewPotionItem(PotionNames.FortitudePotion);

                provisonOpts.AddRange(new List<Iitems>() { item1, item2, item3 }); 
            }
        }
    }
}