using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; 

namespace Quest
{
    public class MistakeBarView : MonoBehaviour
    {
        public void FillMistakeHearts(int netAvailable, int missHits)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (i < netAvailable)
                {
                    transform.GetChild(i).gameObject.GetComponent<Image>().DOFade(0.65f, 0.1f);
                    transform.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                    transform.GetChild(i).gameObject.GetComponent<Image>().DOFade(0.0f, 0.1f);
                }   
            }

            
            for (int i = 0; i < missHits; i++)
            {              
                transform.GetChild(i).gameObject.GetComponent<Image>().DOFade(1.0f, 0.1f);              
            }
        }


    }
}