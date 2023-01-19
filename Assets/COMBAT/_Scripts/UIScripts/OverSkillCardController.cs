using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 


namespace Combat
{
    public class OverSkillCardController : MonoBehaviour
        , IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] bool isHovered; 

        public void OnPointerEnter(PointerEventData eventData)
        {
            SkillServiceView.Instance.pointerOnSkillCard = true;
            gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
         
            SkillServiceView.Instance.pointerOnSkillCard = false;
            if(!isHovered)
            gameObject.SetActive(false);
        }

        void Start()
        {
            gameObject.SetActive(false);
            isHovered = false;
        }

        private void Update()
        {
           if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            {
                isHovered = true; 
            }      
        }
    }


}

