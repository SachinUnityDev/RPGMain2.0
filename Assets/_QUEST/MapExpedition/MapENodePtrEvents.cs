using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;


namespace Quest
{


    public class MapENodePtrEvents : MonoBehaviour
    {      
        PathExpView pathExpView;
        [SerializeField] PathSO pathSO;
        public InterNodeData interNodeData;
        [SerializeField] MapENames mapEName; 

        public void InitMapEPtrEvents(PathExpView pathExpView) 
        {
            this.pathExpView = pathExpView;           
        }

        public void OnMapEChecked(InterNodeData interNodeData, MapENames mapEName)
        {
            this.interNodeData= interNodeData;
            this.mapEName= mapEName;
            OnMapEEnter();
          //  Sequence seq = DOTween.Sequence();
           // seq
           //// .AppendCallback(() =>
           //     //pathExpView.MovePawnStone(interNodeData.nodeTimeData.nodeTrans.position
           //     //                                    , interNodeData.nodeTimeData.time))
           // .AppendCallback(OnMapEEnter); 

           // seq.Play(); 

        }
        void OnMapEEnter()
        {
            EncounterService.Instance.mapEController.ShowMapE(this, mapEName);
        }
        public void OnMapEExit()
        {
            MapService.Instance.pathController.pathBase.OnEmbarkPressed();
        }
    }
}