using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class OfScholar : SuffixBase
    {
        public override SuffixNames suffixName => SuffixNames.OfTheScholar;
        public override GenGewgawQ genGewgawQ { get; set; }
        public override CharController charController { get; set; }
        public override List<int> buffIndex { get; set; }
        public override List<string> displayStrs { get; set; }

        //+8-10% exp	+12-16% exp, +1 focus	+16-20% exp, +2 Focus
        int val21, val31; 
        string str1, str2, str3, str4;

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
            //+8 - 10 % exp



        }
        protected override void ApplyFXFolkloric()
        {
            //+12-16% exp, +1 focus
            val21 = 1;
            int index =
                  charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, StatsName.focus, val21, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

        }
        protected override void ApplyFXEpic()
        {
            //+16 - 20 % exp, +2 Focus
            val31 = 2;
            int index =
                  charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, StatsName.focus, val31, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);
        }

        public override List<string> DisplayStrings()
        {
            displayStrs.Clear();
            switch (genGewgawQ)
            {
                case GenGewgawQ.Lyric:
                    //str1 = $"+{val1} {statsName1}";
                    //displayStrs.Add(str1);
                    break;
                case GenGewgawQ.Folkloric:
                    str2 = $"+{val21} Focus";
                    displayStrs.Add(str2);
                    //str2 = $"+{val22} Focus";
                    //displayStrs.Add(str2);
                    break;
                case GenGewgawQ.Epic:
                    str3 = $"+{val31} Focus";
                    displayStrs.Add(str3);
                    //str3 = $"+{val32} Focus";
                    //displayStrs.Add(str3);
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

