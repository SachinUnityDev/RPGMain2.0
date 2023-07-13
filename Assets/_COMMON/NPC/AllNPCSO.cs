using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;
using Interactables; 
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

        public List<ItemDataWithQty> GetNPCStock(NPCNames npcName, int weekSeq)
        {
            NPCSO npcSO = GetNPCSO(npcName);
            List<ItemDataWithQty> result = new List<ItemDataWithQty>(); 
            foreach (WeeklyItemInStock weekItemStock in npcSO.weeklyItemStock)
            {
                if(weekItemStock.weekSeq == weekSeq)
                {
                    foreach (ItemDataLs itemDataLs in weekItemStock.itemDataLs)
                    {                        
                        int itemName =
                            itemDataLs.itemName.GetItemName(itemDataLs.itemType);
                        ItemData itemData = new ItemData(itemDataLs.itemType, itemName);
                        ItemDataWithQty itemQty = new ItemDataWithQty(itemData, itemDataLs.qty); 
                        result.Add(itemQty);                
                    }  
                }
            }
            return result;
        }

        


    }
}