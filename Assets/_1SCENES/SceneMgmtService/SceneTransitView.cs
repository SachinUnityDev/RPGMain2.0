using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  

namespace Common
{


    public class SceneTransitView : MonoBehaviour
    {
        Sequence seq;
       
        public void StartAnim()
        {
            seq = DOTween.Sequence();

            seq              
                .Append(transform.GetComponentInChildren<Image>().DOFade(1f, .1f))
                ;

            seq.Play().SetLoops(-1);
        }
        public void EndAnim()
        {       
            seq.Pause();
            transform.GetComponentInChildren<Image>().DOFade(0f, .25f)
                            .OnComplete(() => gameObject.SetActive(false));
        }

       
    }
}
//seq.Play().OnComplete(() =>
//{
//    if (!stopLoop)
//    {
//        seq.Play();
//    }
//    else
//    {
//      EndAnim();
//    }
//});