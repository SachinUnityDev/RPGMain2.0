using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{

    public class BracersPoachersToolset : PoeticGewgawBase
    {

        //+2-3 Acc
        public override PoeticGewgawNames poeticGewgawName => PoeticGewgawNames.BracersPoachersToolset;
        int valAcc;
        public override void PoeticInit()
        {
            valAcc = UnityEngine.Random.Range(2, 4);
            string str = $"+{valAcc} Accuracy";
            displayStrs.Add(str);
        }
        public override void EquipGewgawPoetic()
        {
            int index = charController.buffController.ApplyBuff(CauseType.PoeticGewgaw, (int)poeticGewgawName
                , charController.charModel.charID, StatsName.acc, valAcc, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

        }
        public override void UnEquipPoetic()
        {
            buffIndex.ForEach(t => charController.buffController.RemoveBuff(t));
        }
    }
}
