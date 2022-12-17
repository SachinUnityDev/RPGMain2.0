using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
    public class OfTheLeopard : SuffixBase, IFolkloric, IEpic
    {
        public override SuffixNames suffixName => SuffixNames.OfTheLeopard;

        //NA	1-2 haste + 1 acc	2-3 haste +  2 acc
        int val21, val22, val31, val32;
        public void FolkloricInit()
        {
            val21 = 1;
            val22 = UnityEngine.Random.Range(1, 3);
            string str1 = $"+{val21} Accuracy";
            displayStrs.Add(str1);
            string str2 = $"+{val22} Haste";
            displayStrs.Add(str2);
        }

        public void ApplyFXFolkloric()
        {
            // 1 - 2 haste + 1 acc
            charController = InvService.Instance.charSelectController;

            int index =
                  charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, StatsName.acc, val21, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);
          
            index =
                charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, StatsName.haste, val22, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);
        }
        public void EpicInit()
        {
            val31 = 2; val32 = UnityEngine.Random.Range(2, 4);
            string str1 = $"+{val31} Accuracy";
             displayStrs.Add(str1);
            string  str2 = $"+{val32} Haste";
            displayStrs.Add(str2);
        }
        public void ApplyFXEpic()
        {
            //2 acc , +2 - 3 haste
            charController = InvService.Instance.charSelectController;

            int index =
                  charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, StatsName.acc, val31, TimeFrame.Infinity, -1, true);
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

