using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Common
{
    public class DialogueViewController1 : MonoBehaviour
    {
        //tags
        public static event Action<Story> OnCreateStory;  // redundant for INKLE EDITOR 
        [Header("Tags")]
        const string INTERACTION_TAG = "interaction";
        const string CUSTOMINT_TAG = "customint";
        const string CUSTOMCC_TAG = "customchoice";
        const string SPEAKER_TAG = "speaker";
        const string TEXTBOX_TAG = "textbox";

        const string DEFINE_TAG = "define";


        [Header("Dialogue Panel Ref")]
        [SerializeField] GameObject LowerDialogueParent;
        [SerializeField] GameObject dialogueParent;
        [SerializeField] GameObject choiceParent;
        [SerializeField] GameObject topOptions;
        [SerializeField] GameObject textBoxParent;
        [SerializeField] GameObject defineParent;

        [SerializeField] TextMeshProUGUI charTxt;

        [Header("Portrait ref")]
        [SerializeField] GameObject leftPortrait;
        [SerializeField] GameObject rightPortrait;

        // global vvariables 
        [SerializeField] DialogueSO dialogueSO;
        [SerializeField] Story story;
        TextRevealer textRevealer;

        [Header("Global variables")]
        [SerializeField] float fastRevealTime = 0.01f;
        [SerializeField] float slowRevealTime = 4f;
        [SerializeField] int escapeCount = 0;
        [SerializeField] bool isDialoguePlaying = false;
        [SerializeField] bool isDialogueOnHighSpeed = false;
        [SerializeField] bool isInteracting = false;
        [SerializeField] bool isTextBoxing = false;

        [SerializeField] int currStrLen;

        [SerializeField] int ChoicesLen = 0;
        [Header("calculated Variables")]
        [SerializeField] TextMeshProUGUI dialogueTxt;
        [SerializeField] Button fastFwdBtn;

        private void OnEnable()
        {
            dialogueTxt = dialogueParent.GetComponentInChildren<TextMeshProUGUI>();
            fastFwdBtn = dialogueParent.transform.GetChild(1).GetComponent<Button>();
            textRevealer = dialogueTxt.GetComponent<TextRevealer>();
            isDialoguePlaying = false;
            fastFwdBtn.onClick.AddListener(FastFwdPressed);
        }
        public void StartStory(DialogueSO _dialogueSO)
        {
            dialogueSO = _dialogueSO;
            story = new Story(dialogueSO.dialogueAsset.text);
            if (OnCreateStory != null) OnCreateStory(story);
            InitDialogueView();
            DisplayStory();
        }

        void InitDialogueView()
        {
            escapeCount = 1;
            isDialoguePlaying = false;
        }

        void DisplayStory()
        {
            if (story == null) return;
            if (story.canContinue)  // dialogue stops when a new tag is detected
            {

                if (escapeCount > 0)
                {
                    isInteracting = false; isTextBoxing = false;
                    string dialogueString = story.Continue();
                    escapeCount--; // transistion cut
                    currStrLen = dialogueString.Length;
                    ToggleDialoguePanelOn(true);
                    if (escapeCount > 1)
                        SetHighTypeSpeed(currStrLen);
                    else
                        SetLowTypeSpeed(currStrLen);
                    // toogle on Display String 
                    dialogueTxt.text = dialogueString;
                    SpeakerTags();
                    textRevealer.Reveal();
                    dialogueTxt.text = "";
                    //Debug.Log("INSIDE PRINT DIALOGUE");
                }
                else
                {
                    Debug.Log("Dialogue is paused");
                }
                if (story.currentChoices.Count >= 1)
                    DisplayChoices();
              //  CustomInteractTag();
            }
        }

        void DisplayChoices()
        {
            List<Choice> currentChoices = story.currentChoices;
            ToggleDialoguePanelOn(false);
            Transform choiceContainer = choiceParent.transform.GetChild(0);
            InteractTag();


            if (currentChoices.Count > 4)
            {
                Debug.LogError("More choices were given than the UI can support. Number of choices given: "
                    + currentChoices.Count);
            }
            int index = 0;
            foreach (Choice choice in currentChoices)
            {
                Button choiceBtn = choiceContainer.GetChild(index).GetComponent<Button>();
                if (choiceBtn == null) return;

                choiceBtn.gameObject.SetActive(true);
                choiceBtn.GetComponentInChildren<TextMeshProUGUI>().text = choice.text;

                choiceBtn.onClick.AddListener(() => OnChoiceClick(choice));

                InteractionSpriteData InteractSprites = dialogueSO.interactSprites
                                         .Find(t => t.interactionNo ==
                                         DialogueService.Instance.currInteraction.numTag);
                if (InteractSprites != null)
                {
                    topOptions.SetActive(true);
                    Button spriteBtn = topOptions.transform.GetChild(index).GetComponent<Button>();
                    spriteBtn.gameObject.SetActive(true);
                    spriteBtn.GetComponent<Image>().sprite
                        = InteractSprites.allSprites[index];
                    spriteBtn.onClick.AddListener(() => OnChoiceClick(choice));

                }
                index++;

            }
            for (int i = index; i < topOptions.transform.childCount; i++)
            {
                topOptions.transform.GetChild(i).gameObject.SetActive(false);
            }

            for (int i = index; i < choiceContainer.childCount; i++)
            {
                choiceContainer.GetChild(i).gameObject.SetActive(false);
            }
        }
        //void CustomInteractTag()
        //{
        //    List<string> tags = story.currentTags;

        //    foreach (var tag in tags)
        //    {
        //        // get key and value 
        //        string[] splitTag = tag.Split(':');
        //        string keyTag = splitTag[0].Trim().ToLower();
        //        string numTagStr = splitTag[1].Trim();
        //        int numTag = int.Parse(numTagStr);
        //        string valueTag = " ";
        //        if (splitTag.Length > 2)
        //            valueTag = splitTag[2].Trim();
        //        TagData tagData = new TagData(keyTag, numTag, valueTag);
        //        if (keyTag == CUSTOMINT_TAG)
        //        {
        //            DialogueService.Instance.currController
        //                            .ApplyInteraction(1, 2f);
        //            isTextBoxing = true;

        //        }

        //    }
        //}
            void CustomChoiceFX()
            {

            }


            void InteractTag()
            {
                List<string> tags = story.currentTags;

                foreach (var tag in tags)
                {
                    // get key and value 
                    string[] splitTag = tag.Split(':');
                    string keyTag = splitTag[0].Trim().ToLower();
                    string numTagStr = splitTag[1].Trim();
                    int numTag = int.Parse(numTagStr);
                    string valueTag = " ";
                    if (splitTag.Length > 2)
                        valueTag = splitTag[2].Trim();
                    TagData tagData = new TagData(keyTag, numTag, valueTag);

                    if (keyTag == INTERACTION_TAG)
                    {
                        DialogueService.Instance.currInteraction = tagData;
                        DialogueService.Instance.ClearAllList();
                        isInteracting = true;                  

                    }
                    if (keyTag == DEFINE_TAG)
                    {
                        DialogueService.Instance.allDefine.Add(tagData);
                    }
                    if (keyTag == TEXTBOX_TAG)
                    {
                        isTextBoxing = true;
                        textBoxParent.SetActive(true);
                        textBoxParent.GetComponentInChildren<TextMeshProUGUI>().text
                                            = valueTag;
                    }
                    if (keyTag == CUSTOMINT_TAG)
                    {
                        isTextBoxing = true;
                        DialogueService.Instance.currController
                                            .ApplyInteraction(1, 2f);
                    }
                }
            }

            void OnChoiceClick(Choice choice)
            {
                //Debug.Log("Index" + choice.index);
                //Wait(2f);
                story.ChooseChoiceIndex(choice.index);
                    DialogueService.Instance.currController
                                        .ApplyChoices(choice.index, 2f);

            // can also be set from outside as in spritePanel
            escapeCount += 2;
                RemoveListener();
                DisplayStory();

            }

            void RemoveListener()
            {
                foreach (Transform child in choiceParent.transform.GetChild(0))
                {
                    Button btn = child.GetComponent<Button>();
                    btn.onClick.RemoveAllListeners();
                }

            }

            IEnumerator Wait(float time)
            {
                yield return new WaitForSeconds(time);
            }

            void FastFwdPressed()
            {
                // search thru the intract tag and isInteracting stage then print the display tag
                while (story.canContinue && (!isInteracting || !isTextBoxing))
                {
                    InteractTag();
                    story.Continue();
                }

                if (isInteracting || isTextBoxing)
                    DisplayStory();
            }
            public void OnChoicePtrEnter(int index)
            {
                TagData tagData = DialogueService.Instance.allDefine.Find(t => t.numTag == index + 1);
                if (tagData == null) return;
                string displayTxt = tagData.valueTag;

                defineParent.SetActive(true);
                defineParent.GetComponentInChildren<TextMeshProUGUI>().text = displayTxt;


            }

            public void OnChoicePtrExit(int index)
            {
                defineParent.SetActive(false);
                defineParent.GetComponentInChildren<TextMeshProUGUI>().text = "";

            }

            void SpeakerTags()
            {
                List<string> tags = story.currentTags;

                foreach (var tag in tags)
                {
                    // get key and value 
                    string[] splitTag = tag.Split(':');
                    string keyTag = splitTag[0].Trim().ToLower();
                    string numTagStr = splitTag[1].Trim();
                    int numTag = int.Parse(numTagStr);
                    string valueTag = " ";
                    if (splitTag.Length > 2)
                        valueTag = splitTag[2].Trim().ToLower();

                    if (keyTag == SPEAKER_TAG && numTag == 1)  //left speaker 
                    {
                        SetPortrait(leftPortrait, valueTag);

                        TogglePortrait(leftPortrait, true);
                        TogglePortrait(rightPortrait, false);
                        charTxt.text = valueTag;
                        Debug.Log("TAG DETAILS" + keyTag + "NUMBER" + numTag + "VALUE" + valueTag);
                    }
                    else if (keyTag == SPEAKER_TAG)  // right speaker
                    {
                        SetPortrait(rightPortrait, valueTag);

                        TogglePortrait(leftPortrait, false);
                        TogglePortrait(rightPortrait, true);
                        charTxt.text = valueTag;
                        Debug.Log("TAG DETAILS" + keyTag + "NUMBER" + numTag + "VALUE" + valueTag);
                    }
                }
            }

            // CONNECTED TO TEXT REVEALER EVENTS 
            public void OnDialogueStart()
            {
                isDialoguePlaying = true;
            }
            public void OnDialogueEnd()
            {

                isDialoguePlaying = false;
                DisplayStory();
            }

            void SetHighTypeSpeed(int dist)
            {
                escapeCount--; // high speed cut
                textRevealer.RevealTime = 0.1f;
                isDialogueOnHighSpeed = true;
            }
            void SetLowTypeSpeed(int dist)
            {
                textRevealer.RevealTime = slowRevealTime * dist / 100;
                isDialogueOnHighSpeed = false;
            }

            void ToggleDialoguePanelOn(bool isDialogueOn)
            {

                GameObject dialoguePanel = LowerDialogueParent.transform.GetChild(0).gameObject;
                GameObject choicePanel = LowerDialogueParent.transform.GetChild(1).gameObject;

                if (isDialogueOn)
                {
                    dialoguePanel.SetActive(true);
                    charTxt.gameObject.SetActive(true);

                    choicePanel.SetActive(false);
                    topOptions.SetActive(false);
                    textBoxParent.SetActive(false);

                }
                else
                {
                    choicePanel.SetActive(true);

                    dialoguePanel.SetActive(false);
                    charTxt.gameObject.SetActive(false);
                }
            }

            void SetPortrait(GameObject portrait, string name)
            {
                Sprite[] sprites = GetSprite(name);
                portrait.transform.GetChild(0).GetComponent<Image>().sprite = sprites[0];
                portrait.transform.GetChild(1).GetComponent<Image>().sprite = sprites[1];

            }

            Sprite[] GetSprite(string name)
            {
                //can populate a dictionary here for the names found ... else search 
                string nameStr = name.Trim().ToLower();
                Sprite[] sprites = new Sprite[2];
                for (int i = 1; i < Enum.GetNames(typeof(CharNames)).Length; i++)
                {
                    string nameEnum = ((CharNames)i).ToString();
                    if (nameEnum.Trim().ToLower().Contains(nameStr))
                    {
                        sprites = DialogueService.Instance.GetDialogueSprites((CharNames)i, NPCNames.None);
                    }
                }
                for (int i = 1; i < Enum.GetNames(typeof(NPCNames)).Length; i++)
                {
                    string nameEnum = ((NPCNames)i).ToString();
                    if (nameEnum.Trim().ToLower().Contains(nameStr))
                    {
                        sprites = DialogueService.Instance.GetDialogueSprites(CharNames.None, (NPCNames)i);
                    }
                }
                return sprites;
            }
            void TogglePortrait(GameObject portGo, bool toggle)
            {
                if (toggle)
                {
                    portGo.transform.GetChild(0).gameObject.SetActive(true);
                    portGo.transform.GetChild(1).gameObject.SetActive(false);
                }
                else
                {
                    portGo.transform.GetChild(0).gameObject.SetActive(false);
                    portGo.transform.GetChild(1).gameObject.SetActive(true);
                }
            }
            private void Update()
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    escapeCount++;
                    if (!isDialoguePlaying)  // start a new dialogue only when its not playing
                        DisplayStory();
                    else if (!isDialogueOnHighSpeed)
                        SetHighTypeSpeed(currStrLen);

                }
                if (story != null)
                    ChoicesLen = story.currentChoices.Count;


            }


        

    }
}


