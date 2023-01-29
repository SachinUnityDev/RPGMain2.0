using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common; 
namespace Town
{

    public interface ITalkable
    {
        // public NPCNames NPCName { get;  }

        // List<DialogueNames> dialogueName = new List<DialogueNames>();

        void DisplayDialogueOptPanel(); 
        void OnDialogueSelect(int option);// dialogue play will start
        void InitDialogueList();  // Get from NPC SO townService
        void RemoveDialogue(); // On quest completed or target achieved remove this
    }

  
    public interface ITradeable
    {



    }


    public class SoothSayerController : MonoBehaviour, ITalkable
    {
        // remove dialogues etc to subscriptions only

        public void DisplayDialogueOptPanel()
        {

        }

        public void InitDialogueList()
        {

        }

        public void OnDialogueSelect(int option)
        {

        }

        public void RemoveDialogue()
        {

        }

        void Start()
        {

        }

    }
}


