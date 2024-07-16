using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class LoadView : MonoBehaviour, IPanel
    {



        LoadSlotsView loadSlotsView; 
            
        ProfilePgView profilePgView; 
             
        void Start()
        {

        }

       

        public void InitOnLoad()
        {
            loadSlotsView.Init(); 
            profilePgView.Init(this);
        
        }
        public void SetProfilePgActive()
        {

        }
        public void SetLoadSlotViewActive()
        {

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
