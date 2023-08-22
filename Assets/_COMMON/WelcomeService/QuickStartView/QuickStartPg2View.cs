using Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Intro
{
    public class QuickStartPg2View : MonoBehaviour
    {
        [Header("TBR")]
        public TextMeshProUGUI descTxt;
        [SerializeField] Transform containerImg;

        [Header("Global Var")]
        QuickStartView quickStartView;
        DialogueSO dialogueSO;
        DialogueModel dialogueModel;

        public void QuickStartPg2Init(QuickStartView quickStartView)
        {
            this.quickStartView = quickStartView;
            dialogueSO = DialogueService.Instance.GetDialogueSO(DialogueNames.DebtIsClear);
            dialogueModel = DialogueService.Instance.GetDialogueModel(DialogueNames.DebtIsClear);
            dialogueModel.isPlayedOnce = true; 
            InteractionSpriteData InteractSprites = dialogueSO.interactSprites[0];
            int i = 0;
            foreach (Transform child in containerImg)
            {
                child.GetComponent<Pg2OptsPtrEvents>().Init(InteractSprites.allSprites[i], this);
            }
        }

    }
}