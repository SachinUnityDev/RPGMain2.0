using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using Ink.Runtime;
using DG.Tweening;
using System.Linq;
namespace Common
{
	

	public class DialogueViewController : MonoBehaviour
	{
		//public static event Action<Story> OnCreateStory;  // redundant for INKLE EDITOR 
		const string SPEAKER_TAG = "speaker";
		const string TEXTBOX_TAG = "textbox";

		const string INTERACTION_TAG = "interaction";
		const string OPTION_TAG = "option";
		const string DEFINE_TAG = "define";

		const string DIVERT_TAG = "divert"; 

		[Header("Global Variables")]
		public Story story;		
		DialogueSO dialogueSO;
		List<bool> escapeList = new List<bool>();		
		float typingSpeed = 4f;
		[SerializeField] bool isdialoguePlaying = false;
		public bool IsInteracting = false; 
		Coroutine displayCoroutine = null; 

		[Header("Ref in Editor")]

		[SerializeField] TextMeshProUGUI dialogueTxt;
		[SerializeField] TextMeshProUGUI charTxt;
		[SerializeField] TextMeshProUGUI textBoxTxt;

		[SerializeField] GameObject LowerDialogueParent;
		[SerializeField] GameObject dialogueParent;
		[SerializeField] GameObject choiceParent; 


		[SerializeField] GameObject choiceOptionsParent;		
		[SerializeField] GameObject TextBoxParent;
		[SerializeField] GameObject topOptions;

		[Header("Portrait ref")]
		[SerializeField] GameObject leftPortrait;
		[SerializeField] GameObject rightPortrait;

		void Awake()
		{			
			// StartStory();
		}

		// Creates a new Story object with the compiled story which we can then play!
		public void StartStory(DialogueSO _dialogueSO)
		{
			//dialogueSO = _dialogueSO; 
			//story = new Story(dialogueSO.dialogueAsset.text);
			//if (OnCreateStory != null) OnCreateStory(story);
			//InitDialogue();
			//DisplayStory(); 
		}

		IEnumerator DisplayDialogue()
        {
			while (story.canContinue)  // dialogue stops when a new tag is detected
            {
				dialogueTxt.text = story.Continue();
				//yield return new WaitForSeconds(2f);
				SpeakerTags();
				while (isdialoguePlaying)
                {
					yield return new WaitForSeconds(0.2f);
				}				
				InteractTag();
				while (IsInteracting)
                {
					yield return new WaitForSeconds(0.2f); 
                }				
				dialogueTxt.GetComponent<TextRevealer>().Reveal();
				dialogueTxt.text = "";
			}
		}

		void DisplayLine()
		{
			if (story.canContinue)  // dialogue stops when a new tag is detected
			{
				dialogueTxt.text = story.Continue();
				SpeakerTags();

				dialogueTxt.GetComponent<TextRevealer>().Reveal();
				dialogueTxt.text = "";

			}
		}

		void InitDialogue()
        {
			//minimise textbox , Choice parent off , top options scaled 
			TextBoxParent.transform.DOScaleY(0f, 0.2f);
			topOptions.transform.DOScaleY(0f, 0.2f);
			escapeList.Clear();
			isdialoguePlaying = false;
		}

		public void  DisplayStory(string knotStr ="")
		{					
			if(knotStr == "")
            {
				if (story.canContinue)
				{
					ToggleDialogueNChoicesPanel(dialogueParent);

					//if (displayCoroutine != null)
					//	StopCoroutine(displayCoroutine);
					//displayCoroutine = StartCoroutine(DisplayDialogue());
					DisplayLine();

				}

			}else
            {
				story.ChoosePathString(knotStr); 
				if (story.canContinue)
				{
					ToggleDialogueNChoicesPanel(dialogueParent);

					if (displayCoroutine != null)
						StopCoroutine(displayCoroutine);
					displayCoroutine = StartCoroutine(DisplayDialogue());
				}
			}		
		}

