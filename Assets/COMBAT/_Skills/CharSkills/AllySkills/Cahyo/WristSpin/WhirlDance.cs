using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class WhirlDance : PerkBase
    {
        public override PerkNames perkName => PerkNames.Whirldance;

        public override PerkType perkType => PerkType.A3;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.WideSwing };

        public override string desc => "(PR: Wide Swing.) /n  Hits all enemies./n  now 2 rd cd.  6 stamina cost";

        public override CharNames charName => CharNames.Cahyo;

        public override SkillNames skillName => SkillNames.WristSpin;

        public override SkillLvl skillLvl => SkillLvl.Level3;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        public override void AddTargetPos()
        {
            if (skillModel == null) return;
            CombatService.Instance.mainTargetDynas.Clear(); skillModel.targetPos.Clear();
             CombatService.Instance.mainTargetDynas.AddRange(GridService.Instance.GetAllByCharMode(CharMode.Enemy));
            foreach (DynamicPosData dyna in CombatService.Instance.mainTargetDynas)
            {
                skillModel.targetPos.Add(new CellPosData(dyna.charMode, dyna.currentPos));
            }
        }

        public override void BaseApply()
        {
            base.BaseApply();
            skillModel.cd = 2;
            skillModel.staminaReq = 6; 

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

        public override void DisplayFX1()
        {
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


//        public override CharNames charName => CharNames.Cahyo;
//        public override SkillNames skillName => SkillNames.WristSpin;
//        public override SkillLvl skillLvl => SkillLvl.Level3;

//        private PerkSelectState _state = PerkSelectState.Clickable;
//        public override PerkSelectState skillState { get => _state; set => _state = value; }
//        public override PerkNames perkName => PerkNames.WhirlDance;
//        public override PerkType perkType => PerkType.A3;
//        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.WideSwing };

//        public override string desc => "Whirl Dance";
//        private float _chance = 0f;
//        public override float chance { get => _chance; set => _chance = value; }
//        public override void SkillInit()
//        {
//            skillModel = SkillService.Instance.allSkillModels
//                                                .Find(t => t.skillName == skillName);

//            charController = CharacterService.Instance.GetCharCtrlWithName(charName);
//            skillController = SkillService.Instance.currSkillMgr;
//            charGO = SkillService.Instance.GetGO4Skill(charName);
//            skillModel.cd = 2;
//            skillModel.staminaReq = 6; 


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

//            if (!skillModel.castPos.Any(t => t == currCharDyna.currentPos))
//                return;
//            for (int i = 5; i < 8; i++)
//            {
//                CellPosData cellPosData = new CellPosData(CharMode.Enemy, i);
//                skillModel.targetPos.Add(cellPosData);
//            }

//        }
//        public override void BaseApply()
//        {
//            targetGO = SkillService.Instance.currentTargetDyna.charGO;
//            targetController = targetGO.GetComponent<CharController>();
//        }
//        public override void Tick()
//        {
//        }

//        public override void ApplyFX1()
//        {

//        }

//        public override void ApplyFX2()
//        {
//            throw new System.NotImplementedException();
//        }

//        public override void ApplyFX3()
//        {
//            throw new System.NotImplementedException();
//        }

//        public override void ApplyFX4()
//        {
//            throw new System.NotImplementedException();
//        }



//        public override void RemoveFX1()
//        {
//            throw new System.NotImplementedException();
//        }

//        public override void RemoveFX2()
//        {
//            throw new System.NotImplementedException();
//        }

//        public override void RemoveFX3()
//        {
//            throw new System.NotImplementedException();
//        }

//        public override void RemoveFX4()
//        {
//            throw new System.NotImplementedException();
//        }

//        public override void SkillEnd()
//        {
//            throw new System.NotImplementedException();
//        }



//        public override void DisplayFX1()
//        {
//        }

//        public override void DisplayFX2()
//        {
//            throw new System.NotImplementedException();
//        }

//        public override void DisplayFX3()
//        {
//            throw new System.NotImplementedException();
//        }

//        public override void DisplayFX4()
//        {
//            throw new System.NotImplementedException();
//        }

//        public override void WipeFX1()
//        {
//            throw new System.NotImplementedException();
//        }

//        public override void WipeFX2()
//        {
//            throw new System.NotImplementedException();
//        }

//        public override void WipeFX3()
//        {
//            throw new System.NotImplementedException();
//        }

//        public override void WipeFX4()
//        {
//            throw new System.NotImplementedException();
//        }

//        public override void PreApplyFX()
//        {
//            throw new System.NotImplementedException();
//        }

//        public override void PostApplyFX()
//        {
//            throw new System.NotImplementedException();
//        }

