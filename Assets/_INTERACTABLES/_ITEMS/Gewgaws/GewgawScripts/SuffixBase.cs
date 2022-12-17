using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
    public abstract class SuffixBase
    {
        public abstract SuffixNames suffixName { get; }
        public  CharController charController { get; set; } 
        public List<int> buffIndex { get; set; }
        public List<string> displayStrs { get; set; }
        

    }
}

