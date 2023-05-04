using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quest
{
    public class MainPgMapView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI heading;
        [SerializeField] TextMeshProUGUI desc;
        [SerializeField] Button choiceABtn;
        [SerializeField] Button choiceBBtn;


        MapEView mapEView;
        MapEbase mapBase;
        MapEModel mapEModel;

        void Start()
        {
            choiceABtn.onClick.AddListener(OnChoiceAPressed);
            choiceBBtn.onClick.AddListener(OnChoiceBPressed);
        }
        void OnChoiceAPressed()
        {
            mapBase.OnChoiceASelect();
            mapEView.ShowResultPage();
        }
        void OnChoiceBPressed()
        {
            mapBase.OnChoiceBSelect();
            mapEView.ShowResultPage();
        }
        public void InitMainPage(MapEView mapEView, MapEbase mapBase, MapEModel mapEModel)
        {
            this.mapEView = mapEView;
            this.mapBase = mapBase;
            this.mapEModel = mapEModel;

            FillPage();
            FillBtnStr();
        }
        void FillPage()
        {
            heading.text = mapEModel.mapEName.ToString().CreateSpace();
            desc.text = mapEModel.descTxt;
        }

        void FillBtnStr()
        {
            choiceABtn.GetComponentInChildren<TextMeshProUGUI>().text = mapEModel.choiceAStr;
            choiceBBtn.GetComponentInChildren<TextMeshProUGUI>().text = mapEModel.choiceBStr;
        }


    }
}