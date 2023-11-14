using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
using Common;

namespace Combat
{
    public class BuffButtonPtrEvents : MonoBehaviour
    {
        //[SerializeField] bool isBuffView = false;
       
        //float animTime = 0.2f;
        //bool isOpen = false;
        //bool isClicked = false;

        //[Header(" Buff n Debuff View Ref")]
        //[SerializeField] Transform buffView;
        //[SerializeField] Transform debuffView;
        //ScrollRect scroll; 

        //private void Start()
        //{   
        //    scroll = buffView.GetComponentInChildren<ScrollRect>();
        //  //  scroll.verticalNormalizedPosition = 1f;
        //    CombatEventService.Instance.OnCharClicked += OnCharClicked; 
        //}

        //private void OnDisable()
        //{
        //    CombatEventService.Instance.OnCharClicked -= OnCharClicked;
        //}

        //void OnCharClicked(CharController charController)
        //{
        //    buffView.GetComponent<BuffView>().InitBuffView(this, charController, isBuffView);         
        //}

        //public void OnPointerClick(PointerEventData eventData)
        //{
        //    if (!isClicked)
        //    {
        //        UIControlServiceCombat.Instance.CloseAllOpenUI(); 
        //        if(isBuffView) 
        //        UIControlServiceCombat.Instance.ToggleUIStateScale(buffView.gameObject, UITransformState.Open);
        //        else
        //            UIControlServiceCombat.Instance.ToggleUIStateScale(debuffView.gameObject, UITransformState.Open);
        //      //  scroll.verticalNormalizedPosition = 1f;
        //        CombatService.Instance.combatState = CombatState.INCombat_Pause;
        //        isOpen = true;
        //        isClicked = true; 
        //    }
        //    else
        //    {
        //        isOpen = false; isClicked = false;
        //        CombatService.Instance.combatState = CombatState.INCombat_normal;
        //        if(isBuffView)
        //            UIControlServiceCombat.Instance.ToggleUIStateScale(buffView.gameObject, UITransformState.Close);
        //        else                    
        //            UIControlServiceCombat.Instance.ToggleUIStateScale(debuffView.gameObject, UITransformState.Close);
        //    }
        //}

        //public void OnPointerEnter(PointerEventData eventData)
        //{
        //    //if (isBuffView)
        //    //    buffView.gameObject.SetActive(true);
        //    ////  UIControlServiceCombat.Instance.ToggleUIStateScale(buffView.gameObject, UITransformState.Open);
        //    //else
        //    //    buffView.gameObject.SetActive(false);


        //    if (!isOpen)
        //    {
        //        UIControlServiceCombat.Instance.CloseAllOpenUI();
        //        if (isBuffView)
        //            buffView.gameObject.SetActive(true);
        //      //  UIControlServiceCombat.Instance.ToggleUIStateScale(buffView.gameObject, UITransformState.Open);
        //        else
        //            buffView.gameObject.SetActive(false);
        //        UIControlServiceCombat.Instance.ToggleUIStateScale(debuffView.gameObject, UITransformState.Open);
        //        CombatService.Instance.combatState = CombatState.INCombat_Pause;
        //       // scroll.verticalNormalizedPosition = 1f;
        //        isOpen = true;
        //    }
        //}

        //public void OnPointerExit(PointerEventData eventData)
        //{
        //     if(isOpen && !isClicked) 
        //     {
        //        CombatService.Instance.combatState = CombatState.INCombat_normal;
        //        if (isBuffView)
        //            UIControlServiceCombat.Instance.ToggleUIStateScale(buffView.gameObject, UITransformState.Close);
        //        else
        //            UIControlServiceCombat.Instance.ToggleUIStateScale(debuffView.gameObject, UITransformState.Close);
        //    }
        //}
    }
}
