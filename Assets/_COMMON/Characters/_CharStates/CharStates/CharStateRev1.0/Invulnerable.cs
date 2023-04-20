using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DG.Tweening.DOTweenModuleUtils;


namespace Common
{
    public class Invulnerable : CharStatesBase
    {
        //Immune to Magical and Physical dmg(can suffer Pure, Stamina or Fortitude dmgs) -3 Haste
        public override CharStateName charStateName => CharStateName.Invulnerable;     
        public override CharController charController { get; set; }
        public override int charID { get; set; }
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }

        public override void StateApplyFX()
        {
            int buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
            , charID, AttribName.haste, -3, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            // dmg controller magic and physical block
        }

        public override void StateApplyVFX()
        {
            
        }

        public override void StateDisplay()
        {
            str0 = "Immune to Magical and Physical Dmg";
            charStateCardStrs.Add(str0);

            str1 = "-3 Haste";
            charStateCardStrs.Add(str1);
        }
    }
}