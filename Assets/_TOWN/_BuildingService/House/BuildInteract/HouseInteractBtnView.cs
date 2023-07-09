using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Town
{

    public class HouseInteractBtnView : MonoBehaviour
    {
        [SerializeField] Transform btnContainer;
  
        [SerializeField] BuildInteractType buildInteractType;
         HouseView houseView;
        BuildingModel houseModel;
        AllBuildSO allbuildSO;
        private void Awake()
        {
            btnContainer = transform.GetChild(0);
        }

       public void InitInteractBtns(HouseView houseView, BuildingModel houseModel)
        {
            this.houseView = houseView;
            this.houseModel = houseModel;
            allbuildSO = BuildingIntService.Instance.allBuildSO;
            FillHouseBtns();
        }

        void FillHouseBtns()
        {
            int i = 0;
            foreach (BuildIntTypeData buildData in houseModel.buildIntTypes)
            {
                if (buildData.isUnLocked)
                {
                    btnContainer.GetChild(i).gameObject.SetActive(true);
                    InteractionSpriteData interactSprite = allbuildSO.GetInteractData(buildData.BuildIntType);
                    btnContainer.GetChild(i).GetComponent<HouseBtnPtrEvents>().HouseIntInit(buildData, interactSprite, houseView);
                    i++;
                }
            }
            for (int j = i; j < btnContainer.childCount; j++)
            {
                btnContainer.GetChild(j).gameObject.SetActive(false);
            }
            if (i == 0)            
                btnContainer.GetComponent<Image>().enabled= false;            
            else            
                btnContainer.GetComponent<Image>().enabled = true;
            
        }
    

    }
}