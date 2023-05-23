using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
namespace Quest
{
    public class QRoomEndArrowS : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {

        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite spriteHL;

        [SerializeField] Image img;
        [SerializeField] int roomDOWN = -1; 
        public void OnPointerClick(PointerEventData eventData)
        {
            // get from the QRoomModel and base for actions
            img.sprite = spriteHL;
            roomDOWN = QSceneService.Instance.qRoomController.qRoomModel.downRoomNo;
            if (roomDOWN != -1)
            {
                QSceneService.Instance.qRoomController.Move2Room(roomDOWN);
            }
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
           GetComponent<Image>().DOFade(0, 0.01f);
           

        }



    }
}