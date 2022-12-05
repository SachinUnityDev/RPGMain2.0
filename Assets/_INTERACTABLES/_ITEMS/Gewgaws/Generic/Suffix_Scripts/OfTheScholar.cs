using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class OfTheScholar : SuffixBase
    {
        public override SuffixNames suffixName => SuffixNames.OfTheScholar;
        public override GenGewgawQ genGewgawQ { get; set; }
        public override CharController charController { get; set; }
        public override List<int> buffIndex { get; set; }
        public override List<string> displayStrs { get; set; }
       // +8-10% exp	+12-16% exp, +1 focus	+16-20% exp, +2 Focus

        int val21, val22, val31, val32;
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

        }
        protected override void ApplyFXFolkloric()
        {

            val21 = 2;
            int index =
                  charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, StatsName.willpower, val21, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

            val22 = UnityEngine.Random.Range(2, 5);
            index =
                charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, StatsName.fortOrg, val22, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);
        }
        protected override void ApplyFXEpic()
        {
            // 3 wp + 3 - 6 fort.org
            val31 = 3;
            int index =
                  charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, StatsName.willpower, val31, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

            val32 = UnityEngine.Random.Range(3, 7);
            index =
                charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, StatsName.fortOrg, val32, TimeFrame.Infinity, -1, true);
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
                    str2 = $"+{val21} Willpower";
                    displayStrs.Add(str2);
                    str2 = $"+{val22} Fortitude Origin";
                    displayStrs.Add(str2);
                    break;
                case GenGewgawQ.Epic:
                    str3 = $"+{val31} Willpower";
                    displayStrs.Add(str3);
                    str3 = $"+{val32} Fortitude Origin";
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
