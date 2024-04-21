using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using UnityEngine.EventSystems;

namespace Intro
{
    public class Click2Continue : MonoBehaviour, IPointerClickHandler
    {

        public void OnPointerClick(PointerEventData eventData)
        {
            gameObject.GetComponentInParent<IPanel>().UnLoad();
        }

    }
}