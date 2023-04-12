using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quest;
using Common; 

namespace Quest
{
    public class QMainController : MonoBehaviour
    {
        
        QMainModel qMainModel;
        void Start()
        {

        }

        public void InitQMainController(QMainSO qMainSO)
        {
            qMainModel = new QMainModel(qMainSO);


        }
        
    }
}