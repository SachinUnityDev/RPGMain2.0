using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class ScrollOfAir : ScrollBase ,iEnchantmentScroll
    {
        public override ScrollName scrollName => ScrollName.ScrollOfAir;
       
        public void ApplyEnchantmenFX()
        {
            scrollSO = ItemService.Instance.GetScrollSO(scrollName);
            charController = ItemService.Instance.selectChar; 




        }
    }
}