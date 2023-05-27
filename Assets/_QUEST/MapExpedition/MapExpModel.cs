using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Town;

namespace Quest
{
    public abstract class MapExpBasePtrEvents : MonoBehaviour   // base class
    {
        protected PathExpView pathExpView;
        [SerializeField] protected PathModel pathModel;
        protected PathBase pathBase;

        public abstract NodeData nodeData { get; }
        public virtual void InitQuestNodePtrEvents(PathExpView pathExpView)
        {
            this.pathExpView = pathExpView;            
        }

        public virtual void OnEndNodeSelect()
        {
            MapService.Instance.pathController.OnPathEndNodeSelect(this, nodeData);
            pathModel = MapService.Instance.pathController.pathModel;
            pathBase = MapService.Instance.pathController.pathBase;
        }
        public abstract void OnNodeInteractCancel();
       
    }




}   