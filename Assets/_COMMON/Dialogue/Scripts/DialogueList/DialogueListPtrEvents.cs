using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Common
{


    public class DialogueListPtrEvents : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] DialogueModel diaModel; 
        public void InitDialogueLsPtr(DialogueModel diaModel)
        {
            this.diaModel = diaModel;

        }

        public void OnPointerClick(PointerEventData eventData)
        {
           if(diaModel != null)
            {
                DialogueService.Instance.On_DialogueStart(diaModel.dialogueName); 
            }
        }
    }
}