using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common.Dialogue
{
    [CreateAssetMenu(fileName = "DialogueDatabase", menuName = "Dialogue System/Database")]
    public class DialogDatabase : ScriptableObject
    {


        public List<Dialogue> dialogueDB = new List<Dialogue>();



    }
}
