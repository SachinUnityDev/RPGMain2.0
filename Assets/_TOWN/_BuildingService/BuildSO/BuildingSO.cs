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
        
        [SerializeField] List<BuildingData> _allbuildingData = new List<BuildingData>();
        public List<BuildingData> allBuildingData
        {
            get => _allbuildingData;
            set { _allbuildingData = value; }
        }
        
        [SerializeField] List<ItemInteractionStatus> _itemInteraction = new List<ItemInteractionStatus>();   
        public List<ItemInteractionStatus> ItemInteraction { get => _itemInteraction; 
                                                                set  { _itemInteraction = value; } }

        private void Awake()
        {
          
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
    public class ItemInteractionStatus
    {
        public buildItem buildItem;
        public bool isUnlocked;
    }
    public interface IBuildSO
    {
         List<BuildingData> allBuildingData { get; set; }   
        List<ItemInteractionStatus> ItemInteraction { get; set; }


    }


}

