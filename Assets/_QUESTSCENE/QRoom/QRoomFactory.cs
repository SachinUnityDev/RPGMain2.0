using Interactables;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;

namespace Quest
{
    [Serializable]
    public class QRoomData
    {
        public int roomNo;
        public QuestNames questName;
        public ObjNames objName;

        public QRoomData(int roomNo, QuestNames questName, ObjNames objName)
        {
            this.roomNo = roomNo;
            this.questName = questName;
            this.objName = objName;
        }
    }
    public class QRoomFactory : MonoBehaviour
    {

        [Header("QRoom Bases")]
        Dictionary<QRoomData, Type> allQRoomBases = new Dictionary<QRoomData, Type>();
        [SerializeField] int qRoomModelCount = 0;
        private void OnEnable()
        {
            QRoomBaseInit();
        }

        void QRoomBaseInit()
        {
            if (allQRoomBases.Count > 0) return;

            var getAllQRoomBases = Assembly.GetAssembly(typeof(QRoomBase)).GetTypes()
                                 .Where(myType => myType.IsClass
                                 && !myType.IsAbstract && myType.IsSubclassOf(typeof(QRoomBase)));

            foreach (var qRoomBase in getAllQRoomBases)
            {
                var t = Activator.CreateInstance(qRoomBase) as QRoomBase;                
                QRoomData qRoomData = new QRoomData(t.roomNo, t.questName, t.ObjNames);
                allQRoomBases.Add(qRoomData, qRoomBase);
            }
            qRoomModelCount = allQRoomBases.Count;
        }

        public QRoomBase GetQRoomBase(QRoomModel qRoomModel)
        {
            QRoomData qRoomData = new QRoomData(qRoomModel.roomNo, qRoomModel.questName, qRoomModel.objName);
            foreach (var qRoomBases in allQRoomBases)
            {
                if (qRoomBases.Key.roomNo == qRoomData.roomNo && qRoomBases.Key.questName == qRoomData.questName &&
                    qRoomBases.Key.objName == qRoomData.objName)
                {
                    var t = Activator.CreateInstance(qRoomBases.Value) as QRoomBase;
                    return t;
                }
            }
            Debug.LogError("Q Room base Not found" + qRoomModel.roomNo + "QuestNames"+ qRoomModel.questName + "ObjNames" + qRoomModel.objName);
            return null;
        }

    }
}