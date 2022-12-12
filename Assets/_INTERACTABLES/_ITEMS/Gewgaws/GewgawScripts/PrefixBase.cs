using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public abstract class PrefixBase
    {
        public abstract PrefixNames prefixName { get; }
        public abstract GenGewgawQ genGewgawQ { get; set; }
        public CharController charController { get; protected set; }
        public abstract List<int> buffIndex { get; set; }
        public abstract List<string> displayStrs { get; set; }
        public abstract void PrefixInit(GenGewgawQ genGewgawQ);         
        public abstract void ApplyPrefix(CharController charController);
        protected abstract void ApplyFXLyric();
        protected abstract void ApplyFXFolkloric();
        protected abstract void ApplyFXEpic();
        public abstract List<string> DisplayStrings();
        public abstract void EndFX();
    }
}

