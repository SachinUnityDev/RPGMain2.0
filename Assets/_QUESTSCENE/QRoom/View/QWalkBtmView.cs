using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public class QWalkBtmView : MonoBehaviour
    {
        [SerializeField] QRoomPortView qRoomPortView; 
     

        QRoomView qRoomView;

        [SerializeField] QRoomStatsView qRoomStatsView;
        void Start()
        {
            qRoomPortView = transform.GetChild(0).GetComponent<QRoomPortView>();
            qRoomStatsView = transform.GetChild(1).GetComponent<QRoomStatsView>();            
        }

        public void QWalkInit(QRoomView qRoomView)
        {           
            this.qRoomView = qRoomView;
            qRoomPortView.QPortViewInit(qRoomView);
        }

  

        public void OnCharHovered(CharController charController)
        {
            // show stat panel
            qRoomStatsView.gameObject.SetActive(true);  
            qRoomStatsView.StatViewInit(charController);

        }
        public void OnCharHoverExit(CharController charController)
        {
            // hide stat Panel
            qRoomStatsView.gameObject.SetActive(false);
        }

    }
}