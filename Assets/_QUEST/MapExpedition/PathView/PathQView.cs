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
            foreach (Transform node in transform)
            {
                PathNodeView pathNodeView = node.GetComponent<PathNodeView>();
                if (pathNodeView != null)
                {
                    pathNodeView.InitPathNodeView(pathView, this);
                }
            }
        }
    }
}