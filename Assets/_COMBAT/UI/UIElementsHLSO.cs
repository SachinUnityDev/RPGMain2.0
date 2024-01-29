using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    [CreateAssetMenu(fileName = "UIElementHLSO", menuName = "Combat/UIElementHLSO")]

    public class UIElementsHLSO : ScriptableObject
    {
        // update this with perk name 
        [Header("Perk Select Panel UI")]
        public UIElementType UIElementType;
        public Sprite clickableSprite;
        public Sprite clickedSprite;
        public Sprite UnclickableSprite;
        public Color clickedColor;
        public Color clickableColor;
        public Color unClickableColor; 



    }

    
    public enum UIElementType
    {
        None, 
        PerkButton, 
        SkillButton,

    }

}

