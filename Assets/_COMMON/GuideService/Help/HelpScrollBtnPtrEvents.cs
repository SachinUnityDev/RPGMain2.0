using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Common
{


    public class HelpScrollBtnPtrEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {

        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite spriteHL;

       // [SerializeField] bool isClicked = false;

        HelpView helpView;

        Image img;
        public void InitScrollBtn(HelpView helpView)
        {
            this.helpView = helpView;
            img = GetComponent<Image>();
            img.sprite = spriteN; 
        }
        public void OnPointerClick(PointerEventData eventData)
        {
           
            img.sprite = spriteHL;
            helpView.OnPageTurnBtnPressed();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            img.sprite = spriteHL; 
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            img.sprite = spriteN; 
            
        }
        private void Start()
        {
            img = GetComponent<Image>();
        }

    }
}