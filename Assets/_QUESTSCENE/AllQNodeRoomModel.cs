using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Quest
{
    [Serializable]
    public class QNodeAllRoomModel
    {
        public QuestNames questName; 
        public ObjNames objNames;
        public Nodes node; 
        public List<QRoomModel> allQuestRoomModel = new List<QRoomModel>();

        public QNodeAllRoomModel(QNodeAllRoomSO QNodeAllRoomSO)
        {
            questName = QNodeAllRoomSO.questNames;
            objNames = QNodeAllRoomSO.objName; 
            node = QNodeAllRoomSO.node;

            foreach (QRoomSO roomSO in QNodeAllRoomSO.allQRoomSO)
            {
                QRoomModel QRModel = new QRoomModel(roomSO); 
                allQuestRoomModel.Add(QRModel); 
            }
        }
        public QRoomModel GetQRoomModel(int qRoom)
        {
            int index =
                    allQuestRoomModel.FindIndex(t=>t.roomNo== qRoom);   

            if(index != -1)
                return allQuestRoomModel[index];
            else
                Debug.Log("ROOM MODEL not found" + qRoom);
            return null;   
        }
    }
}