using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Common
{
    public class TopOptionsBtnEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        DialogueView dialogueViewController1;
        int index;
        private void OnEnable()
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

