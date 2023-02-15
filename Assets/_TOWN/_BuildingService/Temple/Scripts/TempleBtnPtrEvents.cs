using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace Town
{

    public class TempleBtnPtrEvents : MonoBehaviour,IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] BuildInteractType buildInteractType;
        [SerializeField] TempleViewController templeView;
        [SerializeField] Transform panel;
        public void TempleIntInit(BuildIntTypeData buildData, InteractionSpriteData spriteData, TempleViewController templeView)
        {
            transform.GetComponent<Image>().sprite = spriteData.spriteN;
            buildInteractType = buildData.BuildIntType;
            panel = templeView.GetInteractPanel(buildInteractType);
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            UIControlServiceGeneral.Instance.TogglePanelOnInGrp(panel.gameObject, true);
        }
        public void OnPointerEnter(PointerEventData eventData)
        {

        }

        public void OnPointerExit(PointerEventData eventData)
        {

        }

        void Start()
        {

        }
    }
}