using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Town
{
    public class TavernBtnPtrEvents : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] BuildInteractType buildInteractType;
        [SerializeField] TavernView tavernView;
        [SerializeField] Transform panel;
        public void TavernIntInit(BuildIntTypeData buildData, InteractionSpriteData spriteData, TavernView tavernView)
        {
            transform.GetComponent<Image>().sprite = spriteData.spriteN;
            buildInteractType = buildData.BuildIntType;
            panel = tavernView.GetInteractPanel(buildInteractType);
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