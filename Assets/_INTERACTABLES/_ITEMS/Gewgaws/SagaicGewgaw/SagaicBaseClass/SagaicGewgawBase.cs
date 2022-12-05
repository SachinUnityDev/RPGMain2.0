using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public abstract class SagaicGewgawBase
    {
        public abstract SagaicGewgawNames sagaicgewgawName { get; }
        
        public abstract CharController charController { get; set; }
        public abstract List<int> buffIndex { get; set; }
        public abstract List<string> displayStrs { get; set; }
        public abstract void GewGawInit();  // connect the charController and other things
        public abstract void ApplyGewGawFX(CharController charController);
        public abstract List<string> DisplayStrings();
        public abstract void RemoveFX();
    }



}
