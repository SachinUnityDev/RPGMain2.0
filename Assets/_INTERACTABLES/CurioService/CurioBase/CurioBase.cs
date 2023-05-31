using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public abstract class CurioBase
    {
        public abstract CurioNames curioName { get; }
        public QuestMode questMode;

        public List<ItemType> lootTypes = new List<ItemType>();
        public string str1,str2, str3, str4;
        public abstract void InitCurio();        
        public abstract void CurioInteractWithTool();
        public abstract void CurioInteractWithoutTool();
    }
}