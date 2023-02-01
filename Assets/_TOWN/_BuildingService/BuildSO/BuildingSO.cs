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

        [SerializeField] BuildingData _allbuildingData; 

        public BuildingData buildingData
        {
            get => _allbuildingData;
            set { _allbuildingData = value; }
        }
        
       
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
    public class BuildInteractData
    {
        public buildItem buildItem;
        public bool isUnlocked;
      
    }
    public interface IBuildSO
    {
        BuildingData buildingData { get; set; }   
    }


}

