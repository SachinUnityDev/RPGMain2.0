using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;
using UnityEngine.UI;

namespace Town
{


    public class MarketInteractBtnView : MonoBehaviour
    {
        [SerializeField] Transform btnContainer;

        [SerializeField] BuildInteractType buildInteractType;
        MarketView marketView;
        MarketModel marketModel;
        AllBuildSO allbuildSO;
        private void Awake()
        {
            btnContainer = transform.GetChild(0);
        }

        public void InitInteractBtns(MarketView marketView)
        {
            this.marketView = marketView;
            marketModel = BuildingIntService.Instance.marketController.marketModel;
            allbuildSO = BuildingIntService.Instance.allBuildSO;
            FillHouseBtns();
        }

        void FillHouseBtns()
        {
            int i = 0;
            foreach (BuildIntTypeData buildData in marketModel.buildIntTypes)
            {
                if (buildData.isUnLocked)
                {
                    btnContainer.GetChild(i).gameObject.SetActive(true);
                    InteractionSpriteData interactSprite = allbuildSO.GetInteractData(buildData.BuildIntType);
                    btnContainer.GetChild(i).GetComponent<MarketBtnPtrEvents>().MarketBtnInit(buildData, interactSprite, marketView);
                    i++;
                }
            }
            for (int j = i; j < btnContainer.childCount; j++)
            {
                btnContainer.GetChild(j).gameObject.SetActive(false);
            }
            if (i == 0)
                btnContainer.GetComponent<Image>().enabled = false;
            else
                btnContainer.GetComponent<Image>().enabled = true;

        }
    }
}