using Common;
using System.Collections;
using System.Collections.Generic;




namespace Interactables
{
    public abstract class TGBase
    {
        public abstract TGNames tgName { get; }        
       // public abstract void ApplyFXOnSlot();   // to be removed 
        public CharController charController { get; protected set; }
        public List<int> buffIndex { get; set; }
        public List<int> allLandscapeIndex { get; set; }        
        public List<int> allFameIndex { get; set; }
    }

    public interface ITrophyable
    {
        public TavernSlotType tgSlotType { get; }
        public abstract void TrophyInit();
        public abstract void OnTrophyWalled(); 
        public abstract void OnTrophyRemoved();

        
    }
}
