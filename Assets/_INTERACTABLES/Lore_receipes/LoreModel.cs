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
        public LoreModel(LoreSO _loreSO)
        {
            this.allLoreData = _loreSO.allLoreData.DeepClone();
            this.allLoreStrData = _loreSO.allLoreStrData.DeepClone(); 

        }
    }
}
