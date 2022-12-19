using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Interactables
{



    public class BeltPoachersToolset : PoeticGewgawBase
    {
        // -12-18% Hunger mod
        public override PoeticGewgawNames poeticGewgawName => PoeticGewgawNames.BeltPoachersToolset;
        int valHunger; 

        public override void PoeticInit()
        {
            valHunger = UnityEngine.Random.Range(12, 19);
            string str = $"-{valHunger}% Hunger";
            displayStrs.Add(str);
        }
        public override void EquipGewgawPoetic()
        {
            charController = InvService.Instance.charSelectController;

            charController.charModel.hungerMod += valHunger;
       
        }
        public override void UnEquipPoetic()
        {
            charController.ChangeHungerNThirst(CauseType.PoeticGewgaw, (int)poeticGewgawName
                , charController.charModel.charID, StatsName.hunger, valHunger, true);

            charController.charModel.hungerMod -= valHunger;
        }

    }
}