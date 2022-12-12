using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
    public class Bronze : PrefixBase
    {
        //* -1 (random utility stat),
        //* +8-12% dmg vs serpents"	"
        //* -1 (random utility stat),
        //* +13-18% dmg vs serpents"	NA
        public override PrefixNames prefixName => PrefixNames.Bronze;
        public override GenGewgawQ genGewgawQ { get; set; }
        public override List<int> buffIndex { get; set; }
        public override List<string> displayStrs { get; set; }

        int val1, val2, val3;
        string str1, str2, str3;
        public override void PrefixInit(GenGewgawQ genGewgawQ)
        {
            val1 = -1;
            val2 = -1;
            this.genGewgawQ = genGewgawQ;
        }
        public override void ApplyPrefix(CharController charController)
        {
            this.charController = charController;
            buffIndex = new List<int>();
        }
        protected override void ApplyFXLyric()
        {
           
            StatsName statsName = GetRandomUtilityStat();

            int index =
            charController.buffController.ApplyBuff(CauseType.PrefixGenGewgaw, (int)prefixName
                , charController.charModel.charID, statsName, val1, TimeFrame.Infinity, -1, true);

            buffIndex.Add(index);
        }
        protected override void ApplyFXFolkloric()
        {
         
            StatsName statsName = GetRandomUtilityStat();

            int index =
            charController.buffController.ApplyBuff(CauseType.PrefixGenGewgaw, (int)prefixName
                , charController.charModel.charID, statsName, val2, TimeFrame.Infinity, -1, true);

            buffIndex.Add(index);
        }

        protected override void ApplyFXEpic()
        {
          //NA
           
        }
      
      
        public override void EndFX()
        {
            buffIndex.ForEach(t => charController.buffController.RemoveBuff(t));
            buffIndex.Clear();
        }
        StatsName GetRandomUtilityStat()
        {
            int beginInt = (int)StatsName.focus;
            int ran = Random.Range(beginInt, beginInt + 4);
            return (StatsName)ran;
        }
 
        public override List<string> DisplayStrings()
        {
            displayStrs.Clear();
            switch (genGewgawQ)
            {
                case GenGewgawQ.Lyric:
                    str1 = $"+{val1} Fortitude Origin";
                    displayStrs.Add(str1);
                    break;
                case GenGewgawQ.Folkloric:
                    str2 = $"+{val2} Fortitude Origin";
                    displayStrs.Add(str2);
                    break;
                case GenGewgawQ.Epic:
                    
                    break;
                default:
                    break;
            }

            return displayStrs;
        }

     
    }
}

