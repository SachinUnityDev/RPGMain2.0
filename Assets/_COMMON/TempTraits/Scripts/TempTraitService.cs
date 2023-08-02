﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Common
{
  
    public class TempTraitService : MonoSingletonGeneric<TempTraitService>
    {
        //  FUNCTIONALITY 
        //  will add all temp traits to itself
        // independent mono behaviours will inform when a trait has started to this service.  

        public event Action<TempTraitData> OnTempTraitStart;
        public event Action<TempTraitData> OnTempTraitEnd;
        public event Action<CharNames, TempTraitName> OnTempTraitHovered;
       
        public List<TempTraitController> allTempTraitControllers = new List<TempTraitController>(); 



        public TempTraitsFactory temptraitsFactory;

        public AllTempTraitSO allTempTraitSO; 
        // Start is called before the first frame update
        /// <summary>
        /// get all temp trait controllers => temp traits controller to act as buff controller for temp traits
        /// get all models here
        /// 
        /// </summary>


        void Start()
        {
           //TownEventService.Instance.OnQuestBegin += temptraitsFactory.InitTempTraits;       // working 
        }




        public void ApplyPermTraits(GameObject go)
        {
            CharController charController = go?.GetComponent<CharController>();
            PermaTraitBase[] tempTraits = go.GetComponents<PermaTraitBase>();
            foreach (PermaTraitBase p in tempTraits)
            {
                p.ApplyTrait(charController);
            }
        }

        public bool IsAnyOneSick()
        {
            foreach (CharController c in CharService.Instance.allyInPlayControllers)
            {
                TempTraitController tempTraitController = c.tempTraitController;
                foreach (TempTraitBuffData model in tempTraitController.alltempTraitApplied)
                {
                    TempTraitSO tempSO = TempTraitService.Instance.allTempTraitSO.GetTempTraitSO(model.tempTraitName);
                    if (tempSO.tempTraitType == TempTraitType.Sickness)
                    {
                        return true; 
                    }
                }
            }
            return false; 
        }



    }


}

