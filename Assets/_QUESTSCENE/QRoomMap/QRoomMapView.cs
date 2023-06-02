using Common;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Quest
{
    public class QRoomMapView : MonoBehaviour, IPanel
    {
        public Transform mapImgTrans;
        public Button bgBtn; 

       
        public void Init()
        {
           
        }


        public void Load()
        {
            UIControlServiceGeneral.Instance.TogglePanel(bgBtn.gameObject, true); 
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(bgBtn.gameObject, false);

        }

        private void Start()
        {
            UnLoad();      
            bgBtn.onClick.AddListener(UnLoad); 
        }
    }
}