using System.Collections;
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

        public event Action<TempTraitBuffData> OnTempTraitStart;
        public event Action<TempTraitBuffData> OnTempTraitEnd;
        public event Action<CharController, TempTraitModel> OnTempTraitHovered;

        public GameObject tempTraitCardGO;
        public GameObject tempTraitCardPrefab; 

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
           CreateTempTraitCardGO();
        }


        public void On_TempTraitStart(TempTraitBuffData tempTraitBuffData)
        {
            OnTempTraitStart?.Invoke(tempTraitBuffData);
        }
        public void On_TempTraitEnd(TempTraitBuffData tempTraitBuffData)
        {
            OnTempTraitEnd?.Invoke(tempTraitBuffData); 
        }

        public  TempTraitBase GetNewTempTraitBase(TempTraitName tempTraitName)
        {
            TempTraitBase tempTraitBase = temptraitsFactory.GetNewTempTraitBase(tempTraitName);            
            return tempTraitBase;
        }
        void CreateTempTraitCardGO()
        {
            GameObject canvasGO = GameObject.FindGameObjectWithTag("Canvas");
            if (tempTraitCardGO == null)
            {
                tempTraitCardGO = Instantiate(tempTraitCardPrefab);
            }
            tempTraitCardGO.transform.SetParent(canvasGO.transform);
            tempTraitCardGO.transform.SetAsLastSibling();
            tempTraitCardGO.transform.localScale = Vector3.one;
            tempTraitCardGO.SetActive(false);
        }
        //public void ApplyPermTraits(GameObject go)
        //{
        //    CharController charController = go?.GetComponent<CharController>();
        //    PermaTraitBase[] tempTraits = go.GetComponents<PermaTraitBase>();
        //    foreach (PermaTraitBase p in tempTraits)
        //    {
        //        p.ApplyTrait(charController);
        //    }
        //}

        public bool IsAnyOneSick()
        {
            foreach (CharController c in CharService.Instance.allyInPlayControllers)
            {
                TempTraitController tempTraitController = c.tempTraitController;
                foreach (TempTraitBuffData model in tempTraitController.alltempTraitBuffData)
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

