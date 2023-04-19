using Common;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;


namespace Town
{
    public class HouseController : MonoBehaviour
    {
        public HouseModel houseModel;
        [Header("to be ref")]
        public HouseViewController houseView; 


        private void Start()
        {
            BuildingSO houseSO = BuildingIntService.Instance.allBuildSO.GetBuildSO(BuildingNames.House);
            houseModel = new HouseModel(houseSO);
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
            if (houseModel.restChanceOnUpgrade.GetChance())
            {
                CharController charController= 
                        CharService.Instance.GetCharCtrlWithName(CharNames.Abbas_Skirmisher);
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