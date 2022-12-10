using QFX.IFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    [System.Serializable]
    public class LoreModel
    {
        public List<LoreData> allLoreData = new List<LoreData>();
        public List<LoreStrData> allLoreStrData = new List<LoreStrData>();
        public LoreModel(LoreSO _loreSO)  // used only to keep the updated lore and sublore data Locked status 
        {
           
            for (int k=0; k<_loreSO.allLoreData.Count; k++)
            {
                LoreData _data = _loreSO.allLoreData[k];
                if (_data == null) continue;                
                LoreData loreData = new(_data.loreName,_data.isLocked);
                List<LoreSubData> subData = new List<LoreSubData>();
           
                foreach (LoreSubData _subData in _data.allSubLore)
                {
                    LoreSubData loreSubData = new LoreSubData(_subData.subLoreNames, _subData.isLocked);
                    subData.Add(loreSubData);
                 }
                loreData.allSubLore = subData;
                allLoreData.Add(loreData);  
           
            }            
            this.allLoreStrData = _loreSO.allLoreStrData.DeepClone();
        }
    }
}
