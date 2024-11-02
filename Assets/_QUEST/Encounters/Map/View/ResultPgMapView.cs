using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Common;
using UnityEngine.UI;
using Town;

namespace Quest
{
    public class ResultPgMapView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI heading;
        [SerializeField] TextMeshProUGUI resultStr;
        [SerializeField] TextMeshProUGUI buffStr;
        [SerializeField] Button continueBtn;

        MapEView mapEView;
        MapEbase mapEBase;
        MapEModel mapEModel;

        void Start()
        {
            continueBtn.onClick.AddListener(OnContinueBtnPressed);
        }

        public void OnContinueBtnPressed()
        {
            mapEView.GetComponent<IPanel>().UnLoad();
            mapEBase.MapEContinuePressed();
        }

        public void InitResultPage(MapEView mapEView, MapEbase mapEBase, MapEModel mapEModel)
        {
            this.mapEView = mapEView;
            this.mapEBase = mapEBase;
            this.mapEModel = mapEModel;

            FillPage();
        }
        void FillPage()
        {
            heading.text = mapEModel.mapEName.ToString().CreateSpace();
            resultStr.text = mapEBase.resultStr;
            FillBuffStr();
            FillBtnStr();
        }

        void FillBuffStr()
        {
            buffStr.text = mapEBase.strFX;
        }
        void FillBtnStr()
        {
            continueBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";
        }


    }
}