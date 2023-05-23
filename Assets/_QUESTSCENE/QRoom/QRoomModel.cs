using Quest;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    [Serializable]
    public class QRoomModel
    {
        public QuestNames questNames;
        public ObjNames objName;
        public Nodes node;

        public int roomNo;

        public int upRoomNo = -1;
        public int downRoomNo = -1;

        public bool curio1Chked = false;
        public bool curio2Chked = false;

        public bool trigger1Chked = false;
        public bool trigger2Chked = false;
        public bool trigger3Chked = false;

        public QRoomModel(QRoomSO qRoomSO)
        {
            questNames = qRoomSO.questNames;
            objName = qRoomSO.objName;
            node = qRoomSO.node;
            roomNo = qRoomSO.roomNo;

            upRoomNo = qRoomSO.upRoomNo;
            downRoomNo = qRoomSO.downRoomNo;
        }
    }
}