using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
namespace Quest
{
    public class QRoomPortPtrEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        [SerializeField] QRoomView qRoomView;
        [SerializeField] CharController charController;

        [SerializeField] Sprite charSprite;

        public CharNames charName;
        void Start()
        {

        }

        public void InitPort(QRoomView qRoomView, CharController charController)
        {
            this.qRoomView = qRoomView;
            this.charController = charController;
            if(charController != null )
            {
                CharFleeState fleeState = charController.charModel.charFleeState;
                CharacterSO charSO = CharService.Instance.allCharSO.GetCharSO(charController.charModel.charName);
                charSprite = charSO.dialoguePortraitClicked;
                charName = charSO.charName;
                if (fleeState == CharFleeState.None)
                {
                    charSprite = charSO.charSprite;
                }
                else
                {
                    charSprite = charSO.charSpriteUnClickedFlipped; 
                }
                transform.GetComponent<Image>().sprite = charSprite;
            }
            
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            qRoomView.qWalkBtmView.OnCharHovered(charController); 
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            qRoomView.qWalkBtmView.OnCharHoverExit(charController); 
        }
    }
}