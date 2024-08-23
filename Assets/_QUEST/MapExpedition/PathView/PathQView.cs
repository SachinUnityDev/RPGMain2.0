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
        public void InitPathQ(PathView pathView)
        {
           this.pathView = pathView;
            foreach (Transform node in transform)
            {
                PathNodeView pathNodeView = node.GetComponent<PathNodeView>();
                if (pathNodeView != null)
                {
                    pathNodeView.InitPathNodeView(pathView, this);
                    OnNodeExit(0); 
                }
            }
        }

        public void OnNodeExit(int nodeExit)
        {
            for (int i = 0; i < transform.childCount-1 ; i++)
            {
                if (i == nodeExit + 1)
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
            }
            if(nodeExit == transform.childCount - 1)
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}