using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;


namespace Combat
{
    public class ExterminateTheBackLine : PerkBase
    {
        public override CharNames charName => CharNames.Cahyo;

        public override SkillNames skillName => SkillNames.KrisLunge;

        public override SkillLvl skillLvl => SkillLvl.Level2;

        private PerkSelectState _state = PerkSelectState.Clickable;

        public override PerkNames perkName => PerkNames.ExterminateTheBackline;
        public override PerkType perkType => PerkType.B2;
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.RevealTheBackline };

        public override string desc => "Extedrminate the backline";

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        public override PerkSelectState state { get; set; }

        //public override void SkillInit()
        //{
        //    skillModel = SkillService.Instance.allSkillModels
        //                                     .Find(t => t.skillName == skillName);

        //    charController = CharacterService.Instance.GetCharCtrlWithName(charName);
        //    skillController = SkillService.Instance.currSkillMgr;

        //    skillModel.damageMod = 105f;
        //}
        //public override void SkillHovered()
        //{
        //    SkillInit();

        //    SkillServiceView.Instance.skillCardData.skillModel = skillModel;

        //    SkillService.Instance.SkillHovered += DisplayFX1;
        //    SkillService.Instance.SkillHovered += DisplayFX2;
        //    SkillService.Instance.SkillHovered += DisplayFX3;
        //}
        //public override void SkillSelected()
        //{
        //    SkillService.Instance.SkillApply += BaseApply;
        //    SkillService.Instance.SkillApply += ApplyFX1;
        //    SkillService.Instance.SkillApply += ApplyFX2;
        //    SkillService.Instance.SkillApply += ApplyFX3;
        //    SkillService.Instance.SkillApply += PostApplyFX;
        //}
        //public override void BaseApply()
        //{
        //    CombatEventService.Instance.OnEOR += Tick;
        //    targetGO = SkillService.Instance.currentTargetDyna.charGO;
        //    targetController = targetGO.GetComponent<CharController>();
        //    skillModel.lastUsedInRound = CombatService.Instance.currentRound;
        //}
        public override void Tick()
        {
            //if (roundEnd >= 1)
            //    SkillEnd();
            //roundEnd++;
        }
        public override void DisplayFX1()
        {

        }


        public override void ApplyFX1()
        {
            // rewrite damage after discussions 

        }
        public override void DisplayFX2()
        {
            str1 = $"<style=Allies> <style=States>Confuse </style> backrow1 rd";
            SkillService.Instance.skillCardData.descLines.Add(str1);
        }

        public override void ApplyFX2()
        {
            //if (CkhNSetInCoolDown() || appliedOnce || IsTargetMyAlly()) return;
            //CharStatesService.Instance.SetCharState(targetGO, CharStateName.Confused);
        }

        public override void ApplyFX3()
        {
        }


        public override void RemoveFX1()
        {
        }

        public override void RemoveFX2()
        {
        }

        public override void RemoveFX3()
        {
        }



        public override void SkillEnd()
        {
            //if (targetController != null)
            //    CharStatesService.Instance.RemoveCharState(targetGO, CharStateName.Confused);
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
           
        }

        public override void WipeFX2()
        {
            
        }

        public override void WipeFX3()
        {
           
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

