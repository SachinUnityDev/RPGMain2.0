using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace Interactables
{


    public class StashInvBtnPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] bool isClicked;
        [SerializeField] GameObject stashInvTransferBox;

        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite spriteHL;

        [SerializeField] Image img;
        public void OnPointerClick(PointerEventData eventData)
        {
            if (isClicked)
            {
                UIControlServiceGeneral.Instance.TogglePanel(stashInvTransferBox, false);
                img.sprite = spriteN; 
            }
            else
            {
                UIControlServiceGeneral.Instance.TogglePanel(stashInvTransferBox, true);
                stashInvTransferBox.GetComponent<IPanel>().Load();
                img.sprite = spriteHL;
            }
            isClicked = !isClicked;
            //if (stashInvTransferBox.activeInHierarchy == false)
            //    img.sprite = spriteN;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            img.sprite = spriteHL;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(stashInvTransferBox.activeInHierarchy == false)
            img.sprite = spriteN;
        }

        void Start()
        {
            stashInvTransferBox = InvService.Instance.invRightViewController.stashInvTransferBox.gameObject;
            isClicked = false;
            UIControlServiceGeneral.Instance.TogglePanel(stashInvTransferBox, false);
            img.sprite = spriteN;
        }
    }
}