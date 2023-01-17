using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq; 


namespace Combat
{
    public abstract class PassiveSkillBase
    {

        #region Declarations
        protected GameObject charGO;
        public GameObject targetGO;

        protected CharController targetController;
        protected CharController charController;
        protected SkillController1 skillController;  // ?? to be checked when it init
        protected DynamicPosData currDyna;

        protected SkillData skillData;
        protected string str0, str1, str2, str3;

        public abstract PassiveSkillNames passiveSkillName { get; }
        public abstract CharNames charName { get; set; }
        public abstract SkillNames skillName { get; set; }
        public virtual SkillModel skillModel { get; set; }   
        public abstract SkillLvl skillLvl { get; }
        public abstract string desc { get; }
        public abstract float chance { get; set; }


        #endregion

        #region APPLY and HOVER

        public virtual void SkillInit(SkillNames skillName, int charID)
        {
            charController = CharService.Instance.GetCharCtrlWithCharID(charID);
            charGO = SkillService.Instance.GetGO4SkillCtrller(charName);
            currDyna = GridService.Instance.GetDyna4GO(charGO);

            skillController = SkillService.Instance.GetSkillController(charController);
            skillModel = skillController?.allSkillModels.Find(t => t.skillName == skillName);
            AddTargetPos();
        }
        public virtual void SkillSelected()
        {

            if (!skillModel.castPos.Any(t => t == currDyna.currentPos))
                return;
            AddTargetPos();
            SkillService.Instance.SkillApply += BaseApply;
            SkillService.Instance.SkillApply += ApplyFX1;
            SkillService.Instance.SkillApply += ApplyFX2;
            SkillService.Instance.SkillApply += ApplyFX3;
            SkillService.Instance.SkillApply += ApplyVFx;
            SkillService.Instance.SkillApplyMoveFx += ApplyMoveFX;

            SkillService.Instance.PostSkillApply += PostApplyFX;
            //  GridService.Instance.HLTargetTiles(skillModel.targetPos);
        }
        public abstract void AddTargetPos();

        public virtual void SkillHovered()
        {
            SkillService.Instance.skillModelHovered = skillModel;
            SkillService.Instance.SkillHovered += DisplayFX1;
            SkillService.Instance.SkillHovered += DisplayFX2;
            SkillService.Instance.SkillHovered += DisplayFX3;
            SkillService.Instance.SkillHovered += DisplayFX4;
        }
        public virtual void BaseApply()
        {
            targetGO = SkillService.Instance.currentTargetDyna.charGO;
            targetController = targetGO.GetComponent<CharController>();
            if (skillModel.castTime > 0)
                CombatEventService.Instance.OnSOT += Tick;
        }

        public abstract void ApplyFX1();
        public abstract void ApplyFX2();
        public abstract void ApplyFX3();
        public abstract void ApplyVFx();
        public abstract void ApplyMoveFX();
        public virtual void Tick()
        {
            int roundCounter = CombatService.Instance.currentRound - skillModel.lastUsedInRound;
            if (skillModel.castTime < 0) return;
            if (roundCounter >= skillModel.castTime)
                SkillEnd();
        }
        public virtual void SkillEnd()
        {
            CombatEventService.Instance.OnSOR -= Tick;
            RemoveFX1();
            RemoveFX2();
            RemoveFX3();
            RemoveVFx();
            RemoveMoveFX();
        }
        public abstract void DisplayFX1();
        public abstract void DisplayFX2();
        public abstract void DisplayFX3();
        public abstract void DisplayFX4();
        public abstract void PreApplyFX();
        public abstract void PostApplyFX();
        public virtual void WipeFX1() => SkillService.Instance.SkillHovered -= DisplayFX1;
        public virtual void WipeFX2() => SkillService.Instance.SkillHovered -= DisplayFX2;
        public virtual void WipeFX3() => SkillService.Instance.SkillHovered -= DisplayFX3;
        public virtual void WipeFX4() => SkillService.Instance.SkillHovered -= DisplayFX4;
        public virtual void RemoveFX1() => SkillService.Instance.SkillApply -= ApplyFX1;
        public virtual void RemoveFX2() => SkillService.Instance.SkillApply -= ApplyFX2;
        public virtual void RemoveFX3() => SkillService.Instance.SkillApply -= ApplyFX3;
        public virtual void RemoveVFx() => SkillService.Instance.SkillApply -= ApplyVFx;
        public virtual void RemoveMoveFX() => SkillService.Instance.SkillApplyMoveFx -= ApplyMoveFX;

        #endregion
        public bool IsTargetAlly()
        {
            CharMode strikerCharMode = charController.charModel.charMode;
            return (SkillService.Instance.currentTargetDyna.charMode == strikerCharMode);
        }
        public bool IsTargetEnemy()
        {
            CharMode strikerCharMode = charController.charModel.charMode;
            CharMode targetCharMode = strikerCharMode.FlipCharMode();
            return (SkillService.Instance.currentTargetDyna.charMode == targetCharMode);
        }



    }

}
