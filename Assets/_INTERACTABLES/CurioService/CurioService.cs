using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

namespace Quest
{
    public class CurioService : MonoSingletonGeneric<CurioService>
    {
        public AllCurioSO allCurioSO;
        public CurioController curioController;
        [Header("Curio canvas View")]
        public CurioView curioView;

        [Header("Curio Factory")]
        public CurioFactory curioFactory;

        private void OnEnable()
        {
            curioFactory = GetComponent<CurioFactory>();
            curioController= GetComponent<CurioController>();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "QUEST")
            {
                curioView = FindObjectOfType<CurioView>(true);

                
            }
        }


        public void InitCurioService()
        {
            curioController.InitCurioController(allCurioSO); 
        }



    }


}


