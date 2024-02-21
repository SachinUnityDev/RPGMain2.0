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
    BuildingInterct, 
    NPCInteract, 
    Landscape, 
    ArmorType,
    CityEncounter,
    QuestEncounter, 
    MapEncounter,
    DayEvents, 
    WeekEvents, 
    Loot,
    AttribMinMaxLimit,
    StatMinMaxLimit,
    PassiveSkillName, 
    ActionsPts, 
    
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
    FlatFooted,
    XX3,
    XX4,
    Bleeding,
    Blinded,
    XX1,
    XX2,
    Burning,
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
    Aquaborne,
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
    Lissome,
    XX5,
    XX6,
    Poisoned,
    Radiant,
    Rooted,
    Sneaky,
    Shocked,
    Soaked, 
    Cloaked,
    Unslakable,
    Vanguard,
    XXXX,
}
public enum CharStateBehavior
{     
    Positive, 
    Negative, 
}
public enum CharStateType
{
    None,
    BleedDOT,
    BurnDOT,
    PoisonDOT, 
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
    Abbas,
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
    Kongamato,
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
    //Abbas_Warden,
    //Abbas_Herbalist, 
}
    public enum NPCClassTypes
    {
        None,
        Captain,
        Blacksmith,
        Merchant,
        CityGuard,
        Gambler,
        Governor,
        Tavernkeeper,
        Apothecary,
        Healer,
        Safekeeper,
        Brawler,
        Soothsayer,
        Haggler,
        Fruiterer,
        Lockpicker,
        Mirrorman,
        VillageElder,
    }
public enum NPCNames
{
    None,
    Adalberto,
    Amadi,
    Amish,
    Azhar,
    Belal,
    Gadisa,
    Greybrow,
    HornyBoy,
    Kamila,
    Khalid,
    Leon,
    Mahmood,
    Minami,
    Niyaazi,
    Omobolanle,
    Rashid,
    Rosy,
    Tahir,
    VillageElder,

}

public enum StatName
{
    None,
    health,
    stamina,
    fortitude,
    hunger,
    thirst,
}

public enum AttribName
{
    None,
    dmgMin,
    acc,
    focus,
    luck,
    morale,
    haste,

    vigor,
    willpower,

    armorMin,
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
    armorMax, 
    dmgMax,
    
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
    All, 
}
public enum Archetype
{
    None,
    Warrior,
    Hunter, 
    Mage,

};

public enum CharRole
{
    None, 
    Hero, 
    Companion, 
}

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
    All,
}; 

public enum CharOccupies
{
    Single, 
    Lane, 
    Tris, 
    Diamond, 
    FullHex, 

}
public enum CombatResult
{
    None,
    Victory,
    Draw,
    Defeat,
}




[System.Serializable]
public class CharEnums
{
 
}










