using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Town;
using System.Linq;

namespace Common
{
    [CreateAssetMenu(fileName = "AllCharSO", menuName = "Common/AllCharSO")]

    public class AllCharSO : ScriptableObject
    {
        public List<CharacterSO> allAllySO = new List<CharacterSO>(); 
        public List<CharacterSO> allEnemySO= new List<CharacterSO>();
        public List<CharacterSO> charList = new List<CharacterSO>();
      
        [Header("Hex portraits BG")]
        public Sprite hexPortBg;

        private void Awake()
        {
            charList.Clear();
            charList.AddRange(allAllySO);
            charList.AddRange(allEnemySO);
        }
        public CharacterSO GetAllySO(CharNames charName)
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

        public CharacterSO GetEnemySO(CharNames charName)
        {
            int index = allEnemySO.FindIndex(t => t.charName == charName);
            if (index != -1)
            {
                return allEnemySO[index];
            }
            else
            {
                Debug.Log("Char SO not found" + charName);
                return null;
            }
        }
        public CharacterSO GetCharSO(CharNames charName)
        {
            int index = charList.FindIndex(t=>t.charName == charName);
            if (index != -1)
            {
                return charList[index];
            }
            else
            {
                Debug.Log("Char SO not found" + charName);
                return null;
            }
        }
    }
}