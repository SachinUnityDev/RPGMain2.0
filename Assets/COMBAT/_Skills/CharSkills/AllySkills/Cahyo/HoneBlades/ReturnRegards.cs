using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Combat
{
    public class ReturnRegards : PerkBase
    {
        public override CharNames charName => CharNames.Cahyo;
        public override SkillNames skillName => SkillNames.HoneBlades;
        public override SkillLvl skillLvl => SkillLvl.Level2;        
        public override PerkSelectState state { get; set; }
        public override PerkNames perkName => PerkNames.ReturnedRegards;
        public override PerkType perkType => PerkType.A2;
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };
        public override string desc => "Return regards";        
        public override float chance { get; set; }
        DynamicPosData charDyna;
        //public override void SkillInit()
        //{
        //    skillModel = SkillService.Instance.allSkillModels
        //                                   .Find(t => t.skillName == skillName);

        //    charController = CharacterService.Instance.GetCharCtrlWithName(charName);
        //    skillController = SkillService.Instance.currSkillMgr;
        //    charGO = SkillService.Instance.GetGO4Skill(charName);
        //    charDyna = GridService.Instance.GetDyna4GO(charGO);
        //}


        //public override void SkillHovered()
        //{
        //    SkillInit();

        //    SkillService.Instance.skillCardData.skillModel = skillModel;
        //    SkillService.Instance.SkillHovered += DisplayFX1;
        //}

        //public override void SkillSelected()
        //{
        //    SkillService.Instance.SkillApply += BaseApply;
        //    SkillService.Instance.SkillApply += ApplyFX1;
        //}
        //public override void BaseApply()
        //{
        //    CombatEventService.Instance.OnEOR += Tick;
        //    CombatEventService.Instance.OnDmgDelivered += RetaliateAdjHitWithMelee;

        //}

        void RetaliateAdjHitWithMelee(DmgData _dmgData)
        {
            //if (_dmgData.attackType != AttackType.Melee) return;

            //List<DynamicPosData> adjDynas = GridService.Instance.gridController.GetAllAdjDynaOccupied(charDyna);

            //if (adjDynas.Count == 0) return;
            //foreach (DynamicPosData adjDyna in adjDynas)
            //{
            //    if (adjDyna.charGO.GetComponent<CharController>() == _dmgData.charController)
            //    {
            //        SkillService.Instance.currSkillName = SkillNames.WristSpin;
            //        CombatService.Instance.currentCharSelected = charController;
            //        targetGO = _dmgData.striker.gameObject;
            //        SkillService.Instance.TargetIsSelected(GridService.Instance.GetDyna4GO(targetGO));
            //    }
            //}
        }
        public override void DisplayFX1()
        {
            str1 = $"<style=Enemy> retaliates with Wrist spin for adj Targets";
            SkillService.Instance.skillModelHovered.descLines.Add(str1);
        }
        public override void SkillEnd()
        {
            //CombatEventService.Instance.OnEOR -= Tick;
            //CombatEventService.Instance.OnDmgDelivered -= RetaliateAdjHitWithMelee;
            //roundEnd = 0;
        }
        public override void Tick()
        {
            //if (roundEnd >= skillModel.castTime)
            //    SkillEnd();
            //roundEnd++;
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
        public override void DisplayFX2()
        {
        }

        public override void DisplayFX3()
        {
        }

        public override void DisplayFX4()
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
