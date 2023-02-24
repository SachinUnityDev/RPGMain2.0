using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Common
{
    public abstract class CharStateBase1 
    {
        public abstract CharStateName CharStateName { get; }
        public CharController charController;

        public virtual void Init(CharController charController)
        {
            this.charController = charController;
        }
        public abstract void OnApply();

        public abstract void OnEnd();
    }

}