using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Town
{
    [CreateAssetMenu(fileName = "BuildingSO", menuName = "Town Service/BuildingSO")]
    public class BuildingSO : ScriptableObject
    {
        public List<BuildingData> allBuildingData = new List<BuildingData>();
        public List<InteractionSpriteData> allIntSprites = new List<InteractionSpriteData>();

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




}

