using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI; 
namespace Combat
{
    public class BuffButtonPtrEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] bool isBuffPanel = false;
        [SerializeField] bool isDebuffPanel = false;
        float animTime = 0.2f;
        bool isOpen = false;
        bool isClicked = false; 
        
        Transform buffView;
        ScrollRect scroll; 

        private void Start()
        {
            buffView = transform.GetChild(0);
            scroll = buffView.GetComponent<ScrollRect>();
            scroll.verticalNormalizedPosition = 1f;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!isClicked)
            {

                UIControlServiceCombat.Instance.CloseAllOpenUI(); 
                UIControlServiceCombat.Instance.ToggleUIStateScale(buffView.gameObject, UITransformState.Open);
                scroll.verticalNormalizedPosition = 1f;
                CombatService.Instance.combatState = CombatState.INCombat_Pause;
                isOpen = true;
                isClicked = true; 
            }
            else
            {
                isOpen = false; isClicked = false;
                CombatService.Instance.combatState = CombatState.INCombat_normal;
                UIControlServiceCombat.Instance.ToggleUIStateScale(buffView.gameObject, UITransformState.Close);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!isOpen)
            {
                UIControlServiceCombat.Instance.CloseAllOpenUI();
                UIControlServiceCombat.Instance.ToggleUIStateScale(buffView.gameObject, UITransformState.Open);
                CombatService.Instance.combatState = CombatState.INCombat_Pause;
                scroll.verticalNormalizedPosition = 1f;
                isOpen = true;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
             if(isOpen && !isClicked) 
             {
                CombatService.Instance.combatState = CombatState.INCombat_normal;
                UIControlServiceCombat.Instance.ToggleUIStateScale(buffView.gameObject, UITransformState.Close);
                isOpen = false;
             }
        }
    }
}
