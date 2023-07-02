using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace Quest
{


    public abstract class QuestObjBase
    {
        public abstract QuestNames qMainNames { get; }
        public abstract ObjNames QObjNames { get;  }

        public QuestState action1State;
        public QuestState action2State;
        public QuestState action3State;
        public QuestState action4State;
        public abstract void Action1(); 
        public abstract void Action2();
        public abstract void Action3();
        public abstract void Action4();


    }
}