using Common;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using System.Xml.Serialization;
using UnityEngine;


namespace Town
{
    public class HouseController : MonoBehaviour
    {
        public HouseModel houseModel;
        [Header("to be ref")]
        public HouseView houseView; 

        private void Start()
        {
            
        }
        public void InitHouseController()
        {
            BuildingSO houseSO = BuildingIntService.Instance.allBuildSO.GetBuildSO(BuildingNames.House);
            houseModel = new HouseModel(houseSO);
            BuildingIntService.Instance.allBuildModel.Add(houseModel);

        }
        public void UnLockBuildIntType(BuildInteractType buildIntType, bool unLock)
        {
            foreach (BuildIntTypeData buildData in houseModel.buildIntTypes)
            {
                if (buildData.BuildIntType == buildIntType)
                {
                    buildData.isUnLocked = unLock;
                    houseView.InitBuildIntBtns(houseView as BuildView, houseModel as BuildingModel);
                }
            }
        }


        public void OnPurchase(HousePurchaseOptsData houseData)
        {
            switch (houseData.houseOpts)
            {
                case HousePurchaseOpts.UpgradeBed:
                    OnBedUpgraded(); 
                    break;
                case HousePurchaseOpts.UpgradeStash:
                    OnStashUpgraded();
                    break;
                case HousePurchaseOpts.Fermentor:
                    OnFermentorPurchased();
                    break;
                case HousePurchaseOpts.Dryer:
                    OnDryerPurhase();
                    break;
                case HousePurchaseOpts.Cora:
                    OnCoraPurchase();   
                    break;
                case HousePurchaseOpts.Drums:
                    OnDrumPurchase();   
                    break;
                default:
                    break;
            }
        }

        void OnBedUpgraded()
        {
            houseModel.isBedUpgraded= true;
        }

        public void UpgradedBedBuff()
        {
            if (!houseModel.isBedUpgraded) return; 
            if (houseModel.restChanceOnUpgrade.GetChance())
            {
                CharController charController =
                        CharService.Instance.GetCharCtrlWithName(CharNames.Abbas);
                TempTraitController tempTraitController = charController.tempTraitController;
                tempTraitController
                        .ApplyTempTrait(CauseType.BuildingInterct, (int)BuildInteractType.Purchase
                        , charController.charModel.charID, TempTraitName.WellRested);
            }
        }
        void OnStashUpgraded()
        {

        }
        
        void OnFermentorPurchased()
        {

        }
        void OnDryerPurhase()
        {
            // not in demo 
        }
        void OnCoraPurchase()
        {

        }
        void OnDrumPurchase()
        {

        }



    }
}