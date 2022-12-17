using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
    public class OfSerenity : SuffixBase, ILyric, IFolkloric, IEpic
    {
        public override SuffixNames suffixName => SuffixNames.OfSerenity;

        // 1 Focus 	2 Focus	3 Focus
        int valLyric, valFolk, valEpic;
        public void LyricInit()
        {
            valLyric = 1;
            string str = $"+{valLyric} Focus";
            displayStrs.Add(str);
        }
        public void ApplyFXLyric()
        {
            charController = InvService.Instance.charSelectController;
            int index =
            charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                , charController.charModel.charID, StatsName.focus, valLyric, TimeFrame.Infinity, -1, true);

            buffIndex.Add(index);
        }
        public void FolkloricInit()
        {
            valFolk = 2;
            string str = $"+{valFolk} Focus";
            displayStrs.Add(str);
        }

        public void ApplyFXFolkloric()
        {
            charController = InvService.Instance.charSelectController;

            int index =
            charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                , charController.charModel.charID, StatsName.focus, valFolk, TimeFrame.Infinity, -1, true);

            buffIndex.Add(index);
        }
        public void EpicInit()
        {
            valEpic = 3;
            string str = $"+{valEpic} Focus";
            displayStrs.Add(str);
        }
        public void ApplyFXEpic()
        {
            charController = InvService.Instance.charSelectController;

            int index =
            charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                , charController.charModel.charID, StatsName.focus, valEpic, TimeFrame.Infinity, -1, true);

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

