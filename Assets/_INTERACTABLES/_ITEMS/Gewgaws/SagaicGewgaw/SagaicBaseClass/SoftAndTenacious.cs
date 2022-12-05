using Combat;
using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class SoftAndTenacious : SagaicGewgawBase
    {
        public override SagaicGewgawNames sagaicgewgawName => SagaicGewgawNames.SoftAndTenacious;

        public override CharController charController { get; set; }
        public override List<int> buffIndex { get; set; }
        public override List<string> displayStrs { get; set; }

        //When Unslakable: +36-45% Dmg(attribute)
        //First 3 rds of combat: Soaked	
        //+1 Morale and Luck per Beastmen in party
        int dmgChgVal;

        public override void GewGawInit()
        {
            dmgChgVal = UnityEngine.Random.Range(36, 46);
        }

        public override void ApplyGewGawFX(CharController charController)
        {
            this.charController = charController;
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
                                (int)sagaicgewgawName, StatsName.morale, 1, TimeFrame.Infinity, -1, true);
                    buffIndex.Add(buffID);

                    buffID = c.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
                            (int)sagaicgewgawName, StatsName.luck, 1, TimeFrame.Infinity, -1, true);
                    buffIndex.Add(buffID);
                }
            }

        }

        void OnStartOfCombat()
        {
            CharStatesService.Instance.ApplyCharState(charController.gameObject, CharStateName.Soaked,
                 charController, CauseType.SagaicGewgaw, (int)sagaicgewgawName, TimeFrame.EndOfRound, 3);
        }

        void OnCharStateChg(CharStateData charStateData)
        {
            ApplyIfUnslackableFx();
        }

        void ApplyIfUnslackableFx()
        {
            StatData statData = charController.GetStat(StatsName.damage);
            float dmgMult = dmgChgVal / 100f;
            int buffID = charController.buffController.ApplyBuffOnRange
                (CauseType.SagaicGewgaw, charController.charModel.charID,
                  (int)sagaicgewgawName, StatsName.damage, (int)statData.maxRange * dmgMult,
                  (int)statData.minRange * dmgMult, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);
        }
        public override List<string> DisplayStrings()
        {
            return null;
        }
        public override void RemoveFX()
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

