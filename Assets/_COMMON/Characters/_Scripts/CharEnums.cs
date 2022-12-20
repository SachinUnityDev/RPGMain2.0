using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Combi
{
    None, 
    OR, 
    AND, 
}

public enum CauseType
{
    None, 
    CharSkill, 
    CharState, 
    TempTrait, 
    PermanentTrait, 
    StaminaRegen, 
    HealthRegen,
    LevelUp,
    Items, 
    Potions, 
    GameEvents, 
    Curios, 
    StatChange,
    StatChecks, 
    Gems,
    SuffixGenGewgaw, 
    PrefixGenGewgaw, 
    CriticalStrike,
    FeebleStrike,
    CombatOver, 
    QuestOver, 
    ThornsAttack,
    SagaicGewgaw,
    Herb,
    Food, 
    Fruit,
    PoeticGewgaw, 
    PoeticSetGewgaw, 
    TradeGoods,
}

public enum StatChecks
{
    FocusCheck, 
    AccCheck, 
    LuckCheck, 
    MoraleCheck, 
    FortitudeCheck, 


}


//1 - Items
//2 - Level up - manual bonus
//3 - Level up - auto bonus
//4 - Potions
//5 - Char states
//6 - Traits(temp permanent or sickness)
//7 - Game events(exm: Week of Fighters, Day of Fire...)
//8 - Curios(interactable objects)
//9 - Skills

public enum CharMode
{
    None,
    Ally,
    Enemy,
}
public enum CharStateName
{
    None,
    Ambushed,
    BleedLowDOT,
    BleedMedDOT,
    BleedHighDOT,
    Blinded,
    BurnLowDOT,
    BurnMedDOT,
    BurnHighDOT,
    Buffed,
    Calloused,
    Charged,
    CheatedDeath,
    Concentrated,
    Confused,
    Debuffed,
    Despaired,
    Drunk,
    Energized,
    Enraged,
    Faithful,
    Fearful,
    Feebleminded,
    FirstBlood,
    Frigid,
    FullBlood,
    Guarded,
    Hexed,
    Horrified,
    Inspired,
    Invulnerable,
    Starving,
    LastBreath,
    LastDitch,
    LastDropOfBlood,
    LuckyDuck,
    Lunatic,
    Lightfooted,
    PoisonedLowDOT,
    PoisonedMedDOT,
    PoisonedHighDOT,
    Radiant,
    Rooted,
    SafeSpot,
    Shocked,
    Soaked, 
    Cloaked,
    Unslakable,
    Vanguard,
}
public enum CharStateType
{     
    Positive, 
    Negative, 
}
public enum CharStateClass
{
    None,
    Element_DoT,
    Element_default,
    Utility_pos,
    Utility_neg,
    Darklight_neg,
    MaxRes,
    Positioning,
    HealthBars_full,
    HealthBars_empty,
    Rambo,
    Survival,
    BadHabit,
    External,
    Unstate,
    SkillBased_se,
    Skeletor,
}

public enum CharType
{
    None, 
    CharNames, 
    NPCNames, 

}
public enum CharNames
{
    // HEROES
    None,
    Abbas_Skirmisher,
    Baran,
    Cahyo,
    Ekwueme,
    Isibonakaliso,
    Mawra,
    Rayyan,
    Malik,
    Wafula,
    Battulga,
    // ENEMIES

    BadzeMoonwalker,
    BadzeRuthless,
    Bat,
    BlackLeopard,
    BlackMamba,
    BoudingoTracker,
    BoudingoVanguard,
    CrestedRat,
    DireRat,
    Dragonfly,
    Forestaller,
    Hyena,
    KingScorpio,
    Kongo_mato,
    Leopard,
    Leopardess_pet,
    Lion,
    Lioness,
    Lurcher_pet,
    Obeah,
    RatKing,
    Rhino,
    Spider_pet,
    Spitting_Cobra,
    TikiChieftain,
    TikiDarter,
    TikiShredder,
    TreeSnake,
    Viper,
    Zburator,
    All,
    Abbas_Warden,
    Abbas_Herbalist, 
}

public enum NPCNames
{
    None,
    AdalbertoTheCaptain,
    AmadiTheBlacksmith,
    AmishTheMerchant,
    AzharTheCityGuard,
    BelalTheGambler,
    GadisaTheGovernor,
    GreybrowTheTavernkeeper,
    HornyBoyTheBlacksmith,
    KamilaTheApothecary,
    KhalidTheHealer,
    LeonTheSafekeeper,
    MahmoodTheBrawler,
    MinamiTheSoothsayer,
    NiyaaziTheHaggler,
    OmobolanleTheFruiterer,
    RashidTheLockpicker,
    RosyTheApothecary,
    TahirTheMirrorman,
    VillageElder,

}


public enum StatsName
{
    None,
    health,
    stamina,
    fortitude,
    hunger,
    thirst,

    damage,
    acc,
    focus,
    luck,
    morale,
    haste,

    vigor,
    willpower,

    armor,
    dodge,
    fireRes,
    earthRes,
    waterRes,
    airRes,
    lightRes,
    darkRes,
    staminaRegen,
    hpRegen, 
    fortOrg,     
}

public enum RaceTypeHero  // only for inv segregation 
{
    None,
    Human,
    Elf,
    Dwarf,
    Orc,
    Goblin,
    Beastman,
}


public enum RaceType
{
    None,
    Animal,
    Beastmen,
    Elf,     
    Pygmy,
    Human,
    Undead,
    Dwarf,
    Huwawa, 
    Monster,
    Ogre,
    Orc,
    Goblin, 
    Demon,
    //Cannibals,

}

public enum CultureType
{
    None,
    Abzazulu,
    DarkElf,
    DesertElf,
    Dorian,
    Galtian,
    Galunero,
    Idikummian,
    Kugharian,
    Kujmari,
    Macalaki,
    MeadowDwarf,
    Mngwa,
    OrcOfTheValley,
    Safriman, 
    WindGoblin,
    HalfDead,
    Batling,
    Canine,
    Feline,
    Arachnid,
    Insectoid,
    Reptile,
    Rodent,
    Aqrabulu, 
    BedouinElf,
    Cannibal, 
    Dragonkin,
    Ohto,
    Boudingo,
    Beastblood,
    Ungulate,
    Spirit,   
    Huwawa,
    Fleshly,
    IndigenElf,
}
public enum ArcheType
{
    None,
    Warrior,
    Hunter, 
    Mage,

};


public enum ClassType {

    None,
    Axeman,
    Buccaneer,
    CityGuard,
    Dartblower,
    Exile,
    Nightclaw,
    Skirmisher,
    Thief,
    WaterMage,
    XXX,
    Sparksherald,
    Breezerunner,
    Chaplain,
    Disciple,
    Dunedancer,
    Enchantress,
    Frownbowman,
    Herbalist,
    Jango,
    Javelineer,
    MoonAcolyte,
    MossMage,
    Owler,
    Pyrobender,
    Raider,
    Sanddigger,
    Secudor,
    Warden,
}; 

public enum CharOccupies
{
    Single, 
    Lane, 
    Tris, 
    Diamond, 
    FullHex, 

}




[System.Serializable]
public class CharEnums
{
 
}










