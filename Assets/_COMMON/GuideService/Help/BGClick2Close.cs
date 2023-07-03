using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Common
{


    public class BGClick2Close : MonoBehaviour, IPointerClickHandler
    {

        IPanel panel;

        public void OnPointerClick(PointerEventData eventData)
        {
            panel = GetComponent<IPanel>();
            panel.UnLoad();
        }

        


        
    }
}