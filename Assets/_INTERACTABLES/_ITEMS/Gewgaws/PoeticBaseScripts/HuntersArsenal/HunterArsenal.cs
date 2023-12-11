using Combat;
using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
    public class HunterArsenal : PoeticSetBase
    {
        public override PoeticSetName poeticSetName => PoeticSetName.FirstHuntersArsenal;

        //"when Hungry: +3 vigor,when Thirsty: +3 Wp"	
        //+1 Morale per Animal in enemy party	
        //Enemy Party: Animals: First 3 rds of combat: -4 Haste

        public override void BonusFx()
        {
            CharStatesService.Instance.OnCharStateStart += StarvingBuff;
            CharStatesService.Instance.OnCharStateStart += UnslackableBuff;
            CombatEventService.Instance.OnSOC += PetBuff;
            CombatEventService.Instance.OnSOC += EnemyNAnimalBuff;
            charController = InvService.Instance.charSelectController;
            if(charController.charStateController.HasCharState(CharStateName.Starving))// if already in state
            {
                StarvingBuff(null); 
            }
            if (charController.charStateController.HasCharState(CharStateName.Unslakable)) 
            {
                UnslackableBuff(null);
            }
        }
        void PetBuff()
        {
            foreach (CharController charCtrl in CharService.Instance.allCharsInPartyLocked)
            {
                if(charCtrl.charModel.orgCharMode == CharMode.Enemy)
                {
                    int index =
                    charController.buffController.ApplyBuff(CauseType.PoeticSetGewgaw, (int)poeticSetName
                     , charController.charModel.charID, AttribName.morale, 1, TimeFrame.EndOfCombat, 1, true);
                    allBuffIds.Add(index);
                }
            }
        }
        void EnemyNAnimalBuff()
        {
            foreach (CharController charCtrl in CharService.Instance.charsInPlayControllers)
            {
                if(charCtrl.charModel.charMode == CharMode.Enemy && charCtrl.charModel.raceType == RaceType.Animal)
                {
                    int index =
                     charController.buffController.ApplyBuff(CauseType.PoeticSetGewgaw, (int)poeticSetName
                     , charController.charModel.charID, AttribName.haste, -3, TimeFrame.EndOfRound, 3, false);
                    allBuffIds.Add(index);
                }
            }
        }
        void StarvingBuff(CharStateModData charStateModData)
        {
            int index =
                charController.buffController.ApplyBuff(CauseType.PoeticSetGewgaw, (int)poeticSetName
                , charController.charModel.charID, AttribName.vigor, 3, TimeFrame.Infinity, -1, true);
            allBuffIds.Add(index);
        }
        void UnslackableBuff(CharStateModData charStateModData)
        {
            int index =
                       charController.buffController.ApplyBuff(CauseType.PoeticSetGewgaw, (int)poeticSetName
                       , charController.charModel.charID, AttribName.willpower, 3, TimeFrame.Infinity, -1, true);
            allBuffIds.Add(index);
        }
 

        public override void RemoveBonusFX()
        {
            CharStatesService.Instance.OnCharStateStart -= StarvingBuff;
            CharStatesService.Instance.OnCharStateStart -= UnslackableBuff;
            CombatEventService.Instance.OnSOC -= PetBuff;
            CombatEventService.Instance.OnSOC -= EnemyNAnimalBuff;
            charController = InvService.Instance.charSelectController;

        }
    }
}