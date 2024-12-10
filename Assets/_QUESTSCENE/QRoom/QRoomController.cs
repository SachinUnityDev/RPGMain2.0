using Common;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.ServiceModel.Dispatcher;
using UnityEngine;


namespace Quest
{
    public class QRoomController : MonoBehaviour 
    {
        [Header("ALL Q NODES Quest Room Model")]
        public List<QNodeAllRoomModel> allQNodeAllRoomModel = new List<QNodeAllRoomModel>();
        
        [Header("Current Q NODES ALL Room Model")]
        public QNodeAllRoomModel allQNodeRoomModel;

        [Header("Current Room Model")]
        public QRoomModel qRoomModel;

        [Header("Current Room Model")]
        public List<QRoomBase> allQRoomBase = new List<QRoomBase>();
        [SerializeField] int qRoomBaseCount = 0; 

        public int roomNo;
        public void InitQRoom(QNodeAllRoomSO qNodeAllRoomSO)  // On Q Room Init
        {
            if (allQNodeAllRoomModel.Count > 0)
            {
               LoadQRoomController();
            }
            else
            {
                allQNodeRoomModel = new QNodeAllRoomModel(qNodeAllRoomSO);
                allQNodeAllRoomModel.Add(allQNodeRoomModel);
                roomNo = 1;
                qRoomModel = allQNodeRoomModel.GetQRoomModel(roomNo);
                CurioInit(qRoomModel);
                InteractInit(qRoomModel);
                Debug.Log("QRoomController:INIT: roomNo: " + roomNo);
                InitAllQRoomBase();
            }
        }
        public void InitQRoom(QNodeAllRoomModel allQNodeRoomModel)
        {
            this.allQNodeRoomModel = allQNodeRoomModel.DeepClone(); 

            InitAllQRoomBase();
        }
        void InitAllQRoomBase()
        {
            allQRoomBase.Clear();   qRoomBaseCount = 0; 
            foreach (QRoomModel qRoomModel in allQNodeRoomModel.allQuestRoomModel)
            {
               
                QRoomBase qRoomBase = QRoomService.Instance.qRoomFactory.GetQRoomBase(qRoomModel);
                qRoomBase.Init(qRoomModel);
                allQRoomBase.Add(qRoomBase);                    
            }
            qRoomBaseCount = allQRoomBase.Count;
        }

        public void LoadQRoomController() // scene loaded , from save, 
        {   
           if(qRoomModel == null)
                qRoomModel = GetCurrQRoomModel();
           if(allQRoomBase.Count == 0)
                InitAllQRoomBase();
            Move2Room(qRoomModel.roomNo);
            CurioInit(qRoomModel);
            InteractInit(qRoomModel);
        }

        public void Move2Room(int roomNo)
        {
            this.roomNo = roomNo;
            qRoomModel.isCurrentRoom = true; 
            qRoomModel = allQNodeRoomModel.GetQRoomModel(roomNo);
            //>>>qRoomModel state set and Invoke QRoom State and then Room Check 
            if (qRoomModel.hasQPrep)
                QRoomService.Instance.On_QRoomStateChg(QRoomState.Prep);
            else
                QRoomService.Instance.On_QRoomStateChg(QRoomState.Walk);

            QRoomService.Instance.On_RoomChg(qRoomModel.questName, roomNo);

            CurioInit(qRoomModel);
            InteractInit(qRoomModel);
            QRoomService.Instance.qRoomView.HideEndArrow(); 
        }
        public bool IsWArrowAvail()
        {
            if (qRoomModel.upRoomNo != -1)
                return true;
            return false;
        }
        public bool IsSArrowAvail()
        {
            if (qRoomModel.downRoomNo != -1)
                return true;
            return false;
        }
        
        public void CurioInit(QRoomModel qRoomModel)
        {

            QRoomService.Instance.curio1.GetComponent<CurioColEvents>().InitCurio(qRoomModel);
            QRoomService.Instance.curio2.GetComponent<CurioColEvents>().InitCurio(qRoomModel);
            CurioService.Instance.curioView.gameObject.SetActive(false); 
        }
        void InteractInit(QRoomModel qRoomModel)
        {

            QRoomService.Instance.interact1.GetComponent<InteractEColEvents>().InitInteract(qRoomModel);

            QRoomService.Instance.interact2.GetComponent<InteractEColEvents>().InitInteract(qRoomModel);
            QRoomService.Instance.interact3.GetComponent<InteractEColEvents>().InitInteract(qRoomModel);
            //CurioService.Instance.curioView.gameObject.SetActive(false);
        }

        #region INIT QROOM MODEL AND BASES

        QRoomModel GetCurrQRoomModel()
        {
            int index = allQNodeRoomModel.allQuestRoomModel.FindIndex(t => t.isCurrentRoom == true);
            if (index != -1)
            {
                return allQNodeRoomModel.allQuestRoomModel[index];
            }
            Debug.Log("Curent Q room not found");
            return null;
        }


       public QRoomBase GetQRoomBase(QRoomModel qRoomModel)
        {
            foreach (var qRoomBase in allQRoomBase)
            {
                if (qRoomBase.qRoomModel == qRoomModel)
                {
                    return qRoomBase;
                }
            }
            Debug.LogError("Q Room base Not found" + qRoomModel.roomNo + "QuestNames" + qRoomModel.questName + "ObjNames" + qRoomModel.objName);
            return null;
        }

        #endregion

        #region GET CURRENT INTERACT AND CURIO  

        // get the current Interact



        #endregion

    }


}