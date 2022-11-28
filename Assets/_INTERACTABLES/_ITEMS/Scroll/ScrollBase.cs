using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public abstract class ScrollBase
    {
        public abstract ScrollName scrollName { get; }
        public CharController CharController;         
        public ScrollSO scrollSO { get; set; }       
    }

    public interface iEnchantmentScroll
    {
        void ApplyEnchantmenFX();
        // if weapon is already is enchanted u have option to recharge only AT 0 
        // other wise its wasted 
    }
    public interface ILoreScroll
    {
        void ApplyLoreFX(); 

    }

}
