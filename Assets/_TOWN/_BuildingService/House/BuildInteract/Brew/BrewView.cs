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
        [Header("slot container: to be ref")]
        [SerializeField] Transform slotContainer;

      
        private void Awake()
        {
            slotContainer = transform.GetChild(0).GetChild(1);
            gameObject.SetActive(false);
        }
        public void Init()
        {
         
        }

        public void Load()
        {
            for (int i = 0; i < slotContainer.childCount; i++)
            {
                slotContainer.GetChild(i).GetComponent<BrewSlotView>().InitBrewSlot((AlcoholNames)(i + 1), this);
            }
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
        }


    }
}