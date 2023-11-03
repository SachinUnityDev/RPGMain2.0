using Combat;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;


namespace Common
{
    public class Confused : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Confused;
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }
        public override float chance { get; set; }
        public override void StateApplyFX()
        {
            // -2 acc .... Immune to concentrated
            //...50% chance to misfire ()
            int buffId = 
            charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                       , charID, AttribName.acc, -2, timeFrame, castTime, true);
            allBuffIds.Add(buffId);

            int buffImmuneID =
            charController.charStateController.ApplyImmunityBuff(CauseType.CharState, (int)charStateName
                                        , charID, CharStateName.Concentrated, timeFrame, castTime);
            allImmunityBuffs.Add(buffImmuneID);

           // SkillService.Instance.OnSkillUsed += ChgTargetsORMisfire;
            
        }

        //bool ChgTargetsORMisfire(SkillEventData skillEventData)
        //{

        //    if (skillEventData.strikerController.charModel.charID != charID) return true;
        //    if (50f.GetChance())
        //    {
        //        charController.damageController.ApplyMisFire(); 
        //        SkillService.Instance.PostSkillApply += PostSkillApply; 
        //        return true;
        //    }
        //    else
        //    {
        //        // hit friendly targets... 
        //        // get skill base change targets and apply FX
        //        SkillController1 skillController = charController.skillController;
               
        //        int index =
        //                skillController.allSkillBases.FindIndex(t => t.skillModel.skillName == skillEventData.skillName);
        //        if (index == -1) return true;

        //        SkillBase skillBase = skillController.allSkillBases[index];
        //        SkillModel skillModel = skillBase.skillModel;   
                
        //        CombatService.Instance.currTargetClicked = null;
        //        CombatService.Instance.mainTargetDynas = null;
        //        skillModel.targetPos.Clear();

        //        DynamicPosData myDyna = GridService.Instance.GetDyna4GO(charController.gameObject); 

        //        for (int i = 1; i < 8; i++)
        //        {
        //            if (!(myDyna.currentPos == i))
        //            {
        //                CellPosData cellPosData = new CellPosData(CharMode.Ally, i);
        //                DynamicPosData dyna = GridService.Instance.gridView.GetDynaFromPos(cellPosData.pos, cellPosData.charMode);
        //                if (dyna != null)
        //                {
        //                    skillModel.targetPos.Add(cellPosData);
        //                    CombatService.Instance.mainTargetDynas.Add(dyna);
        //                }
        //            }
        //        }
        //        int ran = UnityEngine.Random.Range(0,CombatService.Instance.mainTargetDynas.Count);


        //        skillBase.targetGO = CombatService.Instance.mainTargetDynas[ran].charGO;
        //        skillBase.targetController = skillBase.targetGO.GetComponent<CharController>();  
        //        skillBase.PreApplyFX();
        //        skillBase.ApplyFX1();
        //        skillBase.ApplyFX2();
        //        skillBase.ApplyFX3();
        //        skillBase.ApplyMoveFx();
        //        skillBase.ApplyVFx();
        //        skillBase.PostApplyFX();
        //    }
        //    return false; 
        //}
        //void PostSkillApply()
        //{
        //    SkillService.Instance.PostSkillApply -= PostSkillApply;
        //}
        public override void StateApplyVFX()
        {

        }

        public override void StateDisplay()
        {
            str0 = "<style=States> Confused </style>";
            allStateFxStrs.Add(str0);
            str1 = $"-2<style=Attributes> Acc </style>";
            allStateFxStrs.Add(str1);          
            str2 = "Immune to <style=States> Concentrated </style>";
            allStateFxStrs.Add(str2);
        }

    }
}

