using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common; 


namespace Common
{
    [System.Serializable]
    public class InteractionSpriteData
    {
        public int interactionNo; 
        public List<Sprite> allSprites = new List<Sprite>(); 
    }


    [CreateAssetMenu(fileName = "DialogueSO", menuName = "Dialogue Service/DialogueSO")]
    public class DialogueSO : ScriptableObject
    {
        // inkle text asset       
        public int DiaID;   // on Click track this ID
        public DialogueNames dialogueName;

        public string dialogueTitle = "";
        [Header("Auto Fill")]
        public NPCNames npcName;
        public CharNames charName;
        public bool isLocked;
        public bool isRepeatable;
        public bool isSkippable; 

        public GameState gameState;
        public List<CharNames> charInDialogue = new List<CharNames>();
        public List<NPCNames> NPCinDialogue = new List<NPCNames>();
        public LocationName locationName;

        [Header("INKY text asset")]
        public TextAsset dialogueAsset; 
        
        [Header(" Interation sprites")]
        public List<InteractionSpriteData> interactSprites = new List<InteractionSpriteData>();
    }



}

