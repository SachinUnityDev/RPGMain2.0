using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class CombatEnums : MonoBehaviour
    {

    }


    public enum CombatState
    {
        None, 
        INDialogue, 
        INTactics,
        INCombat_normal,       
        INCombat_InSkillSelected,
        INPostCombat,
        INCombat_Pause,
        INCombat_Loot,
        INCombat_End,
    }

    public enum AttackType
    {
        None, 
        Melee, 
        Ranged,
        Remote,
    }

    public enum DamageType
    {
        None,
        Physical,
        StaminaDmg,
        Air,   // magical skills 
        Water,
        Earth, 
        Fire, 
        Light, 
        Dark, 
        Pure,        // not stopped by any armor or resistance no chance for dodge , mishits
        Blank1,
        Blank2,
        Blank3, 
        Heal,
        FortitudeDmg, 
        DOT,
        Move,
        
       // HealthDmg,
    }

    public enum SkillTypeCombat
    {
        None,
        Skill1,
        Skill2,
        Skill3,
        Ulti,        
        Patience,
        Move, 
        Weapon, 
        Uzu,
        Retaliate,
        Passive, 
     //   Remote,
    }

    public enum StrikeTargetNos
    {
        None, 
        Single, 
        Multiple, 
    }
    public enum SkillLvl
    {
        Level0,
        Level1, 
        Level2, 
        Level3,  
    }

    [System.Serializable]
    public enum PerkType
    {
        None,
        A1,
        B1,
        A2,
        B2,
        A3,
        B3,
    }

    public enum RelationShip  // for perk realtionship can be used elsewhere too .. 
    {        
        None,
        PreReq,
        PreReqNOT, 
    }

     public enum SkillInclination
     {
        None, 
        Physical, 
        Magical,
        Pure,        // not stopped by any armor or resistance
        Buff,
        Guard,
        Heal,
        Patience,
        Passive,
        Debuff,
        Move,
     }

    public enum StrikeType
    {
        None,
        Feeble,// crit and feeble for both magical and Physical
        Crit,//
        Normal,
        Dodged,// is only for physical attack 
        
    }

    public enum StaticTargetMode
    {
        Front, 
        Mid, 
        Back, 
    }

    public enum DynaTargetMode
    {
        First, 
        Second, 
        Third,
    }

    // GRID ENUMS 
    public enum SelectionState
    {
        Normal,
        Hover,  // on mouse hover tile , Dyna is Highlighted 
        AutoSelect,  //  turn 
        Target,
    }

 

    /*SKILLS ENUMS */
    public enum SkillNames 
    {
        None,
        DefaultPatience, 
        DefaultMove, 
        WristSpin,	
        KrisLunge,
        HoneBlades,
        IntimidatingShout,
        InTheNameOfUsmu,
        Cleave,
        EarthCracker,
        HeadToss,
        IgnorePain,
        NoPatience,
        SplitEarth,
        CleansingWater,
        TidalWaves,
        ColdTouch,
        FistOfWater,
        Tsunami,   
        Scratch,
        RatBite,
        GuardWeak,
        Expectorate,
        PoisonUp,
        RunAway,
        Sweep,  // rat king 
        Intimidate,
        RottenKing,
        HatchetSwing,
        RunguThrow,
        AnimalTrap,
        FeignDeath,
        HideInTheBushes,
        SoulOfTheTamboti,
        WaterShell,
        SuckItUp,  // bats
        Echo,
        UpsideDown,
        LazyPaw,        // lion
        Overpower,
        Roar,
        FastClaw, // lioness
        FelineRush,
        CallForHunt,
        CanineClaw, // Hyena
        JawOfSteel,
        SniffingBlood,
        SpearStab, // Forestaller
        SpearSweep,
        GuardLurcher,
        LongShot,  // lurcher
        ShootTheLeg,
        BanditTrap, 
        SpidaScratch, // spider
        WebbySpit,
        SelfDefense,
        Respire, // equivalent to patience
        Telekinesis,//uzu abbas       
        Retaliate,         


    }


    public enum PassiveSkillName
    {
        None,
        Arachnid,
        Avenger,
        Bandit,
        BehindMilady,
        BirdOfPrey,
        Boss1,
        BullyGang,
        Canine,
        CanineBlood,
        Cannibal,
        CaveLurker,
        Caveman,
        Demon,
        Devastator,
        DivineCreature,
        DreamAnimal,
        EarthChoker,
        FearOfVenom,
        Feline,
        FierceFemale,
        FireShield,
        GhostCat,
        HealingMist,
        HellgateMaster,
        HornyEnd,
        HumanPride,
        Huwawa,
        Intelligent,
        KillerInstinct,
        LodestoneWall,
        Long,
        MadLuck,
        MatingTime,
        Moonwalk,
        MysteryCat,
        NightRunner,
        Nocturnal,
        Ogre,
        Pacer,
        PillarOfFortune,
        Riposte,
        Rodent,
        Scary,
        ScorpioFear,
        SenseTheWeak,
        Serpent,
        Shapeshifter,
        SlowPoison,
        SluggerTracker,
        SmokeChoker,
        TheMoreTheDeadlier,
        Thorns,
        ToxicSkin,
        Triad,
        Undead,
        Undying,
        Vermin,
        WildInTheNight,
        Boss2, 
        Boss3, 
        Boss4,
        Impaler,
        Opportunist,



    }

    public enum PatienceNMoveSkillNames
    {
        None,
        HideInTheBushes,
        HideInTheNight,
        InTheNameOfUsmu,
        NoPatience,
        WarRush,
    }

    public enum CampingSkillsNames
    {
        None,
        BeerLikeWater,
        BodyWater,
        BreakTheChains,
        Dreamer,
        EyeOfTheNight,
        FrighteningWarrior,
        HealWithPain,
        MercyOfTheWhiteTiger,
        PrayforUsmu,
        PrudentUse,
        RedeOfTheAncestors,
        SetTrap,
        ToughAsDeer,
        WhetTheAxe,

    }

    public enum PerkNames
    {
        None, 
        AimTheKnee,
        AimTheWindpipe,
        AintNoDartWithNoPoison,
        AllAtOnce,
        AnimalHunter,
        AwukhoMoya,
        BackupKit,
        BadLuckInTheShip,
        BlackKpinga,
        BladeSlap,
        BlindingSparks,
        BleedItOn,
        Blindeye,
        BloodyAxe,
        BraveMansRetortion,
        BreakTheirBalance,
        Breathtaker,
        BrokenHorn,
        BuffaloCharge,
        CrackTheNose,
        CagedInWeb,
        CanReachFar,
        ICheatDeath,
        CleverCleave,
        ColderThanBlade,
        ComeHere,
        ConcentratedFist,
        ConfuseThem,
        DoubleSwing,
        CrazyWaves,
        Cutpick,
        DarkMark,
        DartGivesCourage,
        DartHealsUp,
        DartSwellsEgo,
        DebilitatingJump,
        DeepCut,
        DeeperSlice,
        Demoralize,
        DontLeaveBlank,
        DontMindTheWaves,
        EarthSensitive,
        EdgyAxe,
        EdgyHatchet,
        EnemyOfFire,
        EnjoyTheTrap,
        ExhaustTarget,
        ExtendingHook,
        ExterminateTheAnemic,
        ExterminateTheBackline,
        FartherDarter,
        FasterThanEyeCanSee,
        FastForward,
        FeastOfWaves,
        FeelAndReact,
        FeelOfImpact,
        FindTheWeakSpot,
        FinishingTouch,
        UnLimitedAmo,
        FoulFour,
        FreeMove,
        Frigidify,
        FringesOfIce,
        FuryOfPain,
        GloriousSlash,
        GravityForce,
        GrindUp,
        HardHandle,
        Harshpull,
        HatTrick,
        HeadOrButt,
        HeadShot,
        HealthyShell,
        HealthySplash,
        HealthyWater,
        HelpThereIsThief,
        HexyBeast,
        HighSparks,
        HookHarder,
        HowLuckyMe,
        HuntingSeason,
        HurlForward,
        ImpactfulWater,
        Inspire,
        InspiringTouch,
        IWillBleedToDeath,
        KillTheSnared,
        KrisPoke,
        Laceterate,
        LeapAndMove,
        LeapAndTear,
        LeapFirst,
        Leapphard,
        LeapToSlice,
        LightWeb,
        LuckySparks,
        MakeItDouble,
        MeSoloYouAll,
        MoveYourFeet,
        MurkyWaters,
        NatureIsTheirEnemy,
        NoBleedNoLuck,
        NoMagicToday,
        NoMagicTolerance,
        NoMatterWhoYouAre,
        NoMercy,
        ShootTheCripple,
        NoMercyForVenomous,
        NoMercyToEntangled,
        NooneCanReveal,
        UnBroken,
        NoPityForTheWeak,
        NotAfraidOfDeath,
        NotSeen,
        NotTired,
        NoWallBehind,
        OneBigClaw,
        OutOfNowhere,
        Payback,
        Piercedent,
        PiercingCold,
        PiercingHorn,
        PiercingKnife,
        PoacherAssassin,
        PoisonAtTheTip,
        PoisonTrap,
        PoorGuyBehind,
        Posseizedem,
        PressurizedWater,
        Pusher,
        QuickRoot,
        QuickToReact,
        ReleaseOfFate,
        RestInCombat,
        ReturnedRegards,
        RevealTheBackline,
        Reverb,
        RidingTheWaves,
        SafeStealth,
        ShalulusMercyAndGrace,
        ShieldBash,
        SlapOfWater,
        SliceThigh,
        SlideAndSlice,
        SlipperyFloor,
        SlitOpen,
        Sluggerungu,
        SmartSnakes,
        SmellsDeadFish,
        SmellsPoopofFear,
        SoftSpot,
        SplintersOfEarth,
        SpreadsFromTheCenter,
        Stab3Get1ForFree,
        StabAndCarve,
        StayWhereYouAre,
        StoneWall,
        StrengthOfTheEarth,
        SuffocatingWeb,
        SweepingWaves,
        TargetArtery,
        TearItDown,
        TearTheirMind,
        ThisIsMyDay,
        ThreeAtOnce,
        ThrowAndPick,
        TireDown,
        TooFastToSee,
        TooHighBleed,
        Trihook,
        UnfateThem,
        Uninterrupted,
        UnlimitedEvasion,
        Unstoppapoon,
        VenomShock,
        WashMeNicely,
        WaterBuffalo,
        WebIsWet,
        WebTide,
        Whirldance,
        WhoStoleMyMoney,
        WideSwing,
        CutTheKnee,
        Flextrap,
        FlayTheSkin,
        TrapOfTheWild,
        Surprise,
        TheHunter,
        TheRuthless,
        TheTrickster,
        ThePuller,
        TheTwister,
        TheEntangler,
    }




}


