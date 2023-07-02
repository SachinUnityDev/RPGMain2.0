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
        //public List<DialogueWithCharNPCSO> allDialogueWithNPCSOs = new List<DialogueWithCharNPCSO>();
        public DialogueViewController1 dialogueViewController1;

        public AllDialogueSO allDialogueSO; 
        public List<DialogueModel> allDiaLogueModels = new List<DialogueModel>();

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
            allDiaLogueModels.Clear();
          
        }
        public void InitDialogueService()
        {
            foreach (DialogueSO dialogueSO in allDialogueSO.allDialogues)
            {
                DialogueModel diaModel = new DialogueModel(dialogueSO);
                allDiaLogueModels.Add(diaModel);
            }
        }
        public DialogueModel GetDialogueModel(DialogueNames dialogueName)
        {
            int  index = allDiaLogueModels.FindIndex(t=>t.dialogueName == dialogueName);    
            if(index != -1)
            {
                return allDiaLogueModels[index];
            }
            Debug.Log("dialogue NOT FOUND" + dialogueName); return null;
        }

        public List<DialogueModel> GetDialogueModel4CharNPC(CharNames charName, NPCNames nPCNames)
        {
            List<DialogueModel> lsModels = new List<DialogueModel>(); 
            foreach (DialogueModel diaModel in allDiaLogueModels)
            {
                if(diaModel.npcName == nPCNames && diaModel.charName == charName)
                {
                    lsModels.Add(diaModel);
                }
            }
            return lsModels;
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

        //public void OptionsClicked(TagData tagData)
        //{
        //    //dialogueViewController.IsInteracting = false;
        //    //dialogueViewController.DisplayStory(tagData.valueTag);
        //    //dialogueViewController.StartStory(GetDialogueSO(101));
        //}

        public void SetCurrDiaBase(DialogueNames dialogueNames)
        {
            currController = dialogueFactory.GetDialogBase(dialogueNames);
        }

        void InitDiaView()
        {
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
        }

        public void StartDialogue(DialogueNames dialogueName)
        {
            InitDiaView();
            DialogueSO diaSO = GetDialogueSO(dialogueName);
            if (diaSO != null)
            {
                SetCurrDiaBase(dialogueName);
                dialogueViewController1.StartStory(diaSO);
            }
            else
            {
                Debug.Log("Dia SO Not Found");
            }   
        }
        public void ShowDialogueLs(CharNames charName, NPCNames npcName)
        {
            InitDiaView();
            dialogueViewController1.ShowDialogueList(CharNames.None, NPCNames.KhalidTheHealer);
        }
        public void OnDialogEnd()
        {


        }

#region GET AND SET HELPERS

        public DialogueSO GetDialogueSO(DialogueNames dialogueName)
        {
          return  allDialogueSO.GetDialogueWithName(dialogueName); 
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
                sprites[0] = CharService.Instance.allNpcSO.GetNPCSO(npcName).dialoguePortraitClicked; 
                sprites[1] = CharService.Instance.allNpcSO.GetNPCSO(npcName).dialoguePortraitUnClicked;
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
            if (Input.GetKeyDown(KeyCode.M))
            {
                ShowDialogueLs(CharNames.None, NPCNames.KhalidTheHealer);
            }
        }

    }
}
