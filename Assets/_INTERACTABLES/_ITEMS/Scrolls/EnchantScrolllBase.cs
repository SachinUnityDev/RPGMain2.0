using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public abstract class EnchantScrollBase
    {
        public abstract ScrollNames scrollName { get; }
        public CharController charController;         
        public ScrollSO scrollSO { get; set; }
        public virtual void ApplyScrollReadFX()
        {
            scrollSO = ItemService.Instance.allItemSO.GetScrollSO(scrollName);
            charController = InvService.Instance.charSelectController;

            ScrollReadData scrollReadData = new ScrollReadData(scrollName, scrollSO.castTime);
            ItemService.Instance.allScrollRead.Add(scrollReadData);

            int expGained = UnityEngine.Random.Range(scrollSO.rechargeExpMin, scrollSO.rechargeExpMax + 1);
            charController.ExpPtsGain(expGained);
        } 
    }

    public abstract class LoreBookBase
    {
        public abstract LoreNames loreName { get; }
        public virtual void ApplyBookReadFx()
        {
           CharController charController = InvService.Instance.charSelectController;
            LoreBookSO loreSO = ItemService.Instance.allItemSO.GetLoreBookSO(loreName);
            int expVal = UnityEngine.Random.Range(loreSO.expGainMin, loreSO.expGainMax + 1);
            charController.ExpPtsGain(expVal);

            // Unlock a Locked Lore Scroll 
            LoreService.Instance.UnLockTheCompleteLore(loreName);
        }
        // if weapon is already is enchanted u have option to recharge only AT 0 
        // other wise its wasted 
    }
    //public interface ILoreScroll
    //{
    //    void ApplyLoreFX(); 

    //}

}
