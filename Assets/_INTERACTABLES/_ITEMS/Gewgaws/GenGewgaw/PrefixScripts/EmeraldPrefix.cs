using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{

    public class EmeraldPrefix : PrefixBase, IFolkloric, IEpic
    {
        //NA	"* +1 luck  , 15% more hunger , *+10% companion money earning"	"* +2 luck , 10% more hunger, *+10% companion money earning"
        public override PrefixNames prefixName => PrefixNames.Emerald;

        public void FolkloricInit()
        {
            string str = $"+1 Luck";
            displayStrs.Add(str);
        }
        public void ApplyFXFolkloric()
        {
            charController = InvService.Instance.charSelectController;

            int index =
            charController.buffController.ApplyBuff(CauseType.PrefixGenGewgaw, (int)prefixName
                , charController.charModel.charID, AttribName.luck, 1, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

        }

        public void EpicInit()
        {
            string str = $"+2 Luck";
            displayStrs.Add(str);
        }
        public void ApplyFXEpic()
        {
            charController = InvService.Instance.charSelectController;

            int index =
            charController.buffController.ApplyBuff(CauseType.PrefixGenGewgaw, (int)prefixName
                , charController.charModel.charID, AttribName.luck, 2, TimeFrame.Infinity, -1, true);
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
    }
}
