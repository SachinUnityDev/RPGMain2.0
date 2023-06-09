using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 



namespace Quest
{
    public class QRoomPreReqEndPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {

        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite spriteHL;

        [SerializeField] Image img; 
        private void Start()
        {
           
        }

        public void OnPointerClick(PointerEventData eventData)
        {          
            QRoomService.Instance.On_QuestStateChg(QRoomState.AutoWalk); 
            transform.gameObject.SetActive(false);
            
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
           transform.GetComponent<Image>().sprite = spriteHL;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.GetComponent<Image>().sprite = spriteN;
        }
    }
}