using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public abstract class ToolBase
    {
        public abstract ToolNames toolName { get; }
        public int toolUses { get; set; }

        ToolsSO toolSO; 
        public virtual void ToolInit(ToolsSO toolSO)
        {
            this.toolSO = toolSO;
            toolUses = toolSO.toolMaxUses; 
        }

        public virtual void OnToolUsed()
        {            
            toolUses--; 
            if(toolUses == 0)
            {
                // remove from All item list
            }
        }
     

    }
}

