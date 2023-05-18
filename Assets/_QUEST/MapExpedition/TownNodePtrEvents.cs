using Common;
using Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Quest
{


    public class TownNodePtrEvents : MapExpBasePtrEvents, IPointerClickHandler
    {
        [SerializeField] LocationName locName; 
        public override NodeData nodeData => new NodeData(locName); 

      
        public void OnPointerClick(PointerEventData eventData)
        {

        }

        public override void OnNodeExit()
        {
            
        }

        public override void OnNodeEnter()
        {
            
        }
    }
}