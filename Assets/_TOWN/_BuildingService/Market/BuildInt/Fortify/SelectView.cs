using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace Town
{
    public class SelectView : MonoBehaviour
    {
        FortifyMainView fortifyMainView;

        [SerializeField] Button fortifyBtn;
        [SerializeField] Button unSocketBtn;

        [SerializeField] Button exitBtn;

        private void Start()
        {
            fortifyBtn.onClick.AddListener(OnFortifyBtnPressed);
            unSocketBtn.onClick.AddListener(OnUnSocketBtnPressed);
            exitBtn.onClick.AddListener(OnExitBtnPressed);
        }
        void OnExitBtnPressed()
        {
            fortifyMainView.OnExitBtnPressed();
        }
        public void InitMainPage(FortifyMainView fortifyMainView)
        {
            this.fortifyMainView = fortifyMainView;
        }
        void OnUnSocketBtnPressed()
        {
            UIControlServiceGeneral.Instance.TogglePanelOnInGrp(fortifyMainView.UnsocketPanel.gameObject, true);
        }
        void OnFortifyBtnPressed()
        {
            UIControlServiceGeneral.Instance.TogglePanelOnInGrp(fortifyMainView.fortifyPanel.gameObject, true);
        }

    }
}