using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using UnityEngine;


namespace Quest
{
    public abstract class QuestEbase
    {
        public abstract QuestENames questEName { get; }

        [Header("Quest Mode")]
        public QuestMode questMode;
        public QuestEModel questEModel;
        
        public string choiceAStr = "";
        public string choiceBStr = "";

        public string resultStr = "";
        public string buffstr = "";
        public abstract void QuestEInit(QuestEModel questEModel);
        public abstract void OnChoiceASelect();
        public virtual void OnChoiceBSelect()
        {
            switch (questMode)
            {
                case QuestMode.None:
                    break;
                case QuestMode.Stealth:
                    OnChoiceB_Stealth();
                    break;
                case QuestMode.Exploration:
                    OnChoiceB_Exploration();
                    break;
                case QuestMode.Taunt:
                    OnChoiceB_Taunt();
                    break;
                default:
                    break;
            }
        }
        public abstract void OnChoiceB_Stealth();
        public abstract void OnChoiceB_Exploration();
        public abstract void OnChoiceB_Taunt();
        public abstract void QuestEContinuePressed();
    }
}