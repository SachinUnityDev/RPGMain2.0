using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
    public class OfTheWaterBuffalo : SuffixBase
    {
        // of the Water Buffalo
        // NA	2 vigor + 3-6 earth and water res	3 vigor + 6-9 earth and water res
        public override SuffixNames suffixName => SuffixNames.OfTheWaterBuffalo;
        public override GenGewgawQ genGewgawQ { get; set; }
        public override CharController charController { get; set; }
        public override List<int> buffIndex { get; set; }
        public override List<string> displayStrs { get; set; }

        int val21, val22, val23, val31, val32, val33;
        string str2, str3;


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
            //2 vigor + 3 - 6 earth and water res
            val21 = 2;
            int index =
                  charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, StatsName.vigor, val21, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

            val22 = UnityEngine.Random.Range(3, 7);
            index =
                charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, StatsName.waterRes, val22, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

            val23 = UnityEngine.Random.Range(3, 7);
            index =
                charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, StatsName.earthRes, val23, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

        }
        protected override void ApplyFXEpic()
        {
            //3 vigor + 6 - 9 earth and water res
            val31 = 3;
            int index =
                  charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, StatsName.vigor, val31, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

            val32 = UnityEngine.Random.Range(6, 10);
            index =
                charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, StatsName.waterRes, val32, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

            val33 = UnityEngine.Random.Range(6, 10);
            index =
                charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, StatsName.earthRes, val33, TimeFrame.Infinity, -1, true);
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
                    str2 = $"+{val21} Vigor";
                    displayStrs.Add(str2);
                    str2 = $"+{val22} Water Resistance";
                    displayStrs.Add(str2);
                    str2 = $"+{val23} Earth Resistance";
                    displayStrs.Add(str2);
                    break;
                case GenGewgawQ.Epic:
                    str3 = $"+{val31} Vigor";
                    displayStrs.Add(str3);
                    str3 = $"+{val32} Water Resistance";
                    displayStrs.Add(str3);
                    str3 = $"+{val33} Earth Resistance";
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

