using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

namespace Town
{
    public class RightBtnController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] Transform centerTrans;
        [SerializeField] Transform sideTrans;
        [SerializeField] Transform topTrans;


        [Header("To be ref")]
        [SerializeField] Button questScrollBtn;
        [SerializeField] Button eventBtn;
        [SerializeField] Button mapBtn;

        [SerializeField] string nameStr;

        [Header("NO TO BE REFER")]
        [SerializeField] TextMeshProUGUI nameTxt;


        public bool isOpen = false;

#region ANIMATION AND POINTER EVENTS
        public void OpenBtns()
        {
            sideTrans.DOLocalMoveX(-100, 0.1f);
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
            nameTxt.gameObject.SetActive(true);
            nameTxt.text = nameStr;
            OpenBtns();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isOpen = false;
            nameTxt.gameObject.SetActive(false);
            CloseBtns();
        }

        #endregion

#region BUTTON EVENTS 

        void OnQuestScrollBtnClick()
        {


        }
        void OnMapBtnClick()
        {
            MapService.Instance.mapIntViewPanel.SetActive(true);
          
        }
        void OnEventBtnClick()
        {

        }

#endregion

        void Start()
        {
            centerTrans = transform;           
            topTrans = transform.parent.GetChild(0);
            sideTrans = transform.parent.GetChild(1);

            questScrollBtn = centerTrans.GetComponent<Button>();
            eventBtn = sideTrans.GetComponent<Button>();
            mapBtn = topTrans.GetComponent<Button>();

            questScrollBtn.onClick.AddListener(OnQuestScrollBtnClick);
            eventBtn.onClick.AddListener(OnEventBtnClick);
            mapBtn.onClick.AddListener(OnMapBtnClick);
            nameTxt = transform.parent.GetChild(3).GetComponent<TextMeshProUGUI>();
            nameTxt.gameObject.SetActive(false); 

            CloseBtns();
        }


    }

}

