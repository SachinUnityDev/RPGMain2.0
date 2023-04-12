using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{


    public abstract class QuestBase
    {
        public abstract QMainNames qMainNames { get; }

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