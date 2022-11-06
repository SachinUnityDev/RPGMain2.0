using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class CurioService : MonoSingletonGeneric<CurioService>
    {
        public List<CurioSO> allCurioSO = new List<CurioSO>();
        public List<CurioModel> allCurioModel = new List<CurioModel>();

        public CurioController curioController;
        public CurioViewController curioViewController;
        

    }


}


