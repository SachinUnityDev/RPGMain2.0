using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class OfTheLion: SuffixBase, IFolkloric, IEpic
    {
        //  NA	2 morale at day + 2 vigor - 1 haste at night	3 morale at day + 3 vigor - 1 haste at night

        public override SuffixNames suffixName => SuffixNames.OfTheLion;

        int val21, val22, val23, val31, val32, val33;
        public void FolkloricInit()
        {
            val21 = 2; val22 = 2; val23 = -1;
            string str = $"+{val21} Vigor";
            displayStrs.Add(str);
            str = $"+{val22} Morale at day";
            displayStrs.Add(str);
            str = $"{val23} Haste at night";
            displayStrs.Add(str);
        }
        public void ApplyFXFolkloric()
        {
            // 2 morale at day + 2 vigor - 1 haste at night
            charController = InvService.Instance.charSelectController;

            int index =
                  charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, AttribName.vigor, val21, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

          
            index =
                charController.buffController.ApplyNInitBuffOnDay(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, AttribName.morale, val22, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

       
            index =
                charController.buffController.ApplyBuffOnNight(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, AttribName.haste, val23, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

        }
        public void EpicInit()
        {
            val31 = 3; val32 = 3; val33 = -1;
            string str = $"+{val31} Vigor";
             displayStrs.Add(str);
            str = $"+{val32} Morale at day";
            displayStrs.Add(str);
            str = $"{val33} Haste at night";
            displayStrs.Add(str);
        }
        public void ApplyFXEpic()
        {
            //3 morale at day + 3 vigor - 1 haste at night
            charController = InvService.Instance.charSelectController;

            int index =
                  charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, AttribName.vigor, val31, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

         
            index =
                charController.buffController.ApplyNInitBuffOnDay(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, AttribName.morale, val32, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

      
            index =
                charController.buffController.ApplyNInitBuffOnDay(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, AttribName.haste, val33, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);
        }

        //public override List<string> DisplayStrings()
        //{
        //    displayStrs.Clear();
        //    switch (genGewgawQ)
        //    {
        //        case GenGewgawQ.Lyric:

        //            break;
        //        case GenGewgawQ.Folkloric:
        //            str2 = $"+{val21} Vigor";
        //            displayStrs.Add(str2);
        //            str2 = $"+{val22} Morale at day";
        //            displayStrs.Add(str2);
        //            str2 = $"{val23} Haste at night";
        //            displayStrs.Add(str2);
        //            break;
        //        case GenGewgawQ.Epic:
        //            str3 = $"+{val31} Vigor";
        //            displayStrs.Add(str3);
        //            str3 = $"+{val32} Morale at day";
        //            displayStrs.Add(str3);
        //            str3 = $"{val33} Haste at night";
        //            displayStrs.Add(str3);
        //            break;
        //        default:
        //            break;
        //    }
        //    return displayStrs;
        //}



   

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
