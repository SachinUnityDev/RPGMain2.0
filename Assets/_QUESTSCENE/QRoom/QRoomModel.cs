using Combat;
using Quest;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    [Serializable]
    public class QRoomModel
    {
        public QuestNames questNames;
        public ObjNames objName;
        public Nodes node;

        public int roomNo;
        public bool hasQPrep = false;


        public int upRoomNo = -1;
        public int downRoomNo = -1;

        [Header("Curios")]
        public List<CurioNames> allCurio1 = new List<CurioNames>();
        public List<CurioNames> allCurio2 = new List<CurioNames>();

        public bool curio1Chked = false;
        public bool curio2Chked = false;
        public CurioNames curio1Name;
        public CurioNames curio2Name;

        public bool trigger1Chked = false;
        public bool trigger2Chked = false;
        public bool trigger3Chked = false;

        public QRoomModel(QRoomSO qRoomSO)
        {
            questNames = qRoomSO.questNames;
            objName = qRoomSO.objName;
            node = qRoomSO.node;
            roomNo = qRoomSO.roomNo;
            hasQPrep= qRoomSO.hasQPrep; 

            allCurio1 = qRoomSO.allCurio1.DeepClone();
            allCurio2 = qRoomSO.allCurio2.DeepClone();

            upRoomNo = qRoomSO.upRoomNo;
            downRoomNo = qRoomSO.downRoomNo;
        }
        public CurioNames GetCurio1Name()
        {
            if(curio1Name != CurioNames.None) return curio1Name;
            if (allCurio1.Count == 0) return CurioNames.None;
            int index = GetCurioIndex(allCurio1);
            curio1Name = allCurio1[index];
            return curio1Name; 
        }
        public CurioNames GetCurio2Name()
        {
            if (curio2Name != CurioNames.None) return curio2Name;
            if (allCurio2.Count == 0) return CurioNames.None;
            int index = GetCurioIndex(allCurio2);
            curio2Name = allCurio2[index];
            return curio2Name;
        }
        public int GetCurioIndex(List<CurioNames> curioLs)
        {
            if (50f.GetChance())
                return 0; 
            else
                return curioLs.Count-1;
        }
        

    }
}