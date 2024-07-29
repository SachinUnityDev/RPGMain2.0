using Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
namespace Intro
{
    public class QuickStartPg1View : MonoBehaviour
    {
        [Header("TBR")]
        public TextMeshProUGUI descTxt;
        [SerializeField] Transform imgContainer; 

        [Header("Global Var")]
        QuickStartView quickStartView;
        DialogueSO dialogueSO;
        DialogueModel dialogueModel;
        public void QuickStartPg1Init(QuickStartView quickStartView)
        {
            this.quickStartView = quickStartView;
            dialogueSO = DialogueService.Instance.GetDialogueSO(DialogueNames.MeetKhalid);
            //dialogueModel = DialogueService.Instance.GetDialogueModel(DialogueNames.MeetKhalid);
            //dialogueModel.isPlayedOnce = true;
            InteractionSpriteData InteractSprites = dialogueSO.interactSprites[0];
            int i = 0; 
            foreach (Transform child in imgContainer)
            {          
                child.GetComponent<Pg1OptsPtrEvents>().Init(InteractSprites.allSprites[i], this, quickStartView);
                i++; 
            }
        }

    }
}