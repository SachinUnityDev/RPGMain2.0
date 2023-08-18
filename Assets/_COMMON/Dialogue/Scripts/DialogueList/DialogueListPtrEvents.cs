using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Common
{


    public class DialogueListPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {


        [SerializeField] DialogueModel diaModel;

        [SerializeField] Color colorN;
        [SerializeField] Color colorHL;

        public void InitDialogueLsPtr(DialogueModel diaModel)
        {
            this.diaModel = diaModel;
            gameObject.GetComponent<TextMeshProUGUI>().DOColor(colorN, 0.4f); 
        }

        public void OnPointerClick(PointerEventData eventData)
        {
           if(diaModel != null)
            {
                DialogueService.Instance.On_DialogueStart(diaModel.dialogueName); 
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            gameObject.GetComponent<TextMeshProUGUI>().DOColor(colorHL, 0.4f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            gameObject.GetComponent<TextMeshProUGUI>().DOColor(colorN, 0.4f);
        }
    }
}