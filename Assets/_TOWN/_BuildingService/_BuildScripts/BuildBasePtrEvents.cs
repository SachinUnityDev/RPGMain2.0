using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace Common
{
    public class BuildBasePtrEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
        , IPointerClickHandler
    {
            
        public BuildingNames buildingName;

        [Header("So and View: NTBR")]
        [SerializeField] BuildingSO buildSO;
        [SerializeField] TownViewController townViewController;

        //[Header("BuildState")]
        //public BuildingState buildState;

        [Header("Image")]
        [SerializeField] Image buildImg;


        [Header("Global Var")]
        [SerializeField] TimeState timeState;
        [SerializeField] bool isSelect = false;
        private void Awake()
        {
            buildImg= GetComponent<Image>();
        }

        void Start()
        {         
            buildImg.alphaHitTestMinimumThreshold = 0.1f;
            Hidetxt();
        }
    
        public void Init(TownViewController townViewController)
        {
            this.townViewController = townViewController;
            buildSO = TownService.Instance.allbuildSO.GetBuildSO(buildingName);
            timeState = CalendarService.Instance.currtimeState;
            SetSpriteNormal();     
        }

        void SetSpriteNormal()
        {
            if (timeState == TimeState.Night)
            {
                buildImg.sprite = buildSO.buildExtNight;
            }
            else
            {
                buildImg.sprite = buildSO.buildExtDayN;                
            }
        }
        void SetSpriteHL()
        {
            if (timeState == TimeState.Night)
            {
                buildImg.sprite = buildSO.buildExtNightHL;
            }
            else
            {                
                buildImg.sprite = buildSO.buildExtDayHL;
            }
        }

        void ShowTxt()
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).DOScaleX(1, 0.1f); 
        }
        void Hidetxt()
        {
            transform.GetChild(0).DOScaleX(0, 0.1f);
            transform.GetChild(0).gameObject.SetActive(false);
            
        }
        public void OnSelect()
        {
            isSelect = true; 
            SetSpriteHL();
        }
        public void OnDeSelect()
        {
            isSelect = false;
            SetSpriteNormal();
        }

        #region POINTER EVENTS 
        public void OnPointerClick(PointerEventData eventData)
        {           
            townViewController.OnBuildSelect(buildingName);
            SetSpriteNormal();
            Hidetxt();
         
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (townViewController == null) return;
            if (townViewController.selectBuild != BuildingNames.None) return; 
            SetSpriteHL();  
            ShowTxt();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (townViewController == null) return;
            if (townViewController.selectBuild != BuildingNames.None) return;
            SetSpriteNormal(); 
            Hidetxt();
        }

        #endregion




    }



}

