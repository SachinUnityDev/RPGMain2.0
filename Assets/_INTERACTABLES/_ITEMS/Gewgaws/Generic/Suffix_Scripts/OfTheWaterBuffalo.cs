using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
    public class OfTheWaterBuffalo : SuffixBase, IFolkloric, IEpic
    {
        // of the Water Buffalo
        // NA	2 vigor + 3-6 earth and water res	3 vigor + 6-9 earth and water res
        public override SuffixNames suffixName => SuffixNames.OfTheWaterBuffalo;

        int val21, val22, val23, val31, val32, val33;
        public void FolkloricInit()
        {
            val21 = 2; val22 = UnityEngine.Random.Range(3, 7); val23 = UnityEngine.Random.Range(3, 7);

            string str = $"+{val21} Vigor";
            displayStrs.Add(str);
            str = $"+{val22} Water Resistance";
            displayStrs.Add(str);
            str = $"+{val23} Earth Resistance";
            displayStrs.Add(str);
        }

        public void ApplyFXFolkloric()
        {
            //2 vigor + 3 - 6 earth and water res
            charController = InvService.Instance.charSelectController;

            int index =
                  charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, StatsName.vigor, val21, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

           
            index =
                charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, StatsName.waterRes, val22, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

           
            index =
                charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, StatsName.earthRes, val23, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

        }
        public void EpicInit()
        {
            val31 = 3; val32 = UnityEngine.Random.Range(6, 10);
            val33 = UnityEngine.Random.Range(6, 10);

            string str = $"+{val31} Vigor";
            displayStrs.Add(str);
            str = $"+{val32} Water Resistance";
            displayStrs.Add(str);
            str = $"+{val33} Earth Resistance";
            displayStrs.Add(str);

        }

        public void ApplyFXEpic()
        {
            //3 vigor + 6 - 9 earth and water res
            charController = InvService.Instance.charSelectController;

            int index =
                  charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, StatsName.vigor, val31, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

           
            index =
                charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, StatsName.waterRes, val32, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

           
            index =
                charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, StatsName.earthRes, val33, TimeFrame.Infinity, -1, true);
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

