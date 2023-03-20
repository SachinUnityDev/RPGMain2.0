using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace Common
{
    public abstract class LandscapeBase
    {
        public abstract LandscapeNames landscapeName { get; }

        public abstract void OnLandscapeInit();        
        public abstract void TrapPositive(); 
        public abstract void TrapNegative();

    }
}