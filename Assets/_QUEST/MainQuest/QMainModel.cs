using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public class QMainModel
    {

        public QMainNames qMainName;
        public List<QuestObj> objectives = new List<QuestObj>();
        public QuestState qMainState;

        public QMainModel(QMainSO qMainSO)
        {
            qMainName = qMainSO.qMainName;
            objectives = qMainSO.objs.DeepClone();
            qMainState= qMainSO.qMainState; 
        }
    }
}