using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    [Serializable]
    public class DialogueModel
    {
        public int DiaID;  
        public DialogueNames dialogueName;

        public string dialogueTitle = "";
        [Header("Npc or char That owns the dialogue")]
        public NPCNames npcName;
        public CharNames charName;
        [Header("Play restrictions")]
        public bool isUnLocked;
        public bool isRepeatable;
        public bool isSkippable;
        public bool isPlayedOnce; 

        

        public GameScene gameScene;
        public List<CharNames> charInDialogue = new List<CharNames>();
        public List<NPCNames> NPCinDialogue = new List<NPCNames>();
        public LocationName locationName;

        public DialogueModel (DialogueSO diaSO)
        {
            dialogueName = diaSO.dialogueName;
            dialogueTitle = diaSO.dialogueTitle;
            npcName = diaSO.npcName;
            charName = diaSO.charName;
            isUnLocked = diaSO.isUnLocked;
            isRepeatable = diaSO.isRepeatable;
            isSkippable= diaSO.isSkippable;
            isPlayedOnce = false; 

            gameScene = diaSO.gameScene;
            charInDialogue = diaSO.charInDialogue.DeepClone();
            NPCinDialogue = diaSO.NPCinDialogue.DeepClone(); 
            locationName =diaSO.locationName;
        }
    }
}