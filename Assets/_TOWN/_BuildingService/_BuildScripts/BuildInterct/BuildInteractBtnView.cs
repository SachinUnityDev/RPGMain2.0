using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;
using UnityEngine.UI;

namespace Town
{
    public class BuildInteractBtnView : MonoBehaviour
    {
        [SerializeField] Transform btnContainer;

        [SerializeField] BuildInteractType buildInteractType;
        BuildView buildView;
        BuildingModel buildModel;
        AllBuildSO allbuildSO;
       

        public void InitInteractBtns(BuildView buildView, BuildingModel buildModel)
        {
            this.buildView = buildView;
            this.buildModel = buildModel;
            allbuildSO = BuildingIntService.Instance.allBuildSO;
            FillBuildIntBtns();
        }

        void FillBuildIntBtns()
        {
            int i = 0;
            foreach (BuildIntTypeData buildData in buildModel.buildIntTypes)
            {
                if (buildData.isUnLocked)
                {
                    btnContainer.GetChild(i).gameObject.SetActive(true);
                    InteractionSpriteData interactSprite = allbuildSO.GetInteractData(buildData.BuildIntType);
                    btnContainer.GetChild(i).GetComponent<BuildBtnPtrEvents>().BuildIntInit(buildData, interactSprite, buildView);
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