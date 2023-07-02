using Common;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Town;
using UnityEngine;
using UnityEngine.UI;

namespace Quest
{


    public class PawnStonePtrEvents : MonoBehaviour
    {
        //PathExpView mapExpView;
        //[Header("Current Node Data")]
        //public NodeData nodeData;

        [Header(" Pawn Path definition")]
        [SerializeField] int index;
        [SerializeField] List<PathData> allNodes = new List<PathData>();
        Sequence movePath;
        private void Start()
        {
            movePath = DOTween.Sequence();
            transform.GetComponent<Image>().DOFade(0.0f, 0.1f);
        }
        //public void InitPawnStoneInit()
        //{
        //    //this.mapExpView = mapExpView;
        //    // move to nekkisari .. make it disappear
        //}

        //public void MovePawn(NodeTimeData endNodeData, MapExpBasePtrEvents mapBasePtrEvents)
        //{
        //    Transform targetTrans = mapBasePtrEvents.gameObject.transform;  
        //    transform.DOMove(targetTrans.position, endNodeData.time);    
        //  //  nodeData = endNodeData.nodeData;
        //}
        public void InitPawnMovement(List<PathData> allNodes)
        {  
            this.allNodes.Clear();
            this.allNodes = allNodes.OrderBy(t=>t.pathSeq).ToList();
            index = -1;

            Vector3[] allPos = new Vector3[allNodes.Count];
            for (int i = 0; i < this.allNodes.Count; i++)
            {
                allPos[i] = this.allNodes[i].transform.localPosition;
            }

            movePath
                .Append(transform.GetComponent<Image>().DOFade(1.0f, 0.4f))
                .Append(transform.DOLocalPath(allPos, 5.0f, PathType.CatmullRom, PathMode.Sidescroller2D))
                .AppendCallback(Transit2Town) 
                ;
            movePath.Play(); 
                //.OnComplete(()=>Transit2Town());
            //Move2Next();
          
        }
        void Transit2Town()
        {
            MapService.Instance.mapIntViewPanel.GetComponent<IPanel>().UnLoad();
            transform.GetComponent<Image>().DOFade(0.0f, 0.1f);

        }
       public void Move2Next()
       {
            index++;
            //transform.DOLocalMove(allNodes[index].transform.localPosition, 0.4f)
            //                    .OnComplete(()=>OnMoveComplete());
                       
       }
       
        void OnMoveComplete()
        {
            if(index < allNodes.Count)
            {
                Move2Next();
            }
            else
            {
                Debug.Log("reached End Node"); 
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("TRIGGER 2");
        }
        public void UnPause()
        {
            DOTween.PlayAll();
        }
        public void Pause()
        {
            DOTween.PauseAll();
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.O))
            {
                DOTween.PlayAll();
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                DOTween.PauseAll();
            }
        }
    }
}