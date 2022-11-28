using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class ScrollOfLight : ScrollBase, iEnchantmentScroll
    {     
        public override ScrollName scrollName => ScrollName.ScrollOfLight;
        public void ApplyEnchantmenFX()
        {
        }
    }
}