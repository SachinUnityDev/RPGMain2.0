using Common;
using Interactables;
using Quest;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;

namespace Quest
{
    public class Hunt1 : MapEbase, iResult
    {
        public override MapENames mapEName => MapENames.Hunt1;

        public GameScene gameScene => GameScene.MAPINTERACT; 

        CharNames charJoined;
        Currency money2Lose;

        List<ItemDataWithQty> allItemDataWithQty = new List<ItemDataWithQty>();    

        public override void MapEContinuePressed()
        {
            EncounterService.Instance.mapEController.On_MapEComplete(mapEName, mapEResult);
            PathModel pathModel = MapService.Instance.pathController.currPathModel; 
            EncounterService.Instance.mapEController.ShowMapE(MapENames.Hunt2, pathModel);
        }

        public override void OnChoiceASelect()
        {
          //  You encountered a Hyena pack, get ready to fight!You gained loot!You gained loot!

            Transform parentTrans = EncounterService.Instance.mapEController.mapEView.transform;

            if (00f.GetChance())
            {
                // trigger combat vs Hyena pack
                resultStr = " You encountered a Hyena pack, get ready to fight!";
            }
            else if (55f.GetChance())
            {
                allItemDataWithQty.Clear(); 
                int qty = UnityEngine.Random.Range(3, 7);
                ItemDataWithQty itemDataWithQty
                    = new ItemDataWithQty(new ItemData(ItemType.Foods, (int)FoodNames.Venison), qty); 
                allItemDataWithQty.Add(itemDataWithQty);
                
                qty = UnityEngine.Random.Range(1, 3);
                itemDataWithQty
                    = new ItemDataWithQty(new ItemData(ItemType.TradeGoods, (int)TGNames.DeerTrophy), qty);
                allItemDataWithQty.Add(itemDataWithQty);

                qty = UnityEngine.Random.Range(2, 5);
                itemDataWithQty
                    = new ItemDataWithQty(new ItemData(ItemType.TradeGoods, (int)TGNames.DeerSkin), qty);
                allItemDataWithQty.Add(itemDataWithQty);

                //LootService.Instance.lootView.InitLootList(allItemDataWithQty, parentTrans);
                LootService.Instance.lootController.ShowLootTable4MapE(allItemDataWithQty, parentTrans);

                resultStr = "You gained loot!";
                mapEResult = true;
            }
            else
            {
                allItemDataWithQty.Clear();
                int qty = UnityEngine.Random.Range(3, 8);
                ItemDataWithQty itemDataWithQty
                    = new ItemDataWithQty(new ItemData(ItemType.Foods, (int)FoodNames.Venison), qty);
                allItemDataWithQty.Add(itemDataWithQty);

                qty = UnityEngine.Random.Range(2, 4);
                itemDataWithQty
                    = new ItemDataWithQty(new ItemData(ItemType.TradeGoods, (int)TGNames.NyalaPelt), qty);
                allItemDataWithQty.Add(itemDataWithQty);

                qty = UnityEngine.Random.Range(1, 2);
                itemDataWithQty
                    = new ItemDataWithQty(new ItemData(ItemType.TradeGoods, (int)TGNames.NyalaTrophy), qty);
                allItemDataWithQty.Add(itemDataWithQty);

                LootService.Instance.lootController.ShowLootTable4MapE(allItemDataWithQty, parentTrans);
                resultStr = "You gained loot!";
                mapEResult = true;
            }
        }

        public override void OnChoiceBSelect()
        {
            mapEResult = true; 
            MapService.Instance.pathController.pathQView.Move2TownFail(); 
            EncounterService.Instance.mapEController.On_MapEComplete(mapEName, mapEResult);
        }

        public void OnResultClicked(Result result)
        {
            

        }

        public void OnResult(Result result)
        {
        }
    }
}
