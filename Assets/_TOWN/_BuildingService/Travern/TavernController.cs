using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;
using Interactables;
using System;
using UnityEngine.SceneManagement;

namespace Town
{
    public class TavernController : MonoBehaviour
    {

        public TavernModel tavernModel;
        BuildingSO tavernSO;

        [Header("TBR")]
        public TavernView tavernView;
    
        void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            CalendarService.Instance.OnChangeTimeState += (TimeState timeState)=> UpdateBuildState(); 
        }
        
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "TOWN")
            {
                tavernView = FindObjectOfType<TavernView>(true);
            }
        }
        public void InitTavernController()
        {
            tavernSO = BuildingIntService.Instance.allBuildSO.GetBuildSO(BuildingNames.Tavern);
            tavernModel = new TavernModel(tavernSO);
            BuildingIntService.Instance.allBuildModel.Add(tavernModel);
        }

        public void InitTavernController(BuildingModel buildModel)
        {
            this.tavernModel =  new TavernModel(buildModel);
            BuildingIntService.Instance.allBuildModel.Add(buildModel);
        }

        public void OnTrophySocketed(TGNames trophyName)
        {

        }
        public void OnPeltSocketed(TGNames peltName)
        {

        }
        public void OnEndDayInTavern()
        {
            if (tavernModel.restChance.GetChance())
            {
                CharController charController =
                        CharService.Instance.GetAllyController(CharNames.Abbas);
                TempTraitController tempTraitController = charController.tempTraitController;
                tempTraitController
                        .ApplyTempTrait(CauseType.BuildingInterct, (int)BuildInteractType.Purchase
                        , charController.charModel.charID, TempTraitName.WellRested);
            }
        }

        public void UpdateBuildState()
        {
            DayName dayName = CalendarService.Instance.calendarModel.currDayName;
            TimeState timeState = CalendarService.Instance.calendarModel.currtimeState;
            if (tavernModel.buildState == BuildingState.Locked) return; 
            if(dayName== DayName.DayOfAir && timeState== TimeState.Night)            
                tavernModel.buildState = BuildingState.UnAvailable;            
            else
                tavernModel.buildState = BuildingState.Available;
        }

        public void UnLockBuildIntType(BuildInteractType buildIntType, bool unLock)
        {
            tavernView = FindObjectOfType<TavernView>(true);
            foreach (BuildIntTypeData buildData in tavernModel.buildIntTypes)
            {
                if (buildData.BuildIntType == buildIntType)
                {
                    buildData.isUnLocked = unLock;
                    tavernView.InitBuildIntBtns(tavernModel as BuildingModel);
                }
            }
        }

        public void WallItem(Iitems item)
        {
            ITrophyable iTrophy = item as ITrophyable;
            if (iTrophy.tavernSlotType == TavernSlotType.Trophy)
            {
                BuildingIntService.Instance.tavernController.tavernModel.trophyOnWall = item;
                BuildingIntService.Instance.On_ItemWalled(item, TavernSlotType.Trophy);
            }
            if (iTrophy.tavernSlotType == TavernSlotType.Pelt)
            {
                BuildingIntService.Instance.tavernController.tavernModel.peltOnWall = item;
                BuildingIntService.Instance.On_ItemWalled(item, TavernSlotType.Pelt);
            }
            InvService.Instance.invMainModel.RemoveItemFrmCommInv(item);
        }
        public void RemoveWalledItem(Iitems item)
        {
            ITrophyable iTrophy = item as ITrophyable;
            if (iTrophy.tavernSlotType == TavernSlotType.Trophy)
            {
                BuildingIntService.Instance.tavernController.tavernModel.trophyOnWall = null;
                BuildingIntService.Instance.On_ItemWalledRemoved(item, TavernSlotType.Trophy);
            }
            if (iTrophy.tavernSlotType == TavernSlotType.Pelt)
            {
                BuildingIntService.Instance.tavernController.tavernModel.peltOnWall = null;
                BuildingIntService.Instance.On_ItemWalledRemoved(item, TavernSlotType.Pelt);
            }
            InvService.Instance.invMainModel.AddItem2CommInv(item);
        }
    }

    [Serializable]
    public class DayNTimeData
    {
        public DayName dayName;
        public TimeState timeState;
        public DayNTimeData(DayName dayName, TimeState timeState)
        { 
            this.dayName = dayName;
            this.timeState = timeState;
        }
    }


}


