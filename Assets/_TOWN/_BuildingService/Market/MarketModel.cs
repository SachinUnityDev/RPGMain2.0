using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{
    public class MarketModel
    {
        [Header("Craft potion")]
        public Iitems healthPotion;
        public Iitems staminaPotion;
        public Iitems fortPotion; 

        public MarketModel(BuildingSO marketSO)
        {

        }


    }
}