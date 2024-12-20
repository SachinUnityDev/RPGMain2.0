using Interactables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;


namespace Town
{
    [Serializable]
    public class ShipModel : BuildingModel
    {
        [Header("Buy Drinks Int")]
        public int selfDrinks = 0;
        public bool canOfferDrink = true;


        public int unLockedOnDay = 0;
        public int lastAvailDay = 0;
        public int lastUnAvailDay = 0;

        [Header("tipping text strings")]
        [SerializeField] List<string> tipTxt = new List<string>();




        BuildingSO shipSO; 
        public ShipModel(BuildingSO shipSO)
        {
            this.shipSO= shipSO;
            buildingName = shipSO.buildingName;
            buildState = shipSO.buildingState;

            buildIntTypes = shipSO.buildIntTypes.DeepClone();
            npcInteractData = shipSO.npcInteractData.DeepClone();
            charInteractData = shipSO.charInteractData.DeepClone();
            tipTxt = shipSO.tipStrs.DeepClone();
        }
        public ShipModel(BuildingModel buildModel)
        {
            buildingName = buildModel.buildingName;
            buildState = buildModel.buildState;
            buildIntTypes = buildModel.buildIntTypes.DeepClone();
            npcInteractData = buildModel.npcInteractData.DeepClone();
            charInteractData = buildModel.charInteractData.DeepClone();
            tipTxt.Clear();
        }
        public string GetTipTxt()
        {
            if (tipTxt.Count == 0)
                tipTxt = shipSO.tipStrs.DeepClone();

            int ran = UnityEngine.Random.Range(0, tipTxt.Count);
            string str = tipTxt[ran];   
            tipTxt.RemoveAt(ran);
            return str; 
        }

    }
}