using Combat;
using Interactables;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{
    [Serializable]
    public class QRoomInteractData
    {
        [Header("QUest Mode Chances")]
        public float stealthChance =100f;
        public float explorationChance = 100f;
        public float tauntChance = 100f;

       
        [Header("Trigger Type")]
        public QuestENames questEName;
        public List<QBarkNames> allBarks = new List<QBarkNames>();  
        public EnemyPack enemyPack;
        public Traps trapNames; 
    }

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

        public QRoomInteractData interact1;
        [Space(10)]
        public QRoomInteractData interact2;
        [Space(10)]
        public QRoomInteractData interact3;

        public Sprite prop;
        public Sprite fgSprite;
        [Header("Q Room map Portrait Cordinates")]
        public Vector2 mapPortCord; 


    }



}