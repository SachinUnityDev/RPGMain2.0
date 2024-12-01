using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public abstract class QRoomBase
    {
        public abstract int roomNo { get; }
        public abstract QuestNames questName { get; } 
        public abstract ObjNames ObjNames { get; }

        public QRoomModel qRoomModel;
        public void Init(QRoomModel qRoomModel)
        {
            this.qRoomModel = qRoomModel;
        }

        public abstract void Trigger1();
        public abstract void Trigger2();
        public abstract void Trigger3();

    }
}