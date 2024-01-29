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
                        CharService.Instance.GetAbbasController(CharNames.Abbas);
                TempTraitController tempTraitController = charController.tempTraitController;
                tempTraitController
                        .ApplyTempTrait(CauseType.BuildingInterct, (int)BuildInteractType.Purchase
                        , charController.charModel.charID, TempTraitName.WellRested);
            }
        }

        public void UpdateBuildState()
        {
            DayName dayName = CalendarService.Instance.currDayName;
            TimeState timeState = CalendarService.Instance.currtimeState;
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


