using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Quest;
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

        [Header(" Dialogue Model and Bases")]
        public List<IDialogue> allDiabases = new List<IDialogue>();
        public List<DialogueModel> allDiaModel = new List<DialogueModel>();
        [SerializeField] int diaBaseCount = 0;


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
            dialogueFactory= GetComponent<DialogueFactory>();   
            dialogueFactory.InitDialogues();
            InitAllDiaModel();
            InitAllDiabase(); 
        }
        void InitAllDiaModel()
        {
            allDiaLogueModels.Clear();
            foreach (DialogueSO diaSO in allDialogueSO.allDialogues)
            {
                DialogueModel diaModel = new DialogueModel(diaSO);
                allDiaLogueModels.Add(diaModel);
            }
            isNewGInitDone= true;   
        }
        void InitAllDiabase()
        {
            allDiabases.Clear();
            foreach (DialogueModel diaModel in allDiaLogueModels)
            {
                Debug.Log("DiaBase" + diaModel.dialogueName);
                IDialogue diaBase = dialogueFactory.GetDialogBase(diaModel.dialogueName);
                allDiabases.Add(diaBase);
            }
            diaBaseCount = allDiabases.Count;
        }
        public IDialogue GetDiaBase(DialogueNames dialogueName)
        {
            int index = allDiabases.FindIndex(t => t.dialogueName == dialogueName);
            if (index != -1)
            {
                return allDiabases[index];
            }
            else
            {
                Debug.Log("Dialogue base not found" + dialogueName);
                return null;
            }
        }
        public DialogueModel GetDialogueModel(DialogueNames dialogueName)
        {
            int index = allDiaLogueModels.FindIndex(t => t.dialogueName == dialogueName);
            if (index != -1)
            {
                return allDiaLogueModels[index];
            }
            else
            {
                Debug.Log("Dia model not found" + dialogueName);
            }
            return null;
        }
        public List<DialogueModel> GetDialogueModel4CharNPC(CharNames charName, NPCNames nPCNames)
        {
            List<DialogueModel> lsModels = new List<DialogueModel>(); 
            foreach (DialogueModel diaModel in allDiaLogueModels)
            {               
                if(diaModel.npcName == nPCNames && diaModel.charName == charName)
                {
                   if (diaModel.isPlayedOnce && !diaModel.isRepeatable)
                        continue; 
                   if(diaModel.isUnLocked)
                    lsModels.Add(diaModel);
                }
            }
            return lsModels;
        }
        public void UpdateDialogueState()
        {
            foreach (BuildingModel  buildModel in BuildingIntService.Instance.allBuildModel)
            {
                if(buildModel.charInteractData.Count> 0)    
                foreach (CharIntData intData in buildModel.charInteractData)
                {
                    foreach (IntTypeData intTypedata in intData.allInteract)
                    {
                        if(intTypedata.nPCIntType == IntType.Talk)
                        {
                            foreach (DialogueData diaData in intTypedata.allDialogueData)
                            {
                                GetDialogueModel(diaData.dialogueName).isUnLocked = diaData.isUnLocked; 
                            }
                        }
                    }
                }
                if (buildModel.npcInteractData.Count > 0)
                foreach (NPCIntData intData in buildModel.npcInteractData)
                {
                    foreach (IntTypeData intTypedata in intData.allInteract)
                    {
                        if (intTypedata.nPCIntType == IntType.Talk)
                        {
                            foreach (DialogueData diaData in intTypedata.allDialogueData)
                            {
                                GetDialogueModel(diaData.dialogueName).isUnLocked = diaData.isUnLocked;
                            }
                        }
                    }
                }
            }
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
            diaBase = GetDiaBase(dialogueName); 
            dialogueModel = GetDialogueModel(dialogueName); 

            StartDialogue(dialogueName);            
            OnDialogueStart?.Invoke(dialogueName);
        }
        public void On_DialogueEnd()
        {
            Destroy(diaGO.gameObject, 0.1f);// destroy the view
            OnDialogueEnd?.Invoke();
            if(diaBase!= null)
                diaBase.OnDialogueEnd();
            dialogueName = DialogueNames.None;
            diaBase = null; 
            isDiaViewInitDone = false;
            if (!dialogueModel.isRepeatable)
                dialogueModel.isUnLocked = true; 
        }

        public void On_DialogueLsDsply()
        {
            
            OnDialogueLsDsply?.Invoke(); 
        }
        //public void SetCurrDiaBase(DialogueNames dialogueNames)
        //{
        //    diaBase = dialogueFactory.GetDialogBase(dialogueNames);
        //}

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
               // SetCurrDiaBase(dialogueName);
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
            On_DialogueLsDsply();
            dialogueView.ShowDialogueList(CharNames.None, npcName);
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
            if (Input.GetKeyDown(KeyCode.M))
            {
                Canvas canvas = FindObjectOfType<Canvas>();
                ShowDialogueLs(CharNames.None, NPCNames.Khalid, canvas.transform);
            }
        }

    }
}
