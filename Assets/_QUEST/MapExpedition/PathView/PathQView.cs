using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


namespace Quest
{
    public class PathQView : MonoBehaviour
    {
        [Header(" TBR")]
        public QuestNames questName;
        public ObjNames objName;

        [Header(" Global Var")]
        public PathView pathView; 
        public List<PathNodeView> allPathNodes = new List<PathNodeView>();

        PathController pathController;
        public int currentNode = -1; 
        public void InitPathQ(PathView pathView, PathController pathController)
        {
            this.pathView = pathView;
            this.pathController = pathController; 
            pathController.pathQView = this; // adding pathQView reference to pathController

            foreach (Transform node in transform)
            {
                PathNodeView pathNodeView = node.GetComponent<PathNodeView>();
                QMarkView qMarkView = node.GetComponent<QMarkView>();   
                if (pathNodeView != null)
                {
                    pathNodeView.InitPathNodeView(pathView, this);
                    currentNode = 0;
                    OnNodeExit(0); 
                }
                if(qMarkView != null)
                {
                    qMarkView.InitPathNodeView(pathView, this);
                }
            }
        }
        public void OnNodeEnter(int node)
        {
            currentNode = node; // track which node is currently active 
        }
        public void Move2NextNode()
        {
            
        }        
        public void SpawnInTown()
        {

        }
        public void MovetoOrgNode()
        {

        }

        public void OnNodeExit(int nodeExit)
        {
            
            for (int i = 0; i < transform.childCount ; i++)
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
            }
            if ((nodeExit == transform.childCount - 2)) // -2 bcoz of q Mark // on exit of Node final 
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}