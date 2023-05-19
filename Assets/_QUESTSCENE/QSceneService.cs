using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public class QSceneService : MonoSingletonGeneric<QSceneService>
    {
        public List<AllQNodeRoomModel> AllQNodeRoomModel = new List<AllQNodeRoomModel>();

        public AllQNodeSO allQNodeSO;

        public QRoomView qRoomView; 


       // public QSceneController QSceneController; 

    }
}