using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
    public class OfWarding :SuffixBase, ILyric, IFolkloric, IEpic
    {       
       // 3-4 elemental   res	   5-7 elemental res	8-12 elemental res
        public override SuffixNames suffixName => SuffixNames.OfWarding;          

        int val11, val21,val31;
        public void LyricInit()
        {
            val11 = UnityEngine.Random.Range(3, 5);
            string str = $"+{val11} Elemental Resistances";
            displayStrs.Add(str);
        }
        public void ApplyFXLyric()
        {
            charController = InvService.Instance.charSelectController;

            int index =
                  charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, AttribName.waterRes, val11, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

            index =
                 charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                         , charController.charModel.charID, AttribName.fireRes, val11, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);


            index =
                 charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                         , charController.charModel.charID, AttribName.earthRes, val11, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);


            index =
                 charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                         , charController.charModel.charID, AttribName.airRes, val11, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);
        }
        public void FolkloricInit()
        {
            val21 = UnityEngine.Random.Range(5, 8);
            string str = $"+{val21} Elemental Resistances";
            displayStrs.Add(str);
        }
        public void ApplyFXFolkloric()
        {

            charController = InvService.Instance.charSelectController;

            int index =
                  charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, AttribName.waterRes, val21, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

             index =
                  charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, AttribName.fireRes, val21, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);


             index =
                  charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, AttribName.earthRes, val21, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);


            index =
                 charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                         , charController.charModel.charID, AttribName.airRes, val21, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);
        }
        public void EpicInit()
        {
            val31 = UnityEngine.Random.Range(8, 13);
            string str = $"+{val31} Elemental Resistances";
            displayStrs.Add(str);
        }

        public void ApplyFXEpic()
        {
            charController = InvService.Instance.charSelectController;

            int index =
                  charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                          , charController.charModel.charID, AttribName.waterRes, val31, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

            index =
                 charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                         , charController.charModel.charID, AttribName.fireRes, val31, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);


            index =
                 charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                         , charController.charModel.charID, AttribName.earthRes, val31, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);


            index =
                 charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                         , charController.charModel.charID, AttribName.airRes, val31, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);
        }
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

