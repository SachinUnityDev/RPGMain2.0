using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 

namespace Common
{
    public class ChoiceBtnPtrEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        DialogueViewController1 dialogueViewController1;
        int index; 
        private void Start()
        {
            index = gameObject.transform.GetSiblingIndex();
            dialogueViewController1 = DialogueService.Instance.dialogueViewController1; 
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            dialogueViewController1.OnChoicePtrEnter(index); 
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            dialogueViewController1.OnChoicePtrExit(index);
        }
    }



}
