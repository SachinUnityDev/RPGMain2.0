using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public class MapEView : MonoBehaviour, IPanel
    {
        public Transform mainPage;
        public Transform resultPage;

        MapEbase mapEBase;
        MapEModel mapEModel;
        private void Awake()
        {
            
        }
        public void Init()
        {
            Load();
        }

        public void Load()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, true);
        }
        public void ShowMainPage()
        {
            mainPage.gameObject.SetActive(true);
            resultPage.gameObject.SetActive(false);
        }
        public void ShowResultPage()
        {
            mainPage.gameObject.SetActive(false);
            resultPage.gameObject.SetActive(true);
            resultPage.GetComponent<ResultPgMapView>().InitResultPage(this, mapEBase, mapEModel);
        }
        public void InitEncounter(MapEModel mapEModel)
        {
            this.mapEModel = mapEModel;
            mapEBase = EncounterService.Instance.mapEController
                        .GetMapEBase(mapEModel.mapEName);
            mainPage.GetComponent<MainPgMapView>().InitMainPage(this, mapEBase, mapEModel);
            ShowMainPage();
            Load();
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
        }
    }
}