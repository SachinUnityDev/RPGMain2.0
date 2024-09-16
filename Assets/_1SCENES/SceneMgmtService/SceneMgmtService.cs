using System;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Intro;


namespace Common
{
    public class SceneMgmtService : MonoSingletonGeneric<SceneMgmtService>
    {
        //    private CancellationTokenSource cts;

        //public Scene currScene;
        //public Scene newScene;
        public SceneMgmtController sceneMgmtController;

        private void OnEnable()
        {
            sceneMgmtController = GetComponent<SceneMgmtController>();
        }

    }
}

