using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public abstract class SagaicGewgawBase
    {
        public abstract SagaicGewgawNames sagaicGewgawName { get; }        
        public  CharController charController { get; set; }
        public  List<int> buffIndex { get; set; }
        public List<int> expIndex { get; set; }
        public  List<string> displayStrs { get; set; }
        public abstract void GewGawSagaicInit();  // connect the charController and other things
        public abstract void EquipGewgawSagaic();
        public  abstract void UnEquipSagaic();
    }



}
