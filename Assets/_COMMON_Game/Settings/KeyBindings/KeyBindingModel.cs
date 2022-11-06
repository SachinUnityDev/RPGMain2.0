using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    [System.Serializable]
    public class KeyBindingModel
    {
        public List<KeyBindingData> allKeyBindingData = new List<KeyBindingData>();

        public KeyBindingModel(KeyBindingSO keyBindingSO)
        {
            this.allKeyBindingData = keyBindingSO.allKeyBindingData.DeepClone();
        }
    }

}


