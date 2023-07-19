using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Intro;
using System;
using Town;

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
        public Action OnDialogueLsDsply; 
        public Action<DialogueNames> OnDialogueStart; 
        public Action OnDialogueEnd;

        [Header("SOs")]
        public DialogueView dialogueView;
        public DialogueNames dialogueName;
        public DialogueModel dialogueModel; 
        public bool isDiaViewInitDone = false; 

        public AllDialogueSO allDialogueSO; 
        public List<DialogueModel> allDiaLogueModels = new List<DialogueModel>();

        [Header("Dia View prefab TBR")]
        public GameObject dialoguePrefab;
        public GameObject diaGO;

        [Header("Canvas")]
        public Canvas canvas; 

        public List<TagData> allOptions = new List<TagData>();
        public List<TagData> allDiverts = new List<TagData>();
        public List<TagData> allDefine = new List<TagData>();
        public TagData currInteraction = null;


        [Header("Dialogue factory")]
        DialogueFactory dialogueFactory; 

        [Header("Dialogue Base")]
        public IDialogue diaBase;

        [Header("Game Init")]
        public bool isNewGInitDone = false;

        private void Start()
        {
            dialogueFactory = GetComponent<DialogueFactory>();
            isDiaViewInitDone = false;
        }
        public void InitDialogueService()
        {
            allDiaLogueModels.Clear();
            foreach (DialogueSO dialogueSO in allDialogueSO.allDialogues)
            {
                DialogueModel diaModel = new DialogueModel(dialogueSO);
                allDiaLogueModels.Add(diaModel);
            }
            isNewGInitDone = true;
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
        public void On_DialogueStart(DialogueNames dialogueName) // when the dialogue is clicked in dialogue LS
        {      
            StartDialogue(dialogueName);            
            OnDialogueStart?.Invoke(dialogueName);
        }
        public void On_DialogueEnd()
        {
            Destroy(diaGO.gameObject, 0.1f);// destroy the view
            OnDialogueEnd?.Invoke();
            dialogueName = DialogueNames.None;
            isDiaViewInitDone = false;
            if (!dialogueModel.isRepeatable)
                dialogueModel.isLocked = true; 
        }

        public void On_DialogueLsDsply()
        {
            OnDialogueLsDsply?.Invoke(); 
        }
        public void SetCurrDiaBase(DialogueNames dialogueNames)
        {
            diaBase = dialogueFactory.GetDialogBase(dialogueNames);
        }

        void InitDiaView(Transform parent)
        {
            if (isDiaViewInitDone) return; // return multiple clicks
            diaGO = Instantiate(dialoguePrefab);

            dialogueView = diaGO.GetComponent<DialogueView>();
            diaGO.transform.SetParent(parent);

            //UIControlServiceGeneral.Instance.SetMaxSiblingIndex(diaGO);
            int index = diaGO.transform.parent.childCount - 2;
            diaGO.transform.SetSiblingIndex(index);
            RectTransform diaRect = diaGO.GetComponent<RectTransform>();

            diaRect.anchorMin = new Vector2(0, 0);
            diaRect.anchorMax = new Vector2(1, 1);
            diaRect.pivot = new Vector2(0.5f, 0.5f);
            diaRect.localScale = Vector3.one;
            diaRect.offsetMin = new Vector2(0, 0); // new Vector2(left, bottom);
            diaRect.offsetMax = new Vector2(0, 0); // new Vector2(-right, -top);
            isDiaViewInitDone= true;
        }

        public void StartDialogue(DialogueNames dialogueName)
        {
           // InitDiaView();
            DialogueSO diaSO = GetDialogueSO(dialogueName);
            dialogueModel = GetDialogueModel(dialogueName);
            if (diaSO != null)
            {
                SetCurrDiaBase(dialogueName);
                dialogueView.StartStory(diaSO, dialogueModel);
            }
            else
            {
                Debug.Log("Dia SO Not Found" + dialogueName);
            }   
        }
        public void ShowDialogueLs(CharNames charName, NPCNames npcName, Transform parent)
        {
            InitDiaView(parent);
            OnDialogueLsDsply();
            dialogueView.ShowDialogueList(CharNames.None, NPCNames.Khalid);
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
                On_DialogueStart(DialogueNames.MeetKhalid);
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                //ShowDialogueLs(CharNames.None, NPCNames.MinamiTheSoothsayer);
            }
        }

    }
}
