using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public abstract class GenGewgawBase
    {
        public abstract GenGewgawNames genGewgawNames { get; }
        public CharController charController { get; private set; }
        public abstract SuffixBase suffixBase { get; set; }
        public abstract PrefixBase prefixBase { get; set; }
        public GenGewgawModel genGewgawModel { get; set; }
        public abstract GenGewgawModel GenGewgawInit(GenGewgawSO genericGewgawSO
                                                            , GenGewgawQ genGewgawQ);

        public abstract void EquipGenGewgawFX(CharController charController);
        public abstract void UnEquipGenGewgawFX();
        public abstract void DisposeGenGewgawFX();
    }
}

