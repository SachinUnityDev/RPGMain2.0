using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class CodexService : MonoSingletonGeneric<CodexService>
    {
        public CodexViewController codexViewController;
        public CodexSO codexSO;


        void Start()
        {

        }

        public void OpenCodexPanel()
        {
            codexViewController.GetComponent<IPanel>().Init();
            codexViewController.GetComponent<IPanel>().Load(); 
        }

        public void CloseCodexPanel()
        {
            codexViewController.GetComponent<IPanel>().UnLoad();
        }

    }
}

