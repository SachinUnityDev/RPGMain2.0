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
        public CharStateModel charStateModel;

        public GameObject card;
       // GameObject cardGO;

        
        void Start()
        {
            card.SetActive(false);
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.6f);

        }

        public void FillCharStateCard() // Max 5 lines of strings 
        {
            card.transform.GetChild(0).GetComponent<Image>().sprite  
                                            = charStateModel.charStateSprite;
           
           
            card.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text
                                        = charStateModel.castTime.ToString();
          

            card.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text
                                        = charStateModel.charStateCardStrs[0];           
    
            for (int k = 0; k < 4; k++)
            {
                card.transform.GetChild(k).gameObject.SetActive(true);
            }
            for (int i = 1; i < charStateModel.charStateCardStrs.Count && i <= 5; i++)
            {
                card.transform.GetChild(3).GetChild(i).gameObject.SetActive(true);
                card.transform.GetChild(3).GetChild(i).GetComponent<TextMeshProUGUI>().text
                                                            = charStateModel.charStateCardStrs[i];
            }
            for (int j = charStateModel.charStateCardStrs.Count; j < card.transform.GetChild(3).childCount; j++)
            {
                card.transform.GetChild(3).GetChild(j).gameObject.SetActive(false);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            FillCharStateCard();
            ShowCard(); 
        }

        void ShowCard()
        {
            card.SetActive(true); 
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.6f);
            card.SetActive(false);
        }

        string GetCastTimeString()
        {



            return ""; 
        }    

    }



}
