using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public class SewersLoot : LootBase
    {
        public override LandscapeNames landscapeName => LandscapeNames.Sewers; 

        public override void InitLootTable()
        {
            //// FOODS 
            ItemLootData itemDataLoot = new ItemLootData(ItemType.Foods, (int)FoodNames.Beef, 1, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Foods, (int)FoodNames.Poultry, 1, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Foods, (int)FoodNames.Fish, 1, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Foods, (int)FoodNames.Bread, 2, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Foods, (int)FoodNames.FlaskOfWater, 2, 15);
            itemDataLs.Add(itemDataLoot);
            // FRUITS

            itemDataLoot = new ItemLootData(ItemType.Fruits, (int)FruitNames.AssegaiFruit, 2, 3);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Fruits, (int)FruitNames.Kiwi, 2, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Fruits, (int)FruitNames.Apple, 2, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Fruits, (int)FruitNames.Mangosteen, 1, 3);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Fruits, (int)FruitNames.Cucumber, 2, 10);
            itemDataLs.Add(itemDataLoot);

           // POTIONS
           itemDataLoot = new ItemLootData(ItemType.Potions, (int)PotionNames.ScorpionRepeller, 1, 14);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Potions, (int)PotionNames.PotionOfPrecision, 2, 11);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Potions, (int)PotionNames.PotionOfNimbleness, 2, 11);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Potions, (int)PotionNames.HealthPotion, 2, 20);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Potions, (int)PotionNames.StaminaPotion, 2, 20);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Potions, (int)PotionNames.FortitudePotion, 2, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Potions, (int)PotionNames.PotionOfHeroism, 1, 20);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Potions, (int)PotionNames.PotionOfRabbitblood, 1, 8);
            itemDataLs.Add(itemDataLoot);

            // HERBS
            itemDataLoot = new ItemLootData(ItemType.Herbs, (int)HerbNames.Hemp, 3, 3);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Herbs, (int)HerbNames.PoisonIvy, 2, 4);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Herbs, (int)HerbNames.Myrsine, 3, 2);
            itemDataLs.Add(itemDataLoot);

            // TRADE GOODS 
            itemDataLoot = new ItemLootData(ItemType.TradeGoods, (int)TGNames.SimpleRing, 1, 16);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.TradeGoods, (int)TGNames.GreenBoots, 1, 12);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.TradeGoods, (int)TGNames.ShinyBoots, 1, 4);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.TradeGoods, (int)TGNames.GreenVelvet, 1, 1);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.TradeGoods, (int)TGNames.CatechuDye, 1, 4);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.TradeGoods, (int)TGNames.GoldenrodDye, 1, 4);
            itemDataLs.Add(itemDataLoot);


            // TOOLS
            itemDataLoot = new ItemLootData(ItemType.Tools, (int)ToolNames.Shovel, 1, 5);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Tools, (int)ToolNames.Firewood, 1, 5);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Tools, (int)ToolNames.Key, 1, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Tools, (int)ToolNames.Rope, 1, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Tools, (int)ToolNames.PlagueMask, 1, 12);
            itemDataLs.Add(itemDataLoot);

            // GEMS
            itemDataLoot = new ItemLootData(ItemType.Gems, (int)GemNames.Ruri, 1, 5);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Gems, (int)GemNames.Malachite, 1, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Gems, (int)GemNames.Carnelian, 1, 5);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Gems, (int)GemNames.Oltu, 1, 3);
            itemDataLs.Add(itemDataLoot);

            itemDataLoot = new ItemLootData(ItemType.LoreBooks, (int)LoreBooksNames.LandsOfShargad, 1, 1);
            itemDataLs.Add(itemDataLoot);

            ////SCROLLS
            itemDataLoot = new ItemLootData(ItemType.Scrolls, (int)ScrollNames.ScrollOfWater, 1, 5);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Scrolls, (int)ScrollNames.ScrollOfEarth, 1, 7);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.Scrolls, (int)ScrollNames.ScrollOfMoss, 1, 3);
            itemDataLs.Add(itemDataLoot);

            ////SAGAIC GEWGAWS 
            itemDataLoot = new ItemLootData(ItemType.SagaicGewgaws, (int)SagaicGewgawNames.HornsAndBalls, 1, 5);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.SagaicGewgaws, (int)SagaicGewgawNames.Spiteeth, 1, 20);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.SagaicGewgaws, (int)SagaicGewgawNames.SoftAndTenacious, 1, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.SagaicGewgaws, (int)SagaicGewgawNames.CowryShellBelt, 1, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.SagaicGewgaws, (int)SagaicGewgawNames.BrassHandProtectors, 1, 6);
            itemDataLs.Add(itemDataLoot);

            ////POETIC GEWGAWS  
            itemDataLoot = new ItemLootData(ItemType.PoeticGewgaws, (int)PoeticGewgawNames.GlovesLegacyOfTheSpida, 1, 6);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.PoeticGewgaws, (int)PoeticGewgawNames.BeltLegacyOfTheSpida, 1, 6);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.PoeticGewgaws, (int)PoeticGewgawNames.RingLegacyOfTheSpida, 1, 10);
            itemDataLs.Add(itemDataLoot);


            //// GENERIC GEWGAWS
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Folkloric
                                            , (int)GenGewgawNames.SkullBeltOfTheRat, 1, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Epic
                                            , (int)GenGewgawNames.SkullBeltOfTheRat, 1, 6);
            itemDataLs.Add(itemDataLoot);
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

            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Lyric
                      , (int)GenGewgawNames.CamelLeatherBeltOfEvasion, 1, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Folkloric
                                            , (int)GenGewgawNames.CamelLeatherBeltOfEvasion, 1, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Epic
                                            , (int)GenGewgawNames.CamelLeatherBeltOfEvasion, 1, 4);
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
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Lyric
                                            , (int)GenGewgawNames.SkullGloves, 1, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Folkloric
                                           , (int)GenGewgawNames.SkullGloves, 1, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Epic
                                           , (int)GenGewgawNames.SkullGloves, 1, 4);
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
                                 , (int)GenGewgawNames.RingOfTheSerpent, 1, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Epic
                                      , (int)GenGewgawNames.RingOfTheSerpent, 1, 4);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Folkloric
                         , (int)GenGewgawNames.EmeraldNecklaceOfDestiny, 1, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Epic
                                      , (int)GenGewgawNames.EmeraldNecklaceOfDestiny, 1, 4);
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
                          , (int)GenGewgawNames.ShinBoneOfTheDonkey, 1, 10);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Folkloric
                                    , (int)GenGewgawNames.ShinBoneOfTheDonkey, 1, 8);
            itemDataLs.Add(itemDataLoot);
            itemDataLoot = new ItemLootData(ItemType.GenGewgaws, GenGewgawQ.Epic
                                      , (int)GenGewgawNames.ShinBoneOfTheDonkey, 1, 4);
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