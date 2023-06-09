using Interactables;
using System;
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
        public bool hasQPrep = false; 

        public int upRoomNo = -1;
        public int downRoomNo= -1;

        public List<CurioNames> allCurio1 = new List<CurioNames>();
        public List<CurioNames> allCurio2 = new List<CurioNames>();

        public Sprite prop;
        public Sprite fgSprite;
        [Header("Q Room map Portrait Cordinates")]
        public Vector2 mapPortCord; 


    }



}