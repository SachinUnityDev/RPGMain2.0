using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;


namespace Town
{
    public class GuitarPtrEvents : HouseBaseEvents, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public override void Init(BuildView buildView, BuildingModel buildModel)
        {
            base.Init(buildView, buildModel);
            if (buildModel.IsBuildIntUnLocked(BuildInteractType.Music))
            {
                gameObject.SetActive(true);

            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (buildModel.IsBuildIntUnLocked(BuildInteractType.Music))
            {
        
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (buildModel.IsBuildIntUnLocked(BuildInteractType.Music))
            {
                SetSpriteHL();
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (buildModel.IsBuildIntUnLocked(BuildInteractType.Music))
            {
                SetSpriteN();
            }
        }
    }
}
