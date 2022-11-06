//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Common; 


//namespace Combat
//{
//    public class AimTheWindPipe : SkillBase, Perk3
//    {
//        public override CharNames charName => CharNames.Cahyo;
//        public override SkillNames skillName => SkillNames.KrisLunge;

//        public override SkillLvl skillLvl => SkillLvl.Level3;

//        private PerkSelectState _state = PerkSelectState.Clickable;
//        public override PerkSelectState skillState { get => _state; set => _state = value; }
//        public override PerkNames perkName => PerkNames.AimTheWindpipe;

//        public override PerkType perkType => PerkType.A3;

//        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

//        public override string desc => "Aim the wind pipe";
//        private float _chance = 0f;
//        public override float chance { get => _chance; set => _chance = value; }
//        public override void SkillInit()
//        {
//            skillModel = SkillService.Instance.allSkillModels
//                                                 .Find(t => t.skillName == skillName);

//            charController = CharacterService.Instance.GetCharCtrlWithName(charName);
//            skillController = SkillService.Instance.currSkillMgr;
//        }
//        public override void SkillHovered()
//        {
//            SkillInit();

//            SkillServiceView.Instance.skillCardData.skillModel = skillModel;

//            SkillService.Instance.SkillHovered += DisplayFX1;
//            SkillService.Instance.SkillHovered += DisplayFX2;
//            SkillService.Instance.SkillHovered += DisplayFX3;
//        }
//        public override void SkillSelected()
//        {
//            SkillService.Instance.SkillApply += BaseApply;
//            SkillService.Instance.SkillApply += ApplyFX1;
//            SkillService.Instance.SkillApply += ApplyFX2;
//            SkillService.Instance.SkillApply += ApplyFX3;
//            SkillService.Instance.SkillApply += PostApplyFX;

//            skillModel.targetPos.Clear();
//        }

//        public override void BaseApply()
//        {
//            targetGO = SkillService.Instance.currentTargetDyna.charGO;
//            targetController = targetGO.GetComponent<CharController>();
//            CombatEventService.Instance.OnEOR += Tick;
//            skillModel.lastUsedInRound = CombatService.Instance.currentRound;
//            skillModel.cd = 3; 
//        }
//        public override void DisplayFX1()
//        {
//            str1 = $"cd increased 3 rd";
//            SkillServiceView.Instance.skillCardData.descLines.Add(str1);
//        }
//        public override void DisplayFX2()
//        {
//            str2 = $"<style=Enemy>half<style=Stamina> Stamina</style>";
//            SkillServiceView.Instance.skillCardData.descLines.Add(str2);
//        }
//        public override void ApplyFX1()
//        {
//            if (CkhNSetInCoolDown() || appliedOnce || IsTargetMyAlly()) return;

//            float stamina = targetController.GetStat(StatsName.stamina).baseValue;
//            targetController.SetStat(StatsName.stamina, stamina / 2, true); 

//        }
       
//        public override void DisplayFX3()
//        {
//            str3 = $"<style=Enemy><style=Fortitude> fortitude</style>, +6-8";
//            SkillServiceView.Instance.skillCardData.descLines.Add(str3);
//        }
//        public override void ApplyFX2()
//        {
//            if (CkhNSetInCoolDown() || appliedOnce || IsTargetMyAlly()) return;

//            targetController.ChangeStat(StatsName.fortitude, (float)Random.Range(6, 9), 0, 0); 
//        }
     
//        public override void ApplyFX3()
//        {

//        }
//        public override void Tick()
//        {

//        }


//        public override void ApplyFX4()
//        {

//        }

//        public override void RemoveFX1()
//        {

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

//        }

       

//        public override void DisplayFX4()
//        {
//        }

//        public override void WipeFX1()
//        {
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
//            appliedOnce = false; 
//        }

//        public override void PostApplyFX()
//        {
//            appliedOnce = true;
//        }
//    }

//}

