using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 


namespace Combat
{
    public class OverSkillCardController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            SkillServiceView.Instance.pointerOnSkillCard = true; 
            gameObject.SetActive(true); 
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SkillServiceView.Instance.pointerOnSkillCard = false;
            gameObject.SetActive(false);
        }

        void Start()
        {

        }

 
    }


}

