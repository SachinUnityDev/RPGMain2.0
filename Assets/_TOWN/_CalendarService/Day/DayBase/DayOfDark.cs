using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{
    public class DayOfDark : DayEventsBase
    {
        public override DayName dayName => DayName.DayOfDark;
        //        
        //5 -10 dark res"
        public override void OnDayApply()
        {
            base.OnDayApply();
            int val = UnityEngine.Random.Range(5, 11);
            foreach (CharController c in CharService.Instance.allyInPlayControllers)
            {
                c.timeBuffController.ApplyDayBuff(CauseType.DayEvents, (int)(dayName), c.charModel.charID, dayName, AttribName.darkRes,
                    val, 1, true);
            }
            string str = $"{val} Dark Res";
            dayModel.daySpecs =str;
        }
    }
}