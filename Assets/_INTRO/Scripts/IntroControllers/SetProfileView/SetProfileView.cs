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
        void SetState_Plank(int slotId)
        {
            int i = 0;
            foreach (Transform child in container)
            {
                if (i == slotId)
                {
                    slotSelect = i;
                    child.GetComponent<SetProfilePtrEvents>().SetClickedState();
                    if (child.GetComponent<SetProfilePtrEvents>().gameModel != null)
                    {
                        SetStateContinueBtn(true);
                    }
                    else
                    {
                        SetStateContinueBtn(false);
                    }  
                    InitProfiles();
                }
                else
                {
                    child.GetComponent<SetProfilePtrEvents>().SetUnclickedState();
                }
                i++;
            }
        }

        public void OnNewProfileSelected()
        {
            int i = 0; bool errorDsply = false; 
            foreach (Transform child in container)
            {
                if (i == slotSelect) continue; 
                SetProfilePtrEvents setProfilePtrEvents = child.GetComponent<SetProfilePtrEvents>();
                if (setProfilePtrEvents.gameModel == null) continue; 
                if(setProfilePtrEvents.gameModel.GetProfileName() 
                    == container.GetChild(slotSelect).GetComponent<SetProfilePtrEvents>().profileTxt)
                {
                    container.GetChild(slotSelect).GetComponent<SetProfilePtrEvents>().ToggleErrorTxt(true);
                    errorDsply = true;
                }                   
                 if(!errorDsply)
                    container.GetChild(slotSelect).GetComponent<SetProfilePtrEvents>().ToggleErrorTxt(false);
            }
            if (!errorDsply)
            {
                SetStateContinueBtn(true);
            }                
        }
        void SetStateContinueBtn(bool clickable)
        {
            if (slotSelect == -1) return; 
            string profileStr = container.GetChild(slotSelect).GetComponent<SetProfilePtrEvents>().profileTxt;
            if (profileStr == string.Empty)
                continueBtnSetProf.SetState(!clickable);
            else
                continueBtnSetProf.SetState(clickable);    
        }
        public void SetClickSlot(int slotId)
        {
            SetState_Plank(slotId);
            // chk if plank has profile 
            // show notification if has profile on click yes clear the gameModel and create a new one

          
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
                GameModel gameModel = GameService.Instance.GetCurrentGameModel(i);
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