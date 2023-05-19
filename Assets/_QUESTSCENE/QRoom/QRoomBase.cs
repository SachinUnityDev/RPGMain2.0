using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public abstract class QRoomBase
    {   
        public int roomNo { get; }
        public QuestNames questName { get; }
        public ObjNames ObjNames { get; }

        public abstract void Trigger1();
        public abstract void Trigger2();
        public abstract void Trigger3();


    }
}