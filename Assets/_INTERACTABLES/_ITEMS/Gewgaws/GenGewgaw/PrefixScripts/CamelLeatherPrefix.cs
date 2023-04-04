using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
    public class CamelLeatherPrefix : PrefixBase, ILyric, IFolkloric, IEpic
    {
        //"+1 wp, 3-5 Fire res"	       "+2 wp, 5-8 Fire res"          	"+3 wp, 9-13 Fire res"
        public override PrefixNames prefixName => PrefixNames.CamelLeather;

        int valLyric, valfolk, valEpic;
        public void LyricInit()
        {
            string str = $"+1 Willpower";
            displayStrs.Add(str);
            valLyric = UnityEngine.Random.Range(3, 6);
            str = $"+{valLyric} Fire Res";
            displayStrs.Add(str);
        }

        public void ApplyFXLyric()
        {
            charController = InvService.Instance.charSelectController;

            int index =
            charController.buffController.ApplyBuff(CauseType.PrefixGenGewgaw, (int)prefixName
                , charController.charModel.charID, AttribName.willpower, 1, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

            index =
            charController.buffController.ApplyBuff(CauseType.PrefixGenGewgaw, (int)prefixName
                , charController.charModel.charID, AttribName.fireRes, valLyric, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);
        }
        public void FolkloricInit()
        {
            string str = $"+2 Willpower";
            displayStrs.Add(str);
            valfolk = UnityEngine.Random.Range(5, 9);
            str = $"+{valfolk} Fire Res";
            displayStrs.Add(str);
        }
        public void ApplyFXFolkloric()
        {
            charController = InvService.Instance.charSelectController;
            int index =
            charController.buffController.ApplyBuff(CauseType.PrefixGenGewgaw, (int)prefixName
           , charController.charModel.charID, AttribName.willpower, 1, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

            index =
            charController.buffController.ApplyBuff(CauseType.PrefixGenGewgaw, (int)prefixName
                , charController.charModel.charID, AttribName.fireRes, valfolk, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);
        }
        public void EpicInit()
        {
            string str = $"+3 Willpower";
            displayStrs.Add(str);
            valEpic = UnityEngine.Random.Range(9, 14);
            str = $"+{valEpic} Fire Res";
            displayStrs.Add(str);
        }
        public void ApplyFXEpic()
        {
            charController = InvService.Instance.charSelectController;

            int index =
            charController.buffController.ApplyBuff(CauseType.PrefixGenGewgaw, (int)prefixName
           , charController.charModel.charID, AttribName.willpower, 1, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

            index =
            charController.buffController.ApplyBuff(CauseType.PrefixGenGewgaw, (int)prefixName
                , charController.charModel.charID, AttribName.fireRes, valEpic, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);
        }
        public void RemoveFXEpic()
        {
            buffIndex.ForEach(t => charController.buffController.RemoveBuff(t));
            buffIndex.Clear();
        }

        public void RemoveFXFolkloric()
        {
            buffIndex.ForEach(t => charController.buffController.RemoveBuff(t));
            buffIndex.Clear();
        }

        public void RemoveFXLyric()
        {
            buffIndex.ForEach(t => charController.buffController.RemoveBuff(t));
            buffIndex.Clear();
        }
   
    }
}

