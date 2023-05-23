using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public class QPreReqView : MonoBehaviour
    {
        QRoomView qRoomView; 
        void Start()
        {
          
        }

        public void InitQPreReqView(QRoomView qRoomView)
        {
            this.qRoomView = qRoomView;
        }
        
  
    }
}