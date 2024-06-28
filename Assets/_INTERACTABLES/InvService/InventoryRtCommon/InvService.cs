using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using Common;
using System;
using Town;
using UnityEngine.SceneManagement;
using System.IO;
using Quest;

namespace Interactables
{
    public class InvService : MonoSingletonGeneric<InvService>, ISaveable
    {
        public int MAX_SIZE_COMM_INV = 0; 

        public event Action<bool, ItemsDragDrop> OnDragResult;
        public event Action<CharModel> OnCharSelectInvPanel;       // int here is charID 
        public event Action<bool> OnToggleInvXLView;

        public event Action<Iitems> OnItemAdded2Comm;
        public event Action<Iitems> OnItemRemovedFrmComm;

        public event Action<int> OnInvOverload; 

        [Header("OverLoaded State")]      
        public int overLoadCount = 0;  //>0

        [Header("Char Select Controller")]
        public CharController charSelectController;
        [Header("Char SKILLS RELATED")]

        public AllSkillSO allSkillSO; 

        [Header("TO BE REF")]
        public InvSO InvSO; // all items SO 

        [Header("InvMainModel")]
        public InvMainModel invMainModel;
        public InvController invController; 

        [Header("Common Inv View NTBR")]
        public InvRightViewController invRightViewController; // ref
        public InvMainViewController invMainViewController; 
      //  public GameObject invPanel;
        public bool isInvPanelOpen; // to track inv panel
        public bool isRightClickHovered= false;

        [Header("Stash Inv : to be ref")]
        public StashInvViewController stashInvViewController;

        [Header("EXCESS INV Panel: to be ref")]       
        public ExcessInvViewController excessInvViewController;

        [Header("Global Var NTBR")]
        public GameObject invXLGO;// lore + beastiary+ skills+ invPanel..parent

        
        public ServicePath servicePath => ServicePath.InvService;

        // public GameObject invXLPrefab; 

        private void Start()
        {
            Init(); 
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;            
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {  
             InitInvXLView();
        }

        public void Init()
        {   
            invController = GetComponent<InvController>();
            CharService.Instance.OnPartyLocked -= On_PartyLocked;// prevent double subscription
            CharService.Instance.OnPartyLocked += On_PartyLocked;
           
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            if (SaveService.Instance.DirectoryExists(path))
            {
                if (IsDirectoryEmpty(path))
                {
                    invMainModel = new InvMainModel();
                    AbbasStatusSet();
                }
                else
                {
                    LoadState();
                    AbbasStatusSet();
                }
            }
            else
            {
                Debug.LogError("Service Directory missing");
            }            
        }

        public void On_DragResult(bool result, ItemsDragDrop itemsDragDrop)
        {
            OnDragResult?.Invoke(result, itemsDragDrop);               
        }
       void AbbasStatusSet()  // try to connec t to on lock 
       {
            charSelectController = CharService.Instance.GetAllyController(CharNames.Abbas);
            ActiveInvData activeInvData = invMainModel.GetActiveInvData(charSelectController.charModel.charID);
            ItemData itemData = new ItemData(ItemType.Potions, (int)PotionNames.HealthPotion);
            Iitems item = ItemService.Instance.GetNewItem(itemData);
            if(activeInvData == null)
                invMainModel.EquipItem2PotionActInv(item, 2);
            else if (activeInvData.potionActiveInv[2] == null)            
                invMainModel.EquipItem2PotionActInv(item, 2);
        }

        public void On_CharSelectInv(CharModel charModel)
        {
            if (!isInvPanelOpen) return; 
          //  charSelect = charModel.charName;
            charSelectController = CharService.Instance.GetAllyController(charModel.charName);
            OnCharSelectInvPanel?.Invoke(charModel);
        }
        public void On_ToggleInvXLView(bool isOpen)
        {
            isInvPanelOpen= isOpen;

            if (isOpen)
            {
                 
                CharController charController = CharService.Instance.GetAllyController(CharNames.Abbas);
                On_CharSelectInv(charController.charModel); // Set Abbas stats as default 
            }
            else
            {
                UIControlServiceGeneral.Instance.TogglePanelNCloseOthers(invXLGO, false);               
            }
            OnToggleInvXLView?.Invoke(isOpen);
        }

        public void ShowInvXLView(bool toOpen)
        {   
            UIControlServiceGeneral.Instance.TogglePanelNCloseOthers(invXLGO, toOpen);
            if(toOpen)
                invXLGO.GetComponent<IPanel>().Init();
        }
        public void InitInvXLView()
        {
            if (isInvPanelOpen) return; // return multiple clicks
            
            Canvas canvas = GameObject.FindWithTag("Canvas").GetComponent<Canvas>();
            //  if (invXLGO == null)
            invXLGO = canvas.GetComponentInChildren<InvXLViewController>(true).gameObject; 


           // invXLGO.transform.SetParent(canvas.transform);

            //UIControlServiceGeneral.Instance.SetMaxSiblingIndex(diaGO);
            int index = invXLGO.transform.parent.childCount - 5;
            invXLGO.transform.SetSiblingIndex(index);
            RectTransform invXLRect = invXLGO.GetComponent<RectTransform>();

            invXLRect.anchorMin = new Vector2(0, 0);
            invXLRect.anchorMax = new Vector2(1, 1);
            invXLRect.pivot = new Vector2(0.5f, 0.5f);
            invXLRect.localScale = Vector3.one;
            invXLRect.offsetMin = new Vector2(0, 0); // new Vector2(left, bottom);
            invXLRect.offsetMax = new Vector2(0, 0); // new Vector2(-right, -top);



            excessInvViewController = invXLGO.GetComponentInChildren<ExcessInvViewController>();
            invRightViewController = invXLGO.GetComponentInChildren<InvRightViewController>();
           invMainViewController = invXLGO.GetComponentInChildren<InvMainViewController>();
        }

        public void On_PartyLocked()
        {
            // get char locked in the party change the size of
            int charCount =CharService.Instance.allCharsInPartyLocked.Count - 1;
            int size = 3 + 3 * charCount; 
            invMainModel.SetCommInvSize(size); 
        }

        public void On_InvOverLoad(int slotOverload)
        {
            OnInvOverload?.Invoke(slotOverload);
        }
        public void On_ItemAdded2Comm(Iitems item)
        {
            OnItemAdded2Comm?.Invoke(item);
        }
        public void On_ItemRemovedFrmComm(Iitems item)
        {
            OnItemRemovedFrmComm?.Invoke(item);
            invRightViewController.ChkOverloadCount(); 
        }

        public void SaveState()
        {
                string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);            
                ClearState();            
                string invMainModelJSON = JsonUtility.ToJson(invMainModel);
                Debug.Log("INV MAIN"+invMainModelJSON);
                string fileName = path + "InvMainModel" + ".txt";
                File.WriteAllText(fileName, invMainModelJSON);            
        }

        public void LoadState()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            path+= "/InvMainModel.txt"; 
            if (File.Exists(path))
            {
                string contents = File.ReadAllText(path);
                invMainModel = JsonUtility.FromJson<InvMainModel>(contents);

            }
            else
            {
                Debug.LogError("INV MAIN MODEL NOT FOUND"); 
            }        
        }

        public void ClearState()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);            
            DeleteAllFilesInDirectory(path);
        }
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.F5))
            {
                SaveState();
            }
            if (Input.GetKeyDown(KeyCode.F2))
            {
                LoadState();
            }
        }
    }
}

