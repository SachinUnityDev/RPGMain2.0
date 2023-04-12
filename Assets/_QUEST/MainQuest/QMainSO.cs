using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{

    [CreateAssetMenu(fileName = "QMainSO", menuName = "Quest/QMainSO")]

    public class QMainSO : ScriptableObject
    {
        public QMainNames qMainName; 
        public List<QuestObj> objs = new List<QuestObj>();
        public QuestState qMainState;  

    }
}