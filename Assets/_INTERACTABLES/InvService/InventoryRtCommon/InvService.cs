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
        public GameObject invPanelPrefab;// Inv main panel
        public GameObject invXLPanel;  // lore + beastiary+ skills+ invPanel .. parent

        [Header("InvMainModel")]
        public InvMainModel invMainModel;
        public InvController invController; 

        [Header("Not to be referenced")]
        public InvRightViewController invViewController; // ref
        public GameObject invPanel;
        public bool isInvPanelOpen; // to track inv panel

        [Header("Stash Inv : to be ref")]
        public StashInvViewController stashInvViewController;
        

        [Header("EXCESS INV Panel: to be ref")]       
        public ExcessInvViewController excessInvViewController;
      
        private void Start()
        {
            invMainModel = new InvMainModel();
            isInvPanelOpen = false;            
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

        #region INV CHECKS

        public bool IsCommInvFull(Iitems item)
        {
            // talk to view get info dpending on current item
            return false;
        }
        public bool IsStashInvFull(Iitems item)
        {

            return false;
        }
        public bool IsExcessInvFull(Iitems item)
        {
            return false;
        }

        public bool IsCommNExcessInvFull(ItemData itemData)
        {


            return false;
        }


        public void OpenInvView()  // dont know where to use // Deprecated 
        {
            if (invPanel == null)
            {
                invPanel = Instantiate(invPanelPrefab);
            }
            UIControlServiceGeneral.Instance.SetMaxSibling2Canvas(invPanel);
            invPanel.SetActive(true);
            invViewController = invPanel.GetComponent<InvRightViewController>();
            invViewController.GetComponent<IPanel>().Init();
        }
        public void CloseInvView() // don t know where to use  // Deprecated
        {
            invViewController.GetComponent<IPanel>().UnLoad();
        }

        #endregion


    }
}



//void InitInventoryService()
//{
//    // integrate with the save system.... 
//    // char in Play.... 

//    //foreach (GameObject charGO in CharService.Instance.charsInPlay)
//    //{
//    //    if(charGO.GetComponent<InvController>()== null)
//    //    {
//    //        //if (SaveService.Instance.currSlotSelected == SaveSlot.New)
//    //        //{
//    //            CharModel charModel = charGO.GetComponent<CharController>().charModel; 

//    //            InvController invController = charGO.AddComponent<InvController>();
//    //            invController.InitController(charModel);// to be build up 
//    //        //}else
//    //        //{
//    //        //    //InventoryController invController = charGO.AddComponent<InventoryController>();
//    //        //    //charGO.InitController(charModel);
//    //        //    // TO BE IMPLEMENTED LATER 
//    //        //}


//    //    }



//    //}
//}

//****************************************************************************************************
//public bool IsItemSellable(Iitems item)
//{
//    ISellable iSellable = item as ISellable;
//    if (iSellable == null)
//        return false;
//    // can be sold only in townState 
//    // NPC Service will match the NPC who can buy the given item


//    return false;
//}
