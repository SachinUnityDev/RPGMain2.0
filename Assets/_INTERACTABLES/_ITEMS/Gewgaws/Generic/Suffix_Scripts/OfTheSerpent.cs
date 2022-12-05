using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
 public class OfTheSerpent : SuffixBase
 {
        public override SuffixNames suffixName => SuffixNames.OfTheSerpent;
        public override GenGewgawQ genGewgawQ { get; set; }
        public override CharController charController { get; set; }
        public override List<int> buffIndex { get; set; }
        public override List<string> displayStrs { get; set; }
        // NA	1 dodge + 1-2 focus	2 dodge, + 2-3 focus

        int  val21, val22, val31, val32;  
        string str1, str2, str3, str4;
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
            //1 dodge + 1 - 2 focus
           
            val21 = 1;
            int index =
                  charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, StatsName.dodge, val21, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

            val22 = UnityEngine.Random.Range(1, 3);
            index =
                charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, StatsName.focus, val22, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);
        }
        protected override void ApplyFXEpic()
        {
            //2 dodge, +2 - 3 focus
            val31 = 2;
            int index =
                  charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, StatsName.dodge, val31, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

            val32 = UnityEngine.Random.Range(2, 4);
            index =
                charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, StatsName.focus, val32, TimeFrame.Infinity, -1, true);
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
                    str2 = $"+{val21} Dodge";
                    displayStrs.Add(str2);
                    str2 = $"+{val22} Focus";
                    displayStrs.Add(str2);
                    break;
                case GenGewgawQ.Epic:
                    str3 = $"+{val31} Dodge";
                    displayStrs.Add(str3);
                    str3 = $"+{val32} Focus";
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
