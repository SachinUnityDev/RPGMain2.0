using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{
    public class DayOfWater :DayEventsBase
    {
        public override DayName dayName => DayName.DayOfWater;
        //        "Water
        //8-12 water  res"
        public override void OnDayApply()
        {
            base.OnDayApply();
            int val = UnityEngine.Random.Range(8, 13);
            foreach (CharController c in CharService.Instance.allyInPlayControllers)
            {
                Debug.Log(c.name + " Char Name");
                c.timeBuffController.ApplyDayBuff(CauseType.DayEvents, (int)(dayName), c.charModel.charID, dayName
                    , AttribName.waterRes, val, 1, true);
            }
            string str = $"{val} Water Res";
            dayModel.daySpecs = str;
        }
    }
}