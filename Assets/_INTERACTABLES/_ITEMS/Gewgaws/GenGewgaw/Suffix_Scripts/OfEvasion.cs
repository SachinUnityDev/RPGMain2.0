using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
    public class OfEvasion : SuffixBase, ILyric, IFolkloric, IEpic
    {
        public override SuffixNames suffixName => SuffixNames.OfEvasion;

        // 1 dodge 	2 dodge	3-4 dodge
        int valLyric, valFolk, valEpic;

        public void LyricInit()
        {
            valLyric = 1;
            string str = $"+{valLyric} Dodge";
            displayStrs.Add(str);
        }
        public void ApplyFXLyric()
        {
            charController = InvService.Instance.charSelectController;

            int index =
            charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                , charController.charModel.charID, StatsName.dodge, valLyric, TimeFrame.Infinity, -1, true);

            buffIndex.Add(index);
        }
        public void FolkloricInit()
        {
            valFolk = 2;
            string str = $"+{valFolk} Dodge";
            displayStrs.Add(str);
        }
        public void ApplyFXFolkloric()
        {
            charController = InvService.Instance.charSelectController;

            int index =
            charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                , charController.charModel.charID, StatsName.dodge, valFolk, TimeFrame.Infinity, -1, true);

            buffIndex.Add(index);
        }

        public void EpicInit()
        {
            valEpic = Random.Range(3, 5);
            string str = $"+{valEpic} Dodge";
            displayStrs.Add(str);
        }
        public void ApplyFXEpic()
        {
            charController = InvService.Instance.charSelectController;

            int index =
            charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                , charController.charModel.charID, StatsName.dodge, valEpic, TimeFrame.Infinity, -1, true);

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

