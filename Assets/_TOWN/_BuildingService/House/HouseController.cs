using Common;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Town
{
    public class HouseController : MonoBehaviour
    {
        public HouseModel houseModel;
        [Header("to be ref")]
        public HouseView houseView; 

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if(scene.name == "TOWN")
            { 
              houseView = FindObjectOfType<HouseView>(true);                      
            }
        }

        public void InitHouseController()
        {
            BuildingSO houseSO = BuildingIntService.Instance.allBuildSO.GetBuildSO(BuildingNames.House);
            houseModel = new HouseModel(houseSO);
            BuildingIntService.Instance.allBuildModel.Add(houseModel);

        }
        public void UnLockBuildIntType(BuildInteractType buildIntType, bool unLock)
        {
            houseView = FindObjectOfType<HouseView>(true);
            foreach (BuildIntTypeData buildData in houseModel.buildIntTypes)
            {
                if (buildData.BuildIntType == buildIntType)
                {
                    buildData.isUnLocked = unLock;
                    houseView.InitBuildIntBtns(houseModel as BuildingModel);
                }
            }
        }

        public void OnPurchase(HousePurchaseOptsData houseData)
        {
            switch (houseData.houseOpts)
            {
                case HousePurchaseOpts.UpgradeBed:
                    UpgradeBuildInt(BuildInteractType.EndDay); 
                    break;
                case HousePurchaseOpts.UpgradeStash:
                    UpgradeBuildInt(BuildInteractType.Chest);
                    break;
                case HousePurchaseOpts.Fermentor:
                    OnFermentorPurchased();
                    break;
                case HousePurchaseOpts.Dryer:
                    OnDryerPurhased();
                    break;
                case HousePurchaseOpts.Cora:
                    OnCoraPurchased();
                    break;
                case HousePurchaseOpts.Drums:
                    OnDrumPurchased(); 
                    break;
                default:
                    break;
            }
        }
        void UpgradeBuildInt(BuildInteractType buildIntType)
        {
            houseModel.BuildIntChg(buildIntType, true); 
            BuildingIntService.Instance.On_BuildIntUpgraded(houseModel, buildIntType, true);
        }

        public void ChkNApplyUpgradeBedBuff()
        {
            if (!houseModel.IsBuildIntUpgraded(BuildInteractType.EndDay)) return; 
            if (houseModel.restChanceOnUpgrade.GetChance())
            {
                CharController charController =
                        CharService.Instance.GetAbbasController(CharNames.Abbas);
                TempTraitController tempTraitController = charController.tempTraitController;
                tempTraitController
                        .ApplyTempTrait(CauseType.BuildingInterct, (int)BuildInteractType.Purchase
                        , charController.charModel.charID, TempTraitName.WellRested);
            }
        }
        void OnStashUpgraded()
        {
            // add new rows to stash 
        }
        
        void OnFermentorPurchased()
        {
            houseModel.UnLockBuildIntType(BuildInteractType.Fermentation);
            BuildingIntService.Instance.On_BuildIntUnLocked(houseModel, BuildInteractType.Fermentation, true);
        }
        void OnDryerPurhased()
        {
            // not in demo 
            houseModel.UnLockBuildIntType(BuildInteractType.DryFood);
            BuildingIntService.Instance.On_BuildIntUnLocked(houseModel, BuildInteractType.DryFood, true);

        }
        void OnCoraPurchased()
        {
          //  houseModel.UnLockBuildIntType(BuildInteractType.Music);
        }
        void OnDrumPurchased()
        {
          //  houseModel.UnLockBuildIntType(BuildInteractType.Music);
        }



    }
}