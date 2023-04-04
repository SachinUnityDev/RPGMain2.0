using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Common;
using System.Linq;
using Interactables;
namespace Combat
{
    [System.Serializable]
    public abstract class SkillBase 
    {
        #region Declarations

        protected GameObject charGO;
        public GameObject targetGO;
        public virtual int charID { get; set; }
        protected CharController targetController;
        protected CharController charController;
        protected SkillController1 skillController;
        protected DynamicPosData myDyna; 

        protected SkillData skillData = new SkillData();
        protected string str0, str1, str2, str3;

        public abstract SkillModel skillModel { get; set; } 
        public abstract CharNames charName { get; set; }
        public abstract SkillNames skillName { get; }      
        public abstract SkillLvl skillLvl { get;  }
        public abstract StrikeTargetNos strikeNos { get; }
        public abstract string desc { get;  }
        public abstract float chance { get; set; }
        #endregion

        #region APPLY and HOVER
        public virtual void  SkillInit(SkillController1 _skillController) 
        {
            //if (SkillService.Instance.allSkillModels
            //    .Any(t => t.skillName == skillName && t.charName == charName 
            //    && charController.charModel.charID == _charID)) return;
           // if (SkillService.Instance.allSkillModels.Any(t => t.skillID == _skillID)) return;

            SkillDataSO skillDataSO = SkillService.Instance.GetSkillSO(charName);
            Debug.Log("SKILLNAME........" + skillName);

            
         
            charController = CharService.Instance.GetCharCtrlWithName(skillDataSO.charName);
            skillController = charController.GetComponent<SkillController1>();
            charID = charController.charModel.charID; 
            skillData = skillDataSO.allSkills.Find(t => t.skillName == skillName);
           
            skillModel = new SkillModel(skillData);
            //skillModel.skillID = skillController.skillID;
            skillModel.charID = charID; 
            //SkillService.Instance.allSkillModels.Add(skillModel);
            skillController.allSkillModels.Add(skillModel);   // skillModel for ref
            charGO = skillController.gameObject;
            if(GameService.Instance.gameModel.gameState == GameState.InCombat)
            {
                myDyna = GridService.Instance.GetDyna4GO(charGO);
                // Debug.Log("INSIDE SKILL INIT" + skillName);
                // Do a Skill Init at the start of the combat.. 

                PopulateTargetPos();
            }
           
           
        }

        //public void SetState(SkillSelectState _skillState)
        //{
        //    if(skillModel != null)
        //    {
        //        skillModel.SetSkillState
        //    }

        //}
        public virtual void SkillSelected() {

            if (!skillModel.castPos.Any(t => t == myDyna.currentPos))
                return;
            PopulateTargetPos(); 
            SkillService.Instance.SkillApply += BaseApply;
            SkillService.Instance.SkillApply += ApplyFX1;
            SkillService.Instance.SkillApply += ApplyFX2;
            SkillService.Instance.SkillApply += ApplyFX3;
            SkillService.Instance.SkillApply += ApplyVFx;
            SkillService.Instance.SkillApplyMoveFx += ApplyMoveFx;
            SkillService.Instance.PostSkillApply += PostApplyFX;
            GridService.Instance.HLTargetTiles(skillModel.targetPos);

        }
        public abstract void PopulateTargetPos(); 
     
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
            if (skillModel.castTime >0)
                CombatEventService.Instance.OnEOR += Tick; 
            
            skillModel.lastUsedInRound = CombatService.Instance.currentRound;
            charController.ChangeStat(CauseType.CharSkill, (int)skillName, charID, AttribName.stamina, -skillModel.staminaReq);
           
           
        }  // actual Skill FX // container Skill Data
        public abstract void ApplyFX1();
        public abstract void ApplyFX2();
        public abstract void ApplyFX3();
        public abstract void ApplyVFx();
        public abstract void ApplyMoveFx();

        public abstract void DisplayFX1();
        public abstract void DisplayFX2();
        public abstract void DisplayFX3();
        public abstract void DisplayFX4();

        public virtual void WipeFX1() => SkillService.Instance.SkillHovered -= DisplayFX1;
        public virtual void WipeFX2() => SkillService.Instance.SkillHovered -= DisplayFX2;
        public virtual void WipeFX3() => SkillService.Instance.SkillHovered -= DisplayFX3;
        public virtual void WipeFX4() => SkillService.Instance.SkillHovered -= DisplayFX4;
        public virtual void RemoveFX1() => SkillService.Instance.SkillApply -= ApplyFX1;
        public virtual void RemoveFX2() => SkillService.Instance.SkillApply -= ApplyFX2;
        public virtual void RemoveFX3() => SkillService.Instance.SkillApply -= ApplyFX3;
        public virtual void RemoveVFX() => SkillService.Instance.SkillApply -= ApplyVFx;
        public virtual void RemoveMoveFX()
        {
            SkillService.Instance.SkillApplyMoveFx -= ApplyMoveFx;
            Debug.Log("REMOVE MOVE FX BASE");

        }
            

        public virtual void PreApplyFX()
        {
            // to be used to get the FX from FX Service

        }

        public virtual void PostApplyFX()
        {
            if (skillModel.cd > 0)
            {
                skillModel.SetSkillState(SkillSelectState.UnClickable_InCd);
            }
            // Debug.Log("Post apply EXCECUTED");
            SkillServiceView.Instance.UpdateSkillState(skillModel);
            SkillService.Instance.On_PostSkill();          
            // set in cool down state
            // if (skillModel.cd == -1) return;
            //  SkillServiceView.Instance.SetSkillState(ref skillModel);            

        }

        public virtual void Tick()
        {
            int roundCounter = CombatService.Instance.currentRound - skillModel.lastUsedInRound;
            //  Debug.Log("INSIDE TICK " + roundCounter + "CAST time " + skillModel.castTime);

            if (roundCounter >= skillModel.castTime)
                SkillEnd();
        }

        public virtual void SkillEnd()
        {
            CombatEventService.Instance.OnEOR -= Tick;
            RemoveFX1(); 
            RemoveFX2();
            RemoveFX3();
            RemoveVFX();            
        }

        #endregion

        #region Get and Set State
     
        #endregion

        # region AI Controls

        public abstract void PopulateAITarget();  // last step in target selection 

        #endregion 

        #region SKILL CHECKS

        public virtual void CharSelectSkillChk()  // ALL SKills SOT 
        {
          
       

        }
     

        public bool IsTargetMyAlly() 
        {

            CharMode targetCharMode = SkillService.Instance.currentTargetDyna.charMode;
            return (charController.charModel.charMode == targetCharMode); 
        }
        public  bool IsTargetMyEnemy()
        {
            CharMode targetCharMode = SkillService.Instance.currentTargetDyna.charMode;
            return (charController.charModel.charMode != targetCharMode);
        }
        #endregion
    }

}

