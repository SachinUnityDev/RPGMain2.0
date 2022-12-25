using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
   
    public class OfTheTiger : SuffixBase, IFolkloric, IEpic
    {
        public override SuffixNames suffixName => SuffixNames.OfTheTiger;

        // of the Tiger NA  2 wp + 2-4 fort.org.   3 wp+ 3-6 fort.org
        int val21, val22, val31, val32;
        public void FolkloricInit()
        {
            val21 = 2; val22 = UnityEngine.Random.Range(2, 5);
            string str = $"+{val21} Willpower";
            displayStrs.Add(str);
            str = $"+{val22} Fortitude Origin";
            displayStrs.Add(str);

        }
        public void ApplyFXFolkloric()
        {
            charController = InvService.Instance.charSelectController;


            int index =
                  charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, StatsName.willpower, val21, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

            index =
                charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, StatsName.fortOrg, val22, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);
        }
        public void EpicInit()
        {
            val31 = 3; val32 = UnityEngine.Random.Range(3, 7);
            string str = $"+{val31} Willpower";
            displayStrs.Add(str);
            str = $"+{val32} Fortitude Origin";
            displayStrs.Add(str);
        }
        public void ApplyFXEpic()
        {
           // 3 wp + 3 - 6 fort.org
            
            charController = InvService.Instance.charSelectController;

            int index =
                  charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, StatsName.willpower, val31, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

    
            index =
                charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, StatsName.fortOrg, val32, TimeFrame.Infinity, -1, true);
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
