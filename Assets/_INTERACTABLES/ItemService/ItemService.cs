using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
    public class ItemService : MonoSingletonGeneric<ItemService>
    {
        public List<ItemController> allItemControllers = new List<ItemController>();
        
        public ItemCardViewController cardViewController;

        [Header("Generic gewgaws SO")]
        public List<GenGewgawSO> allGenGewgawSO = new List<GenGewgawSO>();
        void Start()
        {

        }

        // distributed 
        public void Init()
        {
            
            foreach (CharController charController in CharService.Instance.allCharsInParty)
            {
                ItemController itemController = 
                            charController.gameObject.AddComponent<ItemController>();
                itemController.Init();
                allItemControllers.Add(itemController);
                

            }
        }


#region GET ITEM CONTROLLERS AND MODELS
        public ItemController GetItemController(CharNames charName)
        {
           ItemController itemController =
                        allItemControllers.Find(t => t.itemModel.charName == charName); 
            if (itemController != null)
            {
                return itemController;
            }
            else
            {
                Debug.Log("Item Controller not found");
                return null;
            }
        }
        #endregion

        #region GET SCRIPTABLES 

        public GenGewgawSO GetGewgawSO(GenGewgawNames genGewgawName)
        {
            foreach (GenGewgawSO genGewgaw in allGenGewgawSO)
            {
                if (genGewgaw.genGewgawName == genGewgawName)
                {
                    return genGewgaw;
                }
            }
            Debug.Log("GenGewGawSO not found");
            return null;
        }


        #endregion 
    }







}

