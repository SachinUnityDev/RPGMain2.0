using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Town
{

    public class HouseInteractBtnView : MonoBehaviour
    {
        [SerializeField] Transform btnContainer;

        [SerializeField] BuildInteractType buildInteractType;
        public HouseViewController houseView;
        public HouseModel houseModel; 
        private void Awake()
        {
            btnContainer = transform.GetChild(0);
        }

        void InitInteractBtns(HouseViewController houseView)
        {
            this.houseView = houseView;
            houseModel = BuildingIntService.Instance.houseController.houseModel;
            AllBuildSO allbuildSO = BuildingIntService.Instance.allBuildSO;
            int i = 0; 
            foreach (BuildIntTypeData buildData in houseModel.buildIntTypes)
            {
                if (buildData.isUnLocked)
                {
                    // get sprite from SO populate 
                    InteractionSpriteData interactSprite = allbuildSO.GetInteractData(buildData.BuildIntType);
                    btnContainer.GetChild(i).GetComponent<HouseBtnPtrEvents>().HouseIntInit(buildData, interactSprite, houseView); 
                }


            }

            // house model get interact data for unlocked btns init them
            // init to be done with panel prefab, houseview ref, 

        }



    }
}