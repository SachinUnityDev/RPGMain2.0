using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics.Contracts;

namespace Interactables
{
    [Serializable]
    public class ItemImgData
    {
        public ItemType itemType;
        public Sprite BGSprite;
        public Sprite FilterIconN;
        public Sprite FilterIconHL;
    }
    [Serializable]
    public class SlotImgData
    {
        public GewgawSlotType slotType;
        public Sprite slotBG;  
    }

    [CreateAssetMenu(fileName = "ItemViewSO", menuName = "Item Service/ItemViewSO")]
    public class ItemViewSO : ScriptableObject
    {
        [Header("Gewgaw slot Img data")]
        public List<SlotImgData> allSlotImgData = new List<SlotImgData>();
        [Header("Item BG and filter Img data")]
        public List<ItemImgData> allItemImgData = new List<ItemImgData> ();

        [Header("Generic Gewgaw Quality")]
        public Sprite lyricBG;
        public Sprite folkoricBG;
        public Sprite epicBG;

        private void Awake()
        {
            if (allSlotImgData.Count == 0)
            {
                for (int i = 1; i < Enum.GetNames(typeof(GewgawSlotType)).Length; i++)
                {
                    GewgawSlotType slotType = (GewgawSlotType)i;
                    SlotImgData slotImgData = new SlotImgData();
                    slotImgData.slotType = slotType;
                    allSlotImgData.Add(slotImgData);

                }
            }
            if (allItemImgData.Count == 0)
            {
                for (int i = 1; i < Enum.GetNames(typeof(ItemType)).Length; i++)
                {
                    ItemType itemType = (ItemType)i;
                    ItemImgData itemImgData = new ItemImgData();
                    itemImgData.itemType = itemType;
                    allItemImgData.Add(itemImgData);
                }
            }
      
        }

    }
}