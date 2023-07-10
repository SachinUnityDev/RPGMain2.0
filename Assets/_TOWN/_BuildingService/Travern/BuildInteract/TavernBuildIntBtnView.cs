using System.Collections;
using System.Collections.Generic;
using Common; 
using UnityEngine;
using UnityEngine.UI;


namespace Town
{


    public class TavernBuildIntBtnView : BuildInteractBtnView
    {
        //[SerializeField] Transform btnContainer;

        //[SerializeField] BuildInteractType buildInteractType;
        //TavernView tavernView;
        //[SerializeField] TavernModel tavernModel;
        //AllBuildSO allbuildSO;
        //private void Awake()
        //{
        //    btnContainer = transform.GetChild(0);
        //}

        //public void InitInteractBtns(TavernView tavernView)
        //{
        //    this.tavernView = tavernView;
        //    tavernModel = BuildingIntService.Instance.tavernController.tavernModel;
        //    allbuildSO = BuildingIntService.Instance.allBuildSO;
        //    FillTavernBtns();
        //}

        //void FillTavernBtns()
        //{
        //    int i = 0;
        //    foreach (BuildIntTypeData buildData in tavernModel.buildIntTypes)
        //    {
        //        if (buildData.isUnLocked)
        //        {
        //            btnContainer.GetChild(i).gameObject.SetActive(true);
        //            InteractionSpriteData interactSprite = allbuildSO.GetInteractData(buildData.BuildIntType);
        //            btnContainer.GetChild(i).GetComponent<TavernBtnPtrEvents>().TavernIntInit(buildData, interactSprite, tavernView);
        //            i++;
        //        }
        //    }
        //    for (int j = i; j < btnContainer.childCount; j++)
        //    {
        //        btnContainer.GetChild(j).gameObject.SetActive(false);
        //    }
        //    if (i == 0)
        //        btnContainer.GetComponent<Image>().enabled = false;
        //    else
        //        btnContainer.GetComponent<Image>().enabled = true;
        //}

    }
}
