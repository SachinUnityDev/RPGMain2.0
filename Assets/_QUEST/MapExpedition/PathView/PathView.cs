using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Common;
using Town;
using System;
using UnityEngine.Rendering;

namespace Quest
{
    // static path view .. pathQ View is the dynamic path view attached with current quest /obj prefab 
    public class PathView : MonoBehaviour
    {
        [Header("TBR")]
        public Transform MapPathContainer;
        public PawnMove pawnMove;

        [Header("Global Var")]
        [SerializeField] Transform pawnStone;
        [SerializeField] PathModel currPathModel;

        public PathController pathController;


        private void OnEnable()
        {
            MapPathContainer = FindObjectOfType<MapPathContainer>(true).transform;
        }

        public void PathViewInit(PathController pathController)
        {
            this.pathController = pathController;
            MapPathContainer = FindObjectOfType<MapPathContainer>(true).transform;
            foreach (Transform node in MapPathContainer)
            {
                PathQView pathQView =  node.GetComponent<PathQView>();  
                
                if (pathQView != null)
                {
                    pathQView.InitPathQ(this, pathController);
                }
            }
        }
        

        public void OnPathEmbark(QuestNames questName, ObjNames objName, PathQView pathQView)
        {
            currPathModel = MapService.Instance.pathController.GetPathModel(questName, objName);            
            if (currPathModel != null)
                pawnMove.PawnMoveEmbark(this, pathQView, currPathModel);
        }
     
        public void OnPathExit()
        {
            // chk whether quest/obj is successful or not
            // of completed mark it as completed 



        }
    }
}


