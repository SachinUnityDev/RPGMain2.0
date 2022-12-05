using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class OfTheCommoner : SuffixBase
    {
        public override SuffixNames suffixName => SuffixNames.OfTheCommoner;
        public override GenGewgawQ genGewgawQ { get; set; }
        public override CharController charController { get; set; }
        public override List<int> buffIndex { get ; set; }
        public override List<string> displayStrs { get; set; }

        public override void ApplySuffixFX(CharController charController)
        {
        }

        public override List<string> DisplayStrings()
        {
            return null; 
        }

        public override void RemoveFX()
        {
        }

        public override void SuffixInit(GenGewgawQ genGewgawQ)
        {
        }

        protected override void ApplyFXEpic()
        {
        }

        protected override void ApplyFXFolkloric()
        {
        }

        protected override void ApplyFXLyric()
        {
        }
    }


}


