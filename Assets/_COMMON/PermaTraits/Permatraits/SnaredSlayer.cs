using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat; 

namespace Common
{
    public class SnaredSlayer : PermaTraitBase
    {
        public override PermaTraitName permaTraitName => PermaTraitName.SnaredSlayer;      
        public override void ApplyTrait()
        {
            CombatEventService.Instance.OnSOC += OnSOC; 
            CombatEventService.Instance.OnEOC += OnEOC;
        }

        void OnSOC()
        {
            int index = charController.strikeController.ApplyCharStateDmgAltBuff(+20f, CauseType.PermanentTrait
                , (int)permaTraitName, charID, TimeFrame.Infinity, -1, true, CharStateName.Rooted);
            allCharStateDmgAltBuffIds.Add(index);
        }

        void OnEOC()
        {
            allCharStateDmgAltBuffIds.ForEach(t => charController.strikeController.RemoveDmgAltCharStateBuff(t));
            allCharStateDmgAltBuffIds.Clear(); 
        }
    }




}

