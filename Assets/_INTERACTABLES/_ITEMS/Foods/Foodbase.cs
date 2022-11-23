using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public abstract class Foodbase
    {
        public abstract FoodNames foodName { get; }
        public abstract void ApplyFX1(); 
        public abstract void ApplyFX2();
    }
}

