using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public class QModeNChoiceStrData
    {
        public QuestMode questMode;
        public string choiceAStr = "";
        public string choiceBStr = "";
    }


    [CreateAssetMenu(fileName = "QuestESO", menuName = "Quest/QuestESO")]
    public class QuestESO : ScriptableObject
    {
        public QuestENames questEName;
        public string questENameStr = "";

        [TextArea(5, 10)]
        public string descTxt;
        public List<QModeNChoiceStrData> choiceData = new List<QModeNChoiceStrData>();

    }
}