using Common;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using UnityEngine;


namespace Interactables
{
    public class OfTheDonkey : SuffixBase
    {
        public override SuffixNames suffixName => SuffixNames.OfTheDonkey;
        public override GenGewgawQ genGewgawQ { get; set; }
        public override CharController charController { get; set; }
        public override List<int> buffIndex { get; set; }
        public override List<string> displayStrs { get; set; }

        //of the Donkey NA	2 vigor at day + 2 wp at day	3 vigor at day  + 3 wp at day
        int val21, val22, val31, val32;
        string  str2, str3;
        // StatsName statsName21, statsName22, statsName31, statsName32;

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

        }
        protected override void ApplyFXFolkloric()
        {

            val21 = 2;
            int index =
                  charController.buffController.ApplyBuffOnDay(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, StatsName.willpower, val21, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

            val22 = 2;
            index =
                charController.buffController.ApplyBuffOnDay(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, StatsName.vigor, val22, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);
        }
        protected override void ApplyFXEpic()
        {
          //  3 vigor at day + 3 wp at day
            val31 = 3;
            int index =
                  charController.buffController.ApplyBuffOnDay(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, StatsName.willpower, val31, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

            val32 = 3;
            index =
                charController.buffController.ApplyBuffOnDay(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, StatsName.vigor, val32, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);
        }

        public override List<string> DisplayStrings()
        {
            displayStrs.Clear();
            switch (genGewgawQ)
            {
                case GenGewgawQ.Lyric:

                    break;
                case GenGewgawQ.Folkloric:
                    str2 = $"+{val21} Willpower at day";
                    displayStrs.Add(str2);
                    str2 = $"+{val22} Vigor at day";
                    displayStrs.Add(str2);
                    break;
                case GenGewgawQ.Epic:
                    str3 = $"+{val31} Willpower at day";
                    displayStrs.Add(str3);
                    str3 = $"+{val32} Vigor at day";
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


