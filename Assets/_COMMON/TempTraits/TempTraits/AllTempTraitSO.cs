using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    [System.Serializable]
    public class TempTBehaviourSpriteData
    {
        public TraitBehaviour tempTraitBehaviour;
        public Sprite iconSprite;
        public Sprite cardBG; 
    }
    [System.Serializable]
    public class TempTraitTypeSpriteData
    {
        public TempTraitType tempTraitType; 
        public Sprite iconSprite;        
    }

    [CreateAssetMenu(fileName = "AllTempTraitSO", menuName = "Common/AllTempTraitSO")]
    public class AllTempTraitSO : ScriptableObject
    {
        [Header("temp trait SO s")]
        public List<TempTraitSO> allTempTraitsSO = new List<TempTraitSO>();

        [Header("Temp trait Sprite Data")]
        public List<TempTBehaviourSpriteData> alltempTraitBehaviorSprite = new List<TempTBehaviourSpriteData>();    
        public List<TempTraitTypeSpriteData> allTempTraitTypeSprite = new List<TempTraitTypeSpriteData>();  


        public TempTraitSO GetTempTraitSO(TempTraitName tempTraitName)
        {
            int index = allTempTraitsSO.FindIndex(t=>t.tempTraitName== tempTraitName);
            if(index != -1)
            {
                return allTempTraitsSO[index];
            }
            else
            {
                Debug.Log("temp trait SO not found" + tempTraitName); 
                return null;
            }
        }

        public TempTraitTypeSpriteData GetTraitTypeSpriteData(TempTraitType tempTraitType)
        {
            int index = allTempTraitTypeSprite.FindIndex(t => t.tempTraitType == tempTraitType);
            if (index != -1)
            {
                return allTempTraitTypeSprite[index];
            }
            else
            {
                Debug.Log("temp trait SO not found" + tempTraitType);
                return null;
            }
        }
        public TempTBehaviourSpriteData GetTempTraitBehaviourData(TraitBehaviour tempTraitBehaviour)
        {
            int index = alltempTraitBehaviorSprite.FindIndex(t => t.tempTraitBehaviour == tempTraitBehaviour);
            if (index != -1)
            {
                return alltempTraitBehaviorSprite[index];
            }
            else
            {
                Debug.Log("temp trait SO not found" + tempTraitBehaviour);
                return null;
            }
        }
    }
}