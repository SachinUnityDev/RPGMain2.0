using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Intro
{
    public class SetProfileLoadView : MonoBehaviour, IPanel
    {
      //  [SerializeField] ContinueBtnSetProf continueBtnSetProf;
        [SerializeField] Transform container;
        [Header(" Slot Select")]
        [SerializeField] int slotSelect;
        private void Start()
        {

        }
        public void Init()
        {

        }
        

      
        void InitProfiles()
        {
            int i = 0;
            foreach (Transform child in container)
            {
                GameModel gameModel = GameService.Instance.GetCurrentGameModel(i);
                child.GetComponent<SetProfileLoadPtrEvents>().Init(gameModel, this);
                i++;
            }            
        }

        public void OnProfileBtnClicked(int profile, GameModel gameModel)
        {
            //set profile unload this panel and load save panel
            //Once selected load the scene in the Town 

            // On Profile Panel (Load) panel u go to the Main Menu Panel





        }

        public void Load()
        {
            InitProfiles();
            gameObject.SetActive(true);
            UIControlServiceGeneral.Instance.ToggleInteractionsOnUI(this.gameObject, true);
            IntroServices.Instance.Fade(gameObject, 1.0f);
            UIControlServiceGeneral.Instance.SetMaxSiblingIndex(gameObject);
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.ToggleInteractionsOnUI(this.gameObject, false);
            IntroServices.Instance.Fade(gameObject, 0.0f);
            IntroServices.Instance.LoadNext();
            gameObject.SetActive(false);

        }
    }
}
