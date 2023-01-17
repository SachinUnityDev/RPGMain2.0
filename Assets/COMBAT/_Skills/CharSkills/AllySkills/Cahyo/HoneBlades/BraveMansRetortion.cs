using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;
namespace Combat
{
    public class BraveMansRetortion : PerkBase
    {
        public override CharNames charName => CharNames.Cahyo;

        public override SkillNames skillName => SkillNames.HoneBlades;

        public override SkillLvl skillLvl => SkillLvl.Level3;
       
        public override PerkSelectState state { get;set; }

        public override PerkNames perkName => PerkNames.BraveMansRetortion;

        public override PerkType perkType => PerkType.A3;

        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.ReturnedRegards };

        public override string desc => "Brave Mans retoration";
        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        DynamicPosData charDyna;
        //public override void SkillInit()
        //{
        //    skillModel = SkillService.Instance.allSkillModels
        //                                     .Find(t => t.skillName == skillName);

        //    charController = CharacterService.Instance.GetCharCtrlWithName(charName);
        //    skillController = SkillService.Instance.currSkillMgr;
        //    charGO = SkillService.Instance.GetGO4Skill(charName);
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
        //    DynamicPosData currCharDyna = GridService.Instance.GetDyna4GO(charGO);

        //    if (!skillModel.castPos.Any(t => t == currCharDyna.currentPos))
        //        return;
        //    SkillService.Instance.SkillApply += BaseApply;

        //}

        //public override void BaseApply()
        //{
        //    CombatEventService.Instance.OnEOR += Tick;
        //    CombatEventService.Instance.OnDmgDelivered += RetaliatedAdjHitWithMelee;

        //}

        public override void DisplayFX1()
        {
            str1 = $"<style=Enemy> self gain +3 fort for every retaliation";
            SkillService.Instance.skillModelHovered.descLines.Add(str1);
        }
        void RetaliatedAdjHitWithMelee(DmgData _dmgData)
        {
            //if (_dmgData.attackType != AttackType.Melee) return;
            //// get adjacent from gridService
            //List<DynamicPosData> adjDynas = GridService.Instance.gridController.GetAllAdjDynaOccupied(charDyna);

            //if (adjDynas.Count == 0) return;
            //foreach (DynamicPosData adjDyna in adjDynas)
            //{
            //    if (adjDyna.charGO.GetComponent<CharController>() == _dmgData.charController)
            //    {
            //        charController.ChangeStat(StatsName.fortitude, +3f, 0, 0);
            //    }
            //}

        }
        public override void SkillEnd()
        {
            //CombatEventService.Instance.OnEOR -= Tick;
            //CombatEventService.Instance.OnDmgDelivered -= RetaliatedAdjHitWithMelee;
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

