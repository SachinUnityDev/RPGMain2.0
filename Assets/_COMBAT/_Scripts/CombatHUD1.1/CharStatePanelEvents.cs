using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Common;
using TMPro;
namespace Combat
{
    public class CharStatePanelEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public CharStatesBase charStatebase;
        public CharStateSO1 charStateSO;
        public CharStatesView charStatesView;
        [SerializeField] int lvl; 

        GameObject statesCard; 
        void Start()
        {  
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.6f);
        }

        public void InitCard(CharStateSO1 charStateSO, CharStatesBase charStatebase, CharStatesView charStatesView, int lvl)
        {
            this.charStatebase= charStatebase;
            this.charStateSO= charStateSO;
            this.charStatesView= charStatesView;
            this.lvl = lvl;
        }

   

        public void OnPointerClick(PointerEventData eventData)
        {
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);           
            ShowCard(); 
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.6f);
            HideCard();
        }

        void ShowCard()
        {
            statesCard = charStatesView.GetCharStateCard(charStateSO.charStateBehavior); 
            statesCard.GetComponent<CharStatesCardView>().InitStatesCardView(charStateSO, charStatebase, lvl);
            statesCard.SetActive(true); 
            
        }
        void HideCard()
        {
            statesCard.SetActive(false);
        }

    }



}
