using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{
    public class DayOfAir : DayEventsBase
    {
        public override DayName dayName => DayName.DayOfAir;
        //                //8-12 Air res"
        public override void OnDayApply()
        {
            base.OnDayApply();
            int val = UnityEngine.Random.Range(8, 13);
            foreach (CharController c in CharService.Instance.allyInPlayControllers)
            {
                c.timeBuffController.ApplyDayBuff(CauseType.DayEvents, (int)(dayName), c.charModel.charID, dayName
                    , AttribName.airRes,val, 1, true);
            }         
            string str = $"+{val} Air Res";
            dayModel.daySpecs = str;
        }
    }
}