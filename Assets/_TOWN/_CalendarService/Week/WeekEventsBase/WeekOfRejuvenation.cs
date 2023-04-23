using Interactables;
using System.Collections;
using System.Collections.Generic;
using Town;
using Unity.Mathematics;
using UnityEngine;



namespace Common
{
    public class WeekOfRejuvenation : WeekEventBase
    {
        public override WeekEventsName weekName => WeekEventsName.WeekOfRejuvenation;

        public override void OnWeekApply()
        {
          //  Potion costs are tripled
          //  Herbs are half cost
          //  +1 hp regen to Mage type heroes at day



        }
    }
}