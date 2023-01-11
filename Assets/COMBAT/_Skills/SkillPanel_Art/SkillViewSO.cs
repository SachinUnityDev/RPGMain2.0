using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    [CreateAssetMenu(fileName = "SkillViewSO", menuName = "Skill Service/SkillViewSO")]
    public class SkillViewSO : ScriptableObject
    {
        [Header("Plus Sign")]
        public Sprite skillPtsPlus;
        public Sprite skillPtsPressed;


        [Header("Perk Buttons")]
        public Sprite perkBtnNormal;
        public Sprite perkBtnSelect;
        public Sprite perkBtnUnSelectable;

        [Header("Skill Locked")]
        public Sprite lockedBtn; 

        public List<PerkSpriteData> perkSprites = new List<PerkSpriteData>();
    
    }

    [System.Serializable]
    public class PerkSpriteData
    {
        public PerkType startPerk; 
        public PerkType endPerk;        
        public Sprite pipeSpriteSelect;
        public Sprite pipeSpriteUnSelect;
    }
}

