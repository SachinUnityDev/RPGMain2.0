using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common; 

namespace Combat
{   
    public class CombatController : MonoBehaviour
    {
        // instantiate char, control the movement 
        // initiate FX //
        public DynamicPosData selectedLoc;
        public DynamicPosData TargetLoc;

        //public CombatCharModel combatCharModel; 

        void Start()
        {

        }

        public bool IfSingleInRow(GameObject _charGO)
        {



            return false;
        }

        public bool IsLastManInHeroes(CharController _charSO)
        {


            return false;
        }
        public bool IfRaceInTeam(CultureType _cultType)
        {


            return false;

        }


    }


}
