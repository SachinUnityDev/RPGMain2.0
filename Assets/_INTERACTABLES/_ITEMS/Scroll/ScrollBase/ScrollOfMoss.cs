using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{

    public class ScrollOfMoss : ScrollBase, iEnchantmentScroll
    {
        public override ScrollName scrollName => ScrollName.ScrollOfMoss;
        public void ApplyEnchantmenFX()
        {
        }
    }
}
