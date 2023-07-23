using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace Town
{
    public class TavernView : BuildView
    {
      
        [Header("Build Interaction panel")]
        public Transform bountyBoard;
        public Transform buyDrink;
        public Transform trophy;
        public Transform rest;

        [Header("trophy N PeltContainer")]
        [SerializeField] Transform trophyNPeltContainer;

        [SerializeField] TavernModel tavernModel;

        private void Start()
        {
            BuildingIntService.Instance.OnItemWalled +=
                        (Iitems item , TavernSlotType t) => FillTrophyNPeltOnWall();
            CalendarService.Instance.OnChangeTimeState += (TimeState timeState) => FillTrophyNPeltOnWall();
            FillTrophyNPeltOnWall();
        }
        public override Transform GetBuildInteractPanel(BuildInteractType buildInteract)
        {
            switch (buildInteract)
            {
                case BuildInteractType.None:
                    return null;
                case BuildInteractType.Bounty:
                    return bountyBoard;
                case BuildInteractType.BuyDrink:
                    return buyDrink;
                case BuildInteractType.Trophy:
                    return trophy;                
                case BuildInteractType.EndDay:
                    return rest;
                default:
                    return null;
            }
        }
        public void FillTrophyNPeltOnWall()
        {
            TimeState timeState = CalendarService.Instance.currtimeState; 
            tavernModel = BuildingIntService.Instance.tavernController.tavernModel;

            Transform trophyTrans = trophyNPeltContainer.GetChild(0); 
            Transform peltTrans = trophyNPeltContainer.GetChild(1);
            if (tavernModel.trophyOnWall!= null)
            {
                TGSO tgSO = 
                ItemService.Instance.GetTradeGoodsSO((TGNames)tavernModel.trophyOnWall.itemName);
                trophyNPeltContainer.GetChild(0).gameObject.SetActive(true);
                if(timeState == TimeState.Night)
                   trophyTrans.GetComponent<Image>().sprite = tgSO.trophyOrPeltImg_Night;
                else
                   trophyTrans.GetComponent<Image>().sprite = tgSO.trophyOrPeltImg_Day;
            }
            else
            {
                    trophyTrans.gameObject.SetActive(false);
            }
            if (tavernModel.peltOnWall != null)
            {
                TGSO tgSO =
                ItemService.Instance.GetTradeGoodsSO((TGNames)tavernModel.peltOnWall.itemName);
                peltTrans.gameObject.SetActive(true);
                if (timeState == TimeState.Night)
                    peltTrans.GetComponent<Image>().sprite = tgSO.trophyOrPeltImg_Night;
                else
                    peltTrans.GetComponent<Image>().sprite = tgSO.trophyOrPeltImg_Day;
            }
            else
            {
                peltTrans.gameObject.SetActive(false);
            }

        }
      
    }
}