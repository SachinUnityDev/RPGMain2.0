using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
namespace Combat
{


    public class CombatEndContinueBtn : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("TBR")]
        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite spriteHL;

        [Header("Global Var")]
        CombatEndView combatEndView;
        Image btnImg; 
        void Start()
        {
            btnImg = GetComponent<Image>();
        }
        public void InitContinueBtn(CombatEndView combatEndView)
        {
            this.combatEndView= combatEndView;
            btnImg.sprite = spriteN; 
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            btnImg.sprite = spriteN;
            combatEndView.CloseCombatView(); 
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            btnImg.sprite = spriteHL;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            btnImg.sprite = spriteN;
        }



    }
}