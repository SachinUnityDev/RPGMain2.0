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
        public string resultStr ="";
        public abstract void InitCurio();        
        public abstract void CurioInteractWithTool();
        public abstract void CurioInteractWithoutTool();
    }
}