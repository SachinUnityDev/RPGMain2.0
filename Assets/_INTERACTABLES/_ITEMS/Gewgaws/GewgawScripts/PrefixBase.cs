using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace Interactables
{
    public abstract class PrefixBase
    {
        public abstract PrefixNames prefixName { get; }
        public CharController charController { get; protected set; }
        public List<int> buffIndex { get; set; }
        public List<int> dmgAltBuffIndex { get; set; }
        public List<string> displayStrs { get; set; }  
    }
}

