using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Town
{
    public class BrewView : MonoBehaviour, IPanel
    {
        [Header("slot container")]
        [SerializeField] Transform slotContainer;

      
        private void Awake()
        {
         
        }
        public void Init()
        {
            for (int i = 0; i < slotContainer.childCount; i++)
            {
                slotContainer.GetChild(i).GetComponent<BrewSlotView>().InitBrewSlot((AlcoholNames)(i+1), this);
            }
        }

        public void Load()
        {
            // get all item from values from inv main , stash, and excess 
        }

        public void UnLoad()
        {
            
        }
    }
}