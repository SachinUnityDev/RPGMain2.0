using Combat;
using Interactables;
using System;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;


namespace Common
{
    public class WeekOfHunters : WeekEventBase
    {
        public override WeekEventsName weekName => WeekEventsName.WeekOfHunters;

        public override void OnWeekApply()
        {
            foreach (CharController charController in CharService.Instance.allyInPlayControllers)
            {
                if (charController.charModel.archeType == Archetype.Hunter)
                {
                    charController.buffController.ApplyBuff(CauseType.WeekEvents, (int)weekName,
                          charController.charModel.charID, AttribName.acc, +2, TimeFrame.EndOfWeek, 1, true);
                }
            }
        }
        
        public override void OnWeekBonusClicked()
        {
            // "%30 Venison (2), Deer Skin, Deer Trophy
            //% 30 Hunter type heroes: Ranged Skills +20% More Dmg on Attack Type Ranged  .. ? +2 ACC
            //% 20 Poachers toolset, random item
            //% 20 nothing happens"
            List<float> chances = new List<float>() { 30f, 30f, 20f, 20f };
            int res = chances.GetChanceFrmList();
            switch (res)
            {
                case 1:
                    ItemData itemData = new ItemData(ItemType.Foods, (int)FoodNames.Venison);
                    for (int i = 0; i < 2; i++)
                    {
                        Iitems item = ItemService.Instance.GetNewItem(itemData);
                        InvService.Instance.invMainModel.AddItem2CommORStash(item);
                    }
                    itemData = new ItemData(ItemType.TradeGoods, (int)TGNames.DeerSkin);
                    Iitems item2 = ItemService.Instance.GetNewItem(itemData);
                    InvService.Instance.invMainModel.AddItem2CommORStash(item2);

                    itemData = new ItemData(ItemType.TradeGoods, (int)TGNames.DeerTrophy);
                    Iitems item3 = ItemService.Instance.GetNewItem(itemData);
                    InvService.Instance.invMainModel.AddItem2CommORStash(item3);
                    resultStr = $"Venison, Deer Skin, Deer Trophy";
                    break;
                case 2:
                    foreach (CharController charController in CharService.Instance.allAvailComp)
                    {
                        if (charController.charModel.archeType != Archetype.Hunter) continue; 

                          charController.strikeController.ApplyDmgAltBuff(+20f, CauseType.WeekEvents, (int)weekName
                              , charController.charModel.charID, TimeFrame.EndOfDay, 3, true, AttackType.Ranged, DamageType.Physical);                               
                         
                    }
                    resultStr = $"Hunter Archetype: +20% Physical Ranged Dmg, 3 days";
                    break;
                case 3:
                    List<PoeticGewgawSO> allPoeticSO = ItemService.Instance.allItemSO.GetAllInAToolSet(PoeticSetName.PoachersToolset);
                    int ran = UnityEngine.Random.Range(0,allPoeticSO.Count);                        
                    PoeticGewgawSO poeticSelect = allPoeticSO[ran]; 
                    ItemData itemData1 = new ItemData(ItemType.PoeticGewgaws, (int)poeticSelect.poeticGewgawName);
                    Iitems item1 = ItemService.Instance.GetNewItem(itemData1);
                    InvService.Instance.invMainModel.AddItem2CommORStash(item1);
                    resultStr = $"{poeticSelect.poeticGewgawName}";
                    break;
                case 4:
                    resultStr = $"Nothing!";
                    // nothing happens
                    break;
            }
        }
    }
}