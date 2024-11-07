using Common;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;


namespace Quest
{
    public class PathQView : MonoBehaviour // component for all path nodes
    {
        [Header(" TBR")]
        public QuestNames questName;
        public ObjNames objName;

        [Header(" Global Var")]
        public PathView pathView; 

        PathController pathController;
        public PathModel pathModel;   
        

        public void InitPathQ(PathView pathView, PathController pathController)
        {
            this.pathView = pathView;
            this.pathController = pathController;
            pathController.pathQView = this; // adding pathQView reference to pathController            
            pathModel = pathController.GetPathModel(questName, objName);
            if (pathModel.currNode == null)
            {
                pathModel.currNode = new NodeInfo(0, false, false);
            }
            foreach (Transform node in transform)
            {
                PathNodeView pathNodeView = node.GetComponent<PathNodeView>();
                QMarkView qMarkView = node.GetComponent<QMarkView>();
                if (pathNodeView != null)
                {
                    pathNodeView.InitPathNodeView(pathView, this, pathModel);
                }
                if (qMarkView != null)
                {
                    qMarkView.InitPathNodeView(pathView, this, pathModel);
                }
            }
            // init Pawn move here too
            if (pathModel.currNode.nodeSeq != 0)
            {
                MapService.Instance.pathController.pawnTrans.GetComponent<PawnMove>().PawnMoveLoad(pathView, this, pathModel);
            }
        }

        void SpawnPawnOnLastPos()
        {
            PawnMove pawnMove = MapService.Instance.pathController.pawnTrans.GetComponent<PawnMove>(); 

        }

        public void OnNodeEnter(int node)
        {
            pathModel.currNode = pathModel.nodes[node];
            CheckTownArrival(); // check if town is reached
        }
        public void Move2NextNode(bool isSuccess)
        {
            MapService.Instance.pathController.pawnTrans.GetComponent<PawnMove>().Move(pathModel.currNode.nodeSeq);
            MapService.Instance.pathController.UpdatePathNode(isSuccess); 
            OnNodeExit(pathModel.currNode.nodeSeq);
            OnNodeExit4Base(pathModel.currNode.nodeSeq);     
        }
        public void Move2TownFail()
        {
            MapService.Instance.pathController.pawnTrans.GetComponent<PawnMove>().Move2TownOnFail();
            UpdatePathModelOnQFail();
        }

        void CheckTownArrival()
        {
            if (pathModel.nodes[0].isChecked && pathModel.currNode.nodeSeq == 0)
            {
                MapService.Instance.pathController.currPathModel.currNode = pathModel.nodes[0];
                bool isComplete =
                MapService.Instance.pathController.ChkNMarkPathCompletion();
                Debug.Log("IS COMPLETE: " + isComplete);
                pathController.pawnTrans.GetComponent<PawnMove>().FadeOut();    
                MapService.Instance.mapController.mapView.GetComponent<IPanel>().UnLoad();
            }
        }

        void UpdatePathModelOnQFail()
        {
            for (int i = 1; i < pathModel.nodes.Count; i++)
            {
                pathModel.nodes[i].isChecked = false;
            }
            pathModel.nodes[0].isChecked = true;
        }
        void OnNodeExit4Base(int node)
        {

            PathModel pathModel = MapService.Instance.pathController.GetPathModel(questName, objName);
            if (pathModel != null)
                pathModel.nodes[node].isChecked = true;
            PathBase pathBase = MapService.Instance.pathController.GetPathBase(questName, objName);
            switch (node)
            {
                case 0:
                    pathBase.OnNode0Exit();
                    break;
                case 1:
                    pathBase.OnNode1Exit();
                    break;
                case 2:
                    pathBase.OnNode2Exit();
                    break;
                case 3:
                    pathBase.OnNode3Exit();
                    break;
                case 4:
                    pathBase.OnNode4Exit();
                    break;
                case 5:
                    pathBase.OnNode5Exit();
                    break;
                default:
                    break;
            }

        }
        public void SpawnInTown()
        {

        }
        public void MovetoOrgNode()
        {

        }

        public void OnNodeExit(int nodeExit)
        { 
            for (int i = 1; i < transform.childCount ; i++)
            {
                if (transform.GetChild(i).GetComponent<QMarkView>() != null) continue;  
                
                if (i == nodeExit + 1)
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
                if ((nodeExit == transform.childCount - 2)) // -2 bcoz of q Mark // on exit of Node final 
                {
                    transform.GetChild(0).gameObject.SetActive(true);
                }
            }
          
        }
    }
}