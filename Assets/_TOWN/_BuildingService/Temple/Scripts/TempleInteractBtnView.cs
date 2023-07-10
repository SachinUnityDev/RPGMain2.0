using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Town
{
    public class TempleInteractBtnView : BuildInteractBtnView
    {

      


    }
}

//[SerializeField] Transform btnContainer;

//[SerializeField] BuildInteractType buildInteractType;
//TempleView templeView;
//TempleModel templeModel; 
//AllBuildSO allbuildSO;
//private void Awake()
//{
//    btnContainer = transform.GetChild(0);
//}

//public void InitInteractBtns(TempleView templeView)
//{
//    this.templeView  = templeView;
//    templeModel = BuildingIntService.Instance.templeController.templeModel; 
//    allbuildSO = BuildingIntService.Instance.allBuildSO;
//    FillTempleBtns();
//}

//void FillTempleBtns()
//{
//    int i = 0;
//    foreach (BuildIntTypeData buildData in templeModel.buildIntTypes)
//    {
//        if (buildData.isUnLocked)
//        {
//            btnContainer.GetChild(i).gameObject.SetActive(true);
//            InteractionSpriteData interactSprite = allbuildSO.GetInteractData(buildData.BuildIntType);
//            btnContainer.GetChild(i).GetComponent<TempleBtnPtrEvents>()
//                            .TempleIntInit(buildData, interactSprite, templeView);
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