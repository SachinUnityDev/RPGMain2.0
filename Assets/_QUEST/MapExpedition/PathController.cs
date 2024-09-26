using Common;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;
using UnityEngine.SceneManagement; 

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

        [Header(" Current Views")]         
        public PathQView pathQView;

        public GameObject pathGO;
        public GameObject pathPrefab;


        private void OnEnable()
        {
            SceneManager.activeSceneChanged += OnSceneLoad;
            pathView = FindObjectOfType<PathView>(true);
        }
        private void OnDisable()
        {
            SceneManager.activeSceneChanged -= OnSceneLoad;
        }
        void OnSceneLoad(Scene oldScene, Scene newScene)
        {
            if (newScene.name == "TOWN")
            {
                pathView = FindObjectOfType<PathView>(true);    
            }
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
       public void LoadPaths(List<PathModel> allPathModels)
        {
            allPathModel = allPathModels.DeepClone();
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
            Debug.LogError("Path Model not found" + questName + objName);
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

        public bool HasPath(QuestNames questName, ObjNames objName)
        {
            PathSO pathSO = MapService.Instance.allPathSO?.GetPathSO(questName, objName);
            if(pathSO != null) return true;
            return false; 
        }

        // Check if it has the limit
        public void On_PathUnLock(QuestNames questName, ObjNames objName)
        {
            PathSO pathSO = MapService.Instance.allPathSO.GetPathSO(questName, objName);
            PathOnDsply pathOnDsply = new PathOnDsply(questName, objName, pathSO.pathPrefab); 
            allPathOnDsply.Add(pathOnDsply);           
            pathPrefab = pathSO.pathPrefab;
            PathModel pathModel = GetPathModel(questName, objName);
            if(pathModel != null)
            {
                pathModel.isDsplyed = true;
                currPathModel = pathModel;
            }
            AddPath2View();
        }
        public void On_PathComplete()  
        {
            // remove prefab using pathView
            // update in PathModel

        }



        void AddPath2View()
        {
            //if (isDiaViewInitDone) return; // return multiple clicks
            Transform parent = pathView.MapPathContainer.transform;
            pathGO = Instantiate(pathPrefab);
            pathGO.transform.SetParent(parent);
            pathView.PathViewInit(this);
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
