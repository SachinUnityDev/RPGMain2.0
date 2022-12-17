using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
 public class OfTheSerpent : SuffixBase , IFolkloric, IEpic
 {
        public override SuffixNames suffixName => SuffixNames.OfTheSerpent;

        // NA	1 dodge + 1-2 focus	2 dodge, + 2-3 focus

        int  val21, val22, val31, val32;
        public void FolkloricInit()
        {
            val21 = 1; val22 = UnityEngine.Random.Range(1, 3);
            string str = $"+{val21} Dodge";
            displayStrs.Add(str);
            str = $"+{val22} Focus";
            displayStrs.Add(str);
        }
        public void ApplyFXFolkloric()
        {
            //1 dodge + 1 - 2 focus
            charController = InvService.Instance.charSelectController;
            int index =
                  charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, StatsName.dodge, val21, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

           
            index =
                charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, StatsName.focus, val22, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);
        }
        public void EpicInit()
        {
            val31 = 2; val32 = UnityEngine.Random.Range(2, 4);
            string str = $"+{val31} Dodge";
            displayStrs.Add(str);
            str = $"+{val32} Focus";
            displayStrs.Add(str);
        }
        public void ApplyFXEpic()
        {
            //2 dodge, +2 - 3 focus
            charController = InvService.Instance.charSelectController;
            int index =
                  charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, StatsName.dodge, val31, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

           
            index =
                charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, StatsName.focus, val32, TimeFrame.Infinity, -1, true);
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
