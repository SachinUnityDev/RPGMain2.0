using Combat;
using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class EasyFit : SagaicGewgawBase
    {
        public override SagaicGewgawNames sagaicGewgawName => SagaicGewgawNames.EasyFit;

        //+2 Wp and Vigor ....Upon Dodge: +1 Stamina and Hp Regen, 3 rds

        int dmgChgVal;

        public override void GewGawSagaicInit()
        {

            charController = InvService.Instance.charSelectController;
            dmgChgVal = UnityEngine.Random.Range(36, 46);
        }

        public  void ApplyGewGawFX()
        {
            
            if (CharStatesService.Instance.HasCharState(charController.gameObject, CharStateName.Unslakable))
            {
                ApplyIfUnslackableFx();
            }
            CharStatesService.Instance.OnCharStateStart += OnCharStateChg;
            CombatEventService.Instance.OnSOC += OnStartOfCombat;

            // Beastmen FX
            foreach (CharController c in CharService.Instance.allCharsInParty)
            {
                if (c.charModel.raceType == RaceType.Beastmen)
                {
                    //+1 Morale and Luck
                    int buffID = c.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
                                (int)sagaicGewgawName, AttribName.morale, 1, TimeFrame.Infinity, -1, true);
                    buffIndex.Add(buffID);

                    buffID = c.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
                            (int)sagaicGewgawName, AttribName.luck, 1, TimeFrame.Infinity, -1, true);
                    buffIndex.Add(buffID);
                }
            }

        }

        void OnStartOfCombat()
        {
            CharStatesService.Instance.ApplyCharState(charController.gameObject, CharStateName.Soaked,
                 charController, CauseType.SagaicGewgaw, (int)sagaicGewgawName, TimeFrame.EndOfRound, 3);
        }

        void OnCharStateChg(CharStateModData charStateModData)
        {
            ApplyIfUnslackableFx();
        }

        void ApplyIfUnslackableFx()
        {
            AttribData statData = charController.GetAttrib(AttribName.damage);
            float dmgMult = dmgChgVal / 100f;
            int buffID = charController.buffController.ApplyBuffOnRange
                (CauseType.SagaicGewgaw, charController.charModel.charID,
                  (int)sagaicGewgawName, AttribName.damage, (int)statData.maxRange * dmgMult,
                  (int)statData.minRange * dmgMult, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);
        }

        public override void EquipGewgawSagaic()
        {
            ApplyGewGawFX(); 
        }

        public override void UnEquipSagaic()
        {
            CharStatesService.Instance.OnCharStateStart -= OnCharStateChg;
            CombatEventService.Instance.OnSOC -= OnStartOfCombat;
            foreach (int i in buffIndex)
            {
                charController.buffController.RemoveBuff(i);
            }
        }
    }
}

