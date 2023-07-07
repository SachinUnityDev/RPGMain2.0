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

        [Header("Town BG : to be ref")]
        public Sprite TownBGDay;
        public Sprite TownBGNight;


        [Header("TAVERN SPL DATA")]
        [TextArea(5,10)]
        public List<String> allTipStrs = new List<String>();
         
        public int everyOneDrinkMinVal = 5;
        public int everyOneDrinkMaxVal = 10;

        [Header("TAVERN: buy drinks")]
        public Sprite OnTipable;
        public Sprite OnTipableHL; 
        public Sprite OnUnTipable;


        public GameObject npcInteractPrefab; 

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

        public InteractionSpriteData GetInteractData(BuildInteractType buildIntType)
        {
            int index = allIntSprites.FindIndex(t=>t.intType == buildIntType);  
            if(index != -1)
            {
                return allIntSprites[index];
            }
            else
            {
                Debug.Log("build Interact data not found" + buildIntType);
                return null;
            }
        }

        public BuildingSO GetBuildSO(BuildingNames _buildName)
        {
            int index = allBuildSO.FindIndex(t => t.buildingName == _buildName); 
            if(index != -1)
            {
                return allBuildSO[index];
            }
            else
            {
                Debug.Log("buildSO not found"+ _buildName);
                return null;
            }
        }

    }
}