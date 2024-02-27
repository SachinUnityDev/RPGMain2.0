using Combat;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using Town;
using Unity.Mathematics;
using UnityEngine;



namespace Common
{
    public class WeekOfRejuvenation : WeekEventBase
    {
        public override WeekEventsName weekName => WeekEventsName.WeekOfRejuvenation;

        public override void OnWeekApply()
        {
            //  Potion costs are tripled
            //  Herbs are half cost
            EventCostMultData costData = new EventCostMultData(ItemType.Potions, 3); 
            allEventCostMultData.Add(costData);
            costData = new EventCostMultData(ItemType.Herbs, 0.5f);
            allEventCostMultData.Add(costData);
            EcoServices.Instance.ecoController.ApplyWeekEventCostMultiplier(allEventCostMultData); 

        }
        public override void OnWeekBonusClicked()
        {
            //            "%20 Health Potion (2) 
            //% 30 Gain Buff: +1 Hp regen, 2 days
            //% 20 Gain Elixir of Vigor
            //% 30 nothing happens"
            List<float> chances = new List<float>() { 20f, 30f, 20f, 30f };
            int res = chances.GetChanceFrmList();
            switch (res)
            {
                case 1:

                    ItemData itemData = new ItemData(ItemType.Potions, (int)PotionNames.HealthPotion);
                    for (int i = 0; i < 2; i++)
                    {
                        Iitems item = ItemService.Instance.GetNewItem(itemData);
                        InvService.Instance.invMainModel.AddItem2CommORStash(item);
                    }
                    resultStr = $"{PotionNames.HealthPotion}";
                    break;
                case 2:
                    foreach (CharController charController in CharService.Instance.allAvailComp)
                    {
                        charController.buffController.ApplyBuff(CauseType.WeekEvents, (int)weekName,
                            charController.charModel.charID, AttribName.hpRegen, 1, TimeFrame.EndOfDay, 3, true);
                    }
                    resultStr = $"+1 Hp Regen, 3 days";
                    break;
                case 3:
                    int ran = ItemService.Instance.allItemSO.GetRandomItem(ItemType.Potions);
                    PotionSO potionSO = ItemService.Instance.allItemSO.allPotionSO[ran];
                    ItemData itemData1 = new ItemData(ItemType.Potions, (int)potionSO.potionName);
                    Iitems item1 = ItemService.Instance.GetNewItem(itemData1);
                    InvService.Instance.invMainModel.AddItem2CommORStash(item1);
                    resultStr = $"{potionSO.potionName}";
                    break;
                case 4:
                    resultStr = $"Nothing!";
                    // nothing happens
                    break;
            }

        }
    }
}