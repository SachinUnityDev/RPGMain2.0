using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Combat
{
    public class FindTheWeakSpot : PerkBase
    {
        public override CharNames charName => CharNames.Cahyo;

        public override SkillNames skillName => SkillNames.KrisLunge;
        public override SkillLvl skillLvl => SkillLvl.Level2;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get; set; }
        public override PerkNames perkName => PerkNames.FindTheWeakSpot;
        public override PerkType perkType => PerkType.A2;

        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "this is find the weak spot";
        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        float percent;
        //public override void SkillInit()
        //{
        //    skillModel = SkillService.Instance.allSkillModels
        //                                         .Find(t => t.skillName == skillName);

        //    charController = CharacterService.Instance.GetCharCtrlWithName(charName);
        //    skillController = SkillService.Instance.currSkillMgr;

        //}
        //public override void SkillHovered()
        //{
        //    SkillInit();

        //    SkillServiceView.Instance.skillCardData.skillModel = skillModel;

        //    SkillService.Instance.SkillHovered += DisplayFX1;
        //    SkillService.Instance.SkillHovered += DisplayFX2;
        //    SkillService.Instance.SkillHovered += DisplayFX3;
        //    skillController.allSkillBases.Find(t => t.skillName == skillName).WipeFX2();
        //}
        //public override void SkillSelected()
        //{
        //    SkillService.Instance.SkillApply += BaseApply;
        //    SkillService.Instance.SkillApply += ApplyFX1;
        //    SkillService.Instance.SkillApply += ApplyFX2;
        //    SkillService.Instance.SkillApply += ApplyFX3;
        //    SkillService.Instance.SkillApply += PostApplyFX;

        //    skillModel.targetPos.Clear();

        //    skillController.allSkillBases.Find(t => t.skillName == skillName).RemoveFX2();
        //}
        public override void Tick()
        {
            //if (roundEnd >= 2)
            //    SkillEnd();
            //roundEnd++;

        }

        public override void BaseApply()
        {
            targetGO = SkillService.Instance.currentTargetDyna.charGO;
            targetController = targetGO.GetComponent<CharController>();
            CombatEventService.Instance.OnEOR += Tick;
            skillModel.lastUsedInRound = CombatService.Instance.currentRound;
        }

        public override void ApplyFX1()
        {
            //if (CkhNSetInCoolDown() || appliedOnce || IsTargetMyAlly()) return;

            //percent = 80f;
            //if (percent.GetChance())
            //    CharStatesService.Instance.SetCharState(targetGO, CharStateName.BleedHighDOT);
        }
        public override void DisplayFX1()
        {
            str1 = $"<style=Enemy><style=Bleed> Bleed</style> High {percent}%";
            SkillService.Instance.skillCardData.descLines.Add(str1);
        }



        public override void ApplyFX2()
        {
            //if (CkhNSetInCoolDown() || appliedOnce || IsTargetMyAlly()) return;

            //if (CharStatesService.Instance.HasCharState(targetGO, CharStateName.BleedHighDOT)
            //      || CharStatesService.Instance.HasCharState(targetGO, CharStateName.BleedMedDOT)
            //      || CharStatesService.Instance.HasCharState(targetGO, CharStateName.BleedLowDOT))
            //{

            //    charController.ChangeStat(StatsName.morale, +3, 0, 0);
            //}

        }
        public override void DisplayFX2()
        {
            str2 = $"<style=Enemy> Morale,+3 self";
            SkillService.Instance.skillCardData.descLines.Add(str2);
        }
        public override void ApplyFX3()
        {
        }



        public override void RemoveFX1()
        {
            SkillService.Instance.SkillApply -= ApplyFX1;
        }

        public override void RemoveFX2()
        {
            SkillService.Instance.SkillApply -= ApplyFX2;
        }

        public override void RemoveFX3()
        {
            SkillService.Instance.SkillApply -= ApplyFX3;
        }

 

        public override void SkillEnd()
        {
            //if (targetController != null)
            //    targetController.ChangeStat(StatsName.morale, -3f, 0, 0);

            //roundEnd = 0;
            //CombatEventService.Instance.OnEOR -= Tick;

        }






        public override void DisplayFX3()
        {
        }

        public override void DisplayFX4()
        {

        }

        public override void WipeFX1()
        {
            SkillService.Instance.SkillHovered -= DisplayFX1;
        }

        public override void WipeFX2()
        {
            SkillService.Instance.SkillHovered -= DisplayFX2;
        }

        public override void WipeFX3()
        {
            SkillService.Instance.SkillHovered -= DisplayFX3;
        }

        public override void WipeFX4()
        {

        }

        public override void ApplyVFx()
        {
           
        }

        public override void ApplyMoveFX()
        {
            
        }
    }
}

