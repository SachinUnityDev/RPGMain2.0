using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

namespace Common
{

    public interface INotify
    {
        NotifyName notifyName { get; set; }
        bool isDontShowItAgainTicked { get; set; }
        void OnNotifyAnsPressed(); 
    }

    public class NotifyBoxView : MonoBehaviour, IPanel 
    {
        [Header("Buttons TBR")]
        [SerializeField] Button continueBtn;
        [SerializeField] Button exitBtn;
        [Header("Txt TBR")]

        [SerializeField] TextMeshProUGUI notifytxt;
        [SerializeField] Toggle toggleUI; 

        [SerializeField] NotifyModel notifyModel;
        [SerializeField] NotifyName notifyName;
        INotify notify;
        void Start()
        {
            continueBtn.onClick.AddListener(OnContinueBtnPressed); 
            exitBtn.onClick.AddListener(OnCloseBtnPressed);
            toggleUI.onValueChanged.AddListener(OnTogglePressed);
            UnLoad();
        }
         
        void OnTogglePressed(bool toggle)
        {
            notifyModel.isDontShowAgainTicked = toggle;
            
        }
        public void OnShowNotifyBox(INotify notify, NotifyName notifyName)
        {
            this.notify= notify;
            this.notifyName = notifyName;
           
            notifyModel = UIControlServiceGeneral.Instance.notifyController.GetNotifyModel(notifyName);
            if (notifyModel.isDontShowAgainTicked)
            {
                OnContinueBtnPressed(); return; 
            }
            NotifyView();
            Load();
        }

        void NotifyView()
        {
            notifytxt.text = notifyModel.notifyStr;
        }

        public void OnContinueBtnPressed()
        {
            notify.OnNotifyAnsPressed();
            UnLoad(); 
        }
        void OnCloseBtnPressed() 
        {
            UnLoad(); 
        }

        public void Load()
        {
            toggleUI.isOn = false;
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, true);
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
        }

        public void Init()
        {
        }
    }
}