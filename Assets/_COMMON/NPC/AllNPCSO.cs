using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;

namespace Common
{


    [CreateAssetMenu(fileName = "AllNPCSO", menuName = "Character Service/AllNPCSO")]
    public class AllNPCSO : ScriptableObject
    {       
        public List<NPCSO> allNPCSO = new List<NPCSO>();   
        
        public NPCSO GetNPCSO(NPCNames npcName)
        {
            int index = allNPCSO.FindIndex(t=>t.npcName== npcName); 
            if(index != -1)
            {
                return allNPCSO[index];
            }
            else
            {
                Debug.Log("NPC SO not found"+ npcName);
                return null; 
            }


        }

    }
}