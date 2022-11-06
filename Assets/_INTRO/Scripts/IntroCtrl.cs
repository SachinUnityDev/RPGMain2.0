using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Video;

using Combat;
using TMPro;
using Spine.Unity;
using DG.Tweening;

namespace Common
{               // DEPRECATED ......................


    public class IntroCtrl : MonoSingletonGeneric<IntroCtrl>
    {
        [Header("INTRO PANEL Buttons")]
        public Button newGame;
        public Button loadGame;
        public Button exitGame;
        //[SerializeField] Button credits;
        //[SerializeField] Button options;
        public  Button extras;

        [Header("Enten and Emesh GO")]
        [SerializeField] GameObject Enten;
        [SerializeField] GameObject Emesh;

        [Header("Pop up Panels")]
        [SerializeField] GameObject savePanel;

        [Header("PANELS")]
        [SerializeField] GameObject MainMenu;
        [SerializeField] GameObject NewGameMode;
        [SerializeField] GameObject DifficultyPanel;
        [SerializeField] GameObject StoryPanel;
        [SerializeField] GameObject cutScenePanel;
        [SerializeField] GameObject GatePanel;
        [SerializeField] List<GameObject> allPanels = new List<GameObject>();

        [Header("New game Buttons")]
        [SerializeField] GameObject UphillBtn;
        [SerializeField] GameObject campaignBtn;


        [Header("Video player")]
        [SerializeField] VideoPlayer video; 

        [Header("GameDifficulty Panel")]
        [SerializeField] Button continueBtn;

        [Header("GATE Click to continue")]
        [SerializeField] GameObject textMesh;


        Vector3 initPos = new Vector3(0f, -215f, 0f);
        Vector3 finalPosLeft = new Vector3(-700, 227, 0);
        Vector2 finalPosRight = new Vector3(700, 227, 0);

        void Start()
        {
            newGame.onClick.AddListener(NewGame);
            loadGame.onClick.AddListener(LoadGame);
            exitGame.onClick.AddListener(ExitGame);          
            extras.onClick.AddListener(Extras);

            campaignBtn.GetComponent<Button>().onClick.AddListener(CampaignBtnPressed);

            continueBtn.onClick.AddListener(DiffPanelContinueBtnPressed);
            allPanels.AddRange(new List<GameObject>() { MainMenu, NewGameMode, DifficultyPanel, StoryPanel, cutScenePanel, GatePanel });
          InitUI();
           
        }

        void InitUI()
        {
            MoveUI(initPos, campaignBtn);
            MoveUI(initPos, UphillBtn);
            video.gameObject.SetActive(false);
           // Fade(DifficultyPanel, 0.0f);
            Fade(NewGameMode, 0.0f);
            Fade(StoryPanel, 0.0f);
            Fade(cutScenePanel, 0.0f);
            Fade(MainMenu, 0.0f);
            Fade(GatePanel, 1.0f);
            EntenNEmeshToggleSetState(false);
            GateSpineAnimSetState(true);
        }
        public void RollBack2MainMenu()
        {
            Fade(MainMenu, 1.0f);

            Color color = Enten.GetComponentInChildren<SkeletonAnimation>().Skeleton.GetColor();
            color.a = 1.0f;
            Debug.Log("Alpha value" );
            Enten.GetComponentInChildren<SkeletonAnimation>().Skeleton.SetColor(color);
            Emesh.GetComponentInChildren<SkeletonAnimation>().Skeleton.SetColor(color);

            Fade(NewGameMode, 0.0f);
            MoveUI(initPos, campaignBtn);
            MoveUI(initPos, UphillBtn);
        }

#region MAIN MENU

        void NewGame()
        {

            EntenNEmeshAnim();

            Fade(MainMenu, 0.4f);
            Color color = Enten.GetComponentInChildren<SkeletonAnimation>().Skeleton.GetColor();
            color.a = 0.7f;
            Enten.GetComponentInChildren<SkeletonAnimation>().Skeleton.SetColor(color);
            Emesh.GetComponentInChildren<SkeletonAnimation>().Skeleton.SetColor(color);
           
            Fade(NewGameMode, 1.0f);
            MoveUI(finalPosLeft, campaignBtn);
            MoveUI(finalPosRight, UphillBtn);
          

        }

        void LoadGame()
        {
           
            Debug.Log("LoadGame");
            savePanel.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "LOAD";
            UIControlServiceCombat.Instance.ToggleUIStateScale(savePanel, UITransformState.Open);

           // SceneManager.LoadScene("COMBAT");


        }
        void ExitGame()
        {
            Debug.Log("ExitGame");
        }
        void CreditsDisplay()
        {
            Debug.Log("Credits");
        }
        void Extras()
        {
            Debug.Log("ShowCutScenes");
        }

#endregion

