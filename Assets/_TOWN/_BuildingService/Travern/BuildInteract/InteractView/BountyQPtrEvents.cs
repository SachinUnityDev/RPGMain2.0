using Common;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace Town
{

    public class BountyQPtrEvents : MonoBehaviour, IPointerClickHandler, INotify
    {
        public bool isDontShowItAgainTicked { get; set; }
        public NotifyName notifyName { get; set; }
        Image img;

        [SerializeField] NotifyBoxView notifyBoxView; 
        private void Start()
        {
            img = GetComponent<Image>();           
        }
        void ShowNotifyBox()
        {
            notifyName = NotifyName.BountyQUnLock;
            notifyBoxView.OnShowNotifyBox(this, notifyName);
            NotifyModel notifyModel = UIControlServiceGeneral.Instance
                                    .notifyController.GetNotifyModel(notifyName);
            if (notifyModel.isDontShowAgainTicked)
                return;
            PosNotify();
        }

        public void OnNotifyAnsPressed()
        {
            Debug.Log("Bounty Notify pressed");                              
        }
        void PosNotify()
        {
            float width = notifyBoxView.GetComponent<RectTransform>().rect.width;
            float height = notifyBoxView.GetComponent<RectTransform>().rect.height;

            GameObject canvas = GameObject.FindWithTag("TownCanvas");
            Canvas canvasObj = canvas.GetComponent<Canvas>();


            Transform slotTrans = transform.parent;
            int slotIndex = slotTrans.GetSiblingIndex();
            Vector3 offsetFinal;
            float ht = transform.GetComponent<RectTransform>().rect.height;
            Vector3 offset = new Vector3(250, 0, 0);

            offsetFinal = (offset + new Vector3(width / 2, 0 ,0)) * canvasObj.scaleFactor;
            
            Vector3 pos = transform.position + offsetFinal;

            Sequence seq = DOTween.Sequence();
            seq
                .Append(notifyBoxView.transform.DOMove(pos, 0.1f))
                .Append(notifyBoxView.transform.GetComponent<Image>().DOFade(1.0f, 0.3f))
                ;
           notifyBoxView.gameObject.SetActive(true);
            seq.Play();
        }
      
        public void OnPointerClick(PointerEventData eventData)
        {
            ShowNotifyBox();
        }


    }
}