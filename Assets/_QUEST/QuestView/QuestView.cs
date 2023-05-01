using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public class QuestView : MonoBehaviour, IPanel
    {
        public Transform headerTrans;
        public Transform questBtnTrans;
        public Transform questJournoTrans;
        public Transform objPanel;
        private void Start()
        {
            UnLoad();           
        }
        public void Init()
        {
            Load(); 
        }

        public void Load()
        {
            UIControlServiceGeneral.Instance.TogglePanel(this.gameObject, true); 
            InitBtn();
        }

        void InitBtn()
        {
            foreach (Transform t in questBtnTrans)
            {
                t.GetComponent<QuestBtnPtrEvents>().InitPtrEvents(this);
            }
            questBtnTrans.GetChild(0).GetComponent<QuestBtnPtrEvents>().FillQuestJurno();
        }
        public void UnLoad()
        {   
            UIControlServiceGeneral.Instance.TogglePanel(this.gameObject, false);          
        }
    }
}