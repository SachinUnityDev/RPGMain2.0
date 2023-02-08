using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
namespace Common
{
    public class BuildingPtrEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
       
        /// <summary>
        /// Button HL deprecate 
        /// get plank data directly from the BuildingSO 
        /// IBuild SO from the SO
        /// 
        /// </summary>
        

        [SerializeField] BuildingNames buildingName;
        [SerializeField] BuildingSO buildSO;
        TownViewController townViewController;

        [Header("Image")]
        [SerializeField] Image buildImg;


        [Header("Global Var")]
        [SerializeField] TimeState timeState;
        [SerializeField] bool isSelected; 
        private void Awake()
        {
            buildImg = transform.GetComponent<Image>();
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
          //  Debug.LogError("Build name" + buildSO.buildingData.buildingName);
                 
            if (timeState == TimeState.Day)
            {
                buildImg.sprite = buildSO.buildingData.buildExtDayN;
            }
            else
            {
                buildImg.sprite = buildSO.buildingData.buildExtDayN;
            }
        }
        void SetSpriteHL()
        {
            if (timeState == TimeState.Day)
            {
                buildImg.sprite = buildSO.buildingData.buildExtDayHL;
            }
            else
            {
                buildImg.sprite = buildSO.buildingData.buildExtDayHL;
            }
        }


        public void OnSelect()
        {

            SetSpriteHL();
        }
        public void OnDeSelect()
        {
            isSelected = false; 
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            // select this build
            int index = transform.GetSiblingIndex();
            townViewController.OnBuildSelect(index); 
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

        void Start()
        {
            buildImg = GetComponent<Image>();
            buildImg.alphaHitTestMinimumThreshold = 0.1f;
        }

      
    }



}

