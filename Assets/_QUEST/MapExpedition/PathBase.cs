using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{


    public abstract class PathBase
    {
        public abstract QuestNames questName { get; }
        public abstract ObjNames objName { get; }
        public PathModel pathModel { get; set; }
        public virtual void Init(PathModel pathModel)
        {
            this.pathModel= pathModel;  
        }
        public abstract void OnNode0Enter(); 
        public abstract void OnNode0Exit();
        public abstract void OnNode1Enter();
        public abstract void OnNode1Exit();
        public abstract void OnNode2Enter();
        public abstract void OnNode2Exit();
        public abstract void OnNode3Enter();
        public abstract void OnNode3Exit();
        public abstract void OnNode4Enter();
        public abstract void OnNode4Exit();
        public abstract void OnNode5Enter();
        public abstract void OnNode5Exit();
    }
}