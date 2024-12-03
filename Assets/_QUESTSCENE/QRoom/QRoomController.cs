using Common;
using System.Collections;
using System.Collections.Generic;
using System.ServiceModel.Dispatcher;
using UnityEngine;


namespace Quest
{
    public class QRoomController : MonoBehaviour 
    {
        [Header("ALL Q NODES Quest Room Model")]
        public List<QNodeAllRoomModel> allQNodeAllRoomModel = new List<QNodeAllRoomModel>();
        
        [Header("Current Q NODES ALL Room Model")]
        public QNodeAllRoomModel allQNodeRoomModel;

        [Header("Current Room Model")]
        public QRoomModel qRoomModel;
        public int roomNo;
        public void InitQRoomController(QNodeAllRoomSO qNodeAllRoomSO)  // On Q Room Init
        {
            if (allQNodeAllRoomModel.Count > 0)
            {
                roomNo = qRoomModel.roomNo;
                Move2Room(roomNo);  
                Debug.Log("QRoomController:InitQRoomController: roomNo: " + roomNo);
            }
            else
            {
                allQNodeRoomModel = new QNodeAllRoomModel(qNodeAllRoomSO);
                allQNodeAllRoomModel.Add(allQNodeRoomModel);
                roomNo = 1;
                qRoomModel = allQNodeRoomModel.GetQRoomModel(roomNo);
                CurioInit(qRoomModel);
                InteractInit(qRoomModel);
                Debug.Log("QRoomController:INIT: roomNo: " + roomNo);
            }
        }

        public void LoadQRoomController()
        {   
            // set up three list 
           // Set up QRoomModel
           // Chg to give room

            CurioInit(qRoomModel);
            InteractInit(qRoomModel);
        }


        public void Move2Room(int roomNo)
        {
            this.roomNo = roomNo;
            qRoomModel = allQNodeRoomModel.GetQRoomModel(roomNo);
            //>>>qRoomModel state set and Invoke QRoom State and then Room Check 
            if (qRoomModel.hasQPrep)
                QRoomService.Instance.On_QRoomStateChg(QRoomState.Prep);
            else
                QRoomService.Instance.On_QRoomStateChg(QRoomState.Walk);

            QRoomService.Instance.On_RoomChg(qRoomModel.questName, roomNo);

            CurioInit(qRoomModel);
            InteractInit(qRoomModel);
            QRoomService.Instance.qRoomView.HideEndArrow(); 
        }
        public bool IsWArrowAvail()
        {
            if (qRoomModel.upRoomNo != -1)
                return true;
            return false;
        }
        public bool IsSArrowAvail()
        {
            if (qRoomModel.downRoomNo != -1)
                return true;
            return false;
        }
        
        public void CurioInit(QRoomModel qRoomModel)
        {

            QRoomService.Instance.curio1.GetComponent<CurioColEvents>().InitCurio(qRoomModel);
            QRoomService.Instance.curio2.GetComponent<CurioColEvents>().InitCurio(qRoomModel);
            CurioService.Instance.curioView.gameObject.SetActive(false); 
        }
        void InteractInit(QRoomModel qRoomModel)
        {

            QRoomService.Instance.interact1.GetComponent<InteractEColEvents>().InitInteract(qRoomModel);

            QRoomService.Instance.interact2.GetComponent<InteractEColEvents>().InitInteract(qRoomModel);
            QRoomService.Instance.interact3.GetComponent<InteractEColEvents>().InitInteract(qRoomModel);
            //CurioService.Instance.curioView.gameObject.SetActive(false);
        }

    }


}