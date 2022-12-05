using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;



namespace Interactables
{
    public abstract class TGBase
    {
        public abstract TGNames tgName { get; }        
        public abstract void ApplyFXOnSlot(); 

    }

    public interface ITrophyable
    {
        public TavernSlotType tgSlotType { get; }

    }
}
