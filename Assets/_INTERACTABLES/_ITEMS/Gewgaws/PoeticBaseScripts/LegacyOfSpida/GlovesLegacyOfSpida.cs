using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{


    public class GlovesLegacyOfSpida : PoeticGewgawBase
    {
        public override PoeticGewgawNames poeticGewgawName => PoeticGewgawNames.GlovesLegacyOfTheSpida;
        // +2-3 Dodge
        int valDodge; 
        public override void PoeticInit()
        {
            valDodge = UnityEngine.Random.Range(2, 4);
            string str = $"+{valDodge} Dodge";
            displayStrs.Add(str);
        }
        public override void EquipGewgawPoetic()
        { 
                charController = InvService.Instance.charSelectController;   
                int index =
                charController.buffController.ApplyBuff(CauseType.PoeticGewgaw, (int)poeticGewgawName
                , charController.charModel.charID, StatsName.dodge, valDodge, TimeFrame.Infinity, -1, true);
                 buffIndex.Add(index);

        }
        public override void UnEquipPoetic()
        {
            buffIndex.ForEach(t => charController.buffController.RemoveBuff(t));
        }
    
    }
}