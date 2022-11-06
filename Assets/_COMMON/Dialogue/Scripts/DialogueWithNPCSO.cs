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

        public List<DialogueSO> allDialogueSOs = new List<DialogueSO>();

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
}

