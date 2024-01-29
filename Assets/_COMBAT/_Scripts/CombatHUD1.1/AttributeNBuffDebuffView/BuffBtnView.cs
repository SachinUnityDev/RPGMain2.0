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

        Image img;
        [SerializeField] bool hasBuffs; 
        private void Start()
        {
            CombatEventService.Instance.OnCharClicked += OnCharClicked; 
            buffView.gameObject.SetActive(false);
            img = GetComponent<Image>();
            img.DOFade(0.5f, 0.1f); hasBuffs= false;
        }

        private void OnDisable()
        {
            CombatEventService.Instance.OnCharClicked -= OnCharClicked; 
        }
        void OnCharClicked(CharController charController)
        {
            hasBuffs = buffView.InitBuffView(this, charController, isBuffView);
            BuffBtnInit(charController); 
        }

        void BuffBtnInit(CharController charController)
        {
           if(hasBuffs)            
                img.DOFade(1.0f, 0.1f); 
           else
                img.DOFade(0.5f, 0.1f);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(hasBuffs)
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