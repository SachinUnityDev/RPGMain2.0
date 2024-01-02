using Common;
using Interactables;
using Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Quest
{


    public class FieldLoot : LootBase
    {
        public override LandscapeNames landscapeName => LandscapeNames.Field;

        public override void InitLootTable()
        {
            // FOODS 
            ItemLootData itemDataLoot = new ItemLootData(ItemType.Foods, (int)FoodNames.Beef, 1, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Foods, (int)FoodNames.Venison, 1, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Foods, (int)FoodNames.Poultry, 1, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Foods, (int)FoodNames.Bread, 2, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Foods, (int)FoodNames.FlaskOfWater, 2, 15);
            itemDataLs.Add(itemDataLoot);
            // FRUITS

            itemDataLoot = new ItemLootData(ItemType.Fruits, (int)FruitNames.Ube, 2, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Fruits, (int)FruitNames.Kiwi, 2, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Fruits, (int)FruitNames.Apple, 2, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Fruits, (int)FruitNames.Kiwano, 1, 5);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Fruits, (int)FruitNames.Carrot, 2, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Fruits, (int)FruitNames.AssegaiFruit, 2, 3);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Fruits, (int)FruitNames.Beet, 2, 5);
            itemDataLs.Add(itemDataLoot);

            // POTIONS
            itemDataLoot = new ItemLootData(ItemType.Potions, (int)PotionNames.ScorpionRepeller, 1, 12);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Potions, (int)PotionNames.PotionOfPrecision, 1, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Potions, (int)PotionNames.PotionOfNimbleness, 1, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Potions, (int)PotionNames.HealthPotion, 3, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Potions, (int)PotionNames.StaminaPotion, 2, 24);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Potions, (int)PotionNames.FortitudePotion, 2, 12);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Potions, (int)PotionNames.ElixirOfWillpower, 1, 3);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Potions, (int)PotionNames.DemonsPiss, 1, 12);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Potions, (int)PotionNames.SnowLeopardsBreath, 1, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Potions, (int)PotionNames.PotionOfRabbitblood, 1, 8);
            itemDataLs.Add(itemDataLoot);

            // HERBS
            itemDataLoot = new ItemLootData(ItemType.Herbs, (int)HerbNames.Hemp, 3, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Herbs, (int)HerbNames.DemonsClaw, 2, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Herbs, (int)HerbNames.Buchu, 2, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Herbs, (int)HerbNames.Echinacea, 2, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Herbs, (int)HerbNames.Aloe, 2, 12);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Herbs, (int)HerbNames.TambotiFruit, 3, 12);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Herbs, (int)HerbNames.Myrsine, 3, 5);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Herbs, (int)HerbNames.PoisonIvy, 2, 5);
            itemDataLs.Add(itemDataLoot);

            // TRADE GOODS

            itemDataLoot = new ItemLootData(ItemType.TradeGoods, (int)TGNames.SimpleRing, 1, 16);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.TradeGoods, (int)TGNames.GreenBoots, 1, 5);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.TradeGoods, (int)TGNames.DeerSkin, 1, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.TradeGoods, (int)TGNames.ShinyBoots, 1, 5);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.TradeGoods, (int)TGNames.Spice, 1, 3);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.TradeGoods, (int)TGNames.GreenVelvet, 1, 3);
            itemDataLs.Add(itemDataLoot);


            // TOOLS

            itemDataLoot = new ItemLootData(ItemType.Tools, (int)ToolNames.Firewood, 2, 15);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Tools, (int)ToolNames.Key, 2, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Tools, (int)ToolNames.Rope, 2, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Tools, (int)ToolNames.PlagueMask, 1, 5);
            itemDataLs.Add(itemDataLoot);

            // GEMS
            itemDataLoot = new ItemLootData(ItemType.Gems, (int)GemNames.Meranite, 1, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Gems, (int)GemNames.Malachite, 1, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Gems, (int)GemNames.Amber, 1, 3);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Gems, (int)GemNames.Oltu, 1, 3);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Gems, (int)GemNames.Rutele, 1, 2);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Gems, (int)GemNames.BlueOnyx, 1, 2);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Gems, (int)GemNames.Ruby, 1, 2);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Gems, (int)GemNames.Emerald, 1, 2);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Gems, (int)GemNames.Topaz, 1, 2);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Gems, (int)GemNames.Jacinth, 1, 2);
            itemDataLs.Add(itemDataLoot);


            itemDataLoot = new ItemLootData(ItemType.LoreBooks, (int)LoreBooksNames.LandsOfShargad, 1, 1);
            itemDataLs.Add(itemDataLoot);

            //SCROLLS
            itemDataLoot = new ItemLootData(ItemType.Scrolls, (int)ScrollNames.ScrollOfFire, 1, 6);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Scrolls, (int)ScrollNames.ScrollOfEarth, 1, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Scrolls, (int)ScrollNames.ScrollOfAir, 1, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Scrolls, (int)ScrollNames.ScrollOfLight, 1, 6);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Scrolls, (int)ScrollNames.ScrollOfMoss, 1, 2);
            itemDataLs.Add(itemDataLoot);

            //SAGAIC GEWGAWS 
            itemDataLoot = new ItemLootData(ItemType.SagaicGewgaws, (int)SagaicGewgawNames.FallenShoulder, 1, 5);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.SagaicGewgaws, (int)SagaicGewgawNames.EasyFit, 1, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.SagaicGewgaws, (int)SagaicGewgawNames.SoftAndTenacious, 1, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.SagaicGewgaws, (int)SagaicGewgawNames.CowryShellBelt, 1, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.SagaicGewgaws, (int)SagaicGewgawNames.BrassHandProtectors, 1, 5);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.SagaicGewgaws, (int)SagaicGewgawNames.HolyKris, 1, 2);
            itemDataLs.Add(itemDataLoot);


            //POETIC GEWGAWS  
            itemDataLoot = new ItemLootData(ItemType.PoeticGewgaws, (int)PoeticGewgawNames.GlovesLegacyOfTheSpida, 1, 2);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.PoeticGewgaws, (int)PoeticGewgawNames.BeltLegacyOfTheSpida, 1, 2);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.PoeticGewgaws, (int)PoeticGewgawNames.BeltPoachersToolset, 1, 6);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.PoeticGewgaws, (int)PoeticGewgawNames.BracersPoachersToolset, 1, 6);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.PoeticGewgaws, (int)PoeticGewgawNames.CloakPoachersToolset, 1, 4);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.PoeticGewgaws, (int)PoeticGewgawNames.NecklaceFirstHuntersArsenal, 1, 6);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.PoeticGewgaws, (int)PoeticGewgawNames.ThrowingKnifeFirstHuntersArsenal, 1, 6);
            itemDataLs.Add(itemDataLoot);



            // GENERIC GEWGAWS

            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Lyric
                                , (int)GenGewgawNames.ScarfOfCourage, 1, 15);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Folkloric
                                            , (int)GenGewgawNames.ScarfOfCourage, 1, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Epic
                                            , (int)GenGewgawNames.ScarfOfCourage, 1, 6);
            itemDataLs.Add(itemDataLoot);

            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Lyric
                         , (int)GenGewgawNames.CloakOfTheCommoner, 1, 15);
            itemDataLs.Add(itemDataLoot);

            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Folkloric
                                            , (int)GenGewgawNames.CloakOfTheCommoner, 1, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Epic
                                            , (int)GenGewgawNames.CloakOfTheCommoner, 1, 6);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Lyric
                      , (int)GenGewgawNames.AncientTabletOfEarth, 1, 15);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Folkloric
                                            , (int)GenGewgawNames.AncientTabletOfEarth, 1, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Epic
                                            , (int)GenGewgawNames.AncientTabletOfEarth, 1, 6);
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
                                            , (int)GenGewgawNames.BeltOfTheCommoner, 1, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Folkloric
                                            , (int)GenGewgawNames.BeltOfTheCommoner, 1, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Epic
                                            , (int)GenGewgawNames.BeltOfTheCommoner, 1, 4);
            itemDataLs.Add(itemDataLoot);

            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Folkloric
                                            , (int)GenGewgawNames.BeltOfTheSerpent, 1, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Epic
                                            , (int)GenGewgawNames.BeltOfTheSerpent, 1, 4);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Epic
                                            , (int)GenGewgawNames.GoldenBelt, 1, 4);
            itemDataLs.Add(itemDataLoot);

            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Folkloric
                                           , (int)GenGewgawNames.BracersOfVersatility, 1, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Epic
                                      , (int)GenGewgawNames.BracersOfVersatility, 1, 4);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Folkloric
                                    , (int)GenGewgawNames.EmeraldTiaraOfSerenity, 1, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Epic
                                      , (int)GenGewgawNames.EmeraldTiaraOfSerenity, 1, 4);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Folkloric
                                 , (int)GenGewgawNames.RubyRingOfMirth, 1, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Epic
                                      , (int)GenGewgawNames.RubyRingOfMirth, 1, 4);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Epic
                                      , (int)GenGewgawNames.GoldenAmuletOfTheLion, 1, 4);
            itemDataLs.Add(itemDataLoot);

            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Lyric
                                    , (int)GenGewgawNames.BootsOfTheCommoner, 1, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Folkloric
                                    , (int)GenGewgawNames.BootsOfTheCommoner, 1, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Epic
                                      , (int)GenGewgawNames.BootsOfTheCommoner, 1, 4);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Lyric
                          , (int)GenGewgawNames.SilverNecklaceOfWarding, 1, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Folkloric
                                    , (int)GenGewgawNames.SilverNecklaceOfWarding, 1, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Epic
                                      , (int)GenGewgawNames.SilverNecklaceOfWarding, 1, 4);
            itemDataLs.Add(itemDataLoot);

            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Lyric
                          , (int)GenGewgawNames.BronzePauldrons, 1, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Folkloric
                                    , (int)GenGewgawNames.BronzePauldrons, 1, 8);
            itemDataLs.Add(itemDataLoot);
        }
    }
}