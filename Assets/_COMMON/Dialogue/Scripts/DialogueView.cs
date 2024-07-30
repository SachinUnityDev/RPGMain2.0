using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Common
{
    public class DialogueView : MonoBehaviour, IPanel
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

        #region  declarations
        [Header(" Tags")]
        [SerializeField] List<string> tags; 


        [Header("Dialogue Panel Ref")]
        [SerializeField] GameObject LowerDialogueParent;
        [SerializeField] GameObject dialogueParent;
        [SerializeField] GameObject choiceParent;
        [SerializeField] GameObject topOptions;
        [SerializeField] GameObject textBoxParent;
        [SerializeField] GameObject defineParent;
        
        [Header("Dialogue List")]
        [SerializeField] GameObject dialogueList; 
        [SerializeField] TextMeshProUGUI charTxt;

        [Header("Portrait ref")]
        [SerializeField] GameObject leftPortrait;
        [SerializeField] GameObject rightPortrait;

        // global vvariables 
        [SerializeField] DialogueSO dialogueSO;
        [SerializeField] Story story;
        TextRevealer textRevealer;

        [Header("Is DIALOGUE PLAY ON")]
        [SerializeField] bool isDialoguePlaying = false;

        [Header("Global variables")]
        [SerializeField] float fastRevealTime = 0.01f;
        [SerializeField] float slowRevealTime = 4f;
        [SerializeField] int escapeCount = 0;

        
        [SerializeField] bool isDialogueOnHighSpeed = false;
        [SerializeField] bool isInteracting = false;
        [SerializeField] bool isTextBoxing = false;

        [SerializeField] int currStrLen;

        [SerializeField] int ChoicesLen = 0;
        [Header("calculated Variables")]
        [SerializeField] TextMeshProUGUI dialogueTxt;
        [SerializeField] Button fastFwdBtn;

        [Header("Skip Btn")]
        [SerializeField] DiaSkipBtnPtrEvents diaSkipBtnPtrEvents;
        [SerializeField] Button skipBtn;
        [SerializeField] bool IsSkipStory;
        [SerializeField] int intCount = 0;
        #endregion
        private void OnEnable()
        {
            dialogueTxt = dialogueParent.GetComponentInChildren<TextMeshProUGUI>();
           // fastFwdBtn = dialogueParent.transform.GetChild(1).GetComponent<Button>();
            textRevealer = dialogueTxt.GetComponent<TextRevealer>();
            isDialoguePlaying = false;
            IsSkipStory = false;
            diaSkipBtnPtrEvents.Init(this); 
            //fastFwdBtn.onClick.AddListener(FastFwdPressed);
            //skipBtn.onClick.AddListener(OnSkipBtnPressed); 
        }
        private void OnDisable()
        {
            

        }
        //void OnSkipBtnPressed()
        //{
        //    EndDialogue();  
            
        //    Debug.Log("The ENd "); 
        //}
        public void StartStory(DialogueSO _dialogueSO, DialogueModel diaModel)
        {
            dialogueList.SetActive(false);
            dialogueParent.SetActive(true);
            if(!diaModel.isSkippable)
                skipBtn.gameObject.SetActive(false);
            else
                skipBtn.gameObject.SetActive(true);

            dialogueSO = _dialogueSO;
            CharController charController = CharService.Instance.GetAllyController(CharNames.Abbas); 
            story = new Story(dialogueSO.GetDialogueAsset(charController.charModel.classType).text);
            if (OnCreateStory != null) OnCreateStory(story);
            InitDialogueView();
            DisplayStory();
        }
        public void ShowDialogueList(CharNames charName, NPCNames nPCNames)
        {
            Load();
            dialogueList.SetActive(true);
            dialogueParent.SetActive(false);
            DialogueService.Instance.UpdateDialogueState();
            List<DialogueModel> lsModel = 
                        DialogueService.Instance.GetDialogueModel4CharNPC(charName, nPCNames);         
            dialogueList.GetComponent<DialogueListView>().InitDialogueView(lsModel);            
        }
        void InitDialogueView()
        {
            escapeCount = 1;
            isDialoguePlaying = true;
            UIControlServiceGeneral.Instance.BlockEsc(true);
            TogglePortOnOff(leftPortrait,false);
            TogglePortOnOff(rightPortrait,false);
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
                    escapeCount =0; // transistion cut
                    currStrLen = dialogueString.Length;
                    ToggleDialoguePanelOn(true);
                    //if (escapeCount > 1)
                    //    SetHighTypeSpeed(currStrLen);
                    //else
                        SetLowTypeSpeed(currStrLen);
                    // toogle on Display String 
                    dialogueTxt.text = dialogueString;
                    TogglePortraitWithSpeakerTags();
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
            }
            else
            {
                Debug.Log("Dialogue Stopped");
                if(DialogueService.Instance.allDefine.Count ==0 
                    && DialogueService.Instance.allOptions.Count == 0
                     && DialogueService.Instance.allDiverts.Count == 0)
                {
                    EndDialogue();
                }
            }
        }      
        public void EndDialogue()
        {
            isDialoguePlaying = false;
            DialogueService.Instance.dialogueModel.isPlayedOnce = true;
            DialogueService.Instance.dialogueModel.isUnLocked = false;
            if (story != null)
            {
                story.ResetState();
                story = null;
            }
            UnLoad();
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
                if(choice.text =="Warden" || choice.text == "Herbalist")
                    choiceBtn.enabled= false;
                if (choice.text == "Poacher")  
                    choiceBtn.enabled = false;

                choiceBtn.onClick.AddListener(() => OnChoiceClick(choice));

                InteractionSpriteData InteractSprites = dialogueSO.interactSprites
                                         .Find(t => t.interactionNo ==
                                         DialogueService.Instance.currInteraction.numTag);
                if (InteractSprites != null)
                {
                    topOptions.SetActive(true);
                    topOptions.GetComponent<Image>().enabled= true; 
                    Button spriteBtn = topOptions.transform.GetChild(index).GetComponent<Button>();
                    spriteBtn.gameObject.SetActive(true);
                    spriteBtn.GetComponent<TopOptionsBtnEvents>().Init(InteractSprites.allSprites[index]);                                     
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
        void InteractTag()
        {
            tags = story.currentTags;
            intCount = tags.Count; 
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
                    DialogueService.Instance.diaBase
                                        .ApplyInteraction(1, 2f);
                }
            }
        }
        void OnChoiceClick(Choice choice)
        {        
            story.ChooseChoiceIndex(choice.index);
            bool choiceVal = DialogueService.Instance.diaBase
                                            .ApplyChoices(choice.index, 2f);
          
                escapeCount = 1;
                RemoveListener();
                DisplayStory();
                DialogueService.Instance.ClearAllList();            
        }       
        void RemoveListener()
        {
            foreach (Transform child in choiceParent.transform.GetChild(0))
            {
                Button btn = child.GetComponent<Button>();
                btn.onClick.RemoveAllListeners();
            }

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
        void TogglePortraitWithSpeakerTags()
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
                    charTxt.text = GetString(valueTag);
                    Debug.Log("TAG DETAILS" + keyTag + "NUMBER" + numTag + "VALUE" + valueTag);
                }
                if (keyTag == SPEAKER_TAG && numTag == 2)  // right speaker
                {
                    SetPortrait(rightPortrait, valueTag);
                    TogglePortrait(leftPortrait, false);
                    TogglePortrait(rightPortrait, true);
                    charTxt.text = GetString(valueTag);
                    Debug.Log("TAG DETAILS" + keyTag + "NUMBER" + numTag + "VALUE" + valueTag);
                }
            }
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
                topOptions.GetComponent<Image>().enabled = false;
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
        string GetString(string name)
        {
            string nameStr = name.Trim().ToLower();
            string charNameStr = ""; 
            for (int i = 1; i < Enum.GetNames(typeof(CharNames)).Length; i++)
            {
                string nameEnum = ((CharNames)i).ToString();
                if (nameEnum.Trim().ToLower().Contains(nameStr))
                {
                    charNameStr = CharService.Instance.GetCharName((CharNames)i);                    
                }
            }
            for (int i = 1; i < Enum.GetNames(typeof(NPCNames)).Length; i++)
            {
                string nameEnum = ((NPCNames)i).ToString();
                if (nameEnum.Trim().ToLower().Contains(nameStr))
                {
                    charNameStr = CharService.Instance.GetNPCName((NPCNames)i); 
                }
            }
            return charNameStr;
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
        void TogglePortOnOff(GameObject portGo, bool toggle)
        {
            portGo.transform.GetChild(0).gameObject.SetActive(toggle);
            portGo.transform.GetChild(1).gameObject.SetActive(toggle);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                escapeCount = 1;
                if (isDialoguePlaying)  // start a new dialogue only when its not playing
                    DisplayStory();
                //else if (!isDialogueOnHighSpeed)
                //    SetHighTypeSpeed(currStrLen);

            }
            //if (story != null)
            //    ChoicesLen = story.currentChoices.Count;
        }

        public void Load()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, true);
        }

        public void UnLoad()
        {
            
            if (!isDialoguePlaying)
            {
                UIControlServiceGeneral.Instance.BlockEsc(false);
                UIControlServiceGeneral.Instance.TogglePanel(gameObject, false); 
                DialogueService.Instance.On_DialogueEnd();
            }
            //else
            //{
            //    UIControlServiceGeneral.Instance.BlockEsc(true);
            //    UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
            //}
        }

        public void Init()
        {
           
        }
    }
}
//void SetHighTypeSpeed(int dist)
//{
//    escapeCount--; // high speed cut
//    textRevealer.RevealTime = 0.1f;
//    isDialogueOnHighSpeed = true;
//}


//IEnumerator Wait(float time)
//{
//    yield return new WaitForSeconds(time);
//}
//void FastFwdPressed()
//{
//    //  UnLoad();
//    // search thru the intract tag and isInteracting stage then print the display tag
//    //while (story.canContinue )
//    //    //&& (!isInteracting || !isTextBoxing))
//    //{
//    //    InteractTag();
//    //    story.Continue();
//    //}

//    //if (isInteracting || isTextBoxing)
//    //    DisplayStory();
//}
