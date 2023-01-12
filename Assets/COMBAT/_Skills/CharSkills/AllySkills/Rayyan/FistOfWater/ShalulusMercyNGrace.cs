using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class ShalulusMercyNGrace : PerkBase
    {
        public override PerkNames perkName => PerkNames.ShalulusMercyAndGrace;
        public override PerkType perkType => PerkType.A1;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "180% --> 140% Water /n Heal ally party for 5-10 /n Clears Burning and Soaked on ally party /n Once per combat --> 4 rds cd /n (If Morale 12, heals for 10-20)";

        public override CharNames charName => CharNames.Rayyan;

        public override SkillNames skillName => SkillNames.FistOfWater;

        public override SkillLvl skillLvl => SkillLvl.Level1;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        public List<GameObject> allyGOs = new List<GameObject>();
        public override void AddTargetPos()
        {
            if (skillModel == null) return;
            for (int i = 1; i < 8; i++)
            {            
                    CellPosData cellPosData = new CellPosData(CharMode.Ally, i);
                    DynamicPosData dyna = GridService.Instance.gridView.GetDynaFromPos(cellPosData.pos, cellPosData.charMode);
                    if (dyna != null)
                    {
                        skillModel.targetPos.Add(cellPosData);
                        allyGOs.Add(dyna.charGO);
                    }                
            }
        }

        public override void SkillSelected()
        {
            base.SkillSelected();
            skillModel.damageMod = 140f; 
        }

        public override void ApplyFX1()
        {
            StatData statData = charController.GetStat(StatsName.morale);
            if(statData.currValue < 12)
            {
                allyGOs.ForEach(t => t.GetComponent<CharController>().damageController
                 .ApplyDamage(charController, CauseType.CharSkill, (int)SkillNames.FistOfWater, DamageType.Heal
                                                                    , UnityEngine.Random.Range(6, 12)));
            }else
            {
                allyGOs.ForEach(t => t.GetComponent<CharController>().damageController
                        .ApplyDamage(charController, CauseType.CharSkill, (int)SkillNames.FistOfWater, DamageType.Heal
                                        , UnityEngine.Random.Range(12, 24)));
            }       
        }
        public override void ApplyFX2()
        {
            allyGOs.ForEach(t => CharStatesService.Instance.ClearDOT(t, CharStateName.BurnLowDOT)); 
        }

        public override void ApplyFX3()
        {
            allyGOs.ForEach(t => CharStatesService.Instance
             .ApplyCharState(t, CharStateName.Soaked
                            , charController, CauseType.CharSkill, (int)skillName)
                            );
        }

        public override void ApplyMoveFX()
        {
        }

        public override void ApplyVFx()
        {
        }

        public override void DisplayFX1()
        {
            str0 = $"<style=Heal>Heal</style> 6-12";
            SkillService.Instance.skillCardData.descLines.Add(str0);

            str1 = $"If <style=Attribute>Morale</style> 12, <style=Heal>Heal</style> 12-24";
            SkillService.Instance.skillCardData.descLines.Add(str1);
        }

        public override void DisplayFX2()
        {
            // add clear 
            // add soaked 
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


//public override CharNames charName => CharNames.Rayyan;

//public override SkillNames skillName => SkillNames.FistOfWater;

//public override SkillLvl skillLvl => SkillLvl.Level1;

//private PerkSelectState _state = PerkSelectState.Clickable;
//public override PerkSelectState skillState { get => _state; set => _state = value; }

//public override PerkNames perkName => PerkNames.ShalulusMercyAndGrace;

//public override PerkType perkType => PerkType.A1;
//public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

//public override string desc => "ShalalusMercyNGrace";
//private float _chance = 0f;
//public override float chance { get => _chance; set => _chance = value; }

//List<GameObject> targetGOs = new List<GameObject>();
//List<CharController> targetControllers = new List<CharController>();

//public override void SkillInit()
//{
//    skillModel = SkillService.Instance.allSkillModels
//                              .Find(t => t.skillName == skillName);

//    charController = CharacterService.Instance.GetCharCtrlWithName(charName);
//    skillController = SkillService.Instance.currSkillMgr;
//    charGO = SkillService.Instance.GetGO4Skill(charName);
//    skillModel.damageMod = 140f;
//}
//public override void SkillHovered()
//{
//    SkillInit();

//    SkillServiceView.Instance.skillCardData.skillModel = skillModel;
//    Debug.Log("skill hovered" + perkName);
//    SkillService.Instance.SkillHovered += DisplayFX1;
//    SkillService.Instance.SkillHovered += DisplayFX2;

//}
//public override void SkillSelected()
//{

//    SkillService.Instance.SkillApply += BaseApply;
//    SkillService.Instance.SkillApply += ApplyFX1;
//    SkillService.Instance.SkillApply += ApplyFX2;

//    // skillModel.targetPos.Clear();
//    for (int i = 1; i < 8; i++)
//    {

//        CellPosData cellPosData = new CellPosData(CharMode.Ally, i);
//        skillModel.targetPos.Add(cellPosData);

//    }
//    GridService.Instance.HLTargetTiles(skillModel.targetPos); // ov
//}
//public override void BaseApply()
//{
//    CombatEventService.Instance.OnEOR += Tick;

//}
//public override void ApplyFX1()
//{
//    if (CkhNSetInCoolDown() || appliedOnce) return;
//    if (targetControllers.Count <= 0) return;
//    foreach (var target in targetControllers)
//    {
//        if (target.charModel.charMode == CharMode.Ally)
//        {
//            if (charController.GetStat(StatsName.morale).baseValue >= 12)
//                target.dmgController.ApplyDamage(charController, DamageType.Heal
//                                                      , Random.Range(5, 10), false);
//            else
//                target.dmgController.ApplyDamage(charController, DamageType.Heal
//                                                      , Random.Range(10, 20), false);

//        }
//    }
//}

//public override void ApplyFX2()
//{
//    if (CkhNSetInCoolDown() || appliedOnce) return;

//    foreach (var target in targetControllers)
//    {
//        if (target.charModel.charMode == CharMode.Ally)
//        {
//            CharStatesService.Instance.RemoveCharState(target.gameObject, CharStateName.BurnHighDOT);
//            CharStatesService.Instance.RemoveCharState(target.gameObject, CharStateName.BurnMedDOT);
//            CharStatesService.Instance.RemoveCharState(target.gameObject, CharStateName.BurnLowDOT);
//        }
//    }



//}

//public override void ApplyFX3()
//{
//}

//public override void ApplyFX4()
//{
//}



//public override void RemoveFX1()
//{
//}

//public override void RemoveFX2()
//{
//}

//public override void RemoveFX3()
//{
//}

//public override void RemoveFX4()
//{
//}

//public override void SkillEnd()
//{
//}



//public override void Tick()
//{
//}



//public override void DisplayFX1()
//{
//}

//public override void DisplayFX2()
//{
//}

//public override void DisplayFX3()
//{
//}

//public override void DisplayFX4()
//{
//}

//public override void WipeFX1()
//{
//}

//public override void WipeFX2()
//{
//}

//public override void WipeFX3()
//{
//}

//public override void WipeFX4()
//{
//}

//public override void PreApplyFX()
//{
//}

//public override void PostApplyFX()
//{
//}
