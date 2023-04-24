using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{
    public class DayOfFire : DayEventsBase
    {
        public override DayName dayName => DayName.DayOfFire;
//        "Fire
//8-12 fire res" 
        public override void OnDayApply()
        {
            base.OnDayApply();
            int val = UnityEngine.Random.Range(8, 13); 
            foreach (CharController c in CharService.Instance.allyInPlayControllers)
            {
                c.timeBuffController.ApplyDayBuff(CauseType.DayEvents, (int)(dayName), c.charModel.charID, dayName, AttribName.fireRes,
                    val,1, true); 
            }
            string str = $"{val} Fire Res"; 
            dayModel.daySpecs = str;
        }
    }
}