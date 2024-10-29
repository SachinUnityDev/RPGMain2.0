using DG.Tweening;
using Quest;
using System;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Quest
{

    public class PathNodeView : MonoBehaviour
    {
        public QuestNames questName;
        public ObjNames objName;
        public int nodeSeq;

        [Header("Global Var")]
        PathView pathView;
        PathQView pathQView;

        PathBase pathBase;
        [SerializeField] PathModel pathModel; 
        int index; 
        public void InitPathNodeView(PathView pathView, PathQView pathQView, PathModel pathModel)
        {
            this.pathView = pathView;
            this.pathQView= pathQView;
            this.pathModel = pathModel; 
            questName = pathQView.questName;
            objName = pathQView.objName;
            nodeSeq = transform.GetSiblingIndex();
        }

        //public void LoadPathNodeView(PathView pathView, PathQView pathQView)
        //{
        //    InitPathNodeView(pathView, pathQView); 
        //}
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "PawnStone")
            {
                if (pathQView.currentNode.nodeSeq == nodeSeq)
                {
                    return;
                }
                pathBase = MapService.Instance.pathController.GetPathBase(questName, objName);
                if (pathBase != null)
                    OnNodeEnter();
                else
                    Debug.Log("Path base not found" + questName + objName);
            }
        }
        void OnNodeEnter()
        {
            pathQView.OnNodeEnter(nodeSeq);
            switch (nodeSeq)
            {
                    case 0:
                    pathBase.OnNode0Enter();                    
                    break;
                    case 1:
                    pathBase.OnNode1Enter(); 
                    break; 
                    case 2:
                    pathBase.OnNode2Enter();
                    break; 
                    case 3:
                    pathBase.OnNode3Enter();
                    break;
                    case 4:
                    pathBase.OnNode4Enter();
                    break;
                    case 5:
                    pathBase.OnNode5Enter();
                    break;
                
                default:
                    break;
            }
        }
       
    }
}

