using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Common; 

namespace Combat
{
    public class TopPanelEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        CharController _charController; 

        public CharController charController { get => _charController; 
                                               set
                                               {
                                                  //  Debug.Log("$$ value" + _charController.name);
                                                    _charController = value;                                           
                                               } 
                                            }
        public void OnPointerClick(PointerEventData eventData)
        {
            CombatEventService.Instance.On_CharClicked(charController.gameObject); 
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            gameObject.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1); 
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            gameObject.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);
        }

        void Start()
        {
            gameObject.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);
        }

    }



}
