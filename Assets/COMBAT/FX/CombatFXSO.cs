using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Combat
{
    [CreateAssetMenu(fileName = "SkillDataSO", menuName = "Skill Service/SkillDataSO")]
    public class CombatFXSO : MonoBehaviour
    {
        public List<GameObject> skillSelectState = new List<GameObject>(); 

       //s TileState
        private void Awake()
        {
            
        }

    }


}


