using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Common.Dialogue
{
    public class DialogueCtrlDeprecated : MonoBehaviour
    {

        public Dialogue dialogue; // SO 
        public Text DialogueTxt;
        public Text QuestionTxt;
        public GameObject answerPanel;
        public Button ansBtnPrefab;
        int lineCounter; // Dialogue counter
                         // Start is called before the first frame update
        void Start()
        {
            // StartDialogue(); 
        }

        public void hello()
        {
            Debug.Log("hello");
        }

        void StartDialogue()
        {
            if (lineCounter < dialogue.dialogueLines.Length)
            {
                DialogueLines currentLine = dialogue.dialogueLines[lineCounter];
                if (currentLine.question.questionTxt != "")
                {
                    QuestionTxt.text = currentLine.question.questionTxt.ToString();

                    foreach (var ans in currentLine.question.answers)
                    {
                        Button ansButton = Instantiate(ansBtnPrefab, Vector3.zero, Quaternion.identity, transform);
                        ansButton.transform.SetParent(answerPanel.transform);
                        ansButton.GetComponentInChildren<Text>().text = ans.ToString();

                    }
                    ShowPanel(false, true);

                }
                else  // simple dialogue 
                {
                    string dialogStr = (dialogue.dialogueLines[lineCounter].character.name
                       + ":  " + dialogue.dialogueLines[lineCounter].text).ToString();
                    Debug.Log(dialogStr);
                    DialogueTxt.text = dialogStr.ToString();
                    ShowPanel(true, false);

                }
                ++lineCounter;
            }
        }

        void ShowPanel(bool isDialogue, bool isQues)
        {
            QuestionTxt.transform.parent.gameObject.SetActive(isQues);
            DialogueTxt.transform.parent.gameObject.SetActive(isDialogue);
        }







        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {

                StartDialogue();
            }
        }
        public void DialogueResponse1()
        {


        }


    }
}
