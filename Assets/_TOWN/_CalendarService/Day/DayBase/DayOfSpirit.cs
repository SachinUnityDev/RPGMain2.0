using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{
    public class DayOfSpirit : DayEventsBase
    {
        public override DayName dayName => DayName.DayOfSpirit;
        //5 -10 light res"
        public override void OnDayApply()
        {
            base.OnDayApply();
            foreach (CharController c in CharService.Instance.allyInPlayControllers)
            {
                c.timeBuffController.ApplyDayBuff(CauseType.DayEvents, (int)(dayName), c.charModel.charID, dayName
                    , AttribName.vigor, 1, 1, true);
                c.timeBuffController.ApplyDayBuff(CauseType.DayEvents, (int)(dayName), c.charModel.charID, dayName
                , AttribName.willpower, 1, 1, true);
            }            
            string str = $"+1 Vigor, +1 Wp";
            dayModel.daySpecs = str;
        }
    }
}