using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;
using Common; 
namespace Intro
{
    public class MainMenuController : MonoBehaviour, IPanel
    {
        #region Declarations    
        [Header("MAIN MENU PANEL ")]
        [SerializeField] GameObject mainMenuPanel;
        [SerializeField] Button Continue;
        [SerializeField] Button NewGame;
        [SerializeField] Button LoadGame;
        [SerializeField] Button Extras;
        [SerializeField] Button ExitGame;


        [Header("EXTRA PANEL")]      
        [SerializeField] GameObject extrasMenuPanel;
        public SettingsBtnPtrEvents settings;
        [SerializeField] Button CutScene;
        [SerializeField] Button Credits;
        [SerializeField] Button GameManual;
        [SerializeField] Button ReturnBack;

        [SerializeField] Image abbasSocial;
        [SerializeField] Image demonSocial;
        #endregion
        public void Init()
        {
            foreach (Transform child in NewGame.transform.parent)
            {
                child.GetComponent<Image>().DOFade(0, 0.1f);
            }
        }
        void Start()
        {
            NewGame.onClick.AddListener(OnNewBtnPressed);
            Extras.onClick.AddListener(OnExtrasPressed);
            Continue.onClick.AddListener(OnContinueBtnPressed);
            ReturnBack.onClick.AddListener(OnReturnBackPressed); 
        }

        public void Load()
        {
            gameObject.SetActive(true);
            UIControlServiceGeneral.Instance.ToggleInteractionsOnUI(this.gameObject, true);

            IntroServices.Instance.Fade(gameObject, 1.0f);
            StartCoroutine(Wait(1f));
            UIControlServiceGeneral.Instance.SetMaxSiblingIndex(gameObject);
            IntroServices.Instance.FadeInEntenNEmesh(1.0f, 1f);
            IntroServices.Instance.AnimateEntenNEmesh();
            IntroAudioService.Instance.PlayBGSound(BGAudioClipNames.ShargadMusic);
            settings.Init(this); 
                
        }
        public void UnLoad()
        {
            IntroServices.Instance.Fade(gameObject, 0.4f);
            UIControlServiceGeneral.Instance.ToggleInteractionsOnUI(this.gameObject, false);
            IntroServices.Instance.LoadNext();

        }
        public IEnumerator Wait(float time)
        {
            yield return new WaitForSeconds(time);
            abbasSocial.DOFade(0.4f, 0.1f);
            demonSocial.DOFade(0.4f, 0.1f);
        }


        public void OnNewBtnPressed()
        {         
            UnLoad(); 
        }
        void OnContinueBtnPressed()
        {
            // load the last gamemodel marked as currGameModel
            int index = GameService.Instance.allGameModel.FindIndex(t=>t.isCurrGameModel == true);
            if(index != -1)
            {
                GameService.Instance.LoadGame(GameService.Instance.allGameModel[index]);
                UnLoad(); 
                IntroServices.Instance.LoadLoadingPanel();
            }
            else
            {
                Debug.LogError("No game model found");
            }
        }
    

        /************************EXTRAS ********************************/

        void OnExtrasPressed()
        {
            UIControlServiceGeneral.Instance.TogglePanel(extrasMenuPanel, true);
            UIControlServiceGeneral.Instance.TogglePanel(mainMenuPanel, false);
        }

        void OnReturnBackPressed()
        {
            UIControlServiceGeneral.Instance.TogglePanel(extrasMenuPanel, false);
            UIControlServiceGeneral.Instance.TogglePanel(mainMenuPanel, true);
        }
        public void ToggleContinueBtn(bool isON)
        {
            Continue.gameObject.SetActive(isON);
        }

    }



}
