using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Common
{
    public class LoadSlotBtnPtrEvents : MonoBehaviour, IPointerClickHandler
    {

        LoadSlotsView LoadSlotsView;
        LoadView loadView; 
        public void Init(LoadSlotsView loadSlotsView, LoadView loadView)
        {
            this.loadView = loadView;
            this.LoadSlotsView = loadSlotsView;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
           
        }
    }
}