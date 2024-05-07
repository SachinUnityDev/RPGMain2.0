using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

namespace Common
{
    // -2 Focus	and +3 Haste
    //melee attacker is shocked for 1 rd 
    //melee attacker receives receives 1-7 air dmg   
    //lose 30 earth res 
    //immune to shock
    public class Charged : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Charged;       
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }
        public override float chance { get; set; }
        int thornID = -1; 
        public override void StateApplyFX()
        {
            //int buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
            //    , charID, AttribName.focus, -2, timeFrame, castTime, false);
            //allBuffIds.Add(buffID);

           int buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                  , charID, AttribName.haste, +2, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
               , charID, AttribName.earthRes, -20, timeFrame, castTime, false);
            allBuffIds.Add(buffID);

            int immuneBuffID = charController.charStateController.ApplyImmunityBuff(CauseType.CharState, (int)charStateName
                                            , charID, CharStateName.Shocked, timeFrame, castTime);
            allImmunityBuffs.Add(immuneBuffID);

            if(GameService.Instance.currGameModel.gameScene == GameScene.InCombat)
            thornID = charController.strikeController.AddThornsBuff(DamageType.Air, 1, 9, timeFrame, castTime);

        }
        public override void EndState()
        {
            base.EndState();
            charController.strikeController.RemoveThornsFx(thornID); 
        }
        public override void StateApplyVFX()
        {
            
        }

        public override void StateDisplay()
        {
            str0 = "+2 Haste";
            allStateFxStrs.Add(str0);

            str1 = "-20 Earth Res";
            allStateFxStrs.Add(str1);

            str2 = "Immune to <style=Air>Shocked</style>";
            allStateFxStrs.Add(str2);

            str3 = "Thorn:<style=Air>Shocked</style> 1-7 <style=Air>Air</style> dmg";
            allStateFxStrs.Add(str3);
        }
    }
}
