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
            if (charController.charModel.orgCharMode == CharMode.Enemy) return;
            charController.OnStatChg -= SkillAttackChks;
            charController.OnStatChg += SkillAttackChks; 
        }
        void SkillAttackChks(StatModData statModData)
        {
            if (statModData.statModified != StatName.health) return;
            if (statModData.causeType != CauseType.CharSkill) return;
            if (statModData.modVal > 0) return; 
            LastDropChks();
        }
        void LastDropChks()
        {
            chances = new List<float>() { 60f, chance, 100 - (chance + 60) };
            
            if(charController.charStateController.HasCharState(CharStateName.CheatedDeath))
            {
                chances[1] = 0;  // once you cheat death you can't cheat death again other chances to be applied    
            }   
            switch (chances.GetChanceFrmList())
            {
                case 0:
                    charController.ChangeStat(CauseType.CharState, (int)charStateName, charController.charModel.charID
                                                                               , StatName.fortitude, -6f);
                    break;
                case 1:
                    if (GameService.Instance.currGameModel.gameScene == GameScene.COMBAT)
                        charController.damageController.CheatDeath();
                    break;
                case 2:
                    if (GameService.Instance.currGameModel.gameScene == GameScene.COMBAT)
                        CharService.Instance.On_CharDeath(charController, -1);
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
            allStateFxStrs.Add(str0);
            str1 = "May lose Fortitude";
            allStateFxStrs.Add(str1);
            str2 = "May cheat death";
            allStateFxStrs.Add(str2);
            str3 = "May die";
            allStateFxStrs.Add(str3);
        }
        public override void EndState()
        {
            base.EndState();
            charController.OnStatChg -= SkillAttackChks;

        }
    }
}