using Combat;
using Common;
using DG.Tweening;
using Intro;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace Quest
{
    public class QRoomService : MonoSingletonGeneric<QRoomService>
    {
        public event Action<QRoomState> OnQRoomStateChg;
        public event Action<QuestNames> OnQRoomStart;
        public event Action<QuestNames> OnQRoomEnd;
        public event Action<QuestNames, int> OnRoomChg;
        public event Action OnInteractComplete; 

        [Header("Quest Room SO")]
        public QNodeAllRoomSO qNodeAllRoomSO; 
        public AllQNodeSO allQNodeSO;
        public QRoomView qRoomView;

        public QRoomState qRoomState;
        public QRoomController qRoomController;
        public QRoomFactory qRoomFactory; 
        // SPRITES HERE NOT IN VIEW AS Its sprite renderer not canvas
        [Header("Sprites")]   
        public SpriteRenderer bgSprite;
        public SpriteRenderer fgSprite;
        public SpriteRenderer curio1; 
        public SpriteRenderer curio2;
        public SpriteRenderer prop;

        [Space(10)]
        [Header("Interactions")]
        public Transform interact1;
        public Transform interact2;
        public Transform interact3;

        [Header("Abbas")]
        public bool canAbbasMove = true;

        [Header(" Quest Name")]
        public QuestNames questName;
        public LandscapeNames landscapeName;
        public Nodes nodes;

        public ServicePath servicePath => ServicePath.QRoomService;

        private void Start()
        {
            GetRef(); 
        }
        void GetRef()
        {
            qRoomController = GetComponent<QRoomController>();
            qRoomFactory = GetComponent<QRoomFactory>();    
        }

        public void On_QRoomStateChg(QRoomState qRoomState)
        {
            this.qRoomState = qRoomState;
            
            OnQRoomStateChg?.Invoke(qRoomState);
        }
        public void On_QRoomSceneStart(QuestNames questName)
        {
            GetRef(); 
            //if(qRoomController.allQNodeAllRoomModel.Count == 0)
            //{
                this.questName = questName;
                CurioService.Instance.InitCurioService();
                InitQRooms(questName);
            // }         
            OnQRoomStart?.Invoke(questName);
        }

        public void On_QuestSceneEnd(QuestNames questName)
        {
            OnQRoomEnd?.Invoke(questName);
        }

        public void InitQRooms(QuestNames questName)   // On 1st room Enter 
        {
            qNodeAllRoomSO = 
                      allQNodeSO.GetQuestSceneSO(questName);
            LandscapeService.Instance.On_LandscapeEnter(qNodeAllRoomSO.landscape); 
            ChangeRoomSprites(questName, 1);
            qRoomController= GetComponent<QRoomController>();   
            qRoomController.InitQRoom(qNodeAllRoomSO);
            On_QRoomStateChg(QRoomState.Prep);
        }
        public void On_RoomChg(QuestNames questName, int roomNo)
        {
            ChangeRoomSprites(questName, roomNo);
            
            OnRoomChg?.Invoke(questName, roomNo); 
        }

        public void ChangeRoomSprites(QuestNames questName, int roomNo)
        {
            QNodeAllRoomSO allRoomSO = 
                             allQNodeSO.GetQuestSceneSO(questName);

            bgSprite.sprite = allRoomSO.bGSprite;
            bgSprite.sortingOrder = 0;
            fgSprite.sortingOrder = 5;
            QRoomSO qRoomSO = 
                        allRoomSO.GetQRoomSO(roomNo);
            prop.sprite = qRoomSO.prop;
            prop.sortingOrder = 3;
            fgSprite.sprite = qRoomSO.fgSprite;

        }
        
        public void On_InteractComplete()
        {
            canAbbasMove = true;
            OnInteractComplete?.Invoke(); 
        }

        public void SaveState()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            ClearState();
            // save all char models

            foreach (QNodeAllRoomModel qNodeRoomModel in qRoomController.allQNodeAllRoomModel)
            {
                string qNodeRoomJSON = JsonUtility.ToJson(qNodeRoomModel);
                string fileName = path + qNodeRoomModel.questName.ToString() + "_" + qNodeRoomModel.questName.ToString() + ".txt";
                File.WriteAllText(fileName, qNodeRoomJSON);
            }
        }

        public void LoadState()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            GetRef();  // controller references
            if (ChkSceneReLoad())
            {
                OnSceneReLoad();
                return;
            }
            List<QNodeAllRoomModel> qNodeAllRoomModel = new List<QNodeAllRoomModel>();
            if (SaveService.Instance.DirectoryExists(path))
            {
                string[] fileNames = Directory.GetFiles(path);
                foreach (string fileName in fileNames)
                {
                    // skip meta files
                    if (fileName.Contains(".meta")) continue;
                    string contents = File.ReadAllText(fileName);
                    Debug.Log("pathModel" + contents);
                    QNodeAllRoomModel QNodeAllRoomModel = JsonUtility.FromJson<QNodeAllRoomModel>(contents);
                    qNodeAllRoomModel.Add(QNodeAllRoomModel);
                }
                qRoomController.allQNodeAllRoomModel = qNodeAllRoomModel.DeepClone();
                qRoomController.LoadQRoomController(); 
            }
            else
            {
                Debug.LogError("Service Directory missing");
            }
        }

        public void ClearState()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            DeleteAllFilesInDirectory(path);
        }

        public bool ChkSceneReLoad()
        {
            return qRoomController.allQNodeAllRoomModel.Count > 0;
        }

        public void OnSceneReLoad()
        {
            Debug.Log(" OnSceneReLoad Q Room Service");
            qRoomController.LoadQRoomController();
        }



        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.E))
        //    {
        //        On_QRoomSceneStart(QuestNames.RatInfestation);
        //    }

        //}
    }
}