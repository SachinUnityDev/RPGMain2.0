using Combat;
using Common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            enemyPackBase.InitEnemyPack();  
            allEnemyPackBases.Add(enemyPackBase);   
        }
    }
    public EnemyPackBase GetEnemyPackBase(EnemyPackName enemyPackName)
    {
        int index  = 
                allEnemyPackBases.FindIndex(t=>t.enemyPackName== enemyPackName);  
        if(index != -1)
        {
            return allEnemyPackBases[index];
        }
        else
        {
            Debug.Log(" Enemy pack base not found"); 
            return null;
        }
    }

}
