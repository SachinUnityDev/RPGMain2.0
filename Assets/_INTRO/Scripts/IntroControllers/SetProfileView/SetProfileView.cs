using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Intro
{
    public class SetProfileView : MonoBehaviour, IPanel
    {

        [SerializeField] ContinueBtnSetProf continueBtnSetProf;
        [SerializeField] Transform container;
        [Header(" Slot Select")]
        [SerializeField] int slotSelect; 
        private void Start()
        {
            
        }
        public void Init()
        {
           
        }
        public void SetClickSlot(int slotId)
        {
            int i = 0; 
            foreach (Transform child in container)
            {
                if(i == slotId)
                {
                    slotSelect= i;
                    child.GetComponent<SetProfilePtrEvents>().SetClickedState();
                }                    
                else
                {
                    child.GetComponent<SetProfilePtrEvents>().SetUnclickedState();
                }                    
                i++;
            }
        }

        public void OnContinuePressed()
        {
            if (slotSelect == -1) return;
            string profileStr = container.GetChild(slotSelect).GetComponent<SetProfilePtrEvents>().profileTxt;
            if (profileStr == string.Empty)
                profileStr = $"Profile {slotSelect+1}"; 
            GameService.Instance.CreateNewGame(slotSelect, profileStr); 
            UnLoad(); 
        }
        void InitProfiles()
        {
            int i = 0;
            foreach (Transform child in container)
            {
                GameModel gameModel = GameService.Instance.GetGameModel(i);
                child.GetComponent<SetProfilePtrEvents>().Init(gameModel, this);
                i++;
            }
            continueBtnSetProf.Init(this);
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