using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;

namespace Interactables
{
    public class InteractablesEnums
    {
    }

    [System.Serializable]
    public class Currency
    {
        public int silver;
        public int bronze;
        public Currency(int silver, int bronze)
        {
            this.silver = silver;
            this.bronze = bronze;
        }

        public Currency()
        {
            silver = 0;
            bronze = 0; 
        }
        public void AddMoney(Currency curr)
        {
            silver += curr.silver;
            bronze += curr.bronze; 

        }
        public bool IsMoneySufficent(Currency curr)
        {
            int bronzifyCurr = curr.BronzifyCurrency();
            int bronifyStash = this.BronzifyCurrency(); 
            if(bronifyStash >= bronzifyCurr)
                return true;
            else
                return false; 
        }
        public void SubMoney(Currency curr)
        {
            int bronzifyCurr = curr.BronzifyCurrency(); 
            int bronzifyStash = this.BronzifyCurrency();
            int finalBronze = bronzifyStash- bronzifyCurr;
            Currency finalCurr = new Currency(0, finalBronze); 
            finalCurr= finalCurr.RationaliseCurrency();

            this.silver = finalCurr.silver; 
            this.bronze = finalCurr.bronze;       
        }
    }

    public enum ArmorType
    {
        None,
        Leather,
        Hide,
        Brass,
        Cloth,
        Skull,
        ScaleMail,
    }

    public enum WeaponType
    {
        None,
        Steel,
        Wooden,
    }

    public enum GemState
    {
        None, 
        InInv, 
        Enchanted, 
        Socketed,        
    }

    public enum GemNames
    {
        None,
        Carnelian,
        Malachite,
        Meranite,
        Ruri,
        BlueOnyx,
        Oltu,
        Amber,
        Emerald,
        Ametyst,
        Ruby,
        Rutele,
        MossAgate,
        Moonstone,
        Sapphire,
        Topaz,
        Jacinth,
    }

    public enum SlotType
    {
        None, 
        CommonInv, 
        PotionsActiveInv, 
        ProvActiveInv,
        GewgawsActiveInv,
        StashInv,
        ExcessInv,
        EnchantSlot,
        SocketSlot, 
        NPCSlot, 
        TrophySelectSlot, 
        TrophyScrollSlot, 
    }
    public enum GemType
    {
        Divine, // enchanted and socketed
        Precious, // only craftable ... precious gem on ring => emerald 
        Support,// only socketed
    }
    public enum ItemType
    {
        None,
        Potions,
        GenGewgaws,
        Herbs,
        Foods,
        Fruits,
        Ingredients,
        XXX,  // list to be created
        Scrolls,// to be added
        TradeGoods, 
        Tools,
        Teas, // to be added
        Soups, // to be added 
        Gems,  
        Alcohol,  // to be deleted 
        Meals,// to be added .. only needed in the camp
        SagaicGewgaws, 
        PoeticGewgaws,
        RelicGewgaws, 
        Pouches,
        

    }

    public enum RecipeType
    {
        None, 
        Cooking, 
        Crafting, 
        Merging, 
        Brewing, 
    }
    public enum Pouches
    {
        None, 
        HerbalPouch, 
    }

    public enum AlcoholNames
    {
        None, 
        Beer, 
        Cider,
    }
    public enum TeaNames
    {
        None, 
        PurpleTea, 
        ThymeTea, 
        HoneyBushTea, 
        EchinaceaTea,
    }
    public enum SoupNames
    {
        None, 
        LentilSoup, 
        VegetableSoup, 
        MushroomSoup, 
        FishSoup, 
    }
    public enum MealNames
    {
        None,
        BeefSteak, 
        FishStew,
        SeasonedVenison, 
        MuttonStew,
        BeefStew, 
        CookedFish=6, 
        CookedVenison,
        CookedHam,
        CookedMutton,
        RoastedChicken, 
    }

    public enum PotionNames
    {
        None,
        HealthPotion =1,
        StaminaPotion,
        FortitudePotion,
        PotionOfPrecision,
        PotionOfNimbleness,
        ScorpionRepeller,
        SnowLeopardsBreath,
        DemonsPiss,
        PotionOfHeroism,
        PotionOfRabbitblood,
        ElixirOfVigor,
        ElixirOfWillpower,

    }
    public enum HerbNames
    {
        None,
        Aloe =1,
        Buchu,
        BushmansHat,
        DemonsClaw,
        Echinacea,
        Hemp,
        Moss,
        Myrsine,
        PoisonIvy,
        PurpleTeaLeaf,
        TambotiFruit,
        Thyme, 
    }

