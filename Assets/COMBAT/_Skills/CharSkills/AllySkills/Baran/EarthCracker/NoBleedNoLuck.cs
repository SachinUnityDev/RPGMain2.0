using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Common;


namespace Combat
{
    public class NoBleedNoLuck : PerkBase
    {
        public override PerkNames perkName => PerkNames.NoBleedNoLuck;

        public override PerkType perkType => PerkType.B3;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.BloodyAxe, PerkNames.TooHighBleed };

        public override string desc => "(PR: B1 + B2) /n If Crits on Bleeding target, gain +1 Luck until EOQ(Stacks up) ";

        public override CharNames charName => CharNames.Baran;

        public override SkillNames skillName => SkillNames.EarthCracker; 

        public override SkillLvl skillLvl => SkillLvl.Level3;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        // on damage applied check if Baran hit crit 
        // chk if targets is bleeding 
        // for each bleeding target give him +1 luck EOQ
        public override void BaseApply()
        {
            base.BaseApply();
            CombatEventService.Instance.OnDamageApplied -= ApplyCrit;
            CombatEventService.Instance.OnDamageApplied += ApplyCrit; 
        }

        void ApplyCrit(DmgAppliedData dmgAppliedData)
        {
            if (dmgAppliedData.targetController.charModel.charID != targetController.charModel.charID) return; 

            if(dmgAppliedData.strikeType == StrikeType.Crit)
            {
                if (targetController.GetComponent<CharStateController>().HasCharState(CharStateName.Bleeding))
                {
                    charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                    , AttribName.luck, 1, TimeFrame.EndOfQuest, 1, true);
                }   
            }
        }
        public override void ApplyFX1()
        {
        }

        public override void ApplyFX2()
        {
        }

        public override void ApplyFX3()
        {
        }

        public override void ApplyMoveFX()
        {
        }

        public override void ApplyVFx()
        {
        }
        public override void SkillEnd()
        {
            base.SkillEnd();
            CombatEventService.Instance.OnDamageApplied -= ApplyCrit;
        }
     
        public override void DisplayFX1()
        {
            str0 = "If crit vs <style=Bleed>Bleeding</style>: Gain +1 Luck until eoq";
            SkillService.Instance.skillModelHovered.AddDescLines(str0);
        }
        public override void DisplayFX2()
        {
        }

        public override void DisplayFX3()
        {
        }

        public override void DisplayFX4()
        {
        }

        public override void InvPerkDesc()
        {
            perkDesc = "If crit vs <style=Bleed>Bleeding</style>: Gain +1 Luck until eoq";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }
}
