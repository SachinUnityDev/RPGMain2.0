using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    [Serializable]
    public class ObjModel
    {
        public ObjNames objName;
        public string objNameStr;
        
        public QuestState objState;

        public ObjModel(ObjSO objSO)
        {
           objName = objSO.objName;
           objNameStr = objSO.objNameStr;
         
           objState = objSO.objState; 
        }

    }
}