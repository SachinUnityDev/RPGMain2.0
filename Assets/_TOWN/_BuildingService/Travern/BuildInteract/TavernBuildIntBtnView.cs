using System.Collections;
using System.Collections.Generic;
using Common; 
using UnityEngine;



namespace Town
{


    public class TavernBuildIntBtnView : MonoBehaviour
    {
        [SerializeField] Transform btnContainer;

        [SerializeField] BuildInteractType buildInteractType;
        TavernView tavernView;
        TavernModel tavernModel;
        AllBuildSO allbuildSO;
        private void Awake()
        {
            btnContainer = transform.GetChild(0);
        }

        public void InitInteractBtns(TavernView tavernView)
        {
            this.tavernView = tavernView;
            tavernModel = BuildingIntService.Instance.tavernController.tavernModel;
            allbuildSO = BuildingIntService.Instance.allBuildSO;
            FillHouseBtns();
        }

        void FillHouseBtns()
        {
            int i = 0;
            foreach (BuildIntTypeData buildData in tavernModel.buildIntTypes)
            {
                if (buildData.isUnLocked)
                {
                    btnContainer.GetChild(i).gameObject.SetActive(true);
                    InteractionSpriteData interactSprite = allbuildSO.GetInteractData(buildData.BuildIntType);
                    btnContainer.GetChild(i).GetComponent<TavernBtnPtrEvents>().TavernIntInit(buildData, interactSprite, tavernView);
                    i++;
                }
            }
            for (int j = i; j < btnContainer.childCount; j++)
            {
                btnContainer.GetChild(i).gameObject.SetActive(false);
            }
        }

    }
}
