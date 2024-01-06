using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using Common;
using System;
using Town;
using UnityEngine.SceneManagement;

namespace Interactables
{
    public class InvService : MonoSingletonGeneric<InvService>
    {
        public int MAX_SIZE_COMM_INV = 0; 

        public event Action<bool, ItemsDragDrop> OnDragResult;
        public event Action<CharModel> OnCharSelectInvPanel;       // int here is charID 
        public event Action<bool> OnToggleInvXLView; 


        public CharNames charSelect;
        public CharController charSelectController;
        [Header("Char SKILLS RELATED")]

        public AllSkillSO allSkillSO; 

        [Header("TO BE REF")]
        public InvSO InvSO; // all items SO 

        [Header("InvMainModel")]
        public InvMainModel invMainModel;
        public InvController invController; 

        [Header("Common Inv View NTBR")]
        public InvRightViewController commInvViewController; // ref
      //  public GameObject invPanel;
        public bool isInvPanelOpen; // to track inv panel

        [Header("Stash Inv : to be ref")]
        public StashInvViewController stashInvViewController;
        

        [Header("EXCESS INV Panel: to be ref")]       
        public ExcessInvViewController excessInvViewController;

        [Header("Global Var NTBR")]
        public GameObject invXLGO;// lore + beastiary+ skills+ invPanel..parent
        public GameObject invXLPrefab; 

        private void Start()
        {
            invMainModel = new InvMainModel();
            isInvPanelOpen = false;         
            invController = GetComponent<InvController>();  
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
            if(invXLGO== null)
                    InitInvXLView();
            charSelectController = CharService.Instance.GetCharCtrlWithName(CharNames.Abbas);
        }
        public void On_DragResult(bool result, ItemsDragDrop itemsDragDrop)
        {
            OnDragResult?.Invoke(result,itemsDragDrop); 
        }

        public void On_CharSelectInv(CharModel charModel)
        {
            if (!isInvPanelOpen) return; 
            charSelect = charModel.charName;
            charSelectController = CharService.Instance.GetCharCtrlWithName(charModel.charName);
            OnCharSelectInvPanel?.Invoke(charModel);
        }
        public void On_ToggleInvXLView(bool isOpen)
        {
            isInvPanelOpen= isOpen;
            OnToggleInvXLView?.Invoke(isOpen);

            if (isOpen)
            {
                 
                CharController charController = CharService.Instance.GetCharCtrlWithName(CharNames.Abbas);
                On_CharSelectInv(charController.charModel); // Set Abbas stats as default 
            }
            else
            {
                UIControlServiceGeneral.Instance.TogglePanelNCloseOthers(invXLGO, false);
               // Destroy(invXLGO);
            }
            OnToggleInvXLView?.Invoke(!isOpen);
        }

        public void ShowInvXLView(bool toOpen)
        {
            if(invXLGO== null)           
                InitInvXLView();
           
            UIControlServiceGeneral.Instance.TogglePanelNCloseOthers(invXLGO, toOpen);
            if(toOpen)
                invXLGO.GetComponent<IPanel>().Init();
        }
        public void InitInvXLView()
        {
            if (isInvPanelOpen) return; // return multiple clicks
            if(invXLGO == null)
                invXLGO = Instantiate(invXLPrefab);
            Canvas canvas = GameObject.FindWithTag("Canvas").GetComponent<Canvas>();

            invXLGO.transform.SetParent(canvas.transform);

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
            commInvViewController = invXLGO.GetComponentInChildren<InvRightViewController>();
           
        }

    

    }
}

