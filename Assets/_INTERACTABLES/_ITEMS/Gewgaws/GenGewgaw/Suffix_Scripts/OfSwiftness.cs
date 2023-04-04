using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class OfSwiftness : SuffixBase, ILyric, IFolkloric, IEpic
    {
        public override SuffixNames suffixName => SuffixNames.OfSwiftness;

        // 1 Haste 	2 Haste	3 Haste
        int valLyric, valFolk, valEpic;
        public void LyricInit()
        {
            valLyric = 1;
            string str = $"+{valLyric} Haste";
            displayStrs.Add(str);
        }
        public void ApplyFXLyric()
        {
            charController = InvService.Instance.charSelectController;
            int index =
            charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                , charController.charModel.charID, AttribName.haste, valLyric, TimeFrame.Infinity, -1, true);

            buffIndex.Add(index);
        }
        public void FolkloricInit()
        {
            valFolk = 2;
            string str = $"+{valFolk} Haste";
            displayStrs.Add(str);
        }

        public void ApplyFXFolkloric()
        {
            charController = InvService.Instance.charSelectController;
            int index =
            charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                , charController.charModel.charID, AttribName.haste, valFolk, TimeFrame.Infinity, -1, true);

            buffIndex.Add(index);
        }
        public void EpicInit()
        {
            valEpic = 3;
            string str = $"+{valEpic} Haste";
            displayStrs.Add(str);
        }
        public void ApplyFXEpic()
        {
            charController = InvService.Instance.charSelectController;

            int index =
            charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                , charController.charModel.charID, AttribName.haste, valEpic, TimeFrame.Infinity, -1, true);

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

