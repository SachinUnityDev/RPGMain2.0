using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace Town
{
    public class CraftBtnPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
                                    , IPointerExitHandler
    {
        [Header(" TBR")]
        [SerializeField] Sprite btnN;
        [SerializeField] Sprite btnHL;
        [SerializeField] TextMeshProUGUI onHoverTxt; 

        [Header("Global Var")]
        [SerializeField] Image btnImg; 
        void Awake()
        {
            
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
           
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            
        }
    }
}