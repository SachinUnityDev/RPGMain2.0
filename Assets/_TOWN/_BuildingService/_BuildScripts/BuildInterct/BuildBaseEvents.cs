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

        void Awake()
        {
            btnImg = GetComponent<Image>();
            btnImg.alphaHitTestMinimumThreshold = 0.1f;
        }

        public void Init(BuildView buildView, BuildingModel buildModel)
        {
            this.buildView = buildView;
            this.buildModel = buildModel;
            timeState = CalendarService.Instance.currtimeState;
            SetSpriteN();
        }
        protected void SetSpriteN()
        {
            if (timeState == TimeState.Day)
                btnImg.sprite = dayN;
            else
                btnImg.sprite = nightN;
        }
        protected void SetSpriteHL()
        {
            if (timeState == TimeState.Day)
                btnImg.sprite = dayHL;
            else
                btnImg.sprite = nightHL;
        }


    }
}