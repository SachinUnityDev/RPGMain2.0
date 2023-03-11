using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Town
{
    public class BountyBoardView : MonoBehaviour, IPanel
    {
        [SerializeField] Button exitBtn;
        [SerializeField] Transform bountyContainer;
        [Header("Pages")]
        [SerializeField] Transform countyLsPage;
        [SerializeField] Transform confirmPage; 


        private void Awake()
        {
            exitBtn.onClick.AddListener(OnExitBtnPressed); 
        }
        void OnExitBtnPressed()
        {

        }

        void FillBountyBoardLs()
        {
            // get the ls from the SO 
        }
        void OnConfirmPressed()
        {

        }
        void OnReturnPressed()
        {

        }



        public void Init()
        {
          
        }

        public void Load()
        {
          
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
        }
    }
}