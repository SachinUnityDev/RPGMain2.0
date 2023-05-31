using Common;
using Interactables;
using Quest;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Town;
using UnityEngine;


namespace Quest
{


    public class SavannahLoot : LootBase
    {
        public override LandscapeNames landscapeName => LandscapeNames.Savannah;

        public override void InitLootTable()
        {
            // FOODS 
            ItemLootData itemDataLoot = new ItemLootData(ItemType.Foods, (int)FoodNames.Mutton, 1, 20);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Foods, (int)FoodNames.Venison, 1, 20);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Foods, (int)FoodNames.Bread, 2, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Foods, (int)FoodNames.FlaskOfWater, 2, 15);
            itemDataLs.Add(itemDataLoot);
            // FRUITS

            itemDataLoot = new ItemLootData(ItemType.Fruits, (int)FruitNames.Ube, 2, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Fruits, (int)FruitNames.Kiwi, 2, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Fruits, (int)FruitNames.Apple, 2, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Fruits, (int)FruitNames.Kiwano, 1, 5);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Fruits, (int)FruitNames.Cucumber, 2, 5);
            itemDataLs.Add(itemDataLoot);

            // POTIONS
            itemDataLoot = new ItemLootData(ItemType.Potions, (int)PotionNames.ScorpionRepeller, 1, 12);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Potions, (int)PotionNames.PotionOfPrecision, 2, 14);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Potions, (int)PotionNames.PotionOfNimbleness, 2, 14);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Potions, (int)PotionNames.HealthPotion, 2, 20);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Potions, (int)PotionNames.StaminaPotion, 2, 20);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Potions, (int)PotionNames.FortitudePotion, 1, 20);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Potions, (int)PotionNames.ElixirOfVigor, 1, 3);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Potions, (int)PotionNames.DemonsPiss, 1, 12);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Potions, (int)PotionNames.SnowLeopardsBreath, 1, 12);
            itemDataLs.Add(itemDataLoot);

            // HERBS
            itemDataLoot = new ItemLootData(ItemType.Herbs, (int)HerbNames.Hemp, 3, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Herbs, (int)HerbNames.DemonsClaw, 2, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Herbs, (int)HerbNames.Buchu, 2, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Herbs, (int)HerbNames.Echinacea, 2, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Herbs, (int)HerbNames.Aloe, 2, 5);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Herbs, (int)HerbNames.Myrsine, 3, 5);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Herbs, (int)HerbNames.PurpleTeaLeaf, 3, 15);
            itemDataLs.Add(itemDataLoot);

            // TRADE GOODS 
            itemDataLoot = new ItemLootData(ItemType.TradeGoods, (int)TGNames.SimpleRing, 1, 16);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.TradeGoods, (int)TGNames.NyalaPelt, 1, 5);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.TradeGoods, (int)TGNames.DeerSkin, 1, 14);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.TradeGoods, (int)TGNames.ShinyBoots, 1, 5);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.TradeGoods, (int)TGNames.Flour, 1, 4);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.TradeGoods, (int)TGNames.GoldenrodDye, 1, 4);
            itemDataLs.Add(itemDataLoot);


            // TOOLS
            itemDataLoot = new ItemLootData(ItemType.Tools, (int)ToolNames.Shovel, 1, 5);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Tools, (int)ToolNames.Firewood, 2, 5);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Tools, (int)ToolNames.Key, 1, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Tools, (int)ToolNames.Rope, 1, 5);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Tools, (int)ToolNames.Chalice, 1, 1);
            itemDataLs.Add(itemDataLoot);

            // GEMS
            itemDataLoot = new ItemLootData(ItemType.Gems, (int)GemNames.Meranite, 1, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Gems, (int)GemNames.Malachite, 1, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Gems, (int)GemNames.Amber, 1, 6);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Gems, (int)GemNames.Oltu, 1, 6);
            itemDataLs.Add(itemDataLoot);

            itemDataLoot = new ItemLootData(ItemType.LoreScroll, (int)LoreScrollName.LoreScroll, 1, 1);
            itemDataLs.Add(itemDataLoot);

            //SCROLLS
            itemDataLoot = new ItemLootData(ItemType.Scrolls, (int)ScrollNames.ScrollOfWater, 1, 4);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Scrolls, (int)ScrollNames.ScrollOfEarth, 1, 7);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Scrolls, (int)ScrollNames.ScrollOfMoss, 1, 2);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Scrolls, (int)ScrollNames.ScrollOfFire, 1, 4);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Scrolls, (int)ScrollNames.ScrollOfDark, 1, 2);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Scrolls, (int)ScrollNames.ScrollOfLight, 1, 2);
            itemDataLs.Add(itemDataLoot);

            //SAGAIC GEWGAWS 
            itemDataLoot = new ItemLootData(ItemType.SagaicGewgaws, (int)SagaicGewgawNames.HornsAndBalls, 1, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.SagaicGewgaws, (int)SagaicGewgawNames.EasyFit, 1, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.SagaicGewgaws, (int)SagaicGewgawNames.FallenShoulder, 1, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.SagaicGewgaws, (int)SagaicGewgawNames.CowryShellBelt, 1, 12);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.SagaicGewgaws, (int)SagaicGewgawNames.BrassHandProtectors, 1, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.SagaicGewgaws, (int)SagaicGewgawNames.HolyKris, 1, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.SagaicGewgaws, (int)SagaicGewgawNames.SharpshootersVest, 1, 8);
            itemDataLs.Add(itemDataLoot);


            //POETIC GEWGAWS  
            itemDataLoot = new ItemLootData(ItemType.PoeticGewgaws, (int)PoeticGewgawNames.BeltPoachersToolset, 1, 6);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.PoeticGewgaws, (int)PoeticGewgawNames.BracersPoachersToolset, 1, 6);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.PoeticGewgaws, (int)PoeticGewgawNames.CloakPoachersToolset, 1, 4);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.PoeticGewgaws, (int)PoeticGewgawNames.NecklaceFirstHuntersArsenal, 1, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.PoeticGewgaws, (int)PoeticGewgawNames.ThrowingKnifeFirstHuntersArsenal, 1, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.PoeticGewgaws, (int)PoeticGewgawNames.RaffiaTrapFirstHuntersArsenal, 1, 12);
            itemDataLs.Add(itemDataLoot);


            // GENERIC GEWGAWS
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Folkloric
                                            , (int)GenGewgawNames.PauldronsOfSwiftness, 1, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Epic
                                            , (int)GenGewgawNames.PauldronsOfSwiftness, 1, 6);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Lyric
                                , (int)GenGewgawNames.PauldronsOfSwiftness, 1, 15);
            itemDataLs.Add(itemDataLoot);

            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Folkloric
                                            , (int)GenGewgawNames.BeltOfSerenity, 1, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Epic
                                            , (int)GenGewgawNames.BeltOfSerenity, 1, 6);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Lyric
                         , (int)GenGewgawNames.BeltOfSerenity, 1, 15);
            itemDataLs.Add(itemDataLoot);

            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Lyric
                      , (int)GenGewgawNames.AncientTabletOfEarth, 1, 5);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Folkloric
                                            , (int)GenGewgawNames.AncientTabletOfEarth, 1, 3);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Epic
                                            , (int)GenGewgawNames.AncientTabletOfEarth, 1, 1);
            itemDataLs.Add(itemDataLoot);

            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Lyric
                      , (int)GenGewgawNames.CamelLeatherBeltOfEvasion, 1, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Folkloric
                                            , (int)GenGewgawNames.CamelLeatherBeltOfEvasion, 1, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Epic
                                            , (int)GenGewgawNames.CamelLeatherBeltOfEvasion, 1, 4);
            itemDataLs.Add(itemDataLoot);

            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Folkloric
                                            , (int)GenGewgawNames.BeltOfTheSerpent, 1, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Epic
                                            , (int)GenGewgawNames.BeltOfTheSerpent, 1, 4);
            itemDataLs.Add(itemDataLoot);

            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Epic
                                            , (int)GenGewgawNames.GoldenBelt, 1, 3);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Lyric
                                            , (int)GenGewgawNames.SkullGloves, 1, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Folkloric
                                           , (int)GenGewgawNames.SkullGloves, 1, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Epic
                                           , (int)GenGewgawNames.SkullGloves, 1, 4);
            itemDataLs.Add(itemDataLoot);

            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Lyric
                                      , (int)GenGewgawNames.CamelLeatherGlovesOfTheScholar, 1, 12);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Folkloric
                                           , (int)GenGewgawNames.CamelLeatherGlovesOfTheScholar, 1, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Epic
                                      , (int)GenGewgawNames.CamelLeatherGlovesOfTheScholar, 1, 4);
            itemDataLs.Add(itemDataLoot);

            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Lyric
                                      , (int)GenGewgawNames.HideGlovesOfTheDonkey, 1, 12);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Folkloric
                                           , (int)GenGewgawNames.HideGlovesOfTheDonkey, 1, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Epic
                                      , (int)GenGewgawNames.HideGlovesOfTheDonkey, 1, 4);
            itemDataLs.Add(itemDataLoot);

            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Folkloric
                                           , (int)GenGewgawNames.BracersOfTheLeopard, 1, 12);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Epic
                                      , (int)GenGewgawNames.BracersOfTheLeopard, 1, 6);
            itemDataLs.Add(itemDataLoot);

            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Folkloric
                                    , (int)GenGewgawNames.RubyRingOfMirth, 1, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Epic
                                      , (int)GenGewgawNames.RubyRingOfMirth, 1, 4);
            itemDataLs.Add(itemDataLoot);

            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Folkloric
                                 , (int)GenGewgawNames.RingOfTheSerpent, 1, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Epic
                                      , (int)GenGewgawNames.RingOfTheSerpent, 1, 4);
            itemDataLs.Add(itemDataLoot);

            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Epic
                                            , (int)GenGewgawNames.GoldenAmuletOfTheLion, 1, 3);
            itemDataLs.Add(itemDataLoot);

            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Folkloric
                         , (int)GenGewgawNames.SilverNecklaceOfWarding, 1, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Epic
                                      , (int)GenGewgawNames.SilverNecklaceOfWarding, 1, 4);
            itemDataLs.Add(itemDataLoot);

            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Lyric
                          , (int)GenGewgawNames.ShinBoneOfTheDonkey, 1, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Folkloric
                                    , (int)GenGewgawNames.ShinBoneOfTheDonkey, 1, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Epic
                                      , (int)GenGewgawNames.ShinBoneOfTheDonkey, 1, 4);
            itemDataLs.Add(itemDataLoot);
        }
    }
}