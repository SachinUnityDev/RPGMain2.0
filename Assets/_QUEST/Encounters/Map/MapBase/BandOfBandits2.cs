using Common;
using Interactables;
using Quest;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;


namespace Quest
{


    public class BandOfBandits2 : MapEbase
    {
        public override MapENames mapEName => MapENames.BandOfBanditsTwo;
        [SerializeField] Currency money2Lose; 
        public override void CityEContinuePressed()
        {

        }

        public override void OnChoiceASelect()
        {
                resultStr = "Bandits ambushed you. Watch out!";
                strFX = "Party debuff: Flat Footed, 3 rds";
        }

        public override void OnChoiceBSelect()
        {
            Currency playerInv =
                    EcoServices.Instance.GetMoneyAmtInPlayerInv();
            int allyCount = CharService.Instance.allyInPlayControllers.Count;

            int bronzifyCurr = playerInv.BronzifyCurrency();

            int bronzeM = bronzifyCurr / (2 * allyCount); 
            money2Lose = new Currency(0, bronzeM).RationaliseCurrency();

            EcoServices.Instance.DebitPlayerInv(money2Lose); 

            resultStr =
            "While trying to run away you had to throw away a coin purse to distract them and gain time.";
            strFX = $"{money2Lose.silver} Silver and {money2Lose.bronze} Bronze lost";
        }
    }
}