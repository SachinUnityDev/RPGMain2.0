using Common;
using DG.Tweening;
using Quest;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace Town
{

    public class BountyQPtrEvents : MonoBehaviour, IPointerClickHandler, INotify
    {
        [Header("Bounty")]
        BountyBoardView bountyBoardView;
        [SerializeField] QuestModel questModel;
        [SerializeField] TextMeshProUGUI bountyTxt; 
        
        public bool isDontShowItAgainTicked { get; set; }
        public NotifyName notifyName { get; set; }
        Image img;

        [SerializeField] NotifyBoxView notifyBoxView; 
        private void Start()
        {
            img = GetComponent<Image>();           
        }

        public void InitBountyQ(QuestModel questModel, BountyBoardView bountyBoardView)
        {
            this.bountyBoardView= bountyBoardView;  
            this.questModel = questModel;
            FillBountyTxt();
        }
        void FillBountyTxt()
        {
            if(questModel== null)
            {
                bountyTxt = GetComponent<TextMeshProUGUI>();
                bountyTxt.text = questModel.questNameStr;
            }
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
            questModel.questState = QuestState.UnLocked;
            bountyBoardView.InitBountyBoardLs();
            bountyBoardView.UnLoad(); 
            QuestMissionService.Instance.On_QuestStart(questModel.questName);

        }
        void PosNotify()
        {
            float width = notifyBoxView.GetComponent<RectTransform>().rect.width;
            float height = notifyBoxView.GetComponent<RectTransform>().rect.height;

            GameObject canvas = GameObject.FindWithTag("Canvas");
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
                .Append(notifyBoxView.transform.DOMove(pos, 0.0f))
                .Append(notifyBoxView.transform.GetComponent<Image>().DOFade(1.0f, 0.1f))
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