using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Town
{
    public class BuyDrinksView : MonoBehaviour, IPanel
    {
        [SerializeField] Transform buyDrinksMain;
        [SerializeField] Transform buySelf;
        [SerializeField] Transform buyEveryone;

        [Header("BUY SELF")]
        [SerializeField] Currency currency;
       

        public void Init()
        {
            
        }

        public void Load()
        {
           
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
        }
    }
}