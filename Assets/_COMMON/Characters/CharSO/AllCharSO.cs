using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    [CreateAssetMenu(fileName = "AllCharSO", menuName = "Common/AllCharSO")]

    public class AllCharSO : ScriptableObject
    {
        public List<CharacterSO> allAllySO = new List<CharacterSO>(); 
        public List<CharacterSO> allEnemySO= new List<CharacterSO>();
        public List<NPCSO> allNPCSO = new List<NPCSO>();    

        public NPCSO GetNPCSO(NPCNames nPCName)
        {
            int index = allNPCSO.FindIndex(t => t.npcName == nPCName); 
            if(index != -1)
            {
               return allNPCSO[index];
            }
            else
            {
                Debug.Log("NPC SO not found" + nPCName);  
                return null; 
            }
        }
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