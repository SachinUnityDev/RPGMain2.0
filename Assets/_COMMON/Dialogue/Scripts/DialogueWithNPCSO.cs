using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    [CreateAssetMenu(fileName = "DialogueWithNPCSO", menuName = "Dialogue Service/DialogueWithNPCSO")]

    public class DialogueWithNPCSO : ScriptableObject
    {
        [Header("Owned By")]
        public CharNames charName;
        public NPCNames npcName;
        //public Sprite clickedSprite;
        //public Sprite unClickedSprite; 

        public List<DialogueSO> allDialogueSOs = new List<DialogueSO>();// to be removed 
        public List<DialogueDataSO> allDialogueSO = new List<DialogueDataSO>();
        private void Awake()
        {
            foreach (DialogueSO diaSO in allDialogueSOs)
            {
                diaSO.npcName = npcName;
                diaSO.charName = charName;
                //diaSO.clickedSprite = clickedSprite;
                //diaSO.unClickedSprite = unClickedSprite;

            }


        }

    }

    [Serializable]
    public class DialogueDataSO
    {
        public DialogueSO dialogueSO;
        public bool isLocked;
        public bool isRepeatable;
    }
}

