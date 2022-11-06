using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class OfSwiftness : SuffixBase
    {
        public override SuffixNames suffixName => SuffixNames.OfSwiftness;
        public override GenGewgawQ genGewgawQ { get; set; }
        public override CharController charController { get; set; }
        public override List<int> buffIndex { get; set; }
        public override List<string> displayStrs { get; set; }
        // 1 haste	2 haste	3 haste
        int val1, val2, val3;
        string str1, str2, str3;
        public override void SuffixInit(GenGewgawQ genGewgawQ)
        {
            this.genGewgawQ = genGewgawQ;
        }
        public override void ApplySuffixFX(CharController charController)
        {
            this.charController = charController;
            buffIndex = new List<int>();
        }
        protected override void ApplyFXLyric()
        {
            val1 = 1;

            int index =
            charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                , charController.charModel.charID, StatsName.haste, val1, TimeFrame.Infinity, -1, true);

            buffIndex.Add(index);
        }
        protected override void ApplyFXFolkloric()
        {
            val2 = 2;

            int index =
            charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                , charController.charModel.charID, StatsName.haste, val2, TimeFrame.Infinity, -1, true);

            buffIndex.Add(index);
        }
        protected override void ApplyFXEpic()
        {
            val3 = 3;

            int index =
            charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                , charController.charModel.charID, StatsName.haste, val3, TimeFrame.Infinity, -1, true);

            buffIndex.Add(index);
        }
     

        public override List<string> DisplayStrings()
        {
            displayStrs.Clear();
            switch (genGewgawQ)
            {
                case GenGewgawQ.Lyric:
                    str1 = $"+{val1} Haste";
                    displayStrs.Add(str1);
                    break;
                case GenGewgawQ.Folkloric:
                    str2 = $"+{val2} Haste";
                    displayStrs.Add(str2);
                    break;
                case GenGewgawQ.Epic:
                    str3 = $"+{val3} Haste";
                    displayStrs.Add(str3);
                    break;
                default:
                    break;
            }

            return displayStrs;
        }

        public override void RemoveFX()
        {
            buffIndex.ForEach(t => charController.buffController.RemoveBuff(t));
            buffIndex.Clear();
        }

  
    }
}

