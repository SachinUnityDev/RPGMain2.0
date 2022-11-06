using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
namespace Common
{
    public class BuildingPtrEvents : MonoBehaviour, IPointerEnterHandler
    {
        Image btnImg;


        public void OnPointerEnter(PointerEventData eventData)
        {
            


        }

        void Start()
        {
            // loop thru 

            btnImg = GetComponent<Image>();
            btnImg.alphaHitTestMinimumThreshold = 0.1f;
        }

      
    }



}

