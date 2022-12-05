using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class OfEndurance : SuffixBase
    {
        public override SuffixNames suffixName => SuffixNames.OfEndurance;
        public override GenGewgawQ genGewgawQ { get; set; }
        public override CharController charController { get; set; }
        public override List<int> buffIndex { get; set; }
        public override List<string> displayStrs { get; set; }
      
        //1 vigor or 1 willpower	2 vigor or 2 willpower	3-4 vigor or 3-4 willpower
        int val1, val2, val3;
        string str1, str2, str3;
        StatsName statsName1, statsName2, statsName3;

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
            int index = 0; 
            if (true.TheToss())
            {
                val1 = 1;
                statsName1 = StatsName.vigor;
                index =
                    charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                            ,charController.charModel.charID, statsName1, val1, TimeFrame.Infinity, -1, true);
            }
            else
            {
                val1 = 1;
                statsName1 = StatsName.willpower;
                index =
                    charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, statsName1, val1, TimeFrame.Infinity, -1, true);
            }           
            buffIndex.Add(index);
        }
        protected override void ApplyFXFolkloric()
        {
            int index = 0;
            if (true.TheToss())
            {
                val2 = 2;
                statsName2 = StatsName.vigor;
                index =
                    charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                            , charController.charModel.charID, statsName2, val2, TimeFrame.Infinity, -1, true);
            }
            else
            {
                val2 = 2;
                statsName2 = StatsName.willpower;
                index =
                    charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, statsName2, val2, TimeFrame.Infinity, -1, true);
            }
            buffIndex.Add(index);
        }
        protected override void ApplyFXEpic()
        {
            int index = 0;
            if (true.TheToss())
            {
                val3 = UnityEngine.Random.Range(3, 5);
                statsName3 = StatsName.vigor;
                index =
                    charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                            , charController.charModel.charID, statsName3, val3, TimeFrame.Infinity, -1, true);
            }
            else
            {
                val3 = UnityEngine.Random.Range(3, 5);
                statsName3 = StatsName.willpower;
                index =
                    charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, statsName3, val3, TimeFrame.Infinity, -1, true);
            }
            buffIndex.Add(index);
        }
        //1 vigor or 1 willpower	2 vigor or 2 willpower	3-4 vigor or 3-4 willpower

        public override List<string> DisplayStrings()
        {
            displayStrs.Clear();
            switch (genGewgawQ)
            {
                case GenGewgawQ.Lyric:
                    str1 = $"+{val1} {statsName1}";
                    displayStrs.Add(str1);
                    break;
                case GenGewgawQ.Folkloric:
                    str2 = $"+{val2} {statsName2}";
                    displayStrs.Add(str2);
                    break;
                case GenGewgawQ.Epic:
                    str3 = $"+{val3} {statsName3}";
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

