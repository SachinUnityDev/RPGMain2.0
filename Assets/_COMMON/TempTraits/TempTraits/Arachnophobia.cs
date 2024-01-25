using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class Arachnophobia : TempTraitBase
    {
        public override TempTraitName tempTraitName =>  TempTraitName.Arachnophobia;
        List<int> socBuffs = new List<int>();

        public override void OnApply()
        {
            int dmgAltBuffID = charController.strikeController.ApplyDmgAltBuff(-20f, CauseType.TempTrait, (int)tempTraitName
             , charController.charModel.charID, TimeFrame.Infinity, -1, false, AttackType.None, DamageType.None
             , CultureType.Arachnid);
            allBuffDmgAltIds.Add(dmgAltBuffID);

            CharService.Instance.OnCharDeath += DeathNFleeChk;
            CharService.Instance.OnCharFleeQuest += DeathNFleeChk;
            CombatEventService.Instance.OnCombatInit += OnCombatInit;
            CombatEventService.Instance.OnEOC += OnEOC;

        }
        void DeathNFleeChk(CharController charController)
        {
            if (charController.charModel.cultType == CultureType.Arachnid
                && charController.charModel.charID != charID)
                ApplyBuffs();
        }

        void ApplyBuffs()
        {
            int count = 0;
            ClearBuffs();
            foreach (CharController charCtrl in CharService.Instance.allCharsInPartyLocked)
            {

                if (charController.charModel.cultType == CultureType.Arachnid
                    && charController.charModel.charID != charID)
                    if (charCtrl.charModel.stateOfChar == StateOfChar.UnLocked)
                        count++;
            }
            for (int i = 0; i < count; i++)
            {
                int buffID = charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName
                                    , charID, AttribName.morale, -1, TimeFrame.Infinity, 1, false);
                allBuffIds.Add(buffID);
                buffID = charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName
                         , charID, AttribName.luck, -1, TimeFrame.Infinity, 1, false);
                allBuffIds.Add(buffID);
            }
        }
        void FortChg()
        {
            int count = 0;
            ClearBuffs();
            foreach (CharController charCtrl in CharService.Instance.allCharsInPartyLocked)
            {
                if (charController.charModel.cultType == CultureType.Arachnid
                    && charController.charModel.charID != charID)
                    if (charCtrl.charModel.stateOfChar == StateOfChar.UnLocked)
                        count++;
            }
            for (int i = 0; i < count; i++)
            {
                int buffID = charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName
                  , charID, AttribName.fortOrg, -6, TimeFrame.EndOfCombat, 1, false);
                socBuffs.Add(buffID);
            }
        }
        void OnCombatInit(CombatState c, LandscapeNames l, EnemyPackName e)
        {
            FortChg();
        }

        void OnEOC()
        {
            socBuffs.ForEach(t => charController.buffController.RemoveBuff(t));
            socBuffs.Clear();
        }
        public override void EndTrait()
        {
            base.EndTrait();
            CharService.Instance.OnCharDeath += DeathNFleeChk;
            CharService.Instance.OnCharFleeQuest += DeathNFleeChk;
            CombatEventService.Instance.OnCombatInit -= OnCombatInit;
            CombatEventService.Instance.OnEOC -= OnEOC;
        }


    }
}