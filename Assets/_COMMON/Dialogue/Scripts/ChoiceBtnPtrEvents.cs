using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 

namespace Common
{
    public class ChoiceBtnPtrEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        DialogueView dialogueView;
        int index; 
        private void Start()
        {
            index = gameObject.transform.GetSiblingIndex();
            dialogueView = DialogueService.Instance.dialogueView; 
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            dialogueView.OnChoicePtrEnter(index); 
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            dialogueView.OnChoicePtrExit(index);
        }
    }



}
