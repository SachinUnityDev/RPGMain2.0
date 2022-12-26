using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using Common;
using System;
using Spine;

namespace Interactables
{
    public class InvService : MonoSingletonGeneric<InvService>
    {
        public int MAX_SIZE_COMM_INV = 0; 

        public event Action<bool, ItemsDragDrop> OnDragResult;
        public event Action<CharModel> OnCharSelectInPanel;       // int here is charID 
        public CharNames charSelect;
        public CharController charSelectController; 


        [Header("TO BE REF")]
        public InvSO InvSO; // all items SO 
        public GameObject invPanelPrefab;
        public GameObject invXLPanel;

        [Header("InvMainModel")]
        public InvMainModel invMainModel;
        public InvController invController; 

        [Header("Not to be referenced")]
        public InvRightViewController invViewController; // ref
        public GameObject invPanel;
        

        [Header("House View Controller Not to be ref")]
        public StashInvViewController stashInvViewController;

        [Header("EXCESS INV Panel")]       
        public ExcessInvViewController excessInvViewController;
      

        private void Start()
        {
            invMainModel = new InvMainModel();
        }

        public void On_DragResult(bool result, ItemsDragDrop itemsDragDrop)
        {
            OnDragResult?.Invoke(result,itemsDragDrop); 
        }

        public void On_CharSelectInv(CharModel charModel)
        {
            charSelect = charModel.charName;
            charSelectController = CharService.Instance.GetCharCtrlWithName(charModel.charName);
            OnCharSelectInPanel?.Invoke(charModel);
        }


       


        #region INV CHECKS

        public bool IsCommInvFull()
        {

            return false;
        }
        public bool IsStashInvFull()
        {
            return false;
        }
        public bool IsExcessInvFull()
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
