using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
using Common;
using TMPro;
using Interactables;

namespace Town
{
    public class LeftBtnsController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] Transform centerTrans;
        [SerializeField] Transform sideTrans; 
        [SerializeField] Transform topTrans;

        [SerializeField] Button invBtn;
        [SerializeField] Button jobsBtn;
        [SerializeField] Button rostersBtn;

        [SerializeField] GameObject invPanelXL;
        [SerializeField] GameObject rosterPanel;
        [SerializeField] GameObject jobPanel;


        [SerializeField] string nameStr = "";

        [Header("NOT TO BE REF")]
        [SerializeField] TextMeshProUGUI nametxt;


        #region POINTER EVENTS AND ANIMATION
        public bool isOpen = false; 
        public void OpenBtns()
        {            
            sideTrans.DOLocalMoveX(100, 0.1f);
            sideTrans.GetComponent<Image>().DOFade(1f, 0.1f); 
            topTrans.DOLocalMoveY(100, 0.1f);
            topTrans.GetComponent<Image>().DOFade(1f, 0.1f);
            centerTrans.DOScale(1.25f, 0.1f); 

        }

        public void CloseBtns()
        {
            Sequence closeSeq = DOTween.Sequence();

            closeSeq
                .AppendInterval(1f)                
                .AppendCallback(() => CloseAnim())
                ;
           
                closeSeq.Play(); 
        }

        void CloseAnim()
        {
            if (!isOpen)
            {
                sideTrans.DOLocalMoveX(0, 0.1f);
                sideTrans.GetComponent<Image>().DOFade(0f, 0.1f);
                topTrans.DOLocalMoveY(0, 0.1f);
                topTrans.GetComponent<Image>().DOFade(0f, 0.1f);
                centerTrans.DOScale(1f, 0.1f);
            }
        }
  
        public void OnPointerEnter(PointerEventData eventData)
        {
            isOpen = true;
            nametxt.text = nameStr;
            nametxt.gameObject.SetActive(true);
            OpenBtns();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isOpen = false;
            nametxt.gameObject.SetActive(false);
            CloseBtns();
        }

        #endregion



#region BUTTON EVENTS

        void OnRostersBtnClick()
        {
            UIControlServiceGeneral.Instance.TogglePanelNCloseOthers(rosterPanel, true);
            // init roster here
        }
        void OnJobBtnClick()
        {
            UIControlServiceGeneral.Instance.TogglePanelNCloseOthers(jobPanel, true);
            // init job here 

        }
        void OnInvBtnClick()
        {

            InvService.Instance.ShowInvXLView(true);
         

        }



        #endregion


        void Start()
        {           
            

            centerTrans = transform;
            sideTrans = transform.parent.GetChild(0);
            topTrans = transform.parent.GetChild(1);

            invBtn = centerTrans.GetComponent<Button>();
            rostersBtn = sideTrans.GetComponent<Button>();
            jobsBtn = topTrans.GetComponent<Button>();

            invBtn.onClick.AddListener(OnInvBtnClick);
            jobsBtn.onClick.AddListener(OnJobBtnClick);
            rostersBtn.onClick.AddListener(OnRostersBtnClick);

            nametxt = transform.parent.GetChild(3).GetComponent<TextMeshProUGUI>();           
            nametxt.gameObject.SetActive(false);
           

            CloseBtns();
        }
    }


}

