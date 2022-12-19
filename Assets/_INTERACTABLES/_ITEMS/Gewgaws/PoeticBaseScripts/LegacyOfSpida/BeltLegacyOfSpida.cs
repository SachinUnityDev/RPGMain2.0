using Combat;
using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{


    public class BeltLegacyOfSpida : PoeticGewgawBase
    {
        public override PoeticGewgawNames poeticGewgawName => PoeticGewgawNames.BeltLegacyOfTheSpida;
     
        int valER; 
        public override void PoeticInit()
        {
            valER = UnityEngine.Random.Range(6, 13);            
            string str = $"+{valER} Earth Res";
            displayStrs.Add(str);
        }
        public override void EquipGewgawPoetic()
        {
            charController = InvService.Instance.charSelectController;
   
            int index =
                charController.buffController.ApplyBuff(CauseType.PoeticGewgaw, (int)poeticGewgawName
                , charController.charModel.charID, StatsName.earthRes, valER, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);
        }
        public override void UnEquipPoetic()
        {
            buffIndex.ForEach(t => charController.buffController.RemoveBuff(t));          
        }
    }
}