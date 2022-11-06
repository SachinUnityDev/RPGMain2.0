using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace Town
{
    public class MapBtnPtrEvents : MonoBehaviour
    {
        Image btnImg;
        void Start()
        {
            btnImg = GetComponent<Image>();
            btnImg.alphaHitTestMinimumThreshold = 0.1f;
        }
    }

}
