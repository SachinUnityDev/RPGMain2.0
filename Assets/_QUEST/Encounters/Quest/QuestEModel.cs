using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    [Serializable]
    public class QuestEModel
    {
        public QuestENames questEName;
        public string questENameStr = "";

        [TextArea(5, 10)]
        public string descTxt;
        public bool isCompleted;

        public QuestEModel(QuestESO questESO)
        { 
            this.questEName = questESO.questEName;
            this.questENameStr = questESO.questENameStr;
            this.descTxt = questESO.descTxt;
            isCompleted = false;
        }
    }
}