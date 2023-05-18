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

        public QuestState act1State;
        public QuestState act2State;
        public QuestState act3State;
        public QuestState act4State;

        public abstract void Act1(); 
        public abstract void Act2();
        public abstract void Act3();
        public abstract void Act4();


    }
}