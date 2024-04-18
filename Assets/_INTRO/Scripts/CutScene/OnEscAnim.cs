using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace Common
{


    public class OnEscAnim : MonoBehaviour
    {
        [SerializeField] Image fillImg;
        private void Start()
        {
            fillImg.fillAmount = 0f;
        }

        public void PlayAnim(float fillVal)
        {
            fillImg.fillAmount = fillVal;
        }

        public void ResetAnim()
        {
            fillImg.fillAmount = 0;

        }
    }
}