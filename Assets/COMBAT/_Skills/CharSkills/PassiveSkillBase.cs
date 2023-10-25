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

        protected string str0, str1, str2, str3;

        public SkillModel skillModel { get; set; }
        public abstract PassiveSkillNames passiveSkillName { get; }
        public abstract CharNames charName { get; set; }
        public abstract string desc { get; }
        public abstract float chance { get; set; }
        #endregion

        #region APPLY and HOVER

        public virtual void PassiveSkillInit(SkillController1 skillController)
        {
            this.skillController = skillController;            
            charController = skillController.GetComponent<CharController>();            
            charGO = charController.gameObject;
            currDyna = GridService.Instance.GetDyna4GO(charGO);
            charName = charController.charModel.charName;   
        }
        
       // public abstract void AddTargetPos();

        public virtual void SkillHovered()
        {

        }
        //public virtual void BaseFX(CharController targetController)
        //{
        //    this.targetController = targetController;
        //    targetGO = targetController.gameObject;
        //} 

        public abstract void ApplyFX();    

        #endregion
        protected bool IsTargetAlly()
        {
            CharMode strikerCharMode = charController.charModel.charMode;
            return (SkillService.Instance.currentTargetDyna.charMode == strikerCharMode);
        }
        protected bool IsTargetEnemy()
        {
            CharMode strikerCharMode = charController.charModel.charMode;
            CharMode targetCharMode = strikerCharMode.FlipCharMode();
            return (SkillService.Instance.currentTargetDyna.charMode == targetCharMode);
        }
    }

}
