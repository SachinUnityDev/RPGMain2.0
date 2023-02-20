using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Town;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
namespace Common
{
    public class BuildBasePtrEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
        , IPointerClickHandler
    {
            
        public BuildingNames buildingName;

        [Header("So and View: NTBR")]
        [SerializeField] BuildingSO buildSO;
        TownViewController townViewController;

        [Header("BuildState")]
        public BuildingState buildState;

        [Header("Image")]
        [SerializeField] Image buildImg;


        [Header("Global Var")]
        [SerializeField] TimeState timeState;
        [SerializeField] bool isSelect = false; 
        private void Awake()
        {
            buildImg = transform.GetComponent<Image>();
        }
        void Start()
        {
            buildImg = GetComponent<Image>();
            buildImg.alphaHitTestMinimumThreshold = 0.1f;
        }
        public void Init(TownViewController townViewController)
        {
            this.townViewController = townViewController;
            buildSO = TownService.Instance.allbuildingSO.GetBuildSO(buildingName);
            timeState = CalendarService.Instance.currtimeState;
            SetSpriteNormal();     
        }

        void SetSpriteNormal()
        {
            if (timeState == TimeState.Night)
            {
                buildImg.sprite = buildSO.buildingData.buildExtNight;
            }
            else
            {
                buildImg.sprite = buildSO.buildingData.buildExtDayN;                
            }
        }
        void SetSpriteHL()
        {
            if (timeState == TimeState.Night)
            {
                buildImg.sprite = buildSO.buildingData.buildExtNightHL;
            }
            else
            {                
                buildImg.sprite = buildSO.buildingData.buildExtDayHL;
            }
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
            // select this build
           // int index = transform.GetSiblingIndex();
            townViewController.OnBuildSelect(buildingName);
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (townViewController == null) return;
            if (townViewController.selectBuild != BuildingNames.None) return; 
            SetSpriteHL();  
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (townViewController == null) return;
            if (townViewController.selectBuild != BuildingNames.None) return;
            SetSpriteNormal(); 
        }

        #endregion

    }



}

