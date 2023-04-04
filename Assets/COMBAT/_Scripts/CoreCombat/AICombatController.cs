using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Common; 

namespace Combat
{
    public class CurrentSkillWtData
    {
        public int skillID;
        public int currentWeight;
        public List<DynamicPosData> targetInSight; 

        public CurrentSkillWtData(int _skillID, int _currentWeight, List<DynamicPosData> _targetInSight)
        {
            skillID = _skillID;
            //currentWeight = GetSkillWeight(skillID); 
            targetInSight = _targetInSight; 
        }
    }


    public class AICombatController : MonoBehaviour
    {

        // get all skill of character thru reflection 
        // each target make a list of skills actions 


        // each skills => getTargets in Sight
        // populate skill wt data list below
        //  AI generic rules .... 

        List<CurrentSkillWtData> allSkillWtData = new List<CurrentSkillWtData>(); 
        void ApplyAIGenericRules()
        {
            AI_Rule1();



        }

        void AI_Rule1()
        {

            foreach (var skillwt in allSkillWtData)
            {
                foreach(var target in skillwt.targetInSight)
                {
                    if (target.charGO.GetComponent<CharController>().GetStat(AttribName.health).currValue < 20f)
                    {
                        skillwt.currentWeight +=   20;

                    }
                }
            }


        }

   
    }


}