		void PopulateBtnInTopPanel(TagData tagData)
        {
			TagData interacttagData = DialogueService.Instance.currInteraction;
			InteractionSpriteData interactData = dialogueSO.interactSprites
							.Find(t => t.interactionNo == interacttagData.numTag); 
			if (interactData != null)
			{
				Button btn = topOptions.transform.GetChild(tagData.numTag-1).GetComponent<Button>();
				if (btn != null)
				{
					btn.onClick.AddListener(() => OnClickChoiceButton(tagData));
				}

				btn.GetComponent<Image>().sprite =interactData.allSprites[interacttagData.numTag-1]; 

			}
			

        }

        public void OnDialogueStart()
        {
            //dialogueTxt.transform.parent.gameObject.SetActive(true);
            isdialoguePlaying = true;
        }

        public void OnDialogueCompletion()
        {
			isdialoguePlaying = false;
			DisplayStory();
			Debug.Log("Dialogue Completed");
        }

        IEnumerator Wait(float time)
        {
			yield return new WaitForSeconds(time);
			isdialoguePlaying = false;
			
		}
		private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
				escapeList.Add(true);
            }
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
					IsInteracting = true;
					ToggleDialogueNChoicesPanel(choiceParent);
					DialogueService.Instance.currInteraction = tagData;
					DialogueService.Instance.ClearAllList();
					ToggleOffAllTopPanel();

				}
				if (!IsInteracting) return; 
				if (keyTag == OPTION_TAG)
				{
					DialogueService.Instance.allOptions.Add(tagData);

					// ADD Button
					
					Button choiceBtn = choiceOptionsParent.transform.GetChild(numTag-1).GetComponent<Button>();
					choiceBtn.GetComponentInChildren<TextMeshProUGUI>().text
																	= valueTag.Trim();

					choiceBtn.gameObject.SetActive(true);

				}
				else if (keyTag == DEFINE_TAG)
                {
					DialogueService.Instance.allDefine.Add(tagData);
				}
				else if (keyTag == DIVERT_TAG)
                {
					DialogueService.Instance.allDiverts.Add(tagData);
					int pos = numTag - 1;
					Button btn = choiceOptionsParent.transform.GetChild(pos).GetComponent<Button>();
					if ( btn != null)
                    {
						btn.onClick.AddListener(()=>OnClickChoiceButton(tagData)); 
                    }
					PopulateBtnInTopPanel(tagData); 
				}
				ToggleOffRemainingBtn();

			}

			
		}
		void ToggleOffRemainingBtn()
        {
			int btnCount = DialogueService.Instance.allOptions.Count;
			int childCount = choiceOptionsParent.transform.childCount; 
			if(childCount >  btnCount)
            {

                for (int i = btnCount; i < childCount; i++)
                {
					choiceOptionsParent.transform.GetChild(i).gameObject.SetActive(false);

				}
            }
        }

		void ToggleOffAllTopPanel()
        {
            foreach (Transform child in topOptions.transform)
            {
				child.gameObject.SetActive(false);


            }


        }

		void SpeakerTags()
        {
			List<string> tags = story.currentTags;

			foreach (var tag in tags)
            {
				// get key and value 
				string[] splitTag = tag.Split(':');
				string keyTag = splitTag[0].Trim().ToLower();
				string numTagStr= splitTag[1].Trim();
				int numTag = int.Parse(numTagStr);
				string valueTag = " ";
				if(splitTag.Length>2)
				 valueTag = splitTag[2].Trim().ToLower();

				Debug.Log("TAG DETAILS" + keyTag + "NUMBER" + numTag + "VALUE" + valueTag);
				if (keyTag == SPEAKER_TAG && numTag ==1)  //left speaker 
                {
					SetPortrait(leftPortrait, valueTag);

					TogglePortrait(leftPortrait, true);
					TogglePortrait(rightPortrait, false);
					charTxt.text = valueTag;

				}else if (keyTag == SPEAKER_TAG)  // right speaker
				{
					SetPortrait(rightPortrait, valueTag);

					TogglePortrait(leftPortrait, false);
					TogglePortrait(rightPortrait, true);					
					charTxt.text = valueTag;
				}
            }
        }

        #region HELPERS
		
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
				if(nameEnum.Trim().ToLower().Contains(nameStr))
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

		void ToggleDialogueNChoicesPanel(GameObject TogglePanelON)
        {
            foreach (Transform child in LowerDialogueParent.transform)
            {
				if(child.name == TogglePanelON.name)
                {
					TogglePanelON.gameObject.SetActive(true);
                }else
                {
					child.transform.gameObject.SetActive(false);
                }
            }
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
        #endregion

        // When we click the choice button, tell the story to choose that choice!
        void OnClickChoiceButton(TagData tagData)
		{
			DialogueService.Instance.OptionsClicked(tagData); 

			// deprecated
			//story.ChooseChoiceIndex(choice.index);
			//StartCoroutine(DisplayStory());
		}

		// Creates a textbox showing the the line of text
		void CreateContentView(string text)
		{
			//Text storyText = Instantiate(textPrefab) as Text;
			//storyText.text = text;
			//storyText.transform.SetParent(canvas.transform, false);
		}

		// Creates a button showing the choice text
		Button CreateChoiceView(string text)
		{
			// Creates the button from a prefab
			//Button choice = Instantiate(buttonPrefab) as Button;
			//choice.transform.SetParent(canvas.transform, false);

			//// Gets the text from the button prefab
			//Text choiceText = choice.GetComponentInChildren<Text>();
			//choiceText.text = text;

			//// Make the button expand to fit the text
			//HorizontalLayoutGroup layoutGroup = choice.GetComponent<HorizontalLayoutGroup>();
			//layoutGroup.childForceExpandHeight = false;

			return null;
		}

		// Destroys all the children of this gameobject (all the UI)
		//void RemoveChildren()
		//{
		//	int childCount = canvas.transform.childCount;
		//	for (int i = childCount - 1; i >= 0; --i)
		//	{
		//		GameObject.Destroy(canvas.transform.GetChild(i).gameObject);
		//	}
		//}




		// get dialogue as per the DiaID, 
		// Play the dialogue 
		// comunicate with dialogue view Controller to ensure efficient display of dialogues 
		// 
	}
}
// turn the remaining to false; 



