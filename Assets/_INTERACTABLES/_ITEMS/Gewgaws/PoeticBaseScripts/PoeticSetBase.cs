using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
    public abstract class PoeticSetBase 
    {
        public abstract PoeticSetName poeticSetName { get; }
        public CharController charController { get; set; }
        public List<int> buffIndex { get; set; }
        public List<int> dmgAltBuffIndex { get; set; }

        public abstract void BonusFx();
        public abstract void RemoveBonusFX(); 

    }
}