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
        [SerializeField] bool stopLoop;

        
        private void Start()
        {
            stopLoop= false;
        }
        public void StartAnim()
        {
            seq = DOTween.Sequence();

            seq
               // .Append(transform.GetComponentInChildren<Image>().DOFade(0.8f, 0.5f))
                .Append(transform.GetComponentInChildren<Image>().DOFade(1f, .1f))                // audio Interval here
             
                ;
            seq.Play().OnComplete(() =>
            {
                if (!stopLoop)
                {
                   // seq.Play();
                }
                else
                {
                    seq.Pause();
                    transform.GetComponentInChildren<Image>().DOFade(0f, .25f)
                    .OnComplete(()=>gameObject.SetActive(false));
                }
            });


        }
        public void EndAnim()
        {
            stopLoop= true;
        }

       
    }
}