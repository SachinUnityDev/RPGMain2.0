using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace Interactables
{
    public class PermaTraitBtnView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite spriteOnHover;
        [SerializeField] Sprite spriteOnClick;

        [SerializeField] bool isClicked = false;
        [SerializeField] Image img;

        [SerializeField] TextMeshProUGUI text;

        InvTraitsView invTraitsView;

        private void OnEnable()
        {
            img = GetComponent<Image>();
            img.sprite = spriteN;
            text = GetComponentInChildren<TextMeshProUGUI>(true);   
            InvService.Instance.OnCharSelectInvPanel += (CharModel c) => OnClick(); 
           
        }
        private void OnDisable()
        {
            InvService.Instance.OnCharSelectInvPanel -= (CharModel c) => OnClick();
        }

        public void Init(InvTraitsView invTraitsView)
        {
            this.invTraitsView = invTraitsView;
            text = GetComponentInChildren<TextMeshProUGUI>(true);
            OnClick(); 
        }
        
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!isClicked)
                OnClick();          
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(!isClicked)
                img.sprite = spriteOnHover;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!isClicked)
                img.sprite = spriteN;

        }

        public void OnUnClick()
        {
            img.sprite = spriteN;
            isClicked= false;
            text.gameObject.SetActive(false);
            
        }
        public void OnClick()
        {
            if (invTraitsView == null) return;
            img.sprite = spriteOnClick;
            isClicked= true;
            CharModel charModel = InvService.Instance.charSelectController.charModel;
    
            invTraitsView.ShowPermaTrait(charModel);
            text.gameObject.SetActive(true);
        }


     

        
    }
}