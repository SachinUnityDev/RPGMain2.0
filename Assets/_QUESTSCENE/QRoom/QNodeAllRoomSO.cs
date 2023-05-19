using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{


    [CreateAssetMenu(fileName = "QNodeAllRoomSO", menuName = "Quest/QNodeAllRoomSO")]

    public class QNodeAllRoomSO : ScriptableObject
    {
        public QuestNames questNames;
        public ObjNames objName;
        public Nodes node; 

        public List<QRoomSO> allQRoomSO = new List<QRoomSO>();
        
        public Sprite bGSprite;
        public Sprite fGSprite; 

        //public Sprite GetCurio1(int roomNo)
        //{
        //    int index = allQRoomSO.FindIndex(t=>t.roomNo == roomNo);
        //    if(index == -1)
        //    {
        //        return allQRoomSO[index].curio1;
        //    }
        //    else
        //    {
        //        Debug.Log("Curio 1 not found" + roomNo);
        //        return null; 
        //    }
        //}

        //public Sprite GetCurio2(int roomNo)
        //{
        //    int index = allQRoomSO.FindIndex(t => t.roomNo == roomNo);
        //    if (index == -1)
        //    {
        //        return allQRoomSO[index].curio2;
        //    }
        //    else
        //    {
        //        Debug.Log("Curio 1 not found" + roomNo);
        //        return null;
        //    }
        //}

        public Sprite GetProp(int roomNo)
        {
            int index = allQRoomSO.FindIndex(t => t.roomNo == roomNo);
            if (index == -1)
            {
                return allQRoomSO[index].prop1;
            }
            else
            {
                Debug.Log("room prop 1 not found" + roomNo);
                return null;
            }
        }

        public QRoomSO GetQRoomSO(int roomNo)
        {
            int index = allQRoomSO.FindIndex(t => t.roomNo == roomNo);
            if (index == -1)
            {
                return allQRoomSO[index];
            }
            else
            {
                Debug.Log(" Room SO not found" + roomNo);
                return null;
            }
        }

    }
}