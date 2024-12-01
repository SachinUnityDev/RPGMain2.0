using Interactables;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;

namespace Quest
{
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




        public QRoomBase GetQRoomBase(int roomNo, QuestNames questName, ObjNames objName)
        {
            QRoomData qRoomData = new QRoomData(roomNo, questName, objName);
            foreach (var qRoomBases in allQRoomBases)
            {
                if (qRoomBases.Key == qRoomData)
                {
                    var t = Activator.CreateInstance(qRoomBases.Value) as QRoomBase;
                    return t;
                }
            }
            Debug.LogError("Q Room base Not found" + roomNo+ "QuestNames"+ questName + "ObjNames" + objName);
            return null;
        }

    }
}