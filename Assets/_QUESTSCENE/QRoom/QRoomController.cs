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
            QSceneService.Instance.
                        ChangeRoomSprites(qRoomModel.questNames, roomNo);

        }


    }
}