using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public class ObjModel
    {
        public QuestObjNames ObjName;
        public string objNameStr;
        
        public QuestState objState;

        public ObjModel(ObjSO objSO)
        {
           ObjName = objSO.questObj;
           objNameStr = objSO.objName;
         
           objState = objSO.objState; 
        }

    }
}