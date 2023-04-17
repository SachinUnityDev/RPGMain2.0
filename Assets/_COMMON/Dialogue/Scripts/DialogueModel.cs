using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class DialogueModel
    {
        public int DiaID;  
        public DialogueNames dialogueName;

        public string dialogueTitle = "";
        [Header("Auto Fill")]
        public NPCNames npcName;
        public CharNames charName;
        public bool isLocked;
        public bool isRepeatable;

        public GameState gameState;
        public List<CharNames> charInDialogue = new List<CharNames>();
        public List<NPCNames> NPCinDialogue = new List<NPCNames>();
        public LocationName locationName;

        public DialogueModel (DialogueSO diaSO)
        {
            dialogueName = diaSO.dialogueName;
            dialogueTitle = diaSO.dialogueTitle;
            npcName = diaSO.npcName;
            charName = diaSO.charName;
            isLocked = diaSO.isLocked;
            isRepeatable = diaSO.isRepeatable;

            gameState = diaSO.gameState;
            charInDialogue = diaSO.charInDialogue.DeepClone();
            NPCinDialogue = diaSO.NPCinDialogue.DeepClone(); 
            locationName =diaSO.locationName;
        }
    }
}