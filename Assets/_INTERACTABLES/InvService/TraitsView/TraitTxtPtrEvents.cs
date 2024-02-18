using Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening; 

namespace Interactables
{
    public class TraitTxtPtrEvents : MonoBehaviour, IPointerEnterHandler , IPointerExitHandler
    {
        [SerializeField] Vector3 offset = new Vector3(0, 5, 0); 

        [SerializeField] TempTraitModel tempTraitModel;
        [SerializeField] PermaTraitModel permaTraitModel;

        [SerializeField] TextMeshProUGUI text;

        [SerializeField] GameObject tempTraitCardGO;
        [SerializeField] GameObject permaTraitCardGO; 
        public void InitTxt(TempTraitModel tempTraitModel)
        {
            this.tempTraitModel= tempTraitModel;
            permaTraitModel = null; permaTraitCardGO = null;

            text = transform.GetComponent<TextMeshProUGUI>();
            text.text = tempTraitModel.tempTraitName.ToString();
            tempTraitCardGO = TempTraitService.Instance.tempTraitCardGO;
        }

        public void InitTxt(PermaTraitModel permaTraitModel)
        {
            this.permaTraitModel = permaTraitModel;
            tempTraitModel = null; tempTraitCardGO = null;

            text = transform.GetComponent<TextMeshProUGUI>();
             text.text = permaTraitModel.permaTraitName.ToString();

            permaTraitCardGO = PermaTraitsService.Instance.permaTraitGO; 
        }

        public void FillBlank()
        {
            text = transform.GetComponent<TextMeshProUGUI>();
            text.text = ""; 
        }

        private void OnEnable()
        {
            text = transform.GetComponent<TextMeshProUGUI>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(tempTraitCardGO!= null && text.text != "" && tempTraitModel.tempTraitName  != TempTraitName.None) 
            if (!tempTraitCardGO.activeInHierarchy)            
                    ShowTempTraitCard();
            if (permaTraitCardGO != null && text.text != "" && permaTraitModel.permaTraitName != PermaTraitName.None)
                if (!permaTraitCardGO.activeInHierarchy)
                    ShowPermaTraitCard();

        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (tempTraitCardGO != null)
             if (tempTraitCardGO.activeInHierarchy)
                tempTraitCardGO.SetActive(false);
            if (permaTraitCardGO != null)
                if (permaTraitCardGO.activeInHierarchy)
                    permaTraitCardGO.SetActive(false);
        }
        void ShowTempTraitCard()
        {

            float width = tempTraitCardGO.GetComponent<RectTransform>().rect.width;
            float height = tempTraitCardGO.GetComponent<RectTransform>().rect.height;
            GameObject Canvas = GameObject.FindWithTag("Canvas");
            Canvas canvasObj = Canvas.GetComponent<Canvas>();
            Vector3 offSetFinal = (offset + new Vector3(0, height, 0)) * canvasObj.scaleFactor;
            Vector3 pos = transform.position + offSetFinal;
            tempTraitCardGO.GetComponent<Transform>().DOMove(pos, 0.1f);            
            tempTraitCardGO.GetComponent<TraitCardView>().ShowTempTraitCard(tempTraitModel);
            tempTraitCardGO.SetActive(true);
        }
        void ShowPermaTraitCard()
        {

            float width = permaTraitCardGO.GetComponent<RectTransform>().rect.width;
            float height = permaTraitCardGO.GetComponent<RectTransform>().rect.height;
            GameObject Canvas = GameObject.FindWithTag("Canvas");
            Canvas canvasObj = Canvas.GetComponent<Canvas>();
            Vector3 offSetFinal = (offset + new Vector3(0, height, 0)) * canvasObj.scaleFactor;
            Vector3 pos = transform.position + offSetFinal;
            permaTraitCardGO.GetComponent<Transform>().DOMove(pos, 0.1f);
            permaTraitCardGO.GetComponent<TraitCardView>().ShowPermaTraitCard(permaTraitModel);
            permaTraitCardGO.SetActive(true);
        }
    }
}