using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{


    public abstract class ObjBase
    {
        public abstract QuestNames questName { get; }
        public abstract ObjNames objName { get;  }

        public ObjModel objModel; 
        public void InitQuest(ObjModel objModel)
        {
            this.objModel = objModel;                 
        }
        public virtual void ObjStart()
        {
            objModel.OnObjStart();
        }
        public virtual void ObjComplete()
        {
            objModel.OnObjCompleted();
        }

    }
}