using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Common
{
    [CreateAssetMenu(fileName = "LandscapeSO", menuName = "Common/LandscapeSO")]
    public class LandscapeSO : ScriptableObject
    {
        public LandscapeNames landscapeNames;

        [TextArea(5,10)]        
        public List<string> allLines = new List<string>();
        public SpriteDataLandscape spriteData; 
    }

    [Serializable]
    public class SpriteDataLandscape
    {
        public LocationName locationName; 
        public Sprite spriteBG_Q_day;
        public Sprite spriteBG_Q_night;
        public Sprite spriteBG_C_day;
        public Sprite spriteBG_C_night;
    }
}