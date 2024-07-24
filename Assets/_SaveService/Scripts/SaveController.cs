using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class SaveController : MonoBehaviour
    {        
        public bool ChkIfSaveSlotIsEmpty(SaveSlot saveSlot)
        {
            string slotPath = SaveService.Instance.GetSlotPath(saveSlot);
            


            return false; 
        }
        public void ClearAllFilesInSlot(SaveSlot saveSlot)
        {
            List<ISaveable> allSavables = new List<ISaveable>();
            foreach (ISaveable saveable in allSavables)
            {
                saveable.ClearState();
            }
        }
        public void SaveFilesInASlot()
        {

        }
        

    }
}