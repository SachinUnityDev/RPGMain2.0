using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Town;
using Interactables;
using JetBrains.Annotations;
using Combat;

namespace Common
{
    public class GameController : MonoBehaviour
    {

        public bool isMortalBlood = false;
        public bool isQuickStart = false;
     
        [Header("Game Diff")]
        public AllGameDiffSO allGameDiffSO; 
        public List<GameDiffModel> allGameDiffModel = new List<GameDiffModel>();

        public GameDiffModel currGameDiffModel; 
        public void InitGameController(GameDifficulty gameDifficulty)
        {
            if (allGameDiffModel.Count > 0) return; 
            foreach (GameDiffSO gameDiffSO in allGameDiffSO.allGameDiffSO) 
            {
                GameDiffModel gameDiffModel= new GameDiffModel(gameDiffSO);   
                allGameDiffModel.Add(gameDiffModel); 
                
                if(gameDiffModel.gameDiff == gameDifficulty) // set curr model
                    currGameDiffModel= gameDiffModel;
            }
        }

        public int GetMaxRoundLimit()
        {
            EnemyPackName enemyPack = CombatService.Instance.currEnemyPack; 
            EnemyPacksSO enemyPackSO = CombatService.Instance.allEnemyPackSO.GetEnemyPackSO(enemyPack);

            if (enemyPackSO.isBossPack)
                return currGameDiffModel.maxRoundLimitBoss;
            else
                return currGameDiffModel.maxRoundLimit; 
        }
        public void SetMortalBlood(bool isMortalBlood)
        {
            this.isMortalBlood = isMortalBlood;
        }
        public void SetQuickStart(bool isQuickStart)
        {
            this.isQuickStart= isQuickStart;    
        }
    }



}
