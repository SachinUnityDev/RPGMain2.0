using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;


namespace Quest
{
    [Serializable]
    public class InterNodeData
    {
        //public NodeTimeData nodeTimeData;
        //public List<QModeChanceData> allToNodeChanceData = new List<QModeChanceData>();
        //public List<QModeChanceData> allFrmNodeChanceData = new List<QModeChanceData>();
        public bool isToChked = false;
        public bool isFrmChked = false;
    }
    public class MapENodePtrEvents : MonoBehaviour
    {
        PathView pathExpView;

        [SerializeField] MapENames mapEName;

        [Header("Node Chance Data")]
        public InterNodeData node;

        [SerializeField] int nodeSeq = 0;
        [SerializeField] PathModel pathModel;
        private void Start()
        {
            nodeSeq = GetComponent<PathNodePtrEvents>().NodeSeq;
        }
        public void InitMapEPtrEvents(PathView pathExpView, PathModel pathModel)
        {
            this.pathExpView = pathExpView;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!node.isToChked)
            {
                node.isToChked = true;
              //  OnMapEEnterTO(node.allToNodeChanceData);
            }
            else
            {
                node.isFrmChked = false;
            }
        }

        //void OnMapEEnterTO(List<QModeChanceData> allchanceData)
        //{
        //    mapEName = GetMapENameAfterChanceChk(allchanceData);
        //    if (mapEName != MapENames.None)
        //    {
        //        EncounterService.Instance.mapEController.ShowMapE(this, mapEName);
        //        MapService.Instance.pathController
        //                    .pawnTrans.GetComponent<PawnStonePtrEvents>().Pause();
        //    }
        //}



        public void OnMapEExit()
        {
            // MapService.Instance.pathController.pathBase.OnEmbarkPressed();
        }

        #region 

        //public MapENames GetMapENameAfterChanceChk(List<QModeChanceData> _allChanceData)
        //{
        //    QuestMode questMode = QuestMissionService.Instance.currQuestMode;
        //    if (questMode == QuestMode.None) return MapENames.None;

        //    List<float> chances = new List<float>();

        //    for (int i = 0; i < node.allToNodeChanceData.Count; i++)
        //    {
        //        if (node.allToNodeChanceData[i].questMode == questMode)
        //        {
        //            for (int j = 0; j < node.allToNodeChanceData[i].chanceData.Count; j++)
        //            {
        //                MapChanceData chanceData = node.allToNodeChanceData[i].chanceData[j];
        //                chances.Add(chanceData.chance);
        //            }
        //            int c = chances.GetChanceFrmList();
        //            return node.allToNodeChanceData[i].chanceData[c].mapEName;
        //        }
        //    }

        //    return MapENames.None;
        //}

        #endregion
    }
}

//  Sequence seq = DOTween.Sequence();
// seq
//// .AppendCallback(() =>
//     //pathExpView.MovePawnStone(interNodeData.nodeTimeData.nodeTrans.position
//     //                                    , interNodeData.nodeTimeData.time))
// .AppendCallback(OnMapEEnter); 

// seq.Play(); 