// If we've read all the content and there's no choices, the story is finished!
//else
//{
//	Button choice = CreateChoiceView("End of story.\nRestart?");
//	choice.onClick.AddListener(delegate
//	{
//		//StartStory();
//	});
//}

//      void HandleChoices()
//      {

//	int i = 0;
//	if (story.currentChoices.Count < 1)
//		return;
//	dialogueTxt.transform.parent.gameObject.SetActive(false);


//	choiceParent.SetActive(true);
//		for ( i = 0; i < story.currentChoices.Count; i++)
//		{
//			Choice choice = story.currentChoices[i];

//			// set text to  button
//			Button choiceBtn = choiceParent.transform.GetChild(i).GetComponent<Button>(); 
//			choiceBtn.GetComponentInChildren<TextMeshProUGUI>().text
//				= choice.text.Trim();

//		//choiceBtn.onClick.AddListener(() => dialogueSO.dialogueBase.ApplyChoices(i));
//		//choiceBtn.onClick.AddListener(()=>OnClickChoiceButton()); 
//		//	choiceBtn.gameObject.SetActive(true); 
//		}



//}


//if (currentStory.canContinue)
//{
//	// set text for the current dialogue line
//	if (displayLineCoroutine != null)
//	{
//		StopCoroutine(displayLineCoroutine);
//	}
//	displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));
//	// handle tags
//	HandleTags(currentStory.currentTags);
//}
//else
//{
//	StartCoroutine(ExitDialogueMode());
//}



//
//SPACe and CLICK List  exhaust with every line untill o 
// finishLine, transistionLine 
