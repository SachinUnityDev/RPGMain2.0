using System.Collections;
using System.Collections.Generic;
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
        public void SubMoney(Currency curr)
        {
            silver -= curr.silver;
            bronze -= curr.bronze;
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

    public enum GemName
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

    }
    public enum GemType
    {
        Divine, // enchanted and socketed
        Precious, // only craftable ... precious gem on ring => emerald 
        Support,// only socketed
    }
    public enum ItemType
    {
        Potions,
        GenGewgaws,
        Herbs,
        Foods,
        Fruits,
        Ingredients,
        Recipes,  // list to be created
        Scrolls,// to be added
        TradeGoods, 
        Tools,
        Teas, // to be added
        Soups, // to be added 
        Gems,  
        Alcohol,  // to be deleted 
        Meals,// to be added .. only needed in the camp
    }


    public enum PotionName
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
    }

    public enum FoodNames
    {
        None,
        Venison =1,
        Beef,
        Poultry,
        Fish,
        PieceOfBread,
        FlaskOfWater,

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
    }
    public enum Ingredient  // Can only be ingredient in receipe and cannot be food pr fruits
    {
        None,
        Cardamom =1,
        Cinnamon,
        Garlic,
        Ginger,
        JuniperBerries,
        Onion,
        Pepper,
        Thyme,
        Yeast,
    }
    public enum TGType
    {
        None, 
        Pelt, 
        Trophy,
    }

    public enum TgNames
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
        Lyric,
        Folkloric,
        Epic,
        //Sagaic,
        //Poetic,
        //Relic,
    }

    public enum GenGewgawMidNames
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
        Tiara
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


    }

    public enum SagaicGewgawNames
    {
        None,
        AncientTabletOfEarth,
        BaransLuckyCoin,
        BlessedHands,
        BrassHandProtectors,
        CaptainsPride,
        CowryShellBelt,
        DemonTongue,
        EasyFit,
        GreyWargsHunger,
        HolyKris,
        HomelandsBinder,
        HornsAndBalls,
        Leafwrap,
        LowShouldersGuard,
        MukitisWraps,
        MurabosTosser,
        MwindosCongaScepter,
        PelerineOfTheLastPrince,
        SharpshootersChestBinder,
        SoftAndTenacious,
        SteppeMansBracers,
        TridentsHandle,
        VenomCapsulesSheath,
        WaveTheWaves,
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
        SpidasClutch,
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
    }

    public enum PotionState  // To be discussed with Semih
    {
        None, 
        Equiped, 
        Consumed, 
        Disposed, 
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

    public enum GewgawSlotType    
    {
        None,
        Back,
        Belt,
        Hands,
        Finger,
        Neck,
        Head,
        Misc,
    } // has restrictions for race  and cultures 

    public enum ToolNames
    {
        None,
        Chalice,
        CookingTools,
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







