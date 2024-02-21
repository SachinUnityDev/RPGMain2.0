using Combat;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;


namespace Common
{
    public class WeekOfTheBeast : WeekEventBase
    {
        public override WeekEventsName weekName => WeekEventsName.WeekOfTheBeast;

        public override void OnWeekApply()
        {
            
        }
        public override void OnWeekBonusClicked()
        {
            //            "%20 lion pelt + lioness pelt
            //% 30 Beastmen: +1 Morale, 3 days
            //% 20 leopard, leopardess pelt
            //% 30 nothing happens"
            List<float> chances = new List<float>() { 20f, 30f, 20f, 30f };
            int res = chances.GetChanceFrmList();
            switch (res)
            {
                case 1:
                    ItemData itemData = new ItemData(ItemType.TradeGoods, (int)TGNames.LionessPelt);                    
                    Iitems item = ItemService.Instance.GetNewItem(itemData);
                    InvService.Instance.invMainModel.AddItem2CommORStash(item);
                    
                    itemData = new ItemData(ItemType.TradeGoods, (int)TGNames.LionPelt);
                    Iitems item2 = ItemService.Instance.GetNewItem(itemData);
                    InvService.Instance.invMainModel.AddItem2CommORStash(item2);

                    itemData = new ItemData(ItemType.Ingredients, (int)IngredNames.FelineHeart);
                    Iitems item3 = ItemService.Instance.GetNewItem(itemData);
                    InvService.Instance.invMainModel.AddItem2CommORStash(item3);

                    resultStr = $"Lion Pelt, Lioness Pelt and Feline Heart";
                    break;
                case 2:
                    foreach (CharController charController in CharService.Instance.allAvailComp)
                    {
                        if (charController.charModel.raceType != RaceType.Beastmen) continue;

                        charController.strikeController.ApplyDmgAltBuff(+20f, CauseType.WeekEvents, (int)weekName
                            , charController.charModel.charID, TimeFrame.EndOfDay, 3, true, AttackType.Melee, DamageType.Physical);

                        charController.buffController.ApplyBuff(CauseType.WeekEvents, (int)weekName,
                            charController.charModel.charID, AttribName.morale, +1, TimeFrame.EndOfDay, 3, true);
                    }
                    resultStr = $"Beastmen: +1 Morale, +20% Physical Melee Dmg, 3 days";
                    break;
                case 3:
                    itemData = new ItemData(ItemType.TradeGoods, (int)TGNames.HyenaPelt);
                    Iitems item4 = ItemService.Instance.GetNewItem(itemData);
                    InvService.Instance.invMainModel.AddItem2CommORStash(item4);

                    Iitems item5 = ItemService.Instance.GetNewItem(itemData);
                    InvService.Instance.invMainModel.AddItem2CommORStash(item5);

                    itemData = new ItemData(ItemType.Ingredients, (int)IngredNames.HyenaEar);
                    Iitems item6 = ItemService.Instance.GetNewItem(itemData);
                    InvService.Instance.invMainModel.AddItem2CommORStash(item6);

                    resultStr = $"Hyena Pelt and Hyena Ear";
                    break;
                case 4:
                    resultStr = $"Nothing!";
                    // nothing happens
                    break;
            }
        }
    }
}