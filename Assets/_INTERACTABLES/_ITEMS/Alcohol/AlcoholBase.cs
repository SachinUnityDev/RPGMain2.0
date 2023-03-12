using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Common; 

namespace Interactables
{
    public abstract class AlcoholBase
    {
        public abstract AlcoholNames alcoholName { get; }

        public CharController charController; 
        public abstract void OnDrink();

        public List<string> allDisplayStr = new List<string>();

    }
}