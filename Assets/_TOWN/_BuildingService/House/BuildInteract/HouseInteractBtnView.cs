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
         HouseModel houseModel;
        AllBuildSO allbuildSO;
        private void Awake()
        {
            btnContainer = transform.GetChild(0);
        }

       public void InitInteractBtns(HouseView houseView)
        {
            this.houseView = houseView;
            houseModel = BuildingIntService.Instance.houseController.houseModel;
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