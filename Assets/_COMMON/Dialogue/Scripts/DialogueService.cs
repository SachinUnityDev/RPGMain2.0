using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Intro; 

namespace Common
{
    [System.Serializable]
    public class TagData
    {
        public string keyTag;
        public int numTag;
        public string valueTag;

        public TagData(string keyTag, int numTag, string valueTag)
        {
            this.keyTag = keyTag;
            this.numTag = numTag;
            this.valueTag = valueTag;
        }
    }


    public class DialogueService : MonoSingletonGeneric<DialogueService>
    {
        [Header("SO s")]
        public List<DialogueWithNPCSO> allDialogueWithNPCSOs = new List<DialogueWithNPCSO>();
        public DialogueViewController1 dialogueViewController1; 

        [Header("Dia prefab related")]
        public GameObject dialoguePrefab;
        public GameObject diaGO;

        [Header("Canvas")]
        public Canvas canvas; 

        public List<TagData> allOptions = new List<TagData>();
        public List<TagData> allDiverts = new List<TagData>();
        public List<TagData> allDefine = new List<TagData>();
        public TagData currInteraction = null;


        [Header("Reflection related")]
        DialogueFactory dialogueFactory; 

        [Header("Custom Dialogue Interaction")]
        public IDialogue currController; 




        private void Start()
        {
            dialogueFactory = GetComponent<DialogueFactory>();
        }
        public void ClearAllList()
        {
            allOptions.Clear();
            allDiverts.Clear();
            allDefine.Clear();
        }

        public void SortAllList()
        {
            allOptions = allOptions.OrderBy(t => t.numTag).ToList();
            allDiverts = allDiverts.OrderBy(t => t.numTag).ToList();
            allDefine = allDefine.OrderBy(t => t.numTag).ToList();
        }

        public void OptionsClicked(TagData tagData)
        {
            //dialogueViewController.IsInteracting = false;
            //dialogueViewController.DisplayStory(tagData.valueTag);
            //dialogueViewController.StartStory(GetDialogueSO(101));
        }

        public void SetCurrDiaController(DialogueNames dialogueNames)
        {
            currController = GetDiaController(dialogueNames);
        }

        public IDialogue GetDiaController(DialogueNames dialogueNames)
        {
            return dialogueFactory.GetDiaController(dialogueNames); 
        }


        public void StartDialogue(DialogueNames dialogueName)
        {
            DialogueSO diaSO = GetDialogueSO(dialogueName);

            diaGO = Instantiate(dialoguePrefab);
            dialogueViewController1 = diaGO.GetComponent<DialogueViewController1>();
            diaGO.transform.SetParent(canvas.transform);
            UIControlServiceGeneral.Instance.SetMaxSiblingIndex(diaGO);
            RectTransform diaRect = diaGO.GetComponent<RectTransform>();

            diaRect.anchorMin = new Vector2(0, 0);
            diaRect.anchorMax = new Vector2(1, 1);
            diaRect.pivot = new Vector2(0.5f, 0.5f);
            diaRect.localScale = Vector3.one;
            diaRect.offsetMin = new Vector2(0, 0); // new Vector2(left, bottom);
            diaRect.offsetMax = new Vector2(0, 0); // new Vector2(-right, -top);

            if (diaSO != null)
            {
                SetCurrDiaController(dialogueName);
                dialogueViewController1.StartStory(diaSO);
            }
            else
                Debug.Log("SO Not Found");
        }

      

        public void OnDialogEnd()
        {


        }

#region GET AND SET HELPERS

        public DialogueSO GetDialogueSO(DialogueNames dialogueName)
        {
            foreach (DialogueWithNPCSO diaNPCSO in allDialogueWithNPCSOs)
            {
                foreach (DialogueSO diaSO in diaNPCSO.allDialogueSOs)
                {
                    if (diaSO.dialogueName == dialogueName)
                        return diaSO;
                }
            }
            return null;
        }

        public DialogueSO GetDialogueSO(int diaID) 
        {
            foreach (DialogueWithNPCSO diaNPCSO in allDialogueWithNPCSOs)
            {
                foreach (DialogueSO diaSO in diaNPCSO.allDialogueSOs)
                {
                    if (diaSO.DiaID == diaID)
                        return diaSO; 
                }
            }
            return null; 
        }

        public Sprite[] GetDialogueSprites(CharNames charName, NPCNames npcName)
        {
            Sprite[] sprites = new Sprite[2];
            if (charName != CharNames.None)
            {
                
                sprites[0] = CharService.Instance.allAllySO                              
                    .Find(t => t.charName == charName).dialoguePortraitClicked;
                sprites[1] = CharService.Instance.allAllySO
                   .Find(t => t.charName == charName).dialoguePortraitUnClicked;

                return sprites;
            }
            else if (npcName != NPCNames.None)
            {
                sprites[0] = CharService.Instance.allNPCSOls
                                            .Find(t => t.npcName == npcName).dialoguePortraitClicked;
                sprites[1] = CharService.Instance.allNPCSOls
                                           .Find(t => t.npcName == npcName).dialoguePortraitUnClicked;
                return sprites;
            }
            return null;
        }
#endregion
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                StartDialogue(DialogueNames.MeetKhalid);

            }
        }

    }

    // it will contain ref to all the SO , 
    // contain methods that are needed to make interactions happen , 
    // .. think more !!


}
