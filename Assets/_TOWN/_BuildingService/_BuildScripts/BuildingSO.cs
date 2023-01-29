using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Ink.Runtime;

namespace Town
{
    [CreateAssetMenu(fileName = "BuildingSO", menuName = "Town Service/BuildingSO")]
    public class BuildingSO : ScriptableObject, IBuildSO
    {
        
        public List<InteractionSpriteData> allIntSprites = new List<InteractionSpriteData>();
        [SerializeField] List<BuildingData> _allbuildingData = new List<BuildingData>();
        public List<BuildingData> allBuildingData
        {
            get => _allbuildingData;
            set { _allbuildingData = value; }
        }
        
        [SerializeField] List<BuildingItemData> _buildItemData = new List<BuildingItemData>();   
        public List<BuildingItemData> itemData { get => _buildItemData; 
                                                                set  { _buildItemData = value; } }

        private void Awake()
        {
                if (allIntSprites.Count < 1)   // patch fix to prevent recreation of fields 
                {                      
                    for (int i = 1; i < Enum.GetNames(typeof(IntType)).Length; i++)
                    {
                        InteractionSpriteData iSData = new InteractionSpriteData();
                        iSData.intType = (IntType)i;
                        allIntSprites.Add(iSData);
                    }
                }
        }

    }

    public enum buildItem
    {
        None, 
        Bed, 
        Guitar, 
        Chest, 
    }
    [Serializable]
    public class BuildingItemData
    {
        public buildItem buildItem;

        [Header("Sprites")]
        public Sprite buildItemDay;
        public Sprite buildItemDayHL;
        public Sprite buildItemNight;
        public Sprite buildItemNightHL;

        public bool isUnlocked;

    }
    public interface IBuildSO
    {
         List<BuildingData> allBuildingData { get; set; }   
        List<BuildingItemData> itemData { get; set; }


    }


}

