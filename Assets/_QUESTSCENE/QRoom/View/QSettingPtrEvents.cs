using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace Quest
{


    public class QSettingPtrEvents : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] Transform centerTrans;
        [SerializeField] Transform fleeTrans;
        [SerializeField] Transform optsTrans;


        [Header("To be ref")]
        [SerializeField] Button settingsBtn;
        [SerializeField] Button animSpeedBtn;
        [SerializeField] Button autoWalkBtn;
        [SerializeField] Button fleeBtn;

   


        public bool isOpen = false;

        #region ANIMATION AND POINTER EVENTS
        public void OpenBtns()
        {
            fleeTrans.DOLocalMoveX(-80, 0.2f);
            fleeBtn.GetComponent<Image>().DOFade(1f, 0.2f);
            animSpeedBtn.GetComponent<Image>().DOFade(1f, 0.2f);
            autoWalkBtn.GetComponent<Image>().DOFade(1f, 0.2f);
            optsTrans.DOLocalMoveY(-40, 0.2f);
        }

        public void CloseBtns()
        {
            fleeTrans.DOLocalMoveX(50, 0.2f);
            optsTrans.DOLocalMoveY(100, 0.2f);
            fleeBtn.GetComponent<Image>().DOFade(0f, 0.05f);
            animSpeedBtn.GetComponent<Image>().DOFade(0f, 0.05f);
            autoWalkBtn.GetComponent<Image>().DOFade(0f, 0.05f);
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (isOpen)
            {
                CloseBtns();
            }
            else
            {              
                OpenBtns();
            }
            isOpen = !isOpen;
        }
  

        #endregion

        #region BUTTON EVENTS 

        void OnFleeBtn()
        {
  

        }
        void OnAnimSpeedBtn()
        {
            
        }

        void OnAutoWalkBtn()
        {

        }

        #endregion

        void Start()
        {  
            animSpeedBtn.onClick.AddListener(OnAnimSpeedBtn);
            autoWalkBtn.onClick.AddListener(OnAutoWalkBtn);
            fleeBtn.onClick.AddListener(OnFleeBtn);
         
            CloseBtns();
            isOpen = false; 
        }

      
    }
}