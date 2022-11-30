using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{

    public class ScrollOfMoss : ScrollBase, iReadScroll
    {
        public override ScrollName scrollName => ScrollName.ScrollOfMoss;
        public void ApplyScrollReadFX()
        {
            scrollSO = ItemService.Instance.GetScrollSO(scrollName);
            charController = ItemService.Instance.selectChar;
            ItemController itemController = charController.gameObject.GetComponent<ItemController>();
            if (itemController != null)
                itemController.OnScrollRead(scrollName);

            int expGained = UnityEngine.Random.Range(scrollSO.rechargeExpMin, scrollSO.rechargeExpMax + 1);
            charController.ExpPtsGain(expGained);
        }
    }
}
