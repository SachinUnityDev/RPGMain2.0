using Common;
using Interactables;
using System.Collections;
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

        [Header("Gloabl variables")]
        [SerializeField] TavernSlotType tavernSlotType;

        private void Start()
        {
            exitBtn.onClick.AddListener(OnExitBtnPressed);
            tavernSlotType = TavernSlotType.Pelt; 
        }
        void OnExitBtnPressed()
        {
            UnLoad();
        }
        public void InitSelectPage(TavernSlotType tavernSlotType)
        {
            this.tavernSlotType = tavernSlotType;
            scrollPageTrans.GetComponent<TrophyScrollPagePtrEvents>().InitScrollPage(this, tavernSlotType);
        }
        void InitOptsPage()
        {
            selectPageTrans.GetComponent<SelectPagePtrEvents>().InitSelectPage(this); 
        }
        void InitFameSelect()
        {
            fameTrans.GetComponent<DisplayFame>().Display(); 
        }
        public void DisplaySelectPage()
        {
            selectPageTrans.gameObject.SetActive(true);
            scrollPageTrans.gameObject.SetActive(false); 
        }
        public void DisplayScrollPage()
        {
            selectPageTrans.gameObject.SetActive(false);
            scrollPageTrans.gameObject.SetActive(true);
        }
        public void Init()
        {
            InitFameSelect();
            InitOptsPage();
            InitSelectPage(tavernSlotType);
        }

        public void Load()
        {

        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Init(); // for test

            }
        }
    }
}