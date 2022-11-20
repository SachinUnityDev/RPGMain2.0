using Common;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using Town;
using UnityEngine;


namespace Interactables
{
    public class GewgawService : MonoSingletonGeneric<GewgawService>
    {
        [Header("CharSelect")]
        public CharController charSelect;
        [Header("Generic gewgaws SO")]
        public List<GenGewgawSO> allGenGewgawSO = new List<GenGewgawSO>();

        public List<GenGewgawBase> allGenGewgawbase = new List<GenGewgawBase>();
        public List<GenGewgawModel> allGenGewgawModel = new List<GenGewgawModel>(); 

        [Header("Generic GewGaw Controllers")]
        public GenGewgawController genGewgawController;
        public GenGewgawViewController genGewgawViewController;
        public GenGewgawFactory genGewgawFactory;





#region GETTERS
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
        public GenGewgawBase CreateGewgawBase(GenGewgawNames genGewgawName)
        {
            GenGewgawSO genGewgawSO = GetGewgawSO(genGewgawName);

            GenGewgawBase genGewgawBase = genGewgawFactory.GetGenGewgaws(genGewgawName
                                    , genGewgawSO.prefixName, genGewgawSO.suffixName); 
            return genGewgawBase;
        }
        public GenGewgawModel GetGenGewgawModel(GenGewgawNames genGewgawName)
        {
            GenGewgawModel genGewgawModel = allGenGewgawModel.Find(t => t.genGewgawName == genGewgawName);
            if (genGewgawModel != null)
                return genGewgawModel;
            else
                Debug.Log("GengewGaw MODEL NOT FOUND");
            return null;
        }
        public BronzifiedRange GetPurchasePrice4GewGawNPC(GenGewgawNames genGewgawName)
        {
            GenGewgawModel genGewgawModel = GetGenGewgawModel(genGewgawName);
            return genGewgawModel.currPrchPrice;
        }
        #endregion

        #region Setters

        #endregion

        // START OF EVERY WEEK 
        public BronzifiedRange SetPurchasePriceRange4GewGaw4Week(GenGewgawNames genGewgawName)
        {
            GenGewgawModel genGewgawModel = GetGenGewgawModel(genGewgawName);
            genGewgawModel.currPrchPrice = genGewgawModel
                                        .cost.ApplyCurrencyFluctation(genGewgawModel.fluctuation);
            return genGewgawModel.currPrchPrice;
        }
        public void InitGewgaw2CommonInv(CharController _charController, GenGewgawNames genGewgawName
          , GenGewgawQ genGewgawQ , CharController causedby, CauseType causeType, int causeID)
        {
            GenGewgawBase genGewgawBase = CreateGewgawBase(genGewgawName); 

            EquipGewgaw2Char(genGewgawBase, genGewgawQ);
           // AddPotion2Char(potionBase);  // potion init here too and then added.
           // LocChange(_potionName, InvType.CommonInv);
            InvData invData = new InvData(_charController.charModel.charName
                                                                , genGewgawBase as Iitems);
            InvService.Instance.invMainModel.AddItem2CommInv(invData);
        }

        public void EquipGewgaw2Char(GenGewgawBase genGewgawbase, GenGewgawQ genGewgawQ)
        {
            CharNames charName = InvService.Instance.charSelect;
            charSelect = CharService.Instance.GetCharCtrlWithName(charName);

            allGenGewgawbase.Add(genGewgawbase);
            GenGewgawNames genGewgawName = genGewgawbase.genGewgawNames;
            GenGewgawSO genGewgawSO = GetGewgawSO(genGewgawName);

            GenGewgawModel genGewgawModel = genGewgawbase.GenGewgawInit(genGewgawSO, genGewgawQ); 
            allGenGewgawModel.Add(genGewgawModel);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                CharController charController = CharService.Instance.charsInPlayControllers[0];
                InitGewgaw2CommonInv(charController, GenGewgawNames.GoldenBelt, 
                                    GenGewgawQ.Lyric, charController, CauseType.Items, 3); 
              
            }
            

        }
    }
}
