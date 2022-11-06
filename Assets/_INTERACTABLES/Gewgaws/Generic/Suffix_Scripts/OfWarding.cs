using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
    public class OfWarding :SuffixBase
    {       
       // 3-4 elemental   res	   5-8 elemental res	9-13 elemental res
        public override SuffixNames suffixName => SuffixNames.OfWarding;
        public override GenGewgawQ genGewgawQ { get; set; }
        public override CharController charController { get; set; }
        public override List<int> buffIndex { get; set; }
        public override List<string> displayStrs { get; set; }

        int val11, val21,val31;
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
            val11 = UnityEngine.Random.Range(3, 5);
            int index =
                  charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, StatsName.waterRes, val11, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

            index =
                 charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                         , charController.charModel.charID, StatsName.fireRes, val11, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);


            index =
                 charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                         , charController.charModel.charID, StatsName.earthRes, val11, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);


            index =
                 charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                         , charController.charModel.charID, StatsName.airRes, val11, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);
        }
        protected override void ApplyFXFolkloric()
        {
            
            val21 = UnityEngine.Random.Range(5,9);
            int index =
                  charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, StatsName.waterRes, val21, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

             index =
                  charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, StatsName.fireRes, val21, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);


             index =
                  charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, StatsName.earthRes, val21, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);


            index =
                 charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                         , charController.charModel.charID, StatsName.airRes, val21, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);
        }
        protected override void ApplyFXEpic()
        {
            val31 = UnityEngine.Random.Range(9, 14);
            int index =
                  charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, StatsName.waterRes, val31, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

            index =
                 charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                         , charController.charModel.charID, StatsName.fireRes, val31, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);


            index =
                 charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                         , charController.charModel.charID, StatsName.earthRes, val31, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);


            index =
                 charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                         , charController.charModel.charID, StatsName.airRes, val31, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);
        }

        public override List<string> DisplayStrings()
        {
            displayStrs.Clear();
            switch (genGewgawQ)
            {
                case GenGewgawQ.Lyric:
                    str1 = $"+{val11} Elemental Resistance";
                    displayStrs.Add(str1);
                    break;
                case GenGewgawQ.Folkloric:
                    str2 = $"+{val21} Elemental Resistance";
                    displayStrs.Add(str2);
                    break;
                case GenGewgawQ.Epic:
                    str3 = $"+{val31} Elemental Resistance";
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

