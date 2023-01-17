using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Combat
{
    public class RevealTheBackline : PerkBase
    {
        public override CharNames charName => CharNames.Cahyo;

        public override SkillNames skillName => SkillNames.KrisLunge;

        public override SkillLvl skillLvl => SkillLvl.Level1;

        

        public override PerkSelectState state { get;set; }

        public override PerkNames perkName => PerkNames.RevealTheBackline;

        public override PerkType perkType => PerkType.B1;

        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "reveal the backline";
        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        DynamicPosData targetDyna;
        //public override void SkillInit()
        //{
        //    skillModel = SkillService.Instance.allSkillModels
        //                                      .Find(t => t.skillName == skillName);

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
        //    skillController.allSkillBases.Find(t => t.skillName == skillName).WipeFX1();

        //}
        //public override void SkillSelected()
        //{
        //    SkillService.Instance.SkillApply += BaseApply;
        //    SkillService.Instance.SkillApply += ApplyFX1;

        //    skillModel.targetPos.Clear();
        //    for (int i = 0; i < 8; i++)
        //    {
        //        skillModel.targetPos.Add(new CellPosData(CharMode.Enemy, i));
        //    }
        //    GridService.Instance.HLTargetTiles(skillModel.targetPos);
        //    skillController.allSkillBases.Find(t => t.skillName == skillName).RemoveFX1();

        //}

        //public override void BaseApply()
        //{
        //    targetDyna = SkillService.Instance.currentTargetDyna;
        //    targetGO = SkillService.Instance.currentTargetDyna.charGO;
        //    targetController = targetGO.GetComponent<CharController>();
        //    CombatEventService.Instance.OnEOR += Tick;
        //    skillModel.lastUsedInRound = CombatService.Instance.currentRound;
        //}
        public override void DisplayFX1()
        {
            str1 = $"<style=Enemy>Target all Dmg {skillModel.damageMod}%";
            SkillService.Instance.skillModelHovered.descLines.Add(str1);
        }

        public override void DisplayFX2()
        {
            str1 = $"<style=Enemy> -3 <style=Focus>Focus </style> & <style=Luck>Luck </style>, 2 rds";
            SkillService.Instance.skillModelHovered.descLines.Add(str1);
        }

        public override void DisplayFX3()
        {

        }

        public override void ApplyFX1()
        {
            //if (CkhNSetInCoolDown() || appliedOnce || IsTargetMyAlly()) return;

            //targetController.dmgController.ApplyDamage(charController
            //                 , DamageType.Physical, skillModel.damageMod, false);
        }
        public override void ApplyFX2()
        {
            //if (CkhNSetInCoolDown() || appliedOnce || IsTargetMyAlly()) return;

            //if (targetDyna.currentPos == 5 || targetDyna.currentPos == 6 || targetDyna.currentPos == 7)
            //{
            //    targetController.ChangeStat(StatsName.focus, -3, 0, 0);  // by 3 rds 
            //    targetController.ChangeStat(StatsName.luck, -3, 0, 0);   // by 3 rds 
            //}
        }

        public override void Tick()
        {
            //if (roundEnd >= 3) // by 3 rds 
            //    SkillEnd();
            //roundEnd++;
        }
        public override void SkillEnd()
        {
            //if (targetController != null)
            //{
            //    targetController.ChangeStat(StatsName.focus, 3f, 0, 0);
            //    targetController.ChangeStat(StatsName.focus, 3f, 0, 0);
            //}
            //roundEnd = 0;
            //CombatEventService.Instance.OnEOR -= Tick;
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

