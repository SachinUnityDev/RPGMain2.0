using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class SoftAndTenacious : SagaicGewgawBase
    {
        public override SagaicGewgawNames sagaicgewgawName => SagaicGewgawNames.SoftAndTenacious;

        public override CharController charController { get;set; }
        public override List<int> buffIndex { get; set; }
        public override List<string> displayStrs { get; set; }

        public override void ApplyGewGawFX(CharController charController)
        {
            
        }

        public override List<string> DisplayStrings()
        {
            return null;
        }

        public override void GewGawInit()
        {
            
        }

        public override void RemoveFX()
        {
            throw new System.NotImplementedException();
        }
    }
}

