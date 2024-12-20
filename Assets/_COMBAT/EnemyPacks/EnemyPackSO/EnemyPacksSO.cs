﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common; 


namespace Combat
{
    [System.Serializable]
    public class EnemyDataInPack
    {
        public CharNames enemyName;
      //  public int charID;
        public List<int> preferredPos = new List<int>();      
    }
    public enum CombatLootSize
    {
        None, 
        Small, 
        Medium, 
        Large,  
    }


    [CreateAssetMenu(fileName = "EnemyPackSO", menuName = "Combat/EnemyPackSO")]
    public class EnemyPacksSO : ScriptableObject
    {
        public EnemyPackName enemyPack;
        public bool isBossPack = false; 
        public List<EnemyDataInPack> allEnemyDataInPack = new List<EnemyDataInPack>();
        // public Spread loot = new Spread(0, 0);
        public CombatLootSize combatLootSize; 
        public int sharedExp; 
    }

    public enum EnemyPackName
    {
        None,
        RatPack1,
        RatPack2,
        RatPack3,
        BatPack1,
        BatPack2,
        RatNBatPack,
        RatKing,
        LeopardPack1,
        LeopardPack2,
        LeopardPack3,
        LionPack1,
        LionPack2,
        LionPack3,
        HyenaPack,
        BlackPantherPack,
        TikiPack1,
        TikiPack2,
        TikiPack3,
        TikiBossPack,
        BoudingoPack1,
        BoudingoPack2,
        BoudingoPack3,
        GenericSnakesPack,
        GenericScorpionsPack,
        ForestSnakesPack,
        OgrePack,
        ForestbanditPack1,
        ForestBanditPack2,
        ForestBanditPack3,
        BadzePack1,
        BadzePack2,
        BadzePack3,
        ZburatorPack1,
        ZburatorPack2,
        ZburatorPack3,
        PESpider,
        PEBlackMamba,
        PEDragonfly,
        PEleopardess,
        PEblackleopard,
        PEZburator,
         
    }

}

