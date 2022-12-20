using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public abstract class QuestBase
    {
        public QuestNames questName { get; }

        public abstract void ApplyInteraction1();
        public abstract void ApplyInteraction2();
        public abstract void ApplyInteraction3();
        public abstract void ApplyInteraction4();

    }


}

