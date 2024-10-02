using Common;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class DefaultMove : SkillBase
    {

        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.DefaultMove;
        public override string desc => "this is default move";
        public override SkillLvl skillLvl => SkillLvl.Level0;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        public override void PopulateTargetPos()
        {
            if (skillModel == null) return;
            skillModel.targetPos.Clear(); CombatService.Instance.mainTargetDynas.Clear();

            List<int> allAdjPos = GridService.Instance.gridController.GetAllAdjCell(myDyna.currentPos);
            foreach (int i in allAdjPos)
            {

                CellPosData cellPosData = new CellPosData(charController.charModel.charMode, i);
                DynamicPosData dyna = GridService.Instance.GetDynaAtCellPos(cellPosData.charMode, i);
                if (dyna == null)
                {
                    skillModel.targetPos.Add(cellPosData);
                }
            }
        }

        public override void ApplyFX1()
        {
            if (charController.charModel.charMode == CharMode.Enemy)
            {
                int pos = -1;
                SkillModel skillModel = FindIfSkillIsClickableAndCharIsNotOnCastPos();
                if (skillModel != null)  // has clickable skill and char is not on cast pos
                {
                    List<int> allAdjPos = GridService.Instance.gridController.GetAllAdjCell(myDyna.currentPos);
                    List<int> emptyAdjCell = new List<int>();

                    emptyAdjCell = GetEmptyAdjCell(allAdjPos);
                    List<int> overlapPos = new List<int>();
                    if (emptyAdjCell.Count > 0)
                    {
                        foreach (int pos1 in skillModel.castPos)
                        {
                            if (emptyAdjCell.Contains(pos1))
                            {
                                overlapPos.Add(pos1);
                            }
                        }
                        if (overlapPos.Count > 0)
                        {
                           int index = Random.Range(0, overlapPos.Count);
                            pos = overlapPos[index];    
                        }
                        
                    }
                    else
                    {
                        pos = -1;    // skip move 
                    }

                    Debug.Log("Move2" + pos + " myDyna" + myDyna.charGO.name + myDyna.currentPos);
                    if (pos != -1)
                    {
                        GridService.Instance.gridController.Move2Pos(myDyna, pos);
                        GridService.Instance.gridView.CharOnTurnHL(myDyna);
                    }
                }
            }
            else
            {   // if clicked on target pos... 
                Vector3Int tilePos = GridService.Instance.gridView.currTilePos;
                CellPosData cellPosData = GridService.Instance.gridView.GetPos4TilePos(tilePos);
                if (cellPosData == null)
                {
                    return;
                }
                foreach (CellPosData cellPos in skillModel.targetPos)
                {
                    if (cellPos.charMode == cellPosData.charMode)
                    {
                        if (cellPos.pos == cellPosData.pos)
                        {
                            GridService.Instance.gridController.Move2Pos(myDyna, cellPosData.pos);
                            GridService.Instance.gridView.CharOnTurnHL(myDyna);
                        }
                    }
                }
            }
        }
        


        List<int> GetEmptyAdjCell(List<int> cellList)
        {
            List<int> emptyCells = new List<int>(); 
            foreach (int i in cellList)
            {
                CellPosData cellPosData = new CellPosData(charController.charModel.charMode, i);
                DynamicPosData dyna = GridService.Instance.GetDynaAtCellPos(cellPosData.charMode, i);
                if (dyna == null)
                {
                    emptyCells.Add(i);
                }
            }
            return emptyCells; 
        }


        SkillModel FindIfSkillIsClickableAndCharIsNotOnCastPos() 
        {
            foreach (SkillModel skillModel in skillController.charSkillModel.allSkillModels)                
            {
                if (skillModel.GetSkillState() == SkillSelectState.Unclickable_notOnCastPos)
                {                    
                    if (skillModel.skillInclination == SkillInclination.Physical || skillModel.skillInclination == SkillInclination.Magical)
                    {                        
                        return skillModel;
                    }
                }
            }
            return null; 
        }
    

        public override void ApplyFX2()
        {
        }

        public override void ApplyFX3()
        {
        }

        public override void DisplayFX1()
        {
            str1 = $"<style=Move>Move</style> to adj tile";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }

        public override void DisplayFX2()
        {
            str2 = $"Chance to keep your turn depending on Haste";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }

        public override void DisplayFX3()
        {
        }

        public override void DisplayFX4()
        {
        }

        public override void ApplyVFx()
        {
        }

        public override void PopulateAITarget()
        {
            PopulateTargetPos();
            SkillService.Instance.currentTargetDyna = myDyna;     
        }

        public override void ApplyMoveFx()
        {
           
        }
    }

}

