using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Combat
{
    public class KrisPoke : PerkBase
    {
        public override CharNames charName => CharNames.Cahyo;

        public override SkillNames skillName => SkillNames.KrisLunge;

        public override SkillLvl skillLvl => SkillLvl.Level1;
        
        public override PerkSelectState state { get; set; }
        public override PerkNames perkName => PerkNames.KrisPoke;

        public override PerkType perkType => PerkType.A1;

        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "This Is KrisPoke ";

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        //public override void SkillInit()
        //{
        //    skillModel = SkillService.Instance.allSkillModels
        //                                        .Find(t => t.skillName == skillName);

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
        //    skillController.allSkillBases.Find(t => t.skillName == skillName).WipeFX3();
        //}

        //public override void SkillSelected()
        //{
        //    SkillService.Instance.SkillApply += BaseApply;
        //    SkillService.Instance.SkillApply += ApplyFX1;
        //    SkillService.Instance.SkillApply += ApplyFX2;
        //    SkillService.Instance.SkillApply += ApplyFX3;
        //    skillModel.targetPos.Clear();
        //    skillModel.castPos.Clear();
        //    skillModel.castPos = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };

        //    skillController.allSkillBases.Find(t => t.skillName == skillName).RemoveFX3();

        //    //List<DynamicPosData> sameLaneOccupiedPos = GridService.Instance.GetAllInSameLane
        //    //             (new CellPosData(CharMode.Ally, GridService.Instance.GetDyna4GO(charGO).currentPos));
        //    //skillModel.targetPos.Add(new CellPosData(CharMode.Enemy, sameLaneOccupiedPos[0].currentPos));
        //    //GridService.Instance.HLTargetTiles(skillModel.targetPos); // overr
        //}

        //public override void BaseApply()
        //{
        //    targetGO = SkillService.Instance.currentTargetDyna.charGO;
        //    targetController = targetGO.GetComponent<CharController>();
        //    CombatEventService.Instance.OnEOR += Tick;
        //    skillModel.lastUsedInRound = CombatService.Instance.currentRound;
        //}


        public override void ApplyFX1()
        {
            //if (CkhNSetInCoolDown() || appliedOnce || IsTargetMyAlly()) return;

            //targetController.ChangeStat(StatsName.initiative, -2, 0, 0);

        }
        public override void DisplayFX1()
        {
            str1 = $"<style=Enemy> Init,-2 rd";
            SkillService.Instance.skillCardData.descLines.Add(str1);

        }
        public override void ApplyFX2()
        {
        }

        public override void ApplyFX3()
        {

        }
        public override void Tick()
        {
            //if (roundEnd >= skillModel.castTime)
            //    SkillEnd();
            //roundEnd++;
        }
        public override void SkillEnd()
        {
            //if (IsTargetMyEnemy()) return;

            //targetController.ChangeStat(StatsName.initiative, -2, 0, 0);

            //CombatEventService.Instance.OnEOR -= Tick;

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

        public override void DisplayFX2()
        {
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

