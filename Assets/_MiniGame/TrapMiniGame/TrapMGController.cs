using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class TrapMGController : MonoBehaviour
    {
        public TrapMGModel trapMGModel;
        public TrapMGSO trapMGSO;
        void Start()
        {
            trapMGModel = new TrapMGModel(trapMGSO); 
        }
        public void InitGame()
        {

        }
        public void OnSuccess()
        {

        }
        public void OnFailed()
        {

        }
    }
}