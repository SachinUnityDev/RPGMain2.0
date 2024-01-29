using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FleeBtnPtrEvents : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] FleeView fleeView;

    private void Start()
    {
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!fleeView.gameObject.activeInHierarchy)
        {
            fleeView.InitFleeView(); 
            fleeView.gameObject.SetActive(true);
        }
        else
        {
            fleeView.gameObject.SetActive(false);
        }       
    }
}
