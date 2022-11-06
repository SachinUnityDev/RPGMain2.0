using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
    public class OfVersatility : SuffixBase
    {
        public override SuffixNames suffixName => SuffixNames.OfVersatility;
        public override GenGewgawQ genGewgawQ { get; set; }
        public override CharController charController { get; set; }
        public override List<int> buffIndex { get; set; }
        public override List<string> displayStrs { get; set; }

      //  NA	+1 (two random utility stats)	+2 (two random utility stats)
        int val1, val2, val3;
        string str1, str2, str3, str4;
        StatsName statsName21, statsName22, statsName31, statsName32;

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
            statsName21 = GetRandUtilStat(StatsName.None);             
             val2 = 1;                
              int index =
                    charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                            , charController.charModel.charID, statsName21, val2, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

            statsName22 = GetRandUtilStat(statsName21); 
            index =
                charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, statsName22, val2, TimeFrame.Infinity, -1, true);


            buffIndex.Add(index);
        }
        protected override void ApplyFXEpic()
        {
            statsName31 = GetRandUtilStat(StatsName.None);
            val3 = 2;
            int index =
                  charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, statsName31, val3, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

            statsName32 = GetRandUtilStat(statsName31);
            index =
                charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, statsName32, val3, TimeFrame.Infinity, -1, true);


            buffIndex.Add(index);
        }
      //  NA	+1 (two random utility stats)	+2 (two random utility stats)

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
                    str2 = $"+{val2} {statsName21}";
                    displayStrs.Add(str2);
                    str2 = $"+{val2} {statsName22}";
                    displayStrs.Add(str2);
                    break;
                case GenGewgawQ.Epic:
                    str3 = $"+{val3} {statsName31}";
                    displayStrs.Add(str3);
                    str3 = $"+{val3} {statsName32}";
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

        StatsName GetRandUtilStat(StatsName _statsName)
        {
            StatsName statsName = StatsName.None;

            do
            {
                int ran = UnityEngine.Random.Range(1, 5);
                switch (ran)
                {
                    case 1:
                        statsName = StatsName.focus;
                        break;
                    case 2:
                        statsName = StatsName.morale;
                        break;
                    case 3:
                        statsName = StatsName.haste;
                        break;
                    case 4:
                        statsName = StatsName.luck;
                        break;
                    default:
                        break;
                }
            }
            while (statsName != _statsName);

            return statsName;


        }

    }




}
