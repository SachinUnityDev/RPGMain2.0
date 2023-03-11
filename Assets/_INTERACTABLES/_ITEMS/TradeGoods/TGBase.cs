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

        public List<string> allDisplayStr = new List<string>();

    }

    public interface ITrophyable
    {
        TavernSlotType tavernSlotType { get; }
        int fameYield { get; }
        abstract void TrophyInit();
        abstract void OnTrophyWalled(); 
        abstract void OnTrophyRemoved();

        
    }
}
