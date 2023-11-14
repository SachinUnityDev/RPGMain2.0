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

        public override StrikeTargetNos strikeNos => StrikeTargetNos.Single;

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
                DynamicPosData dyna = GridService.Instance.GetDynaAtCellPos(cellPosData.charMode,i); 
                if(dyna == null)
                {
                    skillModel.targetPos.Add(cellPosData);
                }                
            }
        }

        public override void ApplyFX1()
        {
            if(charController.charModel.charMode == CharMode.Enemy)
            {
                int pos = Random.Range(0, skillModel.targetPos.Count);
                GridService.Instance.gridController.Move2Pos(myDyna, skillModel.targetPos[pos].pos);
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
                    if(cellPos.charMode == cellPosData.charMode)
                    {
                        if(cellPos.pos== cellPosData.pos)
                        {
                            GridService.Instance.gridController.Move2Pos(myDyna, cellPosData.pos);
                        }
                    }
                }     
            }
        }

        public override void ApplyFX2()
        {
        }

        public override void ApplyFX3()
        {
        }

      


        public override void DisplayFX1()
        {
            str1 = $"<style=Move>Move</style> to adj tile // Chance to keep your turn depending on Haste";
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
            SkillService.Instance.currentTargetDyna = myDyna;

        }

        public override void ApplyMoveFx()
        {
           
        }
    }

}

