using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using Common;
using System;
using Town; 

namespace Interactables
{
    public class InvService : MonoSingletonGeneric<InvService>
    {
        public int MAX_SIZE_COMM_INV = 0; 

        public event Action<bool, ItemsDragDrop> OnDragResult;
        public event Action<CharModel> OnCharSelectInvPanel;       // int here is charID 
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
        public GameObject invPanel;
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

        public void ShowInvXLPanel()
        {
            if (isInvPanelOpen) return; // return multiple clicks
            invXLGO = Instantiate(invXLPrefab);
            Canvas canvas = FindObjectOfType<Canvas>();
            //  LootService.Instance.lootView = lootViewGO.GetComponent<LootView>();
            invXLGO.transform.SetParent(canvas.transform);

            //UIControlServiceGeneral.Instance.SetMaxSiblingIndex(diaGO);
            int index = invXLGO.transform.parent.childCount - 2;
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

            //BestiaryService.Instance.bestiaryViewController = invXLGO.<BestiaryViewController>();


            UIControlServiceGeneral.Instance.TogglePanelNCloseOthers(invXLGO, true);
            invXLGO.GetComponent<IPanel>().Init();
        }

    

    }
}

