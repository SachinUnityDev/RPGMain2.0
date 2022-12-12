using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
    public class Golden : PrefixBase
    {
            //        NA NA	"
            //* -2 Luck
            //* +22-28% dmg vs human, elf, dwarf races
            //* +10 light res"

        public override PrefixNames prefixName => PrefixNames.Golden;
        public override GenGewgawQ genGewgawQ { get; set; }
        public override List<int> buffIndex { get; set; }
        public override List<string> displayStrs { get; set; }

        int val31, val32;
        string str3;
        public override void PrefixInit(GenGewgawQ genGewgawQ)
        {
            this.genGewgawQ = genGewgawQ;
        }
        public override void ApplyPrefix(CharController charController)
        {
            this.charController = charController;
            buffIndex = new List<int>();
        }
        protected override void ApplyFXLyric()
        {
           //NA
        }
        protected override void ApplyFXFolkloric()
        {
           //NA
        }

        protected override void ApplyFXEpic()
        {
            val31 = -2; 
            int index =
            charController.buffController.ApplyBuff(CauseType.PrefixGenGewgaw, (int)prefixName
                , charController.charModel.charID, StatsName.luck, val31, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

            val32 = 10;
            index =
            charController.buffController.ApplyBuff(CauseType.PrefixGenGewgaw, (int)prefixName
                , charController.charModel.charID, StatsName.lightRes, val32, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

        }


        public override void EndFX()
        {
            buffIndex.ForEach(t => charController.buffController.RemoveBuff(t));
            buffIndex.Clear();
        }

        public override List<string> DisplayStrings()
        {
            displayStrs.Clear();
            switch (genGewgawQ)
            {
                case GenGewgawQ.Lyric:
                  
                    break;
                case GenGewgawQ.Folkloric:
                   
                    break;
                case GenGewgawQ.Epic:

                    str3 = $"{val31} Luck";
                    displayStrs.Add(str3);
                    str3 = $"+ {val32} Light resistance";
                    displayStrs.Add(str3);
                    break;
                default:
                    break;
            }

            return displayStrs;
        }
    }
}
