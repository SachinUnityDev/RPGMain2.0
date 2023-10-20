using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Common
{
  
    public class RemoteSkillData
    {
        public CellPosData cellPosData;
        public SkillNames skillName; 
        public SkillBase skillBase;
        public SkillModel skillModel; // cast time , timeframe etc val for controlling buff
        public int currentTime;

        public RemoteSkillData(CellPosData cellPosData, SkillNames skillName, SkillBase skillBase
                                                            , SkillModel skillModel, int currentTime)
        {
            this.cellPosData = cellPosData;
            this.skillName = skillName;
            this.skillBase = skillBase;
            this.skillModel = skillModel;
            this.currentTime = currentTime;
        }
    }
    // will act as a view and the buff controller for the skills
    public class RemoteSkillView : MonoBehaviour
    {
        public void ApplyRemoteSkillBuff(SkillController1 skillController,SkillNames skillName
                                        , CellPosData cellPosData)
        {
            // based on cell pos data apply skill
            // add to allskillbases and skillmodel
            SkillBase skillbase = skillController.GetSkillBase(skillName);
            // buff time cast time 
            int currentTime = -1; 
            SkillModel skillModel = skillbase.skillModel; 
            if(GameService.Instance.gameModel.gameState == GameState.InCombat)
            {
                currentTime = CombatService.Instance.currentRound; 
            }
                 

        }


        public void RemoveRemoteSkillBuff()
        {

        }
        
        // EOC EOR 

    }
}