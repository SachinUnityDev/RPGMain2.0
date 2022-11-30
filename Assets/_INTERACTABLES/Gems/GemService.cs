using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System;

namespace Interactables
{


    public class GemService : MonoSingletonGeneric<GemService>
    {
       
        public List<GemSO> allGemSO = new List<GemSO>();
        public List<GemModel> allGemModels = new List<GemModel>(); // gem models with char Data 
        public List<GemBase> allGemBase = new List<GemBase>(); 
         

        public GemController gemController;
        public GemsFactory gemsFactory;
        public CharController selectCharCtrl;


        private void Start()
        {
            gemsFactory = GetComponent<GemsFactory>();
            gemController = GetComponent<GemController>();

        }


        #region CHECKS 

        #endregion

        #region Getters

        public GemSO GetGemSO(GemName gemName)
        {
            foreach (GemSO gemSO in allGemSO)
            {
                if (gemSO.gemName == gemName)
                {
                    return gemSO;
                }
            }
            Debug.Log("GemSO Not found");
            return null;
        }
        public GemBase GetGemBase(GemName _gemName)
        {
            GemBase gemBase = allGemBase.Find(t => t.gemName == _gemName);
            if (gemBase != null)
                return gemBase;
            else
                Debug.Log("GEM BASE NOT FOUND");
            return null;

        }
        public GemModel GetGemModel(GemName _gemName)
        {
            GemModel gemModel = allGemModels.Find(t => t.gemName == _gemName);
            if (gemModel != null)
                return gemModel;
            else
                Debug.Log("gemModel NOT FOUND");
            return null;
        }
        #endregion


        #region Setters
        public void DisposeGem(GemBase gemBase)
        {
           allGemBase.Remove(gemBase);
           GemName gemName = gemBase.gemName;
            int index = allGemModels.FindIndex(t => t.gemName == gemName);
            allGemModels.RemoveAt(index); 
        }


        #endregion

        public void InitGem2CommonInv(CharController _charController, GemName _gemName, CharController causedby
          , CauseType causeType, int causeID)
        {
            GemBase gemBase = gemsFactory.GetGemBase(_gemName);

            AddGem2Char(gemBase);  // Gem init here too and then added.
            LocChange(_gemName, SlotType.CommonInv);
            InvData invData = new InvData(_charController.charModel.charName
                                                        , gemBase as Iitems);
            InvService.Instance.invMainModel.AddItem2CommInv(gemBase as Iitems);
        }
        void LocChange(GemName _gemName, SlotType _invType)
        {
            if (GetGemModel(_gemName) != null)
                GetGemModel(_gemName).invLoc = _invType;

            GemBase gembase = GetGemBase(_gemName);
            Iitems item = gembase as Iitems;
            item.invSlotType = _invType;
        }
        public void AddGem2Char(GemBase gemBase)
        {
            CharNames charName = InvService.Instance.charSelect;
            selectCharCtrl = CharService.Instance.GetCharCtrlWithName(charName);
            allGemBase.Add(gemBase);

            GemName gemName = gemBase.gemName;             
            GemSO gemSO = GetGemSO(gemName);

            //GemModel gemModel = gemBase.GemInit(gemSO, selectCharCtrl);
            //allGemModels.Add(gemModel);
        }



        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                CharController charController = CharService.Instance.charsInPlayControllers[0];
                InitGem2CommonInv(charController, GemName.Ametyst, charController
                                        , CauseType.CharSkill, 2);
            }
            //if (Input.GetKeyDown(KeyCode.U))
            //{
            //    CharController charController = CharService.Instance.CharsInPlayControllers[0];
            //    InitGem2CommonInv(charController, GemsName.Jacinth, charController
            //                              , CauseType.CharSkill, 2);

            //}
          
        }




    }



}
