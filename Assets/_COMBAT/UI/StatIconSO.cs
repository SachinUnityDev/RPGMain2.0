using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System; 
namespace Combat
{
    [CreateAssetMenu(fileName = "StatIconSO", menuName = "Combat/StatIconSO")]

    public class StatIconSO : ScriptableObject
    {
        public List<SpriteData> allSpriteData;

        private void Awake()
        {
            if (allSpriteData.Count > 1) return; 
            allSpriteData = new List<SpriteData>(); 
            for(int i =6; i < Enum.GetNames(typeof(AttribName)).Length; i++)
            {
                SpriteData spriteData = new SpriteData();
                spriteData.statName = (AttribName)i;
                allSpriteData.Add(spriteData); 
            }
        }
    }

    [System.Serializable]
    public class SpriteData
    {
        public AttribName statName;
        public Sprite statSprite;
        public Sprite statSpriteLit;  
    }
}

