using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 

namespace Common
{
    public class ChoiceBtnPtrEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        DialogueView dialogueViewController1;
        int index; 
        private void Start()
        {
            index = gameObject.transform.GetSiblingIndex();
            dialogueViewController1 = DialogueService.Instance.dialogueView; 
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
