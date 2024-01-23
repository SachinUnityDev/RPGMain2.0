using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class PermaTraitController : MonoBehaviour
    {
        CharController charController;
        public List<PermaTraitBase> allPermaTraits = new List<PermaTraitBase>();
        public List<PermaTraitModel> allPermaModels= new List<PermaTraitModel>();

        [SerializeField] int traitID; 
        
        void Start()
        {
            charController = GetComponent<CharController>();
            ApplyPermaTraits();
        }

        public void ApplyPermaTraits()
        {            
            List<PermaTraitName> allPermaTraitNames = new List<PermaTraitName>();

            ClassType classType = charController.charModel.classType;
            CultureType cultType = charController.charModel.cultType;
            allPermaTraitNames =  PermaTraitsService.Instance.allPermaTraitSO.GetAllPermaTraitNames(classType, cultType);

            foreach (PermaTraitName permaTraitName in allPermaTraitNames)
            {
                PermaTraitBase traitBase =
                            PermaTraitsService.Instance.permaTraitsFactory.GetNewPermaTraitBase(permaTraitName);                
                traitID++; 
                PermaTraitSO permaTraitSO= PermaTraitsService.Instance.allPermaTraitSO.GetPermaTraitSO(permaTraitName);
                allPermaTraits.Add(traitBase);
                traitBase.PermaTraitInit(permaTraitSO,charController, traitID); 
                traitBase.ApplyTrait();
            }
        }
        public void RemovePermaTraits()
        {
            allPermaTraits.ForEach(t=>t.EndTrait());    
        }

    }
}