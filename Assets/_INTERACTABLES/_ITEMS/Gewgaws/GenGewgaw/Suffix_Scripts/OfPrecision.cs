using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class OfPrecision : SuffixBase, ILyric, IFolkloric, IEpic
    {
        public override SuffixNames suffixName => SuffixNames.OfPrecision;
 
        // 1 acc 2 acc 3-4 acc
        int valLyric, valFolk, valEpic;
        public void LyricInit()
        {
            valLyric = 1;
            string str = $"+{valLyric} Accuracy";
            displayStrs.Add(str);
        }

        public void ApplyFXLyric()
        {
            charController = InvService.Instance.charSelectController;

            int index =
            charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                , charController.charModel.charID, AttribName.acc, valLyric, TimeFrame.Infinity, -1, true);

            buffIndex.Add(index);
        }
        public void FolkloricInit()
        {
            valFolk = 2;
            string str = $"+{valFolk} Accuracy";
            displayStrs.Add(str);
        }
        public void ApplyFXFolkloric()
        {
            charController = InvService.Instance.charSelectController;

            int index =
            charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                , charController.charModel.charID, AttribName.acc, valFolk, TimeFrame.Infinity, -1, true);

            buffIndex.Add(index);
        }
        public void EpicInit()
        {
            valEpic = Random.Range(3, 5);
            string str = $"+{valEpic} Accuracy";
            displayStrs.Add(str);
        }
        public void ApplyFXEpic()
        {
            charController = InvService.Instance.charSelectController;

            int index =
            charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                , charController.charModel.charID, AttribName.acc, valEpic, TimeFrame.Infinity, -1, true);

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
