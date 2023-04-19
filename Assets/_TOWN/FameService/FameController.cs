using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Common
{
    public class FameController : MonoBehaviour
    {
        public int index; 

        

        void Start()
        {
                                                           
        }

        public void ChgFame(CauseType causeType, int causeName, float val, string desc="")
        {
            
        }


        public int ApplyFameModBuff(CauseType causeType, int causeName, float val
                                                                , TimeFrame timeFrame, int timeValue) 
        {
            return index; 
        }
        
        // mostly instant value no buff like isssues give back  ... etc 
        public void ApplyFameBuff(CauseType causeType, int causeName, float val
                                                                , TimeFrame timeFrame, int timeValue)
        {



        }

        public bool IsFameBehaviorMatching(CharController charController)
        {
          //  FameService.Instance.fameModel.currGlobalFame



            return false; 
        }
        
    }


}

