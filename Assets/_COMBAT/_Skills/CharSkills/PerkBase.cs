using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
   [System.Serializable]
    public abstract class PerkBase
    {
        #region Declarations
        protected GameObject charGO;
        public GameObject targetGO;

        public virtual int charID { get; set;  }
        protected CharController targetController;
        protected CharController charController;
        protected SkillController1 skillController;  // ?? to be checked when it init
        protected DynamicPosData currDyna;

        protected SkillData skillData;
        protected string str0="", str1="", str2 = "", str3 = "";
        protected string perkDesc = ""; 
        public abstract PerkNames perkName { get; }
        public abstract PerkType perkType { get; }
        public abstract PerkSelectState state { get; set; }  // perk select ...
        public abstract List<PerkNames> preReqList { get; }
        public abstract string desc { get; }
     
        public virtual SkillModel skillModel { get; set; }
        public abstract CharNames charName { get;  }
        public abstract SkillNames skillName { get; }
        public abstract SkillLvl skillLvl { get; }
        public abstract float chance { get; set; }
        protected bool incrDone = false;

        //  public List<DynamicPosData> targetDynas  =  new List<DynamicPosData>();
        #endregion

        #region APPLY and HOVER

        public virtual void PerkInit(SkillController1 skillController) 
        {
            this.skillController = skillController;
            skillModel = skillController?.charSkillModel.allSkillModels.Find(t => t.skillName == skillName);

            charController = this.skillController.gameObject.GetComponent<CharController>();
            charID = charController.charModel.charID;
            incrDone = false; 
            if (skillModel == null)
            {
               Debug.LogError(" Could not find the skillModel" + skillName +" CharName" + charController.charModel.charName);

            }
            charGO = this.skillController.gameObject;
            if(GameService.Instance.currGameModel.gameScene == GameScene.COMBAT)
            {
                currDyna = GridService.Instance.GetDyna4GO(charGO);
                AddTargetPos();
            }
        }
        public virtual void PerkSelected()
        {

            if (!skillModel.castPos.Any(t => t == currDyna.currentPos))
                return;
            AddTargetPos();
            SkillService.Instance.OnSkillApply += BaseApply;
            SkillService.Instance.OnSkillApply += ApplyFX1;
            SkillService.Instance.OnSkillApply += ApplyFX2;
            SkillService.Instance.OnSkillApply += ApplyFX3;
            SkillService.Instance.OnSkillApply += ApplyVFx;
            SkillService.Instance.OnSkillApplyMoveFx += ApplyMoveFX; 

            SkillService.Instance.PostSkillApply += PostApplyFX;
            GridService.Instance.HLTargetTiles(skillModel.targetPos);
        }
        public virtual void AddTargetPos()
        {
           

        }

        public virtual void PerkHovered()
        {
            SkillService.Instance.perkDescOnHover.Clear();
            DisplayFX1();
            DisplayFX2(); 
            DisplayFX3();   
            DisplayFX4();
            SkillService.Instance.perkDescOnHover.Add(str0);
            SkillService.Instance.perkDescOnHover.Add(str1);
            SkillService.Instance.perkDescOnHover.Add(str2);
            SkillService.Instance.perkDescOnHover.Add(str3);
        }
        public virtual void SkillHovered()
        {
            SkillService.Instance.skillModelHovered = skillModel;
            SkillService.Instance.OnSkillHovered += DisplayFX1;
            SkillService.Instance.OnSkillHovered += DisplayFX2;
            SkillService.Instance.OnSkillHovered += DisplayFX3;
            SkillService.Instance.OnSkillHovered += DisplayFX4;
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
            int roundCounter = CombatEventService.Instance.currentRound - skillModel.lastUsedInRound;
            if (skillModel.castTime < 0) return;
            if (roundCounter >= skillModel.castTime)
                SkillEnd();
        }
        public virtual void SkillEnd()
        {
            CombatEventService.Instance.OnSOT -= Tick;
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
        public abstract void InvPerkDesc();
        
        public virtual void PreApplyFX() { }
        public virtual void PostApplyFX() { }
        public virtual void WipeFX1() => SkillService.Instance.OnSkillHovered -= DisplayFX1;
        public virtual void WipeFX2() => SkillService.Instance.OnSkillHovered -= DisplayFX2;
        public virtual void WipeFX3() => SkillService.Instance.OnSkillHovered -= DisplayFX3;
        public virtual void WipeFX4() => SkillService.Instance.OnSkillHovered -= DisplayFX4;

        public virtual void RemoveFX1() => SkillService.Instance.OnSkillApply -= ApplyFX1;
        public virtual void RemoveFX2() => SkillService.Instance.OnSkillApply -= ApplyFX2;
        public virtual void RemoveFX3() => SkillService.Instance.OnSkillApply -= ApplyFX3;
        public virtual void RemoveVFx() => SkillService.Instance.OnSkillApply -= ApplyVFx;
        public virtual void RemoveMoveFX() => SkillService.Instance.OnSkillApplyMoveFx -= ApplyMoveFX; 

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


        #region COMMONLY USED METHODS

        //protected void RegainAP()
        //{
        //    CombatController combatController =
        //            charController.GetComponent<CombatController>();
        //    if(combatController!= null ) 
        //    combatController.IncrementAP(); 
        //}
        protected void TargetAnyEnemy()
        {
            if (skillModel == null) return;
            skillModel.targetPos.Clear();
            CombatService.Instance.mainTargetDynas.Clear();
            for (int i = 1; i < 8; i++)
            {
                CellPosData cell = new CellPosData(CharMode.Enemy, i);
                DynamicPosData dyna = GridService.Instance.gridView.GetDynaFromPos(cell.pos, cell.charMode);

                if (dyna != null)
                {
                    skillModel.targetPos.Add(cell);
                    CombatService.Instance.mainTargetDynas.Add(dyna);
                }
            }
        }


        #endregion

        #region DODGE CHK

        public bool IsDodged()
        {
            if (targetController.damageController.strikeType == StrikeType.Dodged)
                return true; 
            return false; 
        }

        #endregion


    }
}

