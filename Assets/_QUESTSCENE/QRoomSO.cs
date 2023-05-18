using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Quest
{
    [CreateAssetMenu(fileName = "QRoomSO", menuName = "Quest/QRoomSO")]

    public class QRoomSO : ScriptableObject
    {
        public QuestNames questNames;
        public ObjNames objName;
        public Nodes node; 

        public int roomNo;

        public int upRoomNo = -1;
        public int downRoomNo= -1;

        public Sprite curio1; 
        public Sprite curio2;

        public Sprite prop1;

    }



}