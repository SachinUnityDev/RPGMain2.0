using Common;
using DG.Tweening;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;
using Town;

namespace Quest
{


    public class LootView : MonoBehaviour, IPanel
    {

        [Header("container")]
        [SerializeField] Transform containerTrans; 


        [Header("Loot All and Continue Btn")]
        //[SerializeField] Button lootAllBtn;
        //[SerializeField] Button continueBtn;

        [Header("Canvas NTBR")]
        [SerializeField] Canvas canvas;

        [Header("Loot Scroll")]
        [SerializeField] int index = 1;

        [SerializeField] Button leftBtn;
        [SerializeField] Button rightBtn;
        [SerializeField] float prevLeftClick = 0f;
        [SerializeField] float prevRightClick = 0f;


        // testing
        [SerializeField] int startPos;
        [SerializeField] int endPos;
        [SerializeField] int max; 



        List<ItemDataWithQty> lootList = new List<ItemDataWithQty>();
        public void InitLootList(List<ItemDataWithQty> lootList)
        {
            this.lootList.Clear();
            this.lootList = lootList.DeepClone();
          
            Load();
            max = (int)lootList.Count / 6;
            if (lootList.Count % 6 != 0)
                max++;
            FillScrollSlots();
        }
        void FillScrollSlots()
        {  
            startPos = index * 6;
           
            for (int i = 0; i < containerTrans.childCount; i++)
            {
                int j = startPos + i; 
                if (j <lootList.Count)
                {
                    containerTrans.GetChild(i).GetComponent<LootSlotView>()
                    .InitSlot(lootList[j]);
                }
                else
                {
                    containerTrans.GetChild(i).GetComponent<LootSlotView>()
                  .InitSlot(null);
                }
                
            }
        }


        void OnLeftBtnPressed()
        {
            if (Time.time - prevLeftClick < 0.3f) return;
            if (index <= 0)
            {
                index = max-1;
                FillScrollSlots();
            }
            else
            {
                --index;
                FillScrollSlots();
            }
            prevLeftClick = Time.time;
        }
        void OnRightBtnPressed()
        {
            if (Time.time - prevRightClick < 0.3f) return;
            if ((index+1) == max)
            {
                index = 0;
                FillScrollSlots();
            }
            else
            {
                ++index;
                FillScrollSlots();
            }
            prevRightClick = Time.time;
        }

        void Start()
        {
            leftBtn.onClick.AddListener(OnLeftBtnPressed);
            rightBtn.onClick.AddListener(OnRightBtnPressed);
        }
        void OnLootAllPressed()
        {
            // remove all 

            for (int i = 0; i < transform.GetChild(0).childCount; i++)
            {
                Transform child = transform.GetChild(0).GetChild(i);  // go
               // child.gameObject.GetComponent<LootSlotView>().RemoveAllItems();// select all items
            }
        }

   
        #region TO_INV_FILL
        void ClearLootFill()   
        {
            for (int i = 0; i < transform.GetChild(0).childCount; i++)
            {
                Transform child = transform.GetChild(0).GetChild(i);  // go
                child.gameObject.GetComponent<LootSlotView>().ClearSlot();
            }
        }
        #endregion


        #region INIT, LOAD, and UNLOAD
        public void Init()
        {
          
        }
        public void Load()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, true);
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
        }
        #endregion
    }
}