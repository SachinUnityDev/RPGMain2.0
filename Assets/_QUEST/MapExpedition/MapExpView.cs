using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 

namespace Quest
{
    public class MapExpView : MonoBehaviour
    {
        [SerializeField] Transform QuestNodeContainer;
        [SerializeField] Transform pawnStone;

        public Nodes nodeSelect;
        void Start()
        {
            MapExpInit();
        }

        public void MapExpInit()
        {
            foreach (Transform node in QuestNodeContainer)
            {
                node.GetComponent<QuestNodePtrEvents>().InitNodes(this, pawnStone); 
            }
        }

        public void OnNodeClicked(Nodes nodeSelect)
        {
            this.nodeSelect = nodeSelect;           
        }

        public void MovePawnStone()
        {
            foreach (Transform child in QuestNodeContainer)
            {
                if(child.GetComponent<QuestNodePtrEvents>().nodeName == nodeSelect)
                    child.GetComponent<QuestNodePtrEvents>().MovePawnStone();   
            }
        }

    }
}