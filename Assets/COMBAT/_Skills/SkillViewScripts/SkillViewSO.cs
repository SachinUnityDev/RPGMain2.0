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
        public Sprite skillPlusN;
        public Sprite skillPlusClicked;


        [Header("Perk Buttons")]
        public Sprite perkBtnNormal;
        public Sprite perkBtnSelect;
        public Sprite perkBtnUnSelectable;

        [Header("Skill Locked")]
        public Sprite lockedBtn; 

        public List<PerkSpriteData> perkSprites = new List<PerkSpriteData>();
    
        public Sprite GetPerkPipe(PerkType startPerk, PerkType endPerk, int select)
        {
            int index 
                = perkSprites.FindIndex(t=>t.startPerk ==startPerk && t.endPerk ==endPerk);
            if(index != -1)
            {
                if (select == 1 || select == 2)
                    return perkSprites[index].pipeSpriteSelect;
                if (select == 3)
                    return perkSprites[index].pipeSpriteUnSelect;
            }
            Debug.Log("Sprite not found"); 
            return null; 
        }

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

