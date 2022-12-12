using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public abstract class GenGewgawBase
    {
        public abstract GenGewgawNames genGewgawNames { get; }
        public CharController charController { get; protected set; }
        public abstract SuffixBase suffixBase { get; set; }  // they are set in Item Factory
        public abstract PrefixBase prefixBase { get; set; }  // They are set in Item Factory
       // public GenGewgawModel genGewgawModel { get; set; }
        //public abstract GenGewgawModel GenGewgawInit(GenGewgawSO genericGewgawSO
        //                                                    , GenGewgawQ genGewgawQ);
        public abstract void EquipGenGewgawFX();
        public abstract void UnEquipGenGewgawFX();
        //public abstract void DisposeGenGewgawFX();
    }
}

