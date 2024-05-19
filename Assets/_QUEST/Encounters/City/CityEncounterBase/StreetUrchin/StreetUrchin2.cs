using Common;
using Interactables;
using System.Collections;
using UnityEngine;


namespace Quest
{
    public class StreetUrchin2 : CityEncounterBase
    {
        public override CityENames encounterName => CityENames.StreetUrchin;

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
            CharController charController = CharService.Instance.GetAbbasController(CharNames.Abbas);
            if (chance.GetChance())
            {
                TempTraitController tempTraitController = charController.tempTraitController;
                if(chance.GetChance())
                {
                    tempTraitController.ApplyTempTrait(CauseType.CityEncounter, (int)encounterName, charController.charModel.charID, TempTraitName.Frail);
                    strFX = "Temporary Trait gained: Frail"; 
                }
                else
                {
                    tempTraitController.ApplyTempTrait(CauseType.CityEncounter, (int)encounterName, charController.charModel.charID, TempTraitName.Spineless);
                    strFX = "Temporary Trait gained: Spineless";

                }
                resultStr = "'Fucking urchins!' you have screamed as they scurried off. It is possible from now on, you will be known as the bully of the city!";
                
            }
            else
            {
                
                charController.buffController.ApplyBuff(CauseType.CityEncounter, (int)encounterName, charController.charModel.charID,
                      AttribName.luck, -2, TimeFrame.EndOfDay, 7, false);
                resultStr = "You gave them a clean beating, though can't help but feel whether the gods will curse you for this!";
                strFX = "Debuff: -2 Luck for 7 days";



            }
            int famechg = UnityEngine.Random.Range(20, 30); 
            FameService.Instance.fameController.ApplyFameChg(CauseType.CityEncounter, (int)encounterName, famechg);
            strFX += $"\n{famechg} Fame lost";
        }

        public override void OnChoiceBSelect()
        {
            // % 50 : Unlock NPC info(their behaviors for each FAME lvl) "%50 : 3 random loots
            //green velvet and random lyric ring and spice"
            float chance = 50f;
            if (chance.GetChance())
            {
                resultStr = "Kids look forever grateful. The one who followed you since the beginning steps forward and says: \"You are a good man. Maybe too good for this city. We will share info about each NPC in this city so you will know what to expect from them and act carefully!";                
            }
            else
            {
                resultStr = "Kids look forever grateful. The one who followed you since the beginning steps forward and says: \"You are a good man and we want to share some of the goods we stole today with you. Here take it. That fat merchant will not need them anyway!\"";
            }
            strFX = "";
        }

        public override bool PreReqChk()
        {
            return EcoService.Instance.HasMoney(PocketType.Inv, new Currency(6, 0));
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