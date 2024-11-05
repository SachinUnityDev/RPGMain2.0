using Common;
using System.Collections;
using System.Collections.Generic;
using Town;
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
        public void ShowResult2Page()
        {
            mainPage.gameObject.SetActive(false);
            resultPage.gameObject.SetActive(true);
            resultPage.GetComponent<ResultPgMapView>().InitResultPage(this, mapEBase, mapEModel);
        }

        public void InitEncounter(MapEModel mapEModel, PathModel pathModel)
        {
            this.mapEModel = mapEModel;
            this.mapEModel.pathModel = pathModel;  
            mapEBase = EncounterService.Instance.mapEController.GetMapEBase(mapEModel.mapEName);

            mainPage.GetComponent<MainPgMapView>().InitMainPage(this, mapEBase, mapEModel);
            ShowMainPage();
            Load();
        }

        public void LoadEncounter(MapEbase mapEBase)  // to be called from iResult
        {
            // check on pathModel which node was checked and then load the result page of MapEbase
            mapEModel = mapEBase.mapEModel; 
            this.mapEBase = mapEBase;
            mainPage.gameObject.SetActive(false);
            resultPage.gameObject.SetActive(true);
            resultPage.GetComponent<ResultPgMapView>().InitResultPage(this, mapEBase, mapEModel);
            Load();
        }

        public void UnLoad()
        {   
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);

        }
    }
}