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
        public SelectPageView selectPageView;
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
        public void OnItemWalled(Iitems item)
        {
            // if filled perform a swap 
            ITrophyable itrophy = item as ITrophyable; 
            if (itrophy.tavernSlotType == TavernSlotType.Trophy)
            {
                selectPageView.trophyslot.AddItem(item); 
            }
            if (itrophy.tavernSlotType == TavernSlotType.Pelt)
            {
                selectPageView.peltSlot.AddItem(item);
            }
            
            DisplaySelectPage(); 
        }

        void InitOptsPage()
        {
            selectPageView.InitSelectPage(this); 
        }
        void InitFameSelect()
        {
            fameTrans.GetComponent<DisplayFame>().Display(); 
        }
        public void DisplaySelectPage()
        {
            selectPageView.gameObject.SetActive(true);
            scrollPageTrans.gameObject.SetActive(false); 
        }
        public void DisplayScrollPage()
        {
            selectPageView.gameObject.SetActive(false);
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
            Init();
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
        }

    }
}