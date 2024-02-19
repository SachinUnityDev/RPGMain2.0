using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{
    public class DayOfLight : DayEventsBase
    {
        public override DayName dayName => DayName.DayOfLight;         
        //5 -10 light res"
        public override void OnDayApply()
        {
            base.OnDayApply();
            int val = UnityEngine.Random.Range(5, 11);
            foreach (CharController c in CharService.Instance.allyInPlayControllers)
            {
                c.timeBuffController.ApplyDayBuff(CauseType.DayEvents, (int)(dayName), c.charModel.charID, dayName
                    , AttribName.lightRes, val, 1, true);
            }        
            string str = $"+{val} Light Res";
            dayModel.daySpecs = str;
        }
    }
}