using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public class QRoomController : MonoBehaviour
    {
        [Header("Quest Room Model")]
        public List<QNodeAllRoomModel> allQNodeAllRoomModel = new List<QNodeAllRoomModel>();
        public int roomNo;
        public QRoomModel qRoomModel;
        public QNodeAllRoomModel allQNodeRoomModel;
       
        public void InitQRoomController(QNodeAllRoomSO qNodeAllRoomSO)
        {
            allQNodeRoomModel = new QNodeAllRoomModel(qNodeAllRoomSO);
            allQNodeAllRoomModel.Add(allQNodeRoomModel);
            roomNo = 1;
            qRoomModel = allQNodeRoomModel.GetQRoomModel(roomNo); 
        }



        public void Move2Room(int roomNo)
        {
            this.roomNo = roomNo;
            qRoomModel = allQNodeRoomModel.GetQRoomModel(roomNo);
            //>>>qRoomModel state set and Invoke QRoom State and then Room Check 
            if (qRoomModel.hasQPrep)
                QRoomService.Instance.On_QuestStateChg(QRoomState.Prep);
            else
                QRoomService.Instance.On_QuestStateChg(QRoomState.Walk);

            QRoomService.Instance.
               On_RoomChg(qRoomModel.questNames, roomNo);
        
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
    }
}