using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
namespace Common
{
    public class BuildingPtrEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        Image btnImg;
        /// <summary>
        /// Button HL deprecate 
        /// get plank data directly from the BuildingSO 
        /// IBuild SO from the SO
        /// 
        /// </summary>
        

        [SerializeField] BuildingNames buildingName;
        [SerializeField] BuildingSO buildSO; 
        public void OnPointerClick(PointerEventData eventData)
        {
           
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            // HL the building
            // incorporte day and night in one

        }

        public void OnPointerExit(PointerEventData eventData)
        {
          
        }

        void Start()
        {
            // loop thru 

            btnImg = GetComponent<Image>();
            btnImg.alphaHitTestMinimumThreshold = 0.1f;
        }

      
    }



}

