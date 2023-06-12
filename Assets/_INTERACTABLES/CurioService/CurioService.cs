using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public class CurioService : MonoSingletonGeneric<CurioService>
    {
        public AllCurioSO allCurioSO;
        public CurioController curioController;
        [Header("Curio canvas View")]
        public CurioView curioView;

        [Header("Curio Factory")]
        public CurioFactory curioFactory;

        private void Start()
        {
            curioFactory = GetComponent<CurioFactory>();
            curioController= GetComponent<CurioController>();   
        }
        public void InitCurioService()
        {
            curioController.InitCurioController(allCurioSO); 
        }



    }


}


