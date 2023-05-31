using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public class CurioService : MonoSingletonGeneric<CurioService>
    {
        public AllCurioSO allCurioSO;
        public List<CurioModel> allCurioModel = new List<CurioModel>();

        public CurioController curioController;
        public CurioView curioView;
        
    }


}


