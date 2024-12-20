﻿using System.Collections;
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
        public CharController targetController;// public for remote skills
        protected CharController charController;
        protected SkillController1 skillController;
        public DynamicPosData myDyna; //public for remote skills

        protected SkillData skillData = new SkillData();
        protected string str0, str1, str2, str3;

        public SkillModel skillModel; 
        public abstract CharNames charName { get; set; }
        public abstract SkillNames skillName { get; }      
        public abstract SkillLvl skillLvl { get;  }
        public abstract string desc { get;  }
        public abstract float chance { get; set; }

        // for enemy target selection
        public DynamicPosData tempDyna = null;
        protected DynamicPosData randomDyna = null;


        #endregion

        #region APPLY and HOVER
        public virtual void  SkillInit(SkillController1 skillController) 
        {
            SkillDataSO skillDataSO = SkillService.Instance.GetSkillSO(charName);
           // Debug.Log("SKILLNAME........" + skillName);

            this.skillController = skillController; 
            charController = skillController.GetComponent<CharController>();
            charID = charController.charModel.charID; 
            skillData = skillDataSO.allSkills.Find(t => t.skillName == skillName);

            skillModel = skillController.charSkillModel.allSkillModels?.Find(t => t.skillName == skillName && t.charID == charID);

            if(skillModel== null)
            {
                skillModel = new SkillModel(skillData);        
                skillModel.charID = charID;
                if (this.skillController.charSkillModel.allSkillModels.Any(t => t.skillName == skillName))
                {
                    int index =
                    this.skillController.charSkillModel.allSkillModels.FindIndex(t => t.skillName == skillName);
                    this.skillController.charSkillModel.allSkillModels.RemoveAt(index);
                }
                this.skillController.charSkillModel.allSkillModels.Add(skillModel);   // lastest skillModel for ref      
            }
            charGO = this.skillController.gameObject;
            if (GameService.Instance.currGameModel.gameScene == GameScene.COMBAT)
            {
                myDyna = GridService.Instance.GetDyna4GO(charGO);      
                if(myDyna == null)
                    Debug.LogError("My dyna missing" +skillModel.charName);
                PopulateTargetPos();
            }            
        }
        //void OnCharClicked(CharController charController)
        //{
        //    if (charController == null) return;
        //    PopulateTargetPos();
        //}
        public virtual void SkillSelected() 
        {


             if (!skillModel.castPos.Any(t => t == myDyna.currentPos))
                return;
            PopulateTargetPos(); 
            
            SkillService.Instance.OnSkillApply += BaseApply;
            SkillService.Instance.OnSkillApply += ApplyFX1;
            SkillService.Instance.OnSkillApply += ApplyFX2;
            SkillService.Instance.OnSkillApply += ApplyFX3;
            SkillService.Instance.OnSkillApply += ApplyVFx;
            SkillService.Instance.OnSkillApplyMoveFx += ApplyMoveFx;
            SkillService.Instance.PostSkillApply += PostApplyFX;
            GridService.Instance.HLTargetTiles(skillModel.targetPos);

        }
        public abstract void PopulateTargetPos(); 
     
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
            if (skillModel.maxUsagePerCombat > 0)
            {
                skillModel.noOfTimesUsed++;
            }
            if (skillModel.attackType != AttackType.Remote)
            {
                if (skillModel.castTime > 0)
                    CombatEventService.Instance.OnEOR1 += Tick;

                if(skillModel.cd >= 0)
                {
                    skillModel.lastUsedInRound = CombatEventService.Instance.currentRound;
                }
                charController.ChangeStat(CauseType.CharSkill, (int)skillName, charID, StatName.stamina, -skillModel.staminaReq);
            }            
        }  
        public virtual void OnRemoteUse()
        {
            if (skillModel.cd >= 0)
            {
                skillModel.lastUsedInRound = CombatEventService.Instance.currentRound;
            }
            charController.ChangeStat(CauseType.CharSkill, (int)skillName, charID, StatName.stamina, -skillModel.staminaReq);
        }

        public abstract void ApplyFX1();
        public abstract void ApplyFX2();
        public abstract void ApplyFX3();
        public abstract void ApplyVFx();
        public abstract void ApplyMoveFx();
        public abstract void DisplayFX1();
        public abstract void DisplayFX2();
        public abstract void DisplayFX3();
        public abstract void DisplayFX4();

        public virtual void WipeFX1() => SkillService.Instance.OnSkillHovered -= DisplayFX1;
        public virtual void WipeFX2() => SkillService.Instance.OnSkillHovered -= DisplayFX2;
        public virtual void WipeFX3() => SkillService.Instance.OnSkillHovered -= DisplayFX3;
        public virtual void WipeFX4() => SkillService.Instance.OnSkillHovered -= DisplayFX4;
        public virtual void RemoveFX1() => SkillService.Instance.OnSkillApply -= ApplyFX1;
        public virtual void RemoveFX2() => SkillService.Instance.OnSkillApply -= ApplyFX2;
        public virtual void RemoveFX3() => SkillService.Instance.OnSkillApply -= ApplyFX3;
        public virtual void RemoveVFX() => SkillService.Instance.OnSkillApply -= ApplyVFx;
        public virtual void RemoveMoveFX()
        {
            SkillService.Instance.OnSkillApplyMoveFx -= ApplyMoveFx;
            Debug.Log("REMOVE MOVE FX BASE");

        }
            

        public virtual void PreApplyFX()
        {
            // to be used to get the FX from FX Service
            
        }

        public virtual void PostApplyFX()
        {
            PopulateTargetPos(); 
            SkillService.Instance.On_PostSkill(skillModel);          
        }

        public virtual void Tick(int roundNo)
        {
            int roundCounter = roundNo - skillModel.lastUsedInRound;
              Debug.Log("INSIDE TICK " + roundCounter + "CAST time " + skillModel.castTime);

            if (roundCounter >= skillModel.castTime)
                SkillEnd();
        }

        public virtual void SkillEnd()
        {
            CombatEventService.Instance.OnEOR1 -= Tick;
            RemoveFX1(); 
            RemoveFX2();
            RemoveFX3();
            RemoveVFX();            
        }

        #endregion

        #region Get and Set State
     
        #endregion

        # region AI Controls

        public virtual void PopulateAITarget()  // last step in target selection 
        {
            PopulateTargetPos();
            SkillService.Instance.currentTargetDyna = null;
            if (skillModel.strikeNos == StrikeNos.Single)
            {
                foreach (CellPosData cell in skillModel.targetPos)
                {
                    DynamicPosData dyna = GridService.Instance.GetDynaAtCellPos(cell.charMode, cell.pos);
                    CharController targetCtrl = dyna.charGO.GetComponent<CharController>();
                    if (dyna != null)
                    {
                        if (targetCtrl.charStateController.HasCharState(CharStateName.Hexed))
                        {
                            SkillService.Instance.currentTargetDyna = dyna; 
                            
                        }
                    }
                }
            }            
        }
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

        protected void AddTargetInRange(int startCell, int endCell, CharMode charMode)
        {
            for (int i = startCell; i <= endCell; i++)
            {
                CellPosData cellPosData = new CellPosData(charMode, i);
                AddTarget(cellPosData);
            }
        }

        protected void AddTarget(CellPosData cellPosData)
        {

            DynamicPosData dyna = GridService.Instance.gridView.GetDynaFromPos(cellPosData.pos, cellPosData.charMode);
            if (dyna != null)
            {
                skillModel.targetPos.Add(cellPosData);
                CombatService.Instance.mainTargetDynas.Add(dyna);
            }

        }
        #endregion

        //protected void RegainAP()
        //{
        //    CombatController combatController =
        //            charController.GetComponent<CombatController>();
        //    if (combatController != null)
        //    {
        //        combatController.IncrementAP();

        //    }
        //}

        protected void AnyWithCharMode(CharMode charMode)
        {
            if (skillModel == null) return;
            CombatService.Instance.mainTargetDynas.Clear(); skillModel.targetPos.Clear();
            CombatService.Instance.mainTargetDynas.AddRange(GridService.Instance.GetAllByCharMode(charMode));
            foreach (DynamicPosData dyna in CombatService.Instance.mainTargetDynas)
            {
                skillModel.targetPos.Add(new CellPosData(dyna.charMode, dyna.currentPos));
            }
        }
        protected void SelfTarget()
        {
            if (skillModel == null) return;
            skillModel.targetPos.Clear(); CombatService.Instance.mainTargetDynas.Clear();

            int pos = GridService.Instance.GetDyna4GO(charGO).currentPos;
            skillModel.targetPos.Add(new CellPosData(myDyna.charMode, pos));

            CombatService.Instance.mainTargetDynas.Add(myDyna);

        }

        protected void FirstOnSamelane()
        {
            if (skillModel == null) return;

            skillModel.targetPos.Clear(); CombatService.Instance.mainTargetDynas.Clear();

            List<DynamicPosData> sameLaneOccupiedPos = new List<DynamicPosData>(); 
            sameLaneOccupiedPos = GridService.Instance.GetInSameLaneOppParty
                                    (new CellPosData(charController.charModel.charMode, GridService.Instance.GetDyna4GO(charGO).currentPos));
            if (sameLaneOccupiedPos.Count > 0)
            {
                CellPosData Pos = new CellPosData(sameLaneOccupiedPos[0].charMode, sameLaneOccupiedPos[0].currentPos);
                skillModel.targetPos.Add(Pos);
                CombatService.Instance.mainTargetDynas
                    .Add(GridService.Instance.GetDynaAtCellPos(sameLaneOccupiedPos[0].charMode, sameLaneOccupiedPos[0].currentPos));
            }
        }
        protected void FirstRow(CharMode charMode)
        {
            if (skillModel == null) return;
            skillModel.targetPos.Clear();
             CombatService.Instance.mainTargetDynas.Clear();

            List<DynamicPosData> firstRowChar = GridService.Instance.GetFirstRowChar(charMode);
            if (firstRowChar != null)
            {
                foreach (var dynaChar in firstRowChar)
                {
                    skillModel.targetPos.Add(new CellPosData(dynaChar.charMode, dynaChar.currentPos));
                    CombatService.Instance.mainTargetDynas.Add(dynaChar);
                }
            }
        }
        protected void AllinCharModeExceptSelf(CharMode charMode)
        {
            skillModel.targetPos.Clear();

            for (int i = 1; i < 8; i++)
            {
                if (!(myDyna.currentPos == i))
                {
                    CellPosData cellPosData = new CellPosData(charMode, i);
                    DynamicPosData dyna = GridService.Instance.gridView.GetDynaFromPos(cellPosData.pos, cellPosData.charMode);
                    if (dyna != null)
                    {
                        skillModel.targetPos.Add(cellPosData);
                        CombatService.Instance.mainTargetDynas.Add(dyna);
                    }
                }
            }
        }
        protected void Target1234(CharMode charMode)
        {
            skillModel.targetPos.Clear();
            for (int i = 1; i <= 4; i++)
            {
                CellPosData cellPosData = new CellPosData(charMode, i); // Allies
                DynamicPosData dyna = GridService.Instance.gridView
                                       .GetDynaFromPos(cellPosData.pos, cellPosData.charMode);
                if (dyna != null)
                {
                    skillModel.targetPos.Add(cellPosData);
                }
            }
        }
        public bool IsDodged()
        {
            if (targetController.damageController.strikeType == StrikeType.Dodged)
                return true;
            return false;
        }
    }

    public interface IRemoteSkill
    {
        CellPosData cellPosData { get; set; }
        public abstract void Init(CellPosData cellPosData); 

    }


}

