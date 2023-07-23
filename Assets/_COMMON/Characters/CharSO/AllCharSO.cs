using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Town;

namespace Common
{
    [CreateAssetMenu(fileName = "AllCharSO", menuName = "Common/AllCharSO")]

    public class AllCharSO : ScriptableObject
    {
        public List<CharacterSO> allAllySO = new List<CharacterSO>(); 
        public List<CharacterSO> allEnemySO= new List<CharacterSO>();
        [Header("Hex portraits BG")]
        public Sprite hexPortBg;
        public CharacterSO GetCharSO(CharNames charName)
        {
            int index = allAllySO.FindIndex(t => t.charName == charName);
            if (index != -1)
            {
                return allAllySO[index];
            }
            else
            {
                Debug.Log("Char SO not found" + charName);
                return null;
            }
        }
    }
}