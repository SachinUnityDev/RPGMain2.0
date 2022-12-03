using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class LoreScroll : iReadScroll
    {
        public LoreScroll LoreScrollName { get; }
        public CharController charController;

        public void ApplyScrollReadFX()
        {
            charController = ItemService.Instance.selectChar;
            LoreScrollSO loreSO = ItemService.Instance.loreScrollSO; 
            int expVal = UnityEngine.Random.Range(loreSO.expGainMin, loreSO.expGainMax+1);
            charController.ExpPtsGain(expVal);

            // Unlock a Locked Lore Scroll 
            LoreService.Instance.UnLockRandomSubLore(LoreNames.LandsOfShargad); 
            


        }

        // public void ApplyFX

    }
}
