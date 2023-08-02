using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace Common
{
    public class ClearmindBtnPtrEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] Sprite btnN;
        [SerializeField] Sprite btnHL;
        [SerializeField] Sprite btnNA;

        ClearmindView clearMindView; 
        TempTraitController tempTraitController;
        [SerializeField] CharNames charName;

        [SerializeField] Transform clearMindtxtTrans;

        [Header("Global var")]
        [SerializeField] bool isClickable;
        [SerializeField] Image img;

        public void InitBtnEvents(CharNames charName, TempTraitController tempTraitController, ClearmindView clearMindView)
        {
            img = GetComponent<Image>();
            this.clearMindView = clearMindView; 
            this.tempTraitController = tempTraitController;
            this.charName = charName;

        }
        public void SetState(bool isClickable)
        {
            this.isClickable = isClickable;
            SetImg();
        }
        void SetImg()
        {
            img = GetComponent<Image>();
            if (isClickable)
            {
                img.sprite = btnN;
            }
            else
            {
                img.sprite = btnNA;
            }
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            tempTraitController.OnClearMindPressed();
            clearMindView.OnClearMindPressed(); 
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
          clearMindtxtTrans.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            clearMindtxtTrans.gameObject.SetActive(false); 
        }
    }
}
