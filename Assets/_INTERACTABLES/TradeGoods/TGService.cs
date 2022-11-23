using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using Common;


namespace Interactables
{
    public class TGService : MonoBehaviour
    {
        [Header("SOs")]
        public List<TGSO> allTgSOs = new List<TGSO>();

        [SerializeField] TradeGoodsFactory tgFactory;
        public TgController TgController;

        public List<TradeGoodBase> allTgBases = new List<TradeGoodBase>();
        public List<TradeGoodsModel> allTgModel = new List<TradeGoodsModel>();

        public CharController selectCharCtrl;

        void Start()
        {

        }

        #region CHECKS 

        #endregion

        #region Getters

        public TGSO GetTGSO(TGNames tgName)
        {
            foreach (TGSO tg in allTgSOs)
            {
                if (tg.tgName == tgName)
                {
                    return tg;
                }
            }
            Debug.Log("trade goods SO Not found");
            return null;
        }
        public TradeGoodBase GetTgBase(TGNames _tgName)
        {
            TradeGoodBase tgBase = allTgBases.Find(t => t.tgName == _tgName);
            if (tgBase != null)
                return tgBase;
            else
                Debug.Log("Trade goods BASE NOT FOUND");
            return null;
        }
        //public TradeGoodsModel GetTgModel(TgNames _tgName)
        //{
        //    PotionModel potionModel = allPotionModel.Find(t => t.potionName == _potionName);
        //    if (potionModel != null)
        //        return potionModel;
        //    else
        //        Debug.Log("POTION MODEL NOT FOUND");
        //    return null;
        //}
        #endregion


        //#region Setters

        //#endregion


        //public void InitPotion2CommonInv(CharController _charController, PotionName _potionName, CharController causedby
        //  , CauseType causeType, int causeID)
        //{

        //    PotionsBase potionBase = potionFactory.GetPotionBase(_potionName);
        //    AddPotion2Char(potionBase);  // potion init here too and then added.
        //    LocChange(_potionName, InvType.CommonInv);
        //    InvData invData = new InvData(_charController.charModel.charName
        //                                                , potionBase as Iitems);
        //    InvService.Instance.invMainModel.AddItem2CommInv(invData);
        //}




        //void LocChange(PotionName potionName, InvType invType)
        //{
        //    if (GetPotionModel(potionName) != null)
        //        GetPotionModel(potionName).potionLoc = invType;

        //    PotionsBase potionbase = GetPotionBase(potionName);
        //    Iitems item = potionbase as Iitems;
        //    item.invType = invType;

        //}
        //public void AddPotion2Char(PotionsBase potionBase)
        //{
        //    CharNames charName = InvService.Instance.charSelect;
        //    selectCharCtrl = CharService.Instance.GetCharCtrlWithName(charName);

        //    allPotionBase.Add(potionBase);

        //    PotionName potionName = potionBase.potionName;
        //    PotionSO potionSO = GetPotionModelSO(potionName);
        //    PotionModel potionModel = potionBase.PotionInit(potionSO, selectCharCtrl);
        //    allPotionModel.Add(potionModel);
        //}

        private void Update()
        {
            //if (Input.GetKeyDown(KeyCode.P))
            //{
            //    CharController charController = CharService.Instance.CharsInPlayControllers[0];
            //    InitPotion2CommonInv(charController, PotionName.HealthPotion, charController
            //                            , CauseType.CharSkill, 2);
            //}
            //if (Input.GetKeyDown(KeyCode.O))
            //{
            //    CharController charController = CharService.Instance.CharsInPlayControllers[0];
            //    InitPotion2CommonInv(charController, PotionName.StaminaPotion, charController
            //        , CauseType.CharSkill, 2);

            //}

        }


    }

}

