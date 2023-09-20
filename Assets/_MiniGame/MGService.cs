using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Common
{
    public class MGService : MonoSingletonGeneric<MGService>
    {

        public TrapMGController trapMGController; 



        void Start()
        {
            trapMGController= GetComponent<TrapMGController>();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.M)) 
            { 
                trapMGController.InitGame();
            }
        }


    }

    public enum MGGameState
    {
        PreStart, 
        Start, 
        End, 
    }

}