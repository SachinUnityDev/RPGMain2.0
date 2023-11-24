using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace Combat
{


    public class RunguMotion : MonoBehaviour
    {
        // Start is called before the first frame update
        Sequence seqRot;
        Sequence seqMove; 
        void Start()
        {
           // RotMotion(new Vector3(1f,0,0)); 
        }

        public void RotMotion(Vector3 targetPos)
        {
            seqRot = DOTween.Sequence();
            seqRot
                .Append(transform.DORotate(new Vector3(0, 0, 360), 0.001f, RotateMode.LocalAxisAdd))
                //.Append(transform.DORotate(new Vector3(0, 0, -360), 0.1f))
                //.AppendCallback(() => { targetPos = new Vector3(targetPos.x+1, targetPos.y,targetPos.z);})
                // .Join(transform.)
                ;
            seqRot.Play().SetLoops(-1);

            seqMove = DOTween.Sequence();
            seqMove.Append(transform.DOLocalMoveX(targetPos.x, 0.8f))
                   .OnComplete(()=>Destroy(this.gameObject, 0.1f));
            
            seqMove.Play(); 
        }
        //private void Update()
        //{
        //   // transform.Rotate(Vector3.forward, 10000 * Time.deltaTime);

        //}
    }
}