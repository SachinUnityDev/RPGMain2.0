using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Common;

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
        public QuestNodePtrEvents GetQuestNodePtrEvents(Nodes nodeSelect)
        {
            foreach (Transform child in QuestNodeContainer)
            {
                if (child.GetComponent<QuestNodePtrEvents>().nodeName == nodeSelect)
                     return child.GetComponent<QuestNodePtrEvents>();
            }
            return null; 
        } 

        public void ExitNode()
        {
            if(nodeSelect == Nodes.None) return;
            QuestNodePtrEvents questNodePtrEvents = GetQuestNodePtrEvents(nodeSelect); 
            questNodePtrEvents.QuestMarkUp();

            nodeSelect = Nodes.None; 
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
            ExitNode();// exit prev node 
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