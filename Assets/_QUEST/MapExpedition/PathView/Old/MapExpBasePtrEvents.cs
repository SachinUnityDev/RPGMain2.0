using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Town;


namespace Quest
{
    public abstract class MapExpBasePtrEvents : MonoBehaviour   // base class
    {
        protected PathView pathExpView;
       // [SerializeField] protected PathModel pathModel;
      //  protected PathBase pathBase;

       // public abstract NodeData nodeData { get; }
        public virtual void InitQuestNodePtrEvents(PathView pathExpView)
        {
            this.pathExpView = pathExpView;            
        }

        public abstract void OnEndNodeSelect(); 
        //{
        //   // find abbas in path Expview and make it move...
        

        //    //MapService.Instance.pathController.OnPathEndNodeSelect(this, nodeData);
        //    //// Following are copied back from the path controller 
         //   pathModel = MapService.Instance.pathController.pathModel;
        //    //pathBase = MapService.Instance.pathController.pathBase;
        //}
        public abstract void OnNodeInteractCancel();
       
    }




}   