        void MainPanelInit()
        {
            Fade(MainMenu, 1.0f); 
            Fade(GatePanel, 0.0f);

            EntenNEmeshToggleSetState(true);
            GateSpineAnimSetState(false);
        }


        void CampaignBtnPressed()
        {
            Fade(MainMenu, 0.0f);
            Fade(NewGameMode, 0.0f);
            FadeTxt(NewGameMode, 0.0f);
            NewGameMode.SetActive(false);
            Fade(DifficultyPanel, 1.0f);           
            EntenNEmeshAnim();
        }

        void DiffPanelContinueBtnPressed()
        {
           // Fade(DifficultyPanel, 0.0f);
            Fade(MainMenu, 0.0f);
            Fade(NewGameMode, 0.0f);
            Fade(StoryPanel, 1.0f);
            StoryPanel.transform.GetChild(0).gameObject.SetActive(true);
            Enten.transform.DOLocalMoveX(-25, 0.4f);
            Emesh.transform.DOLocalMoveX(25, 0.4f);
            EntenNEmeshAnim();
        }

        public void OnStoryPanelContinuePressed()
        {
            StoryPanel.transform.GetChild(0).gameObject.SetActive(false);
            EntenNEmeshToggleSetState(false);

          
            Fade(StoryPanel, 0.0f);
            Fade(cutScenePanel, 1.0f);
            video.gameObject.SetActive(true);
            video.Play(); 
        }

#region Helpers 

        void EntenNEmeshAnim()
        {
            SkeletonAnimation skeletonAnimation = Enten.GetComponentInChildren<SkeletonAnimation>();

            skeletonAnimation.state.ClearTracks();
            skeletonAnimation.AnimationName = "animation";
            
            SkeletonAnimation skeletonAnimation2 = Emesh.GetComponentInChildren<SkeletonAnimation>();
            skeletonAnimation2.state.ClearTracks();
            skeletonAnimation2.AnimationName = "animation";

        }

        void EntenNEmeshToggleSetState(bool IsGOActive)
        {         
                Enten.SetActive(IsGOActive);
                Emesh.SetActive(IsGOActive);           
        }

        void GateSpineAnimSetState(bool isGOActive)
        {
            GatePanel.transform.GetChild(0).gameObject.SetActive(isGOActive);
            textMesh.SetActive(isGOActive);
        }

        void MoveUI(Vector3 pos, GameObject GO)
        {
            GO.transform.DOLocalMove(pos, 0.8f);
        }

        void Fade(GameObject GO, float alpha)
        {
            if (alpha > 0.5f)
            {
                int childCount = GO.transform.parent.childCount;
                GO.transform.SetSiblingIndex(childCount);
                Debug.Log("Inside the sibling index " + childCount);
               
            }
            Image[] imgs = GO.transform.GetComponentsInChildren<Image>();
            for (int i = 0; i < imgs.Length; i++)
            {
                imgs[i].DOFade(alpha, 1.0f);
                if (imgs[i].GetComponent<Button>() != null)
                {
                    if (alpha > 0.5f)  
                    {                    
                        int childCount = GO.transform.parent.childCount-1; 
                        GO.transform.SetSiblingIndex(childCount);
                      
                        imgs[i].GetComponent<Image>().DOFade(1.0f, 0.2f);
                        imgs[i].GetComponent<Button>().enabled = true;
                    }
                    else
                    {
                        imgs[i].GetComponent<Image>().DOFade(0.0f, 0.2f)
                            .OnComplete(()=> imgs[i].GetComponent<Button>().enabled = false);

                    }
                }

            }
            FadeTxt(GO, alpha);

              Debug.Log("Alpha in fade " + GO.name + "alpha " + alpha);
        }

        void FadeTxt(GameObject GO, float alpha)
        {
            if (alpha < 0.5)
            {
                alpha = 0f; 
            }
            TextMeshProUGUI[] txts = GO.transform.GetComponentsInChildren<TextMeshProUGUI>();
            for (int i = 0; i < txts.Length; i++)
            {
                txts[i].DOFade(alpha, 1.0f);
            }
        }


        #endregion

        //void TogglePanels(GameObject activePanel)
        //{

        //    for (int i = 0; i < allPanels.Count; i++)
        //    {
        //        if(allPanels[i] == activePanel)
        //        {
        //            Fade(activePanel, 1.0f); 
        //        }else
        //        {
        //            Fade(allPanels[i], 0.0f);
        //        }
        //    }           
        //    // bring it fwd make the alpha 1
        //    // rest of panels make them go back 



        //}



        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                video.Stop(); 
            }

            if (Input.GetMouseButtonDown(0))
            {

                MainPanelInit();
            }

        }
    }



}
