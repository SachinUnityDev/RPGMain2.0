using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DG.Tweening.DOTweenModuleUtils;

namespace Interactables
{
    public class RubyPrefix : PrefixBase, IFolkloric, IEpic
    {
       // NA	//"* -2 focus  * +%10 physical attack skills(at night)"....
       //"* -2 focus * +%16 physical skills at night"
        public override PrefixNames prefixName => PrefixNames.Ruby;
        string str;
        public void FolkloricInit()
        {
            str = $"-2 Luck";
            displayStrs.Add(str);
        }

        public void ApplyFXFolkloric()
        {
            charController = InvService.Instance.charSelectController;
            int index =
            charController.buffController.ApplyBuff(CauseType.PrefixGenGewgaw, (int)prefixName
                , charController.charModel.charID, AttribName.focus, -2, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);
        }

        public void EpicInit()
        {
            str = $"-2 Luck";
            displayStrs.Add(str);
        }
        public void ApplyFXEpic()
        {
            charController = InvService.Instance.charSelectController;
            int index =
            charController.buffController.ApplyBuff(CauseType.PrefixGenGewgaw, (int)prefixName
                , charController.charModel.charID, AttribName.focus, -2, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);
        }    
        public void RemoveFXFolkloric()
        {
            buffIndex.ForEach(t => charController.buffController.RemoveBuff(t));
            dmgAltBuffIndex.ForEach(t => charController.strikeController.RemoveDmgBuff(t));
            dmgAltBuffIndex.Clear();
            buffIndex.Clear();
        }
        public void RemoveFXEpic()
        {
            buffIndex.ForEach(t => charController.buffController.RemoveBuff(t));
            dmgAltBuffIndex.ForEach(t => charController.strikeController.RemoveDmgBuff(t));
            dmgAltBuffIndex.Clear();
            buffIndex.Clear();
        }

    }
}
