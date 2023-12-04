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
            int addVal =  curr.BronzifyCurrency() + this.BronzifyCurrency();
            Currency addCurr = new Currency(0, addVal); 
            addCurr = addCurr.RationaliseCurrency();
            silver = addCurr.silver;
            bronze = addCurr.bronze; 

        }
        public bool IsMoneySufficent(Currency curr)
        {
            int bronzifyCurr = curr.BronzifyCurrency();
            int bronifyPlayerAccount = this.BronzifyCurrency(); 
            if(bronifyPlayerAccount >= bronzifyCurr)
                return true;
            else
                return false; 
        }
        public bool SubMoney(Currency curr)
        {
            if (!IsMoneySufficent(curr))
                return false; 

            int bronzifyCurr = curr.BronzifyCurrency(); 
            int bronzifyPlayerAccount = this.BronzifyCurrency();
            int finalBronze = bronzifyPlayerAccount- bronzifyCurr;
            Currency finalCurr = new Currency(0, finalBronze); 
            finalCurr= finalCurr.RationaliseCurrency();

            this.silver = finalCurr.silver; 
            this.bronze = finalCurr.bronze;
            return true; 
        }
    }

    public enum ArmorType
    {
        None,
        Leather,
        Hide,
        Brass,
        Cloth,
        Bone,
        Scale,
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
        CraftSlot,
        TradeScrollSlot, 
        TradeSelectSlot,
        PotionActInCombat, 
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
        LoreScroll,

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
        Rum, 
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
        BitterAloe,
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
        SimpleNecklace,
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
        Tablet,
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
        CamelLeatherGlovesOfTheScholar,
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
        AncientTabletOfEarth,  // to be coded
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
        Ancient,
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
        OfEarth,
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
        xxx,
        Fermentor, 
        Mortar,
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







