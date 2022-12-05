using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
    public abstract class SuffixBase
    {
        public abstract SuffixNames suffixName { get; }
        public abstract GenGewgawQ genGewgawQ { get; set; }
        public abstract CharController charController { get; set; }
        public abstract List<int> buffIndex { get; set; }
        public abstract List<string> displayStrs { get; set;}
        public abstract void SuffixInit(GenGewgawQ genGewgawQ);        
        public abstract void ApplySuffixFX(CharController charController);
        protected abstract void ApplyFXLyric();
        protected abstract void ApplyFXFolkloric();
        protected abstract void ApplyFXEpic();
        public abstract List<string> DisplayStrings();
        public abstract void RemoveFX();

    }
}

