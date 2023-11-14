using Common;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace Combat
{
    public class BuffBtnView : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Buff View")]
        [SerializeField] BuffView buffView;
        [SerializeField] bool isBuffView ; 
        private void Start()
        {
            CombatEventService.Instance.OnCharClicked += OnCharClicked; 
        }

        private void OnDisable()
        {
            CombatEventService.Instance.OnCharClicked -= OnCharClicked; 
        }
        void OnCharClicked(CharController charController)
        {
            buffView.InitBuffView(this, charController, isBuffView); 
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            buffView.gameObject.SetActive(true); 
            
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            buffView.gameObject.SetActive(false);
        }
        public void OnPointerClick(PointerEventData eventData)
        {
         
        }
    }
}