using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Common
{
    public class SkillPtsBtnPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("TO BE REF")]
        [SerializeField] InvSkillViewMain InvSkillViewMain;

        [SerializeField] bool isClicked;
      
        [SerializeField] Sprite spriteClicked;
        [SerializeField] Sprite spriteN; 
        public void OnPointerClick(PointerEventData eventData)
        {
            isClicked =!isClicked;
            SetClicked();
        }
        void SetClicked()
        {
            if (isClicked)
            {
                transform.GetComponent<Image>().sprite = spriteClicked;
            }
            else
            {
                transform.GetComponent<Image>().sprite = spriteN;
            }
            InvSkillViewMain.isPerkClickAvail = isClicked;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
           
        }

        public void OnPointerExit(PointerEventData eventData)
        {
          
        }

        void Start()
        {
            spriteClicked = InvSkillViewMain.skillViewSO.skillPlusClicked; 
            spriteN = InvSkillViewMain.skillViewSO.skillPlusN;
            isClicked = false;
            SetClicked(); 
        }

    }
}