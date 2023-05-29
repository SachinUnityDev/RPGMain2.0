using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public abstract class PathBase
    {
            [SerializeField] protected PathModel pathModel;
            protected PathSO pathSO; 
            public abstract NodeData startNode { get; }
            public abstract NodeTimeData endNode { get; }
            
            public Transform endNodeTrans;
            public int currNodeIndex = 0;

            public virtual void OnInitPath(PathModel pathModel, PathSO pathSO)
            {
                this.pathModel = pathModel;
                this.pathSO = pathSO;
            } 
            public abstract void OnEmbarkPressed();
            public abstract void OnEndNodeEnter();
            public abstract void OnEndNodeClicked(Transform endNode); 

    }
}