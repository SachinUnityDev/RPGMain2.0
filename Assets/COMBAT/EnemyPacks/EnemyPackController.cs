using Combat;
using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPackController : MonoBehaviour
{

    public List<EnemyPackBase> allEnemyPackBases = new List<EnemyPackBase>();
    EnemypackFactory enemypackFactory;
    private void OnEnable()
    {
        enemypackFactory= GetComponent<EnemypackFactory>();
    }
    public void InitEnemyPackController(AllEnemyPackSO allEnemyPackSO)
    {
        enemypackFactory.InitEnemyPack();
        if (allEnemyPackBases.Count > 0) return;
        foreach (EnemyPacksSO enemyPacksSO in allEnemyPackSO.allEnemyPack)
        {
            EnemyPackBase enemyPackBase = enemypackFactory.GetEnemyPack(enemyPacksSO.enemyPack); 
            allEnemyPackBases.Add(enemyPackBase);   
        }
    }
}
