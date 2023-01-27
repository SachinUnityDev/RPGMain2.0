using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace Interactables
{
    public abstract class PoeticGewgawBase 
    {
        public abstract PoeticGewgawNames poeticGewgawName { get; }
        public CharController charController { get; set; }
        public List<int> buffIndex { get; set; } = new List<int>(); 
        public List<int> expIndex { get; set; } = new List<int>();  
        public List<string> displayStrs { get; set; } = new List<string>(); 
        public abstract void PoeticInit();  // connect the charController and other things
        public abstract void EquipGewgawPoetic();
        public abstract void UnEquipPoetic();

    }
}

