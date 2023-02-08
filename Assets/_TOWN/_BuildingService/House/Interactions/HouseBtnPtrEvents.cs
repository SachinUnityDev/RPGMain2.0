using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace Town
{
    public class HouseBtnPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] BuildInteractType buildInteractType;
        [SerializeField] HouseViewController houseView;

        public void HouseIntInit(BuildIntTypeData buildData, InteractionSpriteData spriteData, HouseViewController houseView)
        {
            transform.GetComponent<Image>().sprite = spriteData.spriteN; 


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

        void Start()
        {

        }

    }
}