using Combat;
using Common;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Quest
{
    public class CurioBarkController : MonoBehaviour
    {
        public QbarkView qbarkView;
        private void OnEnable()
        {
            
            SceneManager.activeSceneChanged += OnSceneLoaded;
        }
        private void OnDisable()
        {
            SceneManager.activeSceneChanged -= OnSceneLoaded;
        }
        void OnSceneLoaded(Scene oldScene, Scene newScene)
        {
            if (newScene.name == "QUEST")
            {
                qbarkView = FindObjectOfType<QbarkView>(true);
            }
        }
        public void ShowCurioBark(CurioNames curioName, CurioColEvents curioColEvents)
        {
            CurioBarkData curioBarkData = BarkService.Instance.allBarkSO.GetCurioBarkData(curioName);
            
            BarkCharData barkCharData = new BarkCharData(CharNames.Abbas
                                            , curioBarkData.barkline, curioBarkData.VOaudioClip);

            qbarkView.InitCurioBark(barkCharData, curioColEvents, curioBarkData.UIaudioClip);            
        }
    }
}