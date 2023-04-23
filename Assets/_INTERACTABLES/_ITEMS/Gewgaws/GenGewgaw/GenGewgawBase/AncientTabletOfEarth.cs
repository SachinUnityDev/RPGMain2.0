using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{


    public class AncientTabletOfEarth : GenGewgawBase, Iitems, IEquipAble
    {
        public override GenGewgawNames genGewgawNames => GenGewgawNames.AncientTabletOfEarth;
        public int itemId { get; set; }
        public ItemType itemType => ItemType.GenGewgaws;
        public int itemName => (int)GenGewgawNames.AncientTabletOfEarth;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; }
        int valLuck;
        bool expFXAdded;
        public void ApplyEquipableFX()
        {
            charController = InvService.Instance.charSelectController;
            //    expFXAdded = false;
            //    CalendarService.Instance.OnStartOfCalDay += ExpIncrBasedOnDay;
            //    LandScapeFX();
        }

        public void InitItem(int itemId, int maxInvStackSize)
        {
            this.itemId = itemId;
            this.maxInvStackSize = maxInvStackSize;
        }

        public void OnHoverItem()
        {
            //valLuck = UnityEngine.Random.Range(2, 4);
            //string str = $"+{valLuck} Luck in RainForestm Jungle and Earth";
            //displayStrs.Add(str);
        }

        public void RemoveEquipableFX()
        {
            
        }
    }
}

////Shrine of Ruru(curio) has double effect for temporary specs * skip to be looked at Later
////Double exp from during the day of Earth 
////	+2-3 Luck in Rainforest, Jungle landscapes	
////	+20% dmg mod for Earth Attack Skills(TO BE DONE....)
//int valLuck;
//bool expFXAdded;

//public override void GewGawSagaicInit()
//{
//    valLuck = UnityEngine.Random.Range(2, 4);
//    string str = $"+{valLuck} Luck in RainForestm Jungle and Earth";
//    displayStrs.Add(str);
//}

//public override void EquipGewgawSagaic()
//{
//    charController = InvService.Instance.charSelectController;
//    expFXAdded = false;
//    CalendarService.Instance.OnStartOfCalDay += ExpIncrBasedOnDay;
//    LandScapeFX();


//}

//void ExpIncrBasedOnDay(int day)
//{
//    DayName dayName = CalendarService.Instance.currDayName;
//    if (dayName == DayName.DayOfEarth && !expFXAdded)
//    {
//        int index = charController.buffController.ApplyExpBuff(CauseType.SagaicGewgaw, (int)sagaicGewgawName
//              , charController.charModel.charID, 100, TimeFrame.Infinity, 1, true);

//        expIndex.Add(index);
//        expFXAdded = true;
//    }
//}
//void LandScapeFX()
//{
//    if (GameService.Instance.gameModel.landscapeNames == LandscapeNames.Rainforest
//        || GameService.Instance.gameModel.landscapeNames == LandscapeNames.Jungle)
//    {
//        int buffID = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
//            (int)sagaicGewgawName, AttribName.luck, valLuck, TimeFrame.EndOfRound, 3, true);
//        buffIndex.Add(buffID);
//    }
//}

//public override void UnEquipSagaic()
//{
//    buffIndex.ForEach(t => charController.buffController.RemoveBuff(t));
//    buffIndex.Clear();
//    expIndex.ForEach(t => charController.buffController.RemoveBuff(t));
//    expIndex.Clear();
//}


