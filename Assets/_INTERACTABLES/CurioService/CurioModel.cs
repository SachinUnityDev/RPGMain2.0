using Interactables;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Quest
{
    [Serializable]
    public class CurioModel
    {
        public CurioNames curioName;
        public ToolNames toolName;
        public ToolNames toolName2;
        [TextArea(2, 5)]
        public string openDesc = "";
        [TextArea(2, 5)]
        public string interactDesc = "";

        public List<Currency> lootMoneyRange = new List<Currency>();

        [TextArea(2, 10)]
        public string curioBark;

        public CurioModel(CurioSO curioSO)
        {
            this.curioName = curioSO.curioName;
            this.toolName = curioSO.toolName;
            this.toolName2 = curioSO.toolName2;
            this.openDesc = curioSO.openDesc;
            this.lootMoneyRange = curioSO.lootMoneyRange.DeepClone();
            this.curioBark = curioSO.curioBark;
        }
    }
}
