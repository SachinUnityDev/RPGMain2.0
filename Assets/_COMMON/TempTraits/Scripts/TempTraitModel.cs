using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat; 

namespace Common
{
    public class TempTraitModel
    {
        public int charID;
        public CharNames charName;

        [Header("TraitNames....")]
        public int maxArmortriggered;
        public int minArmorTriggered;

        // temp controller 
        // controller is going to check for the start conditions by looping
        // thru the temp trait condition init
    }

    public class CombatModel
    {
        public int CharID;
        public CharNames charName; 

        // STRIKE 
        // critical(LuckCheck()), feeble(LuckCheck()),ExtraAction(MoraleCheck()),LoseAction(MoraleCheck())  
        // MisFire(-veFocusCheck()) // ExtraAction(HasteCheck())// 
        // Killed a Char...minDamage, maxDamage

        //Defense 
        // minArmorTriggered, maxArmorTriggered,Dodge,
        // CharStates to be recorded ... CheatDeath, FirstBlood,lastDropOfBlood, LastBreath
        // Fled in Combat, Die in Combat,Save in Combat(Heal/Guard below 12% HP)  
        //



    }

    public class QuestModel
    {
        public int CharID;
        public CharNames charName;

        // Fled in Quest, Money collected, Other loots Items collected ,
        // net kills ,net saves in Quest,  Experience gained , Trap Data, 
        // Traits gained .....

    }

    
    // Quest .. preparation, walking, curioInteraction, Dialogues/story Interaction
    //  QuestEndOptions, QuestPause, Combat,
    //  

    // Map 


}

