using Common;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;


namespace Quest
{
    public class PathOnDsply
    {
        public QuestNames questNames;
        public ObjNames objName;
        public GameObject pathGO;

        public PathOnDsply(QuestNames questNames, ObjNames objName, GameObject pathGO)
        {
            this.questNames = questNames;
            this.objName = objName;
            this.pathGO = pathGO;
        }
    }

    public class PathController : MonoBehaviour
    {
        [Header("Path View TBR")]
        public PathView pathView;

        public List<PathBase> allPathBase = new List<PathBase>();
        public List<PathModel> allPathModel = new List<PathModel>();

        [Header("Diaplays Unlocked and InComplete Paths")]
        public List<PathOnDsply> allPathOnDsply = new List<PathOnDsply>();    

        public PathFactory pathFactory;

        [Header(" Current data")]
        public PathSO pathSO;
        public PathBase pathBase;
        public PathModel currPathModel;
        public Transform pawnTrans;


        public GameObject pathGO;
        public GameObject pathPrefab; 


        //public InterNodeData interNodeData; 
        
        //public MapExpBasePtrEvents mapExpBasePtrEvents; 

        void Start()
        {
           
        }
        public Transform GetPawnStone()
        {
            return pawnTrans;
        }
        public void InitPath(AllPathSO allPathSO)
        {
            foreach (PathSO pathSO in allPathSO.allPathSO)
            {
                PathModel pathModel = new PathModel(pathSO);
                allPathModel.Add(pathModel);
            }
            InitPathBases();
        }
       
        void InitPathBases()
        {
            pathFactory = GetComponent<PathFactory>();
            foreach (PathModel pathModel in allPathModel)
            {
                PathBase pathbaseNew =
                        pathFactory.GetPathBase(pathModel.questName, pathModel.objName);
             
                if (pathbaseNew != null)
                    pathbaseNew.Init(pathModel);

                allPathBase.Add(pathbaseNew);
            }
        }

        public PathModel GetPathModel(QuestNames questName, ObjNames objName)
        {
            int index = allPathModel.FindIndex(t => t.questName == questName && t.objName == objName); 
                                         
            if (index != -1)
            {
                return allPathModel[index];
            }
            return null;
        }

        public PathBase GetPathBase(QuestNames questName, ObjNames objName)
        {
            int index = allPathBase.FindIndex(t => t.questName == questName && t.objName == objName); 
                                             
            if (index != -1)
            {
                return allPathBase[index];
            }
            return null;
        }

        #region LOCK AND UNLOCK A PATH   

        // Check if it has the limit
        public void On_PathUnLock(QuestNames questName, ObjNames objName)
        {
            PathSO pathSO = MapService.Instance.allPathSO.GetPathSO(questName, objName);
            PathOnDsply pathOnDsply = new PathOnDsply(questName, objName, pathSO.pathPrefab); 
            allPathOnDsply.Add(pathOnDsply);
           
            pathPrefab = pathSO.pathPrefab;
            ShowPath();
        }
        public void On_PathComplete()
        {

        }

        void ShowPath()
        {
            //if (isDiaViewInitDone) return; // return multiple clicks
            Transform parent = pathView.MapPathContainer.transform;
            pathGO = Instantiate(pathPrefab);
            pathGO.transform.SetParent(parent);
            pathView.PathViewInit();
            RectTransform pathRect = pathGO.GetComponent<RectTransform>();
            pathRect.anchorMin = new Vector2(0, 0);
            pathRect.anchorMax = new Vector2(1, 1);
            pathRect.pivot = new Vector2(0.5f, 0.5f);
            pathRect.localScale = Vector3.one;
            pathRect.offsetMin = new Vector2(0, 0); // new Vector2(left, bottom);
            pathRect.offsetMax = new Vector2(0, 0); // new Vector2(-right, -top);
                                                    // 
        }

        # endregion 

    }
}
//public void OnPathEndNodeSelect( MapExpBasePtrEvents mapExpBasePtrEvents, NodeData endNode)
//{
//    //this.endNode= endNode;

//    //this.mapExpBasePtrEvents = mapExpBasePtrEvents;
//    //pathSO = MapService.Instance.allPathSO.GetPathSO(startNode, endNode);

//    //pathModel= GetPathModel(startNode, endNode);
//    //pathBase = GetPathBase(startNode, endNode);

//    //if (pathModel.IsAnyUnCrossedInterNode())
//    //{
//    //    InterNodeData interNodeData = pathModel.GetNextUnCrossedInterNode();
//    //    MapENames mapEName = pathModel.GetMapENameFromInterNodeBasedOnChance(interNodeData);
//    //    //seq


//    ////    EncounterService.Instance.mapEController.ShowMapE(mapEName); 

//    //}

//}