using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common; 


namespace Combat
{
    public class WaterShell : SkillBase   // replacement for patience
    {
        public override SkillModel skillModel { get ; set; }
        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.WaterShell;

        public override SkillLvl skillLvl => SkillLvl.Level0;
        public override StrikeTargetNos strikeNos => StrikeTargetNos.Single;
        public override string desc => "This Is water Shell ";

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        float minArmor, maxArmor = 0; 

         int staminaIncr = 0; 
        public override void PopulateTargetPos()
        {
            skillModel.targetPos.Clear();
            int pos = GridService.Instance.GetDyna4GO(charGO).currentPos;
            skillModel.targetPos.Add(new CellPosData(myDyna.charMode, pos));
        }
        //public override void BaseApply()
        //{
        //    base.BaseApply();
           
        //}

        void StaminaRes(DmgData dmgData)
        {
            if (dmgData.targetController == charController)
            {
                if(dmgData.dmgRecievedType == DamageType.StaminaDmg)
                {
                    charController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName,
                                                                                           DamageType.StaminaDmg, -dmgData.dmgDelivered, true); 
                }
            }
        }
        public override void ApplyFX1()
        {

            staminaIncr = UnityEngine.Random.Range(2, 4);
            CombatEventService.Instance.OnDmgDelivered += StaminaRes;
            charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID, AttribName.staminaRegen
                , staminaIncr, TimeFrame.EndOfRound, skillModel.castTime, false);

            charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID, AttribName.haste
                , -3f, TimeFrame.EndOfRound, skillModel.castTime, false);
            charController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName,
                                                            DamageType.Heal, UnityEngine.Random.Range(4f, 9f), false);
        }

        public override void ApplyFX2()
        {
            //  +% 60 Armor, 
            float statDataMin = charController.GetAttrib(AttribName.armorMin).currValue;
            float statDataMax = charController.GetAttrib(AttribName.armorMax).currValue;
         
            charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                , AttribName.armorMin, statDataMin*0.6f, TimeFrame.EndOfRound, skillModel.castTime, true);

            charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                , AttribName.armorMin, statDataMax * 0.6f, TimeFrame.EndOfRound, skillModel.castTime, true);
        }

        public override void ApplyFX3()
        {
            //+14 Elemental resistances(water, fire, earth, air), Ignore incoming stamina dmg
            charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID, 
                AttribName.waterRes, +14, TimeFrame.EndOfRound, skillModel.castTime, true);
            charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID, AttribName.fireRes, +14,
                TimeFrame.EndOfRound, skillModel.castTime, true);
          
        }


        public override void SkillEnd()
        {
            base.SkillEnd();
           // charController.charModel.staminaRegen.currValue -= staminaIncr;
            //charController.ChangeStat(CauseType.CharSkill, (int)skillName, charController, StatsName.haste, 3f);

            //charController.ChangeStat(CauseType.CharSkill, (int)skillName, charController, StatsName.waterRes, -14);
            //charController.ChangeStat(CauseType.CharSkill, (int)skillName, charController, StatsName.fireRes, -14);
      
            //charController.ChangeStatRange(CauseType.CharSkill, (int)skillName, charController
            //  , StatsName.armor, -minArmor, -maxArmor);

            CombatEventService.Instance.OnDmgDelivered -= StaminaRes;

        }

        public override void ApplyMoveFx()
        {


        }

        public override void ApplyVFx()
        {
            SkillService.Instance.skillFXMoveController.SingleTargetRangeStrike(PerkType.None);


        }

        public override void PopulateAITarget()
        {
        }

        public override void DisplayFX1()
        {
            str0 = $"2 -3 <style=Attributes>Stamina regen</style>, {skillModel.castTime} rd ";
            SkillService.Instance.skillModelHovered.descLines.Add(str0);
        }

        public override void DisplayFX2()
        {
            str1 = $"-3 <style=Attributes>Haste</style>, {skillModel.castTime} rd ";
            SkillService.Instance.skillModelHovered.descLines.Add(str1);
        }

        public override void DisplayFX3()
        {
            str2 = $"4-10 <style=Attributes>Heal</style>, {skillModel.castTime} rd ";
            SkillService.Instance.skillModelHovered.descLines.Add(str2);
            str3 = $"+14 <style=Attributes>Elemental Res</style>, {skillModel.castTime} rd ";
            SkillService.Instance.skillModelHovered.descLines.Add(str3);
        }

        public override void DisplayFX4()
        {
            str0 = $"Ignore <style=Attributes>Stamina Dmg</style>, {skillModel.castTime} rd ";
            SkillService.Instance.skillModelHovered.descLines.Add(str0);
        }

  
    }
}



//public override SkillModel skillModel { get; set; }

//private CharNames _charName;
//public override CharNames charName { get => _charName; set => _charName = value; }

//public override SkillNames skillName => SkillNames.WaterShell;

//public override SkillLvl skillLvl => SkillLvl.Level0;

//public override string desc => "Water Shell";

//private float _chance = 0f;
//public override float chance { get => _chance; set => _chance = value; }

//public override void PopulateTargetPos()
//{
//    skillModel.targetPos.Clear();
//    int pos = GridService.Instance.GetDyna4GO(charGO).currentPos;
//    skillModel.targetPos.Add(new CellPosData(myDyna.charMode, pos));
//}
//public override void ApplyFX1()
//{
//    charController.charModel.staminaRegen += UnityEngine.Random.Range(2, 4);
//    charController.ChangeStat(CauseType.CharSkill, (int)skillName, charController, StatsName.haste, -2f);
//    charController.damageController.ApplyDamage(charController, skillName, DamageType.Heal, UnityEngine.Random.Range(4f, 10f), false);

//}

//public override void ApplyFX2()
//{
//}

//public override void ApplyFX3()
//{
//}

//public override void ApplyVFx()
//{
//}

//public override void AutoSelectTarget()
//{
//}

//public override void DisplayFX1()
//{
//    str0 = "<margin=1.2em>Ranged";
//    SkillServiceView.Instance.skillCardData.descLines.Add(str0);
//    str1 = "<margin=1.2em>Ranged";
//    SkillServiceView.Instance.skillCardData.descLines.Add(str1);
//    str3 = "<margin=1.2em>Ranged";
//    SkillServiceView.Instance.skillCardData.descLines.Add(str2);
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

//public override void ApplyMoveFx()
//{
//}
