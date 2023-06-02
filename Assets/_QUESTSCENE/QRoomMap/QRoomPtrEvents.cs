using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Quest
{
    public class QRoomPtrEvents : MonoBehaviour, IPointerClickHandler
    {
        // Start is called before the first frame update
        public void OnPointerClick(PointerEventData eventData)
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
        }
    }
}