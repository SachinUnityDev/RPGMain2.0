using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace Town
{
    public class BuildBaseEvents : MonoBehaviour
    {
        [Header("to be filled")]
        [SerializeField] BuildInteractType buildIntType; 

        [Header("To be ref Base")]
        [SerializeField] Sprite dayN;
        [SerializeField] Sprite dayHL;
        [SerializeField] Sprite nightN;
        [SerializeField] Sprite nightHL;

        [Header("To be ref Base")]
        [SerializeField] Sprite dayNUp;
        [SerializeField] Sprite dayHLUp;
        [SerializeField] Sprite nightNUp;
        [SerializeField] Sprite nightHLUp;


        [Header("Not be ref")]
        Image btnImg;
        public BuildView buildView;
        public BuildingModel buildModel;
        TimeState timeState;
        [SerializeField] bool isBuildIntUpgraded = false; 

        void Awake()
        {
            btnImg = GetComponent<Image>();
            btnImg.alphaHitTestMinimumThreshold = 0.1f;
        }

        public virtual void Init(BuildView buildView, BuildingModel buildModel)
        {
            this.buildView = buildView;
            this.buildModel = buildModel;
            timeState = CalendarService.Instance.currtimeState;
            isBuildIntUpgraded = buildModel.IsBuildIntUpgraded(buildIntType);
            SetSpriteN();
        }
        protected void SetSpriteN()
        {
            if (timeState == TimeState.Day)
            {
                if (isBuildIntUpgraded)
                    btnImg.sprite = dayNUp;
                else
                    btnImg.sprite = dayN;
            }
            else
            {
                if (isBuildIntUpgraded) 
                    btnImg.sprite = nightNUp;
                else
                    btnImg.sprite = nightN;
            }
                
        }
        protected void SetSpriteHL()
        {
            if (timeState == TimeState.Day)
            {
                if (isBuildIntUpgraded)
                    btnImg.sprite = dayHLUp;
                else
                    btnImg.sprite = dayHL;
            }
            else
            {
                if (isBuildIntUpgraded)
                    btnImg.sprite = nightHLUp;
                else
                    btnImg.sprite = nightHL;
            }               
        }


    }
}