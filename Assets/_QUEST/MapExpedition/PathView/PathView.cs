using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Common;
using Town;
using System;

namespace Quest
{

    public class PathView : MonoBehaviour
    {
        [Header("TBR")]
        public Transform MapPathContainer;
        public PawnMove pawnMove;

        [Header("Global Var")]
        [SerializeField] Transform pawnStone;
        [SerializeField] PathModel pathModel;

        public bool isQInProgress = false; 
        
        public void PathViewInit()
        {
            MapPathContainer = FindObjectOfType<MapPathContainer>(true).transform;
            foreach (Transform node in MapPathContainer)
            {
                PathQView pathQView =  node.GetComponent<PathQView>();  
                if (pathQView != null)
                {
                    pathQView.InitPathQ(this);
                }
            }
        }

        public void OnPathEmbark(QuestNames questName, ObjNames objName, PathQView pathQView)
        {
            pathModel = MapService.Instance.pathController.GetPathModel(questName, objName);            
            if (pathModel != null)
                pawnMove.PawnMoveInit(this, pathQView, pathModel);
        }
        #region PAWN MOVEMENT CONTROL
        public void MovePawnStone(Vector3 pos, float time)
        {
            pawnStone.DOMove(pos, time); 
        }
        
        #endregion


        public void OnPathExit()
        {

        }
    }
}


