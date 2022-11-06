using Common;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.Windows.Forms;
using UnityEngine;


namespace Interactables
{
    public class OfTheLion: SuffixBase
    {
        //  NA	2 morale at day + 2 vigor - 1 haste at night	3 morale at day + 3 vigor - 1 haste at night

        public override SuffixNames suffixName => SuffixNames.OfTheLion;
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
           // 2 morale at day + 2 vigor - 1 haste at night
            val21 = 2;
            int index =
                  charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, StatsName.vigor, val21, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

            val22 = 2;
            index =
                charController.buffController.ApplyBuffOnDay(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, StatsName.morale, val22, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

            val23 = -1;
            index =
                charController.buffController.ApplyBuffOnDay(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, StatsName.haste, val23, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

        }
        protected override void ApplyFXEpic()
        {
            //3 morale at day + 3 vigor - 1 haste at night
             val31 = 3;
            int index =
                  charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, StatsName.vigor, val31, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

            val32 = 3;
            index =
                charController.buffController.ApplyBuffOnDay(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, StatsName.morale, val32, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

            val33 = -1;
            index =
                charController.buffController.ApplyBuffOnDay(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, StatsName.haste, val33, TimeFrame.Infinity, -1, true);
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
                    str2 = $"+{val22} Morale at day";
                    displayStrs.Add(str2);
                    str2 = $"{val23} Haste at night";
                    displayStrs.Add(str2);
                    break;
                case GenGewgawQ.Epic:
                    str3 = $"+{val31} Vigor";
                    displayStrs.Add(str3);
                    str3 = $"+{val32} Morale at day";
                    displayStrs.Add(str3);
                    str3 = $"{val33} Haste at night";
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
