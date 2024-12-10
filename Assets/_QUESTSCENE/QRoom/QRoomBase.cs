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

        public virtual void Trigger1()
        {
            qRoomModel.currentInteract = 1;

        }
        public virtual void Trigger2()
        {
            qRoomModel.currentInteract = 2;            
        }
        public virtual void Trigger3()
        {
            qRoomModel.currentInteract = 3;            
        }
        protected void OnPosChked()
        {
            QRoomService.Instance.canAbbasMove = false;
            if(qRoomModel.currentInteract == 1)
                qRoomModel.interact1Chked = true;
            if (qRoomModel.currentInteract == 2)
                qRoomModel.interact2Chked = true;
            if (qRoomModel.currentInteract == 3)
                qRoomModel.interact3Chked = true;
        }
    }
}