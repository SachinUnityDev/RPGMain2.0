using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class OfTheRat: SuffixBase, IFolkloric, IEpic
    {
        public override SuffixNames suffixName => SuffixNames.OfTheRat;

        //NA	1 dodge + 1-2 haste,  	2 dodge + 2-3 haste
        int val21, val22, val31, val32;

        public void FolkloricInit()
        {
            val21 = 1; val22 = UnityEngine.Random.Range(1, 3);
            string str = $"+{val21} Dodge";
             displayStrs.Add(str);
            str = $"+{val22} Haste";
            displayStrs.Add(str);
        }
        public void ApplyFXFolkloric()
        {
            charController = InvService.Instance.charSelectController;

            //1 dodge + 1 - 2 haste
            int index =
                  charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, StatsName.dodge, val21, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

       
            index =
                charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, StatsName.haste, val22, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);
        }
        public void EpicInit()
        {
            val31 = 2; val32 = UnityEngine.Random.Range(2, 4);
            string str = $"+{val31} Dodge";
            displayStrs.Add(str);
            str = $"+{val32} Haste";
            displayStrs.Add(str);
        }
        public void ApplyFXEpic()
        {
            charController = InvService.Instance.charSelectController;

            //2 dodge, +2 - 3 haste

            int index =
                  charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, StatsName.dodge, val31, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

         
            index =
                charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, StatsName.haste, val32, TimeFrame.Infinity, -1, true);
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
