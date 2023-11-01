using Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Common
{
    //%12 die	
    //%24 to cheat death(by base once per combat)
    //%64 lose fortitude  chances change with game mode or we can add more Char States instd of tying with difficulty Block DOT dmg(not Clear)

    public class LastDropOfBlood : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.LastDropOfBlood;
        public override StateFor stateFor => StateFor.Heroes;
        public override int castTime { get; protected set; }

        List<float> chances;
        public override float chance { get; set; } = 24f; 
        public override void StateApplyFX()
        {
            chances = new List<float>() { 60f, chance, 100-(chance +60) };
            switch (chances.GetChanceFrmList())
            {
                case 0:
                    charController.ChangeStat(CauseType.CharState, (int)charStateName, charController.charModel.charID
                                                                               , StatName.fortitude, -6f);                    
                    break;
                case 1:
                    charController.damageController.CheatDeath(); 
                    break;
                case 2:
                    CharService.Instance.On_CharDeath(charController);
                    break;
                default:
                    break;
            }

        }

        public override void StateApplyVFX()
        {
            
        }

        public override void StateDisplay()
        {
            str0 = "Upon recieving damage:";
            charStateCardStrs.Add(str0);
            str1 = "May lose Fortitude";
            charStateCardStrs.Add(str1);
            str2 = "May cheat death";
            charStateCardStrs.Add(str2);
            str3 = "May die";
            charStateCardStrs.Add(str3);
        }
    }
}