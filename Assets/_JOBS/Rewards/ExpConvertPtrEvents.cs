using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 

namespace Town
{


    public class ExpConvertPtrEvents : MonoBehaviour, IPointerClickHandler
    {
   
        WoodGameView1 woodGameView; 
        public void Init( WoodGameView1 woodGameView)
        {
            this.woodGameView = woodGameView;           
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            woodGameView.On_ExpConvert(); 
        }
    }
}