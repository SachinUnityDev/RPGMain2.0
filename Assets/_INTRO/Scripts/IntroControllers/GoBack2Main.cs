using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Intro
{

    public class GoBack2Main : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            gameObject.GetComponent<NewGameModeController>().Unload2Main();            
        }

        // Start is called before the first frame update
        
        
   
    }
}