    public enum FoodNames
    {
        None,
        Venison =1,
        Beef,
        Poultry,
        Fish,
        Bread,
        FlaskOfWater,
        RottenFood,
        Ham, 
        Mutton,
        

    }

    public enum ScrollNames
    {
        None,
        ScrollOfWater, 
        ScrollOfFire,
        ScrollOfAir,
        ScrollOfEarth,
        ScrollOfMoss,
        ScrollOfDark,
        ScrollOfLight,        
    }

    public enum LoreScrollName
    {
        None, 
        LoreScroll,
    }


    public enum FruitNames
    {
        None,
        AssegaiFruit =1,
        Kiwi =2,
        Apple,
        Ube,
        Kiwano,
        Banana,
        Beet,
        Carrot,
        Cucumber,
        Mangosteen,
        Citrus,
    }

    public enum IngredNames  // Can only be ingredient in receipe and cannot be food pr fruits
    {
        None,
        Cardamom =1,
        Cinnamon,
        Garlic,
        Ginger,
        JuniperBerries,
        Onion,
        RedPepper,
        xxxx,
        Yeast,
        SpiderEgg =10,
        BatEar,
        DragonflyWings,
        FelineHeart,
        Hoof, 
        HumanEar, 
        RatFang, 
        RatKingScales, 
        Wheat, 
        Skull,
        WhiteMushroom =20, 
        HyenaEar, 
        Lentils,
    }
    public enum TavernSlotType   // two slots on tavern wall one for pelt and one for the trophy
    {
        None, 
        Pelt, // animal skin hung on the wall 
        Trophy, // head of the animal 
    }

    public enum TGNames
    {
        None,
        CatechuDye,
        DeerSkin,
        DeerTrophy,
        Flour,
        GoldenrodDye,
        GreenBoots,
        GreenVelvet,
        HyenaPelt,
        LionPelt,
        LionessPelt,
        MageRobe,
        NyalaTrophy,
        RoyalPurpleDye,
        ShinyBoots,
        SimpleRing,
        Spice,
        NyalaPelt,
    }
    public enum GenGewgawQ
    {
        None,
        Lyric,
        Folkloric,
        Epic,
        //Sagaic,
        //Poetic,
        //Relic,
    }
    public enum GewgawSlotType
    {
        None,
        Back,
        Belly,
        Hands,
        Finger,
        Neck,
        Head,
        Misc,
    } // has restrictions for race  and cultures 
    public enum GewgawMidNames
    {
        None,
        Amulet,
        Belt,
        Boots,
        Bracers,
        Cloak,
        Gloves,
        Necklace,
        Pauldrons,
        Ring,
        Scarf,
        Shinbone,
        Tiara, 
        ThrowingKnife, 
        RaffiaTrap,
    }

  
    public enum GenGewgawNames
    {
        None,
        GoldenAmuletOfTheLion,
        CamelLeatherBeltOfEvasion,
        BeltOfSerenity,
        BeltOfTheCommoner,
        SkullBeltOfTheRat,
        BeltOfTheSerpent,
        GoldenBelt,
        BootsOfTheCommoner,
        BracersOfTheLeopard,
        BracersOfVersatility,
        CloakOfTheCommoner,
        LeatherhideGlovesOfTheDonkey,
        CamelleatherGlovesOfTheScholar,
        SkullGloves,
        EmeraldNecklaceOfDestiny,
        SilverNecklaceOfWarding,
        PauldronsOfSwiftness,
        BronzePauldrons,
        RubyRingOfMirth,
        RingOfTheSerpent,
        ScarfOfCourage,
        EmeraldTiaraOfSerenity,
        RubyTiaraOfSwiftness,
        RubyRing, 
        EmeraldRing, 
        AmetystRing, 
        HideGlovesOfTheDonkey, // added from here
        JacinthRing,
        SapphireRing,
        ShinBoneOfTheDonkey,
        TopazRing, 
    }

