using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class ToolService : MonoSingletonGeneric<ToolService>
    {
        public List<ToolsSO> toolSO = new List<ToolsSO>(); 
        public ToolsModel toolsModel; 
        public GameObject toolsPanel;

       
        public ToolsViewController toolsViewController;
        public ToolsController toolsController;

        void Start()
        {

        }


    }



}
