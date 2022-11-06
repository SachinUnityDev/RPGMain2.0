using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Common.Dialogue
{

    [CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue System/Dialogue")]
    public class Dialogue : ScriptableObject
    {
        public int dialogueID = -1;
        public DialogueLines[] dialogueLines;

        public void Awake()
        {
            Debug.Log("Awake");
        }
    }



    [System.Serializable]
    public class DialogueLines
    {
        public Character character;
        public Question question;
        [TextArea(4, 5)]
        public string text;
    }
    [System.Serializable]
    public class Question
    {
        public string questionTxt;
        public string[] answers;

    }
}
