using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class LoadView : MonoBehaviour, IPanel
    {



        [SerializeField] LoadSlotsView loadSlotsView; 
            
        [SerializeField] ProfilePgView profilePgView; 
             
        void Start()
        {

        }
        public void OnProfileBtnClicked(List<GameModel> allGameModelsInProfile)
        {
            // toggle to load view 
            SetLoadSlotViewActive();
            loadSlotsView.Init(allGameModelsInProfile); 
        }   

        public void OnLoadSlotBtnClicked(GameModel gameModel)
        {
            // load the game 
            GameService.Instance.LoadGame(gameModel);
            UnLoad();
        }

        public void InitOnLoad()
        {   
            SetProfilePgActive();
            profilePgView.Init(this);        
        }
        public void SetProfilePgActive()
        {
            profilePgView.gameObject.SetActive(true);
            loadSlotsView.gameObject.SetActive(false);
        }
        public void SetLoadSlotViewActive()
        {
            profilePgView.gameObject.SetActive(false);
            loadSlotsView.gameObject.SetActive(true);
        }

        public void Load()
        {
            UIControlServiceGeneral.Instance.TogglePanel(this.gameObject, true);
            InitOnLoad();
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(this.gameObject, false);
        }

        public void Init()
        {

        }
    }
}
