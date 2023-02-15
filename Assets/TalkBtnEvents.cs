using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace Town
{
    public class TalkBtnEvents : MonoBehaviour
        //, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        
        //[SerializeField] TempleController templeController;
      
        //public void OnPointerClick(PointerEventData eventData)
        //{
           
        //}

        //public void OnPointerEnter(PointerEventData eventData)
        //{
        //    // increase scale of Panel 
        //    templeController.buttonsPanel.transform.DOScaleY(1.0f, 0.2f);


        //    //foreach (Transform child in templeController.buttonsPanel.transform)
        //    //{
        //    //    child.gameObject.SetActive(true);
        //    //    PopUp(child); 
        //    //}
        //    //templeController.ButtonsPanel.transform.DOScale(1.0f, 0.25f); 
        //}

        //public void OnPointerExit(PointerEventData eventData)
        //{
        //    Sequence Wait = DOTween.Sequence();
        //    Wait.AppendInterval(1.5f)
        //        .Append(templeController.buttonsPanel.transform.DOScaleY(0.0f, 0.2f))
        //        ;

        //    Wait.Play(); 
        //    //foreach (Transform child in templeController.buttonsPanel.transform)
        //    //{
        //    //    PopOut(child);
        //    //}
        //    //templeController.ButtonsPanel.transform.DOScale(0.0f, 0.25f);
        //}

        ////void PopOut(Transform trans)
        ////{
        ////    Sequence popOut = DOTween.Sequence();
        ////    popOut
        ////         .AppendInterval(1.1f)
        ////         .AppendCallback(() => trans.gameObject.SetActive(false))
        ////         .Append(trans.DOScale(0.0f, 0.1f))
        ////        ;
        ////}
        ////void PopUp(Transform trans)
        ////{
        ////    Sequence popUp = DOTween.Sequence();
        ////    popUp.Append(trans.DOScale(1.25f, 0.1f))
        ////         .AppendInterval(0.1f)
        ////         .Append(trans.DOScale(1.0f, 0.1f))
        ////        ;
        ////}
        //// Start is called before the first frame update
        //void Start()
        //{

        //    templeController.buttonsPanel.transform.DOScaleY(0.0f, 0.2f);


        //    //foreach (Transform child in templeController.buttonsPanel.transform)
        //    //{
        //    //    child.DOScale(0, 0.1f);
        //    //}

        //}


    }




}
