using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Common
{
    [CreateAssetMenu(fileName = "AllDialogueSO", menuName = "Dialogue Service/AllDialogueSO")]
    public class AllDialogueSO : ScriptableObject
    {        
        public List<DialogueSO> allDialogues = new List<DialogueSO>();    
        public DialogueSO GetDialogueWithName(DialogueNames dialogueName)
        {
            foreach (DialogueSO dia in allDialogues)
            {
                if (dia.dialogueName == dialogueName)
                {
                    return dia; 
                }
            }
            Debug.Log("Dialogue Name not found" + dialogueName);
            return null;
        }
    }
}