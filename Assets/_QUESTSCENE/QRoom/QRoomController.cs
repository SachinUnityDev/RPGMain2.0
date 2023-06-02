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
            
            //Is here bcoz sprites renderers not part of canvas .. can be easily controlled here 
            QSceneService.Instance.
                        On_RoomChg(qRoomModel.questNames, roomNo);
            QSceneService.Instance.qRoomView.HideEndArrow(); 
        }


    }
}