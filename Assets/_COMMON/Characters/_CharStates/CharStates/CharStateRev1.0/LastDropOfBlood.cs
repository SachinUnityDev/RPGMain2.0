using Combat;
using Common;
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
        public override CharController charController { get; set; }
        public override int charID { get; set; }
        public override StateFor stateFor => StateFor.Heroes;
        public override int castTime { get; protected set; }

        List<float> chances;
        public override float chance { get; set; } = 24f; 
        public override void StateApplyFX()
        {
            chances = new List<float>() { 12f, chance, 64f };
            switch (chances.GetChanceFrmList())
            {
                case 0:
                    CharService.Instance.On_CharDeath(charController);              
                    break;
                case 1:
                    charController.damageController.CheatDeath(); 
                    break;
                case 2:
                    charController.ChangeStat(CauseType.CharState, (int)charStateName, charController.charModel.charID
                                                                               , StatName.fortitude, -6f);
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
            
        }
    }
}