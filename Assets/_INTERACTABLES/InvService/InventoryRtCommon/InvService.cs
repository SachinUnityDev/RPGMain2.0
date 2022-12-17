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

        void OnEnable()
        {
            // attach Inventory Controller as  soon as Game Start 
            // for now make it start of combat 
            // CombatEventService.Instance.OnSOC += InitInventoryService; 
            
            //  InitInventoryService(); 
        }
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
        public void OpenInvView()
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
        public void CloseInvView()
        {
            invViewController.GetComponent<IPanel>().UnLoad();
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


        #region ITEM_ACTIONS CHECKS
        public bool IsItemEquipable(Iitems item)
        {
            IEquipAble iequipable = item as IEquipAble;
            if (iequipable == null)
                return false;
            // check with the Active Inv View Controller 
            return true;
        }

        public bool IsItemConsumable(Iitems item)
        {
            IConsumable iConsumable = item as IConsumable;
            if (iConsumable == null)
                return false;
            // will depend on gameState and SubState 
            // will depend on ItemType 

            return true;
        }

        public bool IsEnchantable(Iitems item)
        {
            // temple NPC only 
            // X days after reading Enchantment Scroll .. Town and QuestPrep Scene
            // check with WeaponViewController / EnchantController for recharging or new Enchantment 

            return false;
        }
        public bool IsSocketable(Iitems item)
        {
            // Town and Quest prep Scene // no other conditions needed 
            // check with Armorview Controller for sockets slots available 

            return false;
        }
        //public bool IsItemSellable(Iitems item)
        //{
        //    ISellable iSellable = item as ISellable;
        //    if (iSellable == null)
        //        return false;
        //    // can be sold only in townState 
        //    // NPC Service will match the NPC who can buy the given item


        //    return false;
        //}

        public bool IsItemDispoable(Iitems item)
        {
            //IItemDisposable iDisposable = item as IItemDisposable;
            //if (iDisposable == null)
            //    return false;
            return true;
        }

        public bool IsItemPurchaseable(Iitems item)
        {
            IPurchaseable iPurchaseable = item as IPurchaseable;
            if (iPurchaseable == null)
                return false;

            // NPC service will provide whether NPC has item to sell
            // 

            return false;
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
