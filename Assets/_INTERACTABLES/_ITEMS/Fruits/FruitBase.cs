using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public abstract class FruitBase
    {
        public abstract FruitNames fruitName { get; }
        public virtual void ApplyHPStaminaRegenFX() { }
        public virtual void ApplyHungerThirstRegenFX() { }
        public virtual void ApplySicknessFX() { } 
        public abstract void ApplyBuffFX();
    }
}