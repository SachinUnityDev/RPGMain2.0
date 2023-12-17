using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Combat
{
    public class LogNAttribBtnView : MonoBehaviour, IPointerClickHandler
    {

        [SerializeField] Transform attribTrans;
        [SerializeField] Transform logTrans;

        [SerializeField] bool isAttribOpen;
        [SerializeField] bool islogOpen;


        [Header("Height and width")]
        float logHt;
        float attribWd;


        [Header("OffSets")]
        float logOffsetY = 45f;
        float attribOffsetX = 20f;
        void Start()
        {
            isAttribOpen = false;
            islogOpen= false;

            logHt = logTrans.GetComponent<RectTransform>().sizeDelta.y;
            attribWd = attribTrans.GetComponent<RectTransform>().sizeDelta.x;

            ToggleAttrib(false);
            ToggleLog(false);

        }
        void ToggleAttrib(bool toOpen)
        {
            if(toOpen)            
                attribTrans.DOLocalMoveX(0, 0.4f);            
            else            
                attribTrans.DOLocalMoveX(-attribWd, 0.4f);
            isAttribOpen = toOpen; 
        }
        void ToggleLog(bool toOpen)
        {
            if (toOpen)
                logTrans.DOLocalMoveY(0, 0.4f);
            else
                logTrans.DOLocalMoveY(logHt, 0.4f);
            islogOpen= toOpen;
        }
        public void OnButtonClick()
        {
            //0,0 
            //1,0
            //1,1
            //0,1
            
            if (!isAttribOpen && !islogOpen)  // 0, 0
            {
                ToggleLog(true);              
            }
            else if (!isAttribOpen && islogOpen) //0,1
            {
                ToggleAttrib(true);
            }
            else if (isAttribOpen && islogOpen) // 1,1
            {
                ToggleAttrib(true);
                ToggleLog(false);            
            }
            else if (isAttribOpen && !islogOpen) // 1,0
            {
                ToggleAttrib(false);
                ToggleLog(false);              
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnButtonClick();
        }
    }
}