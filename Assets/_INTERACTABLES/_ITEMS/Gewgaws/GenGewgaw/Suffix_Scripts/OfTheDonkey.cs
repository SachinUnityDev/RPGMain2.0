using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class OfTheDonkey : SuffixBase, IFolkloric, IEpic, ILyric
    {
        public override SuffixNames suffixName => SuffixNames.OfTheDonkey;


        //of the Donkey NA	2 vigor at day + 2 wp at day	3 vigor at day  + 3 wp at day
        int val21, val22, val31, val32, val11, val12;

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
                  charController.buffController.ApplyNInitBuffOnDayNNight(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, AttribName.willpower, val21, TimeFrame.Infinity, -1, true, TimeState.Day);
            buffIndex.Add(index);

     
            index =
                charController.buffController.ApplyNInitBuffOnDayNNight(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, AttribName.vigor, val22, TimeFrame.Infinity, -1, true, TimeState.Day);
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
                  charController.buffController.ApplyNInitBuffOnDayNNight(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, AttribName.willpower, val31, TimeFrame.Infinity, -1, true, TimeState.Day);
            buffIndex.Add(index);

          
            index =
                charController.buffController.ApplyNInitBuffOnDayNNight(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, AttribName.vigor, val32, TimeFrame.Infinity, -1, true, TimeState.Day);
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

        public void LyricInit()
        {
            val11 = 1;
            val12 = 1;
            string str1 = $"+{val11} Willpower at day";
            displayStrs.Add(str1);
            string str2 = $"+{val12} Vigor at day";
            displayStrs.Add(str2);
        }

        public void ApplyFXLyric()
        {
            charController = InvService.Instance.charSelectController;

            int index =
                  charController.buffController.ApplyNInitBuffOnDayNNight(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, AttribName.willpower, val11, TimeFrame.Infinity, -1, true, TimeState.Day);
            buffIndex.Add(index);


            index =
                charController.buffController.ApplyNInitBuffOnDayNNight(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, AttribName.vigor, val12, TimeFrame.Infinity, -1, true, TimeState.Day);
            buffIndex.Add(index);
        }

        public void RemoveFXLyric()
        {
            buffIndex.ForEach(t => charController.buffController.RemoveBuff(t));
            buffIndex.Clear();
        }
    }
}


