using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Interactables
{
    public class HerbService : MonoSingletonGeneric<HerbService>
    {

        //public List<HerbSO> allHerbSO = new List<HerbSO>();
        //public List<HerbModel> allHerbModels = new List<HerbModel>(); // gem models with char Data 
        //public List<HerbBase> allHerbBase = new List<HerbBase>();


        //public HerbController herbController;
        //public HerbsFactory herbsFactory;
        //public CharController selectCharCtrl;

        //void Start()
        //{

        //}

        //#region CHECKS 

        //#endregion

        //#region Getters

        //public HerbSO GetHerbModelSO(HerbNames herbName)
        //{
        //    foreach (HerbSO herb in allHerbSO)
        //    {
        //        if (herb.herbName == herbName)
        //        {
        //            return herb;
        //        }
        //    }
        //    Debug.Log("herb SO Not found");
        //    return null;
        //}
        //public HerbBase GetHerbBase(HerbNames _herbName)
        //{
        //    HerbBase herbBase = allHerbBase.Find(t => t.herbName == _herbName);
        //    if (herbBase != null)
        //        return herbBase;
        //    else
        //        Debug.Log("HERB BASE NOT FOUND"+ _herbName);
        //    return null;

        //}
        //public HerbModel GetHerbModel(HerbNames _herbName)
        //{
        //    HerbModel herbModel = allHerbModels.Find(t => t.herbName == _herbName);
        //    if (herbModel != null)
        //        return herbModel;
        //    else
        //        Debug.Log("HERB MODEL NOT FOUND");
        //    return null;
        //}
        //#endregion

        //#region Setters

        //#endregion

        //public void InitHerb2CommonInv(CharController _charController, HerbNames _herbName, CharController causedby
        //  , CauseType causeType, int causeID)
        //{

        //    HerbBase herbBase = herbsFactory.GetHerbBase(_herbName);
        //    AddHerb2Char(herbBase);  // Herb init here too and then added.
        //                             // LocChange(_herbName, InvType.CommonInv);
        //    Iitems item = herbBase as Iitems;
        //    item.invSlotType = SlotType.CommonInv; 
        //    InvData invData = new InvData(_charController.charModel.charName
        //                                                , herbBase as Iitems);
        //    InvService.Instance.invMainModel.AddItem2CommInv(herbBase as Iitems);
        //}




        //void LocChange(HerbNames herbName, SlotType invType)
        //{
        //    if (GetHerbModel(herbName) != null)
        //        GetHerbModel(herbName).invLoc = invType;

        //    HerbBase herbbase = GetHerbBase(herbName);
        //    Iitems item = herbbase as Iitems;
        //    item.invSlotType = invType;
        //}
        //public void AddHerb2Char(HerbBase herbBase)
        //{
        //    CharNames charName = InvService.Instance.charSelect;
        //    selectCharCtrl = CharService.Instance.GetCharCtrlWithName(charName);

        //    allHerbBase.Add(herbBase);

        //    HerbNames herbName = herbBase.herbName; 
        //    HerbSO herbSO = GetHerbModelSO(herbName);
        //    HerbModel herbModel = herbBase.HerbInit(herbName,selectCharCtrl);
        //    allHerbModels.Add(herbModel);
        //}

        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.U))
        //    {
        //        CharController charController = CharService.Instance.charsInPlayControllers[0];
        //        InitHerb2CommonInv(charController, HerbNames.Aloe, charController
        //                                , CauseType.CharSkill, 2);
        //    }
        //    //if (Input.GetKeyDown(KeyCode.O))
        //    //{
        //    //    CharController charController = CharService.Instance.CharsInPlayControllers[0];
        //    //    InitPotion2CommonInv(charController, PotionName.StaminaPotion, charController
        //    //        , CauseType.CharSkill, 2);

        //    //}

        //    }
    }
}

