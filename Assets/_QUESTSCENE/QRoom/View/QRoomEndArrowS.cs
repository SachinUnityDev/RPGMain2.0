using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace Quest
{
    public class QRoomEndArrowS : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {

        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite spriteHL;

        [SerializeField] Image img;

        public void OnPointerClick(PointerEventData eventData)
        {
            // get from the QRoomModel and base for actions
            img.sprite = spriteHL;

        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            img.sprite = spriteHL;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            img.sprite = spriteN;
        }

        void Start()
        {
            img = GetComponent<Image>();
        }



    }
}