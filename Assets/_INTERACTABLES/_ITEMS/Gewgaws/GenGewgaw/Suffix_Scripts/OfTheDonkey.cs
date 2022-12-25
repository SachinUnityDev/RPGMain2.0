using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class OfTheDonkey : SuffixBase, IFolkloric, IEpic
    {
        public override SuffixNames suffixName => SuffixNames.OfTheDonkey;


        //of the Donkey NA	2 vigor at day + 2 wp at day	3 vigor at day  + 3 wp at day
        int val21, val22, val31, val32;

        public void FolkloricInit()
        {
            val21 = 2;
            val22 = 2;
            string str1 = $"+{val21} Willpower at day";
             displayStrs.Add(str1);
            string str2 = $"+{val22} Vigor at day";
             displayStrs.Add(str2);
        }

        public void ApplyFXFolkloric()
        {
            charController = InvService.Instance.charSelectController;


            int index =
                  charController.buffController.ApplyNInitBuffOnDay(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, StatsName.willpower, val21, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

     
            index =
                charController.buffController.ApplyNInitBuffOnDay(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, StatsName.vigor, val22, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);
        }
        public void EpicInit()
        {
            val31 = 3; val32 = 3;
            string str1 = $"+{val31} Willpower at day";
            displayStrs.Add(str1);
            string str2 = $"+{val32} Vigor at day";
            displayStrs.Add(str2);
        }
        public void ApplyFXEpic()
        {
            charController = InvService.Instance.charSelectController;

            //  3 vigor at day + 3 wp at day
           
            int index =
                  charController.buffController.ApplyNInitBuffOnDay(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, StatsName.willpower, val31, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

          
            index =
                charController.buffController.ApplyNInitBuffOnDay(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, StatsName.vigor, val32, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);
        }
        public void RemoveFXFolkloric()
        {
            buffIndex.ForEach(t => charController.buffController.RemoveBuff(t));
            buffIndex.Clear();
        }
        public void RemoveFXEpic()
        {
            buffIndex.ForEach(t => charController.buffController.RemoveBuff(t));
            buffIndex.Clear();
        }
    }
}


