using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{


    public class RingLegacyOfSpida : PoeticGewgawBase
    {
        //1-3 focus
        public override PoeticGewgawNames poeticGewgawName => PoeticGewgawNames.RingLegacyOfTheSpida;
        int valFocus; 
        public override void PoeticInit()
        {
            valFocus = UnityEngine.Random.Range(1, 4);
            string str = $"+{valFocus} Focus";
            displayStrs.Add(str);
        }
        public override void EquipGewgawPoetic()
        {
            charController = InvService.Instance.charSelectController;

            int index =
                charController.buffController.ApplyBuff(CauseType.PoeticGewgaw, (int)poeticGewgawName
                , charController.charModel.charID, StatsName.focus, valFocus, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);
        }
        public override void UnEquipPoetic()
        {
            buffIndex.ForEach(t => charController.buffController.RemoveBuff(t));
        }   
    }
}