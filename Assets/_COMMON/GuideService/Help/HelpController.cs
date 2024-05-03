using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class HelpController : MonoBehaviour
    {
        [Header("TBR")]
        public AllHelpSO allHelpSO;        
        public GameObject helpViewPreFab;

        public GameObject helpViewGO;
        public HelpName helpName; 

        void Start()
        {

        }

        public void ShowHelp()
        {
            // get last help panel open from UiControl Service
            HelpName helpName = UIControlServiceGeneral.Instance.helpName; 
            if(helpName != HelpName.None)
            {
                ShowHelpView(helpName);
            }
        }


        public void ShowHelpView(HelpName _helpName)
        {
            HelpSO helpSO = allHelpSO.GetHelpSO(_helpName);
            if (this.helpName == _helpName)
            {
                RemoveHelpView();
            }
            if (GameService.Instance.currGameModel.gameState == GameScene.None)
                return;

            GameObject canvasGo = null;
            this.helpName = _helpName;

            if (GameService.Instance.currGameModel.gameState == GameScene.InTown)
            {                
                canvasGo = GameObject.FindGameObjectWithTag("Canvas");                
            }
            if (GameService.Instance.currGameModel.gameState == GameScene.InQuestRoom)
            {
                canvasGo = GameObject.FindGameObjectWithTag("QuestCanvas");
            }

            helpViewGO = Instantiate(helpViewPreFab);
            helpViewGO.transform.SetParent(canvasGo.transform);
            helpViewGO.transform.SetAsLastSibling();
            RectTransform helpRect = helpViewGO.GetComponent<RectTransform>();

            helpRect.anchorMin = new Vector2(0, 0);
            helpRect.anchorMax = new Vector2(1, 1);
            helpRect.pivot = new Vector2(0.5f, 0.5f);
            helpRect.localScale = Vector3.one;
            helpRect.offsetMin = new Vector2(0, 0); // new Vector2(left, bottom);
            helpRect.offsetMax = new Vector2(0, 0); // new Vector2(-right, -top);
            helpViewGO.GetComponent<HelpView>().InitHelp(_helpName, helpSO, this);
        }
        public void RemoveHelpView()
        {
            if(helpViewGO != null) 
            {
                Destroy(helpViewGO, 0.4f);
                helpName = HelpName.None; 
            }
            

        }
    }
}