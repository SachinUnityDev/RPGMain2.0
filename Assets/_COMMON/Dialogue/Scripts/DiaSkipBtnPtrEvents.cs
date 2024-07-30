using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Common
{


    public class DiaSkipBtnPtrEvents : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            if (dialogueView != null)
            {
                dialogueView.EndDialogue();
            }
        }

        DialogueView dialogueView;
        public void Init(DialogueView dialogueView)
        {
            this.dialogueView = dialogueView;
        }
    }
    
}