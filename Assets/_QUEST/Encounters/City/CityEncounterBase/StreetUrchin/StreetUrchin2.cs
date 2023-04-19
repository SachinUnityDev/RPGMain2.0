using Common;
using Interactables;
using System.Collections;
using UnityEngine;


namespace Quest
{
    public class StreetUrchin2 : CityEncounterBase
    {
        public override CityEncounterNames encounterName => CityEncounterNames.StreetUrchin;

        public override int seq => 2;

        public override void OnChoiceASelect()
        {
            //            "%50
            //Gain temp trait(Frail or Spineless)
            //Lose 20 - 30 fame"	"
            //% 50
            // - 2 Luck for 7 days
            //Lose 20 - 30 fame"
            float chance = 50f;
            CharController charController = CharService.Instance.GetCharCtrlWithName(CharNames.Abbas_Skirmisher);
            if (chance.GetChance())
            {
                TempTraitController tempTraitController = charController.tempTraitController;
                if(chance.GetChance()) 
                    tempTraitController.ApplyTempTrait(CauseType.CityEncounter, (int)encounterName, charController.charModel.charID, TempTraitName.Frail); 
                else
                    tempTraitController.ApplyTempTrait(CauseType.CityEncounter, (int)encounterName, charController.charModel.charID, TempTraitName.Spineless);

            }
            else
            {
                
                charController.buffController.ApplyBuff(CauseType.CityEncounter, (int)encounterName, charController.charModel.charID,
                      AttribName.luck, -2, TimeFrame.EndOfDay, 7, false); 

            }
            FameService.Instance.fameController.ChgFame(CauseType.CityEncounter, (int)encounterName, UnityEngine.Random.Range(20, 30));
        }

        public override void OnChoiceBSelect()
        {
            // % 50 : Unlock NPC info(their behaviors for each FAME lvl) "%50 : 3 random loots
            //green velvet and random lyric ring and spice"
            float chance = 50f;
            if (chance.GetChance())
            {

            }
            else
            {

            }
        }

        public override bool PreReqChk()
        {
            return EcoServices.Instance.HasMoney(PocketType.Inv, new Currency(6, 0));
        }

        public override bool UnLockCondChk()
        {
            int cal = CalendarService.Instance.dayInGame;
            int urchin1Day = EncounterService.Instance.cityEController.GetPreModel(encounterName, seq).dayEventTaken;
            if (cal > urchin1Day + 4)
                return true;
            return false;
        }  
    }
}