using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Common;
using System.Linq; 

namespace Combat
{
    public class TurnController : MonoBehaviour
    {
        //  Get ally and enemy in play
        //  
        

        void Start()
        {
            
         
           
        }

  


     

       

        public CharController GetCharWithMax(StatsName _StatName, List<CharController> charList)
        {
            float maxValue= 0;
            CharController maxValueChar = charList[0]; 
            foreach (CharController charCtrl in charList)
            {
                StatData statdata = charCtrl.GetStat(_StatName);
                if (statdata.currValue > maxValue)
                {
                    maxValue = statdata.currValue;
                    maxValueChar = charCtrl; 
                }                    
            }

            return maxValueChar; 
        }


    }



}
