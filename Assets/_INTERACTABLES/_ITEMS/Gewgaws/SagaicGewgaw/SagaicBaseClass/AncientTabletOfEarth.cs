using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using UnityEngine;


namespace Interactables
{
    public class AncientTabletOfEarth : SagaicGewgawBase
    {
        public override SagaicGewgawNames sagaicgewgawName => SagaicGewgawNames.AncientTabletOfEarth;

        public override CharController charController { get; set; }
        public override List<int> buffIndex { get ; set ; }
        public override List<string> displayStrs { get; set; }
        //Shrine of Ruru(curio) has double effect for temporary specs
        //Double exp from combat during the day of Earth
        //	+2-3 Luck in Rainforest, Jungle landscapes	
        //	+20% dmg mod for Earth Attack Skills
        int valLuck; 

        public override void ApplyGewGawFX(CharController charController)
        {
            this.charController = charController;
        }
        public override void GewGawInit()
        {
            valLuck = UnityEngine.Random.Range(2, 4);

        }

        void OnStartOfCombat()
        {
            if (GameService.Instance.gameModel.landscapeNames == LandscapeNames.Rainforest
                || GameService.Instance.gameModel.landscapeNames == LandscapeNames.Jungle)
            {
                int  buffID = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
                    (int)sagaicgewgawName, StatsName.luck, valLuck, TimeFrame.EndOfRound, 3, true);
                buffIndex.Add(buffID);
            }


        }

        public override List<string> DisplayStrings()
        {
            return null;
        }
     
        public override void EndFx()
        {
            
        }
    }
}

