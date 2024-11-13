using Common;
using DG.Tweening;
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
           // Load();
        }

        public void Load()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, true);
          //  gameObject.SetActive(true);
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
        }

        public void InitEncounter(MapEModel mapEModel, PathModel pathModel)
        {
            this.mapEModel = mapEModel;
            this.mapEModel.pathModel = pathModel;  
            mapEBase = EncounterService.Instance.mapEController.GetMapEBase(mapEModel.mapEName);
            EncounterService.Instance.mapEController.SetMapEBaseAsCurrent(mapEBase);
            mainPage.GetComponent<MainPgMapView>().InitMainPage(this, mapEBase, mapEModel);
            ShowMainPage();
            Load();
        }

        public void LoadEncounterResult(MapEbase mapEBase)  // to be called from iResult
        {
            mapEModel = mapEBase.mapEModel; 
            this.mapEBase = mapEBase;
         
            resultPage.GetComponent<ResultPgMapView>().InitResultPage(this, mapEBase, mapEModel);
            ShowResult2Page();
            Sequence loadSeq = DOTween.Sequence();
            loadSeq
                .AppendInterval(2.0f)  // delay added to ensure MapView is loaded before MapEView 
                .AppendCallback(() => ShowResult2Page())
                .AppendCallback(() => Load())
               // .AppendCallback(() => GameService.Instance.currGameModel.gameScene = GameScene.InMapInteraction) // correction as scene load overrides this "InTown" due to timing issue 

                ;

            loadSeq.Play(); 
        }

        public void UnLoad()
        {   
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
        }
    }
}