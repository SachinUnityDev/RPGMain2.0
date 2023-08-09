using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System;

namespace Common
{
    [System.Serializable]
    public class InteractionSpriteData
    {
        public int interactionNo; 
        public List<SpriteOpts> allSprites = new List<SpriteOpts>(); 

        public SpriteOpts GetSprites(int index)
        {
            if(index <= allSprites.Count)
                return allSprites[index];
            else
                return null;
        }
    }
    [Serializable]
    public class SpriteOpts
    {
        public Sprite spriteN;
        public Sprite spriteHL;
        public Sprite spriteNA; 
        public bool isUnlocked; 
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
       
        public bool isUnLocked= false;        
        public bool isRepeatable;
        public bool isSkippable; 

        public GameState gameState;
        public List<CharNames> charInDialogue = new List<CharNames>();
        public List<NPCNames> NPCinDialogue = new List<NPCNames>();
        public LocationName locationName;

        [Header("INKY text asset")]
        public TextAsset dialogueAsset;

        [Header("INKY Text asset: ClassType.Skirmisher")]
        public TextAsset diaAssetSkirmish;

        [Header("INKY Text asset: ClassType.Herbalist")]
        public TextAsset diaAssetHerbalist;

        [Header("INKY Text asset: ClassType.Warden")]
        public TextAsset diaAssetWarden;

        [Header(" Interation sprites")]
        public List<InteractionSpriteData> interactSprites = new List<InteractionSpriteData>();

        public TextAsset GetDialogueAsset(ClassType  classType)
        {
            TextAsset txt = null; 
            switch (classType)
            {
                case ClassType.None:
                    txt = dialogueAsset;break;                                    
                case ClassType.Skirmisher:
                    txt =  diaAssetSkirmish;break;                                               
                case ClassType.Herbalist:
                    txt = diaAssetHerbalist;  break; 
                case ClassType.Warden:
                    txt = diaAssetWarden;break; 
                default:
                    break;
            }
            if(txt != null)
            {
                Debug.Log("Dialogue Inkle file not found" + classType);
                return txt; 
                
            }
            return dialogueAsset;
        }


    }



}

