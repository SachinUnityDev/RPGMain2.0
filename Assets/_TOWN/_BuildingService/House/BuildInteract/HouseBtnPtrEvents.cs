using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace Town
{
    public class HouseBtnPtrEvents : BuildBtnPtrEvents
    {
   

    }
}

//[SerializeField] BuildInteractType buildInteractType;
//[SerializeField] BuildView buildView;
//[SerializeField] Transform panel; 
//public void HouseIntInit(BuildIntTypeData buildData, InteractionSpriteData spriteData, BuildView buildView)
//{
//    this.buildView= buildView;
//    transform.GetComponent<Image>().sprite = spriteData.spriteN;
//    buildInteractType = buildData.BuildIntType;
//    panel = buildView.GetBuildInteractPanel(buildInteractType);
//}

//public void OnPointerClick(PointerEventData eventData)
//{
//    UIControlServiceGeneral.Instance.TogglePanelOnInGrp(panel.gameObject, true);
//}

//public void OnPointerEnter(PointerEventData eventData)
//{

//}

//public void OnPointerExit(PointerEventData eventData)
//{

//}

//void Start()
//{

//}