using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class FasterThanEyeCanSee : PerkBase
    {
        public override PerkNames perkName => PerkNames.FasterThanEyeCanSee;

        public override PerkType perkType => PerkType.A2;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "%30 Confuse, 1 rd";

        public override CharNames charName => CharNames.Cahyo;

        public override SkillNames skillName => SkillNames.WristSpin;

        public override SkillLvl skillLvl => SkillLvl.Level2;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
    
        public override void ApplyFX1()
        {
             foreach (DynamicPosData dyna in CombatService.Instance.mainTargetDynas)
                {
                    if(chance.GetChance())
                    CharStatesService.Instance
                        .ApplyCharState(targetGO, CharStateName.Confused
                                     , charController, CauseType.CharSkill, (int)skillName);
            }
            
        }
        public override void SkillEnd()
        {
            base.SkillEnd();      
            foreach (DynamicPosData dyna in CombatService.Instance.mainTargetDynas)
            {
                CharStatesService.Instance.RemoveCharState(targetGO, CharStateName.Confused);
            }
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

        public override void DisplayFX1()
        {
            str0 = $"30 % chance, <style=States>Confused</style>, {skillModel.castTime} rd ";
            SkillService.Instance.skillCardData.descLines.Add(str0);
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

        public override void PostApplyFX()
        {
          
        }

        public override void PreApplyFX()
        {
           
        }
    }

}
//public override CharNames charName => CharNames.Cahyo;

//        public override SkillNames skillName => SkillNames.WristSpin;

//        public override SkillLvl skillLvl => SkillLvl.Level2;

//        private PerkSelectState _state = PerkSelectState.Clickable;
//        public override PerkSelectState skillState { get => _state; set => _state = value; }

//        public override PerkNames perkName => PerkNames.FasterThanEyeCanSee;

//        public override PerkType perkType => PerkType.A2;

//        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

//        public override string desc => "FasterThanEyeCanSee";
//        private float _chance = 0f;
//        public override float chance { get => _chance; set => _chance = value; }
//        public override void SkillInit()
//        {
//            skillModel = SkillService.Instance.allSkillModels
//                                                   .Find(t => t.skillName == skillName);

//            charController = CharacterService.Instance.GetCharCtrlWithName(charName);
//            skillController = SkillService.Instance.currSkillMgr;
//            charGO = SkillService.Instance.GetGO4Skill(charName);
//        }
//        public override void SkillHovered()
//        {
//            SkillInit();

//            SkillServiceView.Instance.skillCardData.skillModel = skillModel;

//            SkillService.Instance.SkillHovered += DisplayFX1;


//        }
//        public override void SkillSelected()
//        {
//            DynamicPosData currCharDyna = GridService.Instance.GetDyna4GO(charGO);
//            if(skillModel.castPos != null)
//            {
//                Debug.Log("CAst pos is fine" + charGO.name); //+ currCharDyna.charMode); 
//            }

//            if (!skillModel.castPos.Any(t => t == currCharDyna.currentPos))
//                return;

//            skillController.allPerkBases.Find(t => t.skillName == skillName).RemoveFX2();
//            SkillService.Instance.SkillApply += BaseApply;
//            SkillService.Instance.SkillApply += ApplyFX1;
//        }

//        public override void BaseApply()
//        {
//            targetGO = SkillService.Instance.currentTargetDyna.charGO;
//            targetController = targetGO.GetComponent<CharController>();
//            CombatEventService.Instance.OnEOR += Tick;
//        }

//        public override void Tick()
//        {
//            if (roundEnd >= 1)
//                SkillEnd();
//            roundEnd++;
//        }
//        public override void DisplayFX1()
//        {
//            str1 = $"<style=Enemy> <style=States> Confused</style> 1 rd ";
//            SkillServiceView.Instance.skillCardData.descLines.Add(str1);
//        }

//        public override void ApplyFX1()
//        {
//            if (CkhNSetInCoolDown() || appliedOnce || IsTargetMyEnemy()) return;
//            float chance = 30f;
//            if (targetController && chance.GetChance())
//                CharStatesService.Instance.SetCharState(targetGO, CharStateName.Confused); 
//        }

//        public override void ApplyFX2()
//        {

//        }

//        public override void ApplyFX3()
//        {


//        }

//        public override void ApplyFX4()
//        {
//        }


//        public override void RemoveFX1()
//        {
//            SkillService.Instance.SkillApply -= ApplyFX1;

//        }

//        public override void RemoveFX2()
//        {
//        }

//        public override void RemoveFX3()
//        {
//        }

//        public override void RemoveFX4()
//        {
//        }

//        public override void SkillEnd()
//        {
//            if (IsTargetMyEnemy()) return;


//            CharStatesService.Instance.RemoveCharState(targetGO, CharStateName.Confused);

//            CombatEventService.Instance.OnEOR -= Tick;
//            RemoveFX1();
//        }




//        public override void DisplayFX2()
//        {
//        }

//        public override void DisplayFX3()
//        {
//        }

//        public override void DisplayFX4()
//        {
//        }

//        public override void WipeFX1()
//        {
//            SkillService.Instance.SkillHovered += DisplayFX1;

//        }

//        public override void WipeFX2()
//        {
//        }

//        public override void WipeFX3()
//        {
//        }

//        public override void WipeFX4()
//        {
//        }
//        public override void PreApplyFX()
//        {
//        }

//        public override void PostApplyFX()
//        {
//        }