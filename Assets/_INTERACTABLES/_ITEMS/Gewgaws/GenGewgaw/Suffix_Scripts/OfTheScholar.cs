using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class OfTheScholar : SuffixBase, ILyric, IFolkloric, IEpic
    {
        public override SuffixNames suffixName => SuffixNames.OfTheScholar;

       // +8-10% exp	+12-16% exp, +1 focus	+16-20% exp, +2 Focus

        int val21, val22, val31, val32;
       
        public void LyricInit()
        {
            // exp 
        }
        public void ApplyFXLyric()
        {

        }
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
                          , charController.charModel.charID, AttribName.willpower, val21, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

 
            index =
                charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, AttribName.fortOrg, val22, TimeFrame.Infinity, -1, true);
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
            charController = InvService.Instance.charSelectController;

            // 3 wp + 3 - 6 fort.org
       
            int index =
                  charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, AttribName.willpower, val31, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

           
            index =
                charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                        , charController.charModel.charID, AttribName.fortOrg, val32, TimeFrame.Infinity, -1, true);
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
        //            str2 = $"+{val21} Willpower";
        //            displayStrs.Add(str2);
        //            str2 = $"+{val22} Fortitude Origin";
        //            displayStrs.Add(str2);
        //            break;
        //        case GenGewgawQ.Epic:
        //            str3 = $"+{val31} Willpower";
        //            displayStrs.Add(str3);
        //            str3 = $"+{val32} Fortitude Origin";
        //            displayStrs.Add(str3);
        //            break;
        //        default:
        //            break;
        //    }
        //    return displayStrs;
        //}

   

      
        public void RemoveFXLyric()
        {
            buffIndex.ForEach(t => charController.buffController.RemoveBuff(t));
            buffIndex.Clear();
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
