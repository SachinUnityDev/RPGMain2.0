using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
namespace Quest
{
    public class QRoomEndArrowW : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler    
    {
        // UP ARROW 
        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite spriteHL;

        [SerializeField] Image img;
        [SerializeField] int UpRoom =-1;
        public void OnPointerClick(PointerEventData eventData)
        {
            // get from the QRoomModel and base for actions
            img.sprite = spriteHL;
            UpRoom = QSceneService.Instance.qRoomController.qRoomModel.upRoomNo; 
            if (UpRoom!= -1)
            {
                QSceneService.Instance.qRoomController.Move2Room(UpRoom); 
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
            img.DOFade(0, 0.01f);
            
        }

    
    }
}