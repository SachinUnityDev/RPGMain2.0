using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


namespace Town
{
    public class TrophyView : MonoBehaviour, IPanel
    {
        [Header("Transform")]
        [SerializeField] Transform fameTrans;
        public Transform selectPageTrans;
        public Transform scrollPageTrans;

        [Header("Exit btn")]
        [SerializeField] Button exitBtn; 

        [Header("Global variables")]
        [SerializeField] TavernModel tavernModel; 
        private void Start()
        {
            exitBtn.onClick.AddListener(OnExitBtnPressed);
           
        }
        void OnExitBtnPressed()
        {
            UnLoad();
        }
        void InitOptsPage()
        {
            selectPageTrans.GetComponent<SelectPageView>().InitSelectPage(this); 
        }
        void InitFameSelect()
        {
            fameTrans.GetComponent<DisplayFame>().Display(); 
        }
        public void DisplaySelectPage()
        {
            selectPageTrans.gameObject.SetActive(true);
            scrollPageTrans.gameObject.SetActive(false); 
           InitOptsPage();
        }
        public void DisplayScrollPage()
        {
            selectPageTrans.gameObject.SetActive(false);
            scrollPageTrans.gameObject.SetActive(true);
        }
        public void Init()
        {
            DisplaySelectPage();
            InitFameSelect();
            InitOptsPage();         
        }

        public void Load()
        {

        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
        }

    }
}