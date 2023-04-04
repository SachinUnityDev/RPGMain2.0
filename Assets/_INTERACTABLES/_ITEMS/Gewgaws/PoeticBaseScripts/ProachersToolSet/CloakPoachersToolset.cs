using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class CloakProachertoolset : PoeticGewgawBase
    {
        //+2-3 dodge
        public override PoeticGewgawNames poeticGewgawName => PoeticGewgawNames.CloakPoachersToolset;

        int valDodge;
        public override void PoeticInit()
        {
            valDodge = UnityEngine.Random.Range(2, 4);
            string str = $"+{valDodge} Dodge";
            displayStrs.Add(str);
        }
        public override void EquipGewgawPoetic()
        {
            int index = charController.buffController.ApplyBuff(CauseType.PoeticGewgaw, (int)poeticGewgawName
                , charController.charModel.charID, AttribName.dodge, valDodge, TimeFrame.Infinity, -1, true);
            buffIndex.Add(index);

        }
        public override void UnEquipPoetic()
        {
            buffIndex.ForEach(t => charController.buffController.RemoveBuff(t));
        }
    }
}