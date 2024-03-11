using Common;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
namespace Town
{


    public class BuildBtnPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] BuildInteractType buildInteractType;
        [SerializeField] BuildView buildView;
        [SerializeField] Transform panel;
        public void BuildIntInit(BuildIntTypeData buildData, InteractionSpriteData spriteData, BuildView buildView)
        {
            this.buildView = buildView;
            transform.GetComponent<Image>().sprite = spriteData.spriteN;
            buildInteractType = buildData.BuildIntType;
            panel = buildView.GetBuildInteractPanel(buildInteractType);
            transform.DOScale(1.0f, 0.25f); 
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            UIControlServiceGeneral.Instance.TogglePanelOnInGrp(panel.gameObject, true);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOScale(1.15f, 0.25f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(1.0f, 0.25f);
        }

        void Start()
        {

        }
    }
}