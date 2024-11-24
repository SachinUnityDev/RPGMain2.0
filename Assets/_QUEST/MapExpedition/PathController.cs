using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Town;
using UnityEngine;
using UnityEngine.SceneManagement; 

namespace Quest
{
    [Serializable]
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
        public Transform pawnTrans;
        public PathModel currPathModel; 

        [Header(" Current Views")]         
        public PathQView pathQView;

        public GameObject pathGO;
        public GameObject pathPrefab;


        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoad;
            pathView = FindObjectOfType<PathView>(true);
        }
        private void OnDisable()
        {
           // SceneManager.sceneLoaded -= OnSceneLoad;
        }
        void OnSceneLoad(Scene newScene, LoadSceneMode loadSceneMode)
        {
            if (newScene.name == "TOWN")
            {
                pathView = FindObjectOfType<PathView>(true);
                pathView.MapPathContainer = FindObjectOfType<MapPathContainer>(true).transform;
                pawnTrans = FindObjectOfType<PawnMove>(true).transform;
            }
            else
            {
              //  allPathOnDsply.Clear();
            }
        }

        public Transform GetPawnStone()
        {
            return pawnTrans;
        }
        public void InitPath(AllPathSO allPathSO)
        {
            //if(allPathModel.Count > 0)
            //{
            //    ResetAllInCompletePaths();  // no do this on town reached
            //}
            //else
            //{
                foreach (PathSO pathSO in allPathSO.allPathSO)
                {
                    PathModel pathModel = new PathModel(pathSO);
                    allPathModel.Add(pathModel);
                }
            //}
            InitPathBases();
            UpdatePathDsplyed();
        }
        public void LoadPaths()
        {
            allPathOnDsply.Clear();          
            UpdatePathDsplyed();   // will update the pathOnDsply and set the pawn in place
            //if (currPathModel != null)
            //{
            //    if(currPathModel.isDsplyed)
            //    {   
            //       MapEbase mapEBase = EncounterService.Instance.mapEController.GetCurrentDsplyedMapEBase();
            //       EncounterService.Instance.mapEController.ShowMapEResult2(mapEBase); 
            //    }
            //}
            //else
            //{
            //    Debug.Log("!!!!! CURRENT PATH MODEL IS NULL ");  
            //}
        }
        public void LoadPathOnMapOpen()
        {
            UpdatePathDsplyed();   // will update the pathOnDsply and set the pawn in place
        }
        public void NodeResult(bool isSuccess)
        {
            MapService.Instance.pathController.UpdatePathNode(isSuccess);
            if(isSuccess)
            {
                MapService.Instance.pathController.pathQView.Move2NextNode();  
            }
            else
            {
                pathQView.Move2TownFail();
            }
        }
        PathModel GetCurrentPath()
        {
            foreach (PathModel pathModel in allPathModel)
            {
                if (pathModel.isDsplyed && !pathModel.isCompleted)
                {
                    if(pathModel.currNode != pathModel.nodes[0])
                        return pathModel;                      
                }
            }
            return null; 
        }
        public void ResetModel(PathModel pathModel)
        {
            foreach (NodeInfo node in pathModel.nodes)
            {
                node.isChecked = false;
                node.isSuccess = false;
            }
        }
        public bool ChkNMarkPathCompletion()
        {
            foreach (NodeInfo node in currPathModel.nodes)
            {
                if (!node.isSuccess)
                    return false;
            }
            currPathModel.isCompleted = true;
            currPathModel.isDsplyed = false;
            UpdatePathDsplyed();
            QuestMissionService.Instance.On_ObjEnd(currPathModel.questName, currPathModel.objName); 
            return true; 
        }
        void InitPathBases()
        {
            pathFactory = GetComponent<PathFactory>(); allPathBase.Clear(); 
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
                PathModel pathModel =  allPathModel[index];
                return pathModel;
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

        public bool HasPath(QuestNames questName, ObjNames objName)
        {
            PathSO pathSO = MapService.Instance.allPathSO?.GetPathSO(questName, objName);
            if(pathSO != null) return true;
            return false; 
        }

        // Check if it has the limit
        private void UpdatePathDsplyed()
        {
            foreach (PathModel pathModel in allPathModel)
            {
                if (pathModel.isDsplyed)
                {
                    if(ChkIfPathAlreadyDsplyed(pathModel.questName, pathModel.objName))
                        continue; // already added in view
                    //if (allPathOnDsply.Any(t => t.questNames == pathModel.questName && t.objName == pathModel.objName))
                    //    continue; // already added in view

                    PathSO pathSO = MapService.Instance.allPathSO.GetPathSO(pathModel.questName, pathModel.objName);
                    PathOnDsply pathOnDsply = new PathOnDsply(pathModel.questName, pathModel.objName, pathSO.pathPrefab);
                    allPathOnDsply.Add(pathOnDsply);
                    pathPrefab = pathSO.pathPrefab;
                    
                    AddPath2View();
                }
                else
                {
                    RemovePathFrmView(pathModel);
                }
            }

        }    
        void RemovePathFrmView(PathModel pathModel)
        {
            foreach (Transform child in pathView.MapPathContainer.transform)
            {
                PathQView pathQView = child.GetComponent<PathQView>();
                if(pathQView != null)
                {
                    if(pathQView.questName == pathModel.questName && pathQView.objName == pathModel.objName)
                    {
                        allPathOnDsply.Remove(allPathOnDsply.Find(t => t.questNames == pathModel.questName && t.objName == pathModel.objName));
                        Destroy(child.gameObject, 0.1f);
                        break; 
                    }
                }
            }
        }

        public void UpdatePathNode(bool isSuccess)
        {
            currPathModel.currNode.isSuccess = isSuccess;
           // currPathModel.nodes.Find(t => t.nodeSeq == currPathModel.currNode.nodeSeq).isSuccess = isSuccess; // should work without this
        }
        public void On_PathUnLock(QuestNames questName, ObjNames objName)
        {           
            PathModel pathModel = GetPathModel(questName, objName);
            if(pathModel != null)
            {
                pathModel.isDsplyed = true;
                ResetModel(pathModel);
                pathModel.questState = QuestState.UnLocked;
                pathModel.objState = QuestState.UnLocked;
            }            
            UpdatePathDsplyed();           
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
            //if (isLoaded)
                pathView.PathViewInit(this);    //Load for this path
            //else
            //    pathView.PathViewLoaded(this); 

            RectTransform pathRect = pathGO.GetComponent<RectTransform>();
            pathRect.anchorMin = new Vector2(0, 0);
            pathRect.anchorMax = new Vector2(1, 1);
            pathRect.pivot = new Vector2(0.5f, 0.5f);
            pathRect.localScale = Vector3.one;
            pathRect.offsetMin = new Vector2(0, 0); // new Vector2(left, bottom);
            pathRect.offsetMax = new Vector2(0, 0); // new Vector2(-right, -top);
                                                    // 
        }
        bool ChkIfPathAlreadyDsplyed(QuestNames questName, ObjNames objName)
        {
            foreach (Transform child in pathView.MapPathContainer.transform)
            {
              PathQView pathQView = child.GetComponent<PathQView>();    
                if(pathQView != null)
                {
                    if(pathQView.questName == questName && pathQView.objName == objName)
                    {
                        return true; 
                    }
                }
            }
            return false; 
        }
        # endregion 

    }
}
