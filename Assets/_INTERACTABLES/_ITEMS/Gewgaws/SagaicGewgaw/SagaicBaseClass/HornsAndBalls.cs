using Combat;
using Common;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEngine;



namespace Interactables
{
    public class HornsAndBalls : SagaicGewgawBase
    {
        public override SagaicGewgawNames sagaicGewgawName => SagaicGewgawNames.HornsAndBalls;

        public override void GewGawSagaicInit()
        {

        }
        //   +4 vigor when Starving(buff)  
        //    Gain +2 Acc until eoc upon Crit(buff) 
        //    Gains 4-6 Fortitude upon No Patience(instant buff)
  
        public override void EquipGewgawSagaic()
        {
            CharStatesService.Instance.OnCharStateStart += OnCharStateStart;
            CharStatesService.Instance.OnCharStateEnd += OnCharStateEnd;
            charController.damageController.OnDamageApplied += OnCritHit;
            SkillService.Instance.OnSkillUsed += OnSkillUsed;
        }
        void OnCharStateStart(CharStateModData charStateModData)
        {
            if (charStateModData.charStateName != CharStateName.Starving) return;
             int buffID =   charController.buffController.ApplyBuff(CauseType.SagaicGewgaw,
                             (int)sagaicGewgawName, charStateModData.causeByCharID,AttribName.vigor,
                             +4, TimeFrame.Infinity, -1, true); 
            buffIndex.Add(buffID);
        }

        void OnCharStateEnd(CharStateData charStateData)
        {
            charController.buffController.RemoveBuff(buffIndex[0]);// vigor buff  
        }

        void OnCritHit(DmgAppliedData dmgAppliedData)
        {
            if(dmgAppliedData.strikeType != StrikeType.Crit) return;
            int buffID = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw,
                             (int)sagaicGewgawName, charController.charModel.charID, AttribName.acc,
                             +2, TimeFrame.EndOfCombat, 1, true);
            buffIndex.Add(buffID); // id # 1 
        }


        void OnSkillUsed(SkillEventData skillEventData)
        {
            if (skillEventData.strikerController != charController.strikeController) return; 
            if(skillEventData.skillModel.skillName  == SkillNames.NoPatience)
            {
                charController.ChangeStat(CauseType.SagaicGewgaw, (int)sagaicGewgawName, charController.charModel.charID
                    , StatName.fortitude, UnityEngine.Random.Range(4,6),true); 
            }
        }
        public override void UnEquipSagaic()
        {
            CharStatesService.Instance.OnCharStateStart -= OnCharStateStart;
            CharStatesService.Instance.OnCharStateEnd -= OnCharStateEnd;

            charController.damageController.OnDamageApplied -= OnCritHit;

            SkillService.Instance.OnSkillUsed -= OnSkillUsed;
            charController.buffController.RemoveBuff(buffIndex[0]);// vigor buff // no other buff sticks  
        }
    }


}