    public enum SagaicGewgawNames
    {
        None,
        HornsAndBalls,
        Spiteeth,
        DemonTongue,
        SoftAndTenacious,
        EasyFit,
        CowryShellBelt,
        FallenShoulder,
        SharpshootersVest,
        BrassHandProtectors,
        AncientTabletOfEarth,
        HolyKris,
        GreyWargsHunger,
    }

    public enum PoeticGewgawsNames
    {
        None,
        SpiderRing,
        SpiderGloves,
        SpiderBelt,
        BracersOfThePoacher,
        CloakOfThePoacher,
        BeltOfThePoacher,
        LonkundosNecklace,
        TKnife,
        RaffiaTrap,
    }

    public enum PoeticSetName
    {
        None,
        LegacyOfTheSpida,
        PoachersToolset,
        FirstHuntersArsenal,
    }

    public enum ItemActions
    {
        None, 
        Equipable, 
        Consumable, 
        Disposable, 
        Sellable, 
        Purchaseable,
        Craftable, 
        Readable,
        Enchantable,
        Rechargeable,
        Socketable,
    }

    public enum PotionState  // To be discussed with Semih
    {
        None, 
        Equiped, 
        Consumed, 
        Disposed, 
    }

    public enum PoeticSetNames
    {
        None, 
        LegacyOfTheSpida,
        PoachersToolset, 
        FirstHuntersArsenal, 
    }
    public enum PoeticGewgawNames
    {
        None,
        RingLegacyOfTheSpida,
        GlovesLegacyOfTheSpida,
        BeltLegacyOfTheSpida,
        BracersPoachersToolset,
        CloakPoachersToolset,
        BeltPoachersToolset,
        NecklaceFirstHuntersArsenal,
        ThrowingKnifeFirstHuntersArsenal,
        RaffiaTrapFirstHuntersArsenal,
    }

    public enum GenGewgawState
    {
        None, 
        Equiped, 
        Disposed, 
    }


    public enum PrefixNames
    {
        None,
        Ametyst,
        Bronze,
        CamelLeather,        
        Emerald,
        Golden,
        Jacinth,
        Hide,
        XXX,
        Ruby,
        Sapphire,
        Silver,
        Skull,
        Topaz,

    }

    public enum SuffixNames
    {
        None,
        OfCourage,
        OfDestiny,
        OfEvasion,
        OfMirth,
        OfPrecision,
        OfSerenity,
        OfSwiftness,
        OfTheCommoner,
        OfTheDonkey,
        OfTheLeopard,
        OfTheLion,
        OfTheRat,
        OfTheSerpent,
        OfTheTiger,
        OfTheWaterBuffalo,
        OfVersatility,
        OfWarding,
        OfEndurance,
        OfTheScholar,
    }

 

    public enum ToolNames
    {
        None,
        Chalice,
        CookingPot,
        Firewood,
        FrostBandage,
        Key,
        PlagueMask,
        MeadHorn,
        Pickaxe,
        Rope,
        Shovel,
        Trap,
        HerbalPouch,
        Fermentor, 
        Mortar,
    }
    

    public enum CurioNames
    {
        None,
        AnimalBones,
        AnimalCarcass,
        Barrel,
        BatNest,
        Bush,
        Cactus,
        Cart,
        Chest,
        Crate,
        Cocoon,
        DuneHole,
        Fountain,
        Hive,
        Lorestone,
        MineVein,
        PileOfBones,
        PileOfThrash,
        RatNest,
        Sack,
        SacrificialAltar,
        ShrineOfRuru,
        TreeTrunk,
        Whetstone,
        Any,
    }

    public enum LoreNames
    {
        None,
        ElementsOfShargad, 
        GodsOfShargad, 
        GuildsOfShargad, 
        HeroesOfShargad,
        HistoryOfShargad,
        LandsOfShargad,
        RacesOfShargad,
        UniverseOfShargad,
    }

    public enum SubLores
    {
        None, 
        Elamia,
        Galsa,
        Galmartu,
        Galunas,
        Gornia,
        Greenhearth,
        Idimyr,
        Kugharia,
        Kujmar,
        Kushima,
        Macanegheri,
        MinorLands,
        Safrima,
        Volevia,       
      
    }
   
}







