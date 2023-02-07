using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 
namespace Town
{


    [CreateAssetMenu(fileName = "AllBuildSO", menuName = "Town Service/AllBuildSO")]
    public class AllBuildSO : ScriptableObject
    {

        public List<InteractionSpriteData> allIntSprites = new List<InteractionSpriteData>();

        public List<BuildingSO> allBuildSO = new List<BuildingSO> ();

        private void Awake()
        {
            if (allIntSprites.Count < 1)   // patch fix to prevent recreation of fields 
            {
                for (int i = 1; i < Enum.GetNames(typeof(BuildInteractType)).Length; i++)
                {
                    InteractionSpriteData iSData = new InteractionSpriteData();
                    iSData.intType = (BuildInteractType)i;
                    allIntSprites.Add(iSData);
                }
            }
        }

        public BuildingSO GetBuildSO(BuildingNames _buildName)
        {
            //int index = allBuildSO.FindIndex(t=>t.)

            return null; 
        }

    }
}