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

        public void LoadController(PermaTraitModel model)
        {   
            PermaTraitBase traitBase =
                        PermaTraitsService.Instance.permaTraitsFactory.GetNewPermaTraitBase(model.permaTraitName);
            
            PermaTraitSO permaTraitSO = PermaTraitsService.Instance.allPermaTraitSO.GetPermaTraitSO(model.permaTraitName);
            allPermaTraits.Add(traitBase);
            charController = GetComponent<CharController>();
            traitBase.PermaTraitInit(permaTraitSO, charController, model.permaTraitID);              

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
                PermaTraitModel permaTraitModel = 
                        traitBase.PermaTraitInit(permaTraitSO,charController, traitID); 
                traitBase.ApplyTrait();
                PermaTraitsService.Instance.On_PermaTraitAdded(permaTraitModel);
            }
        }
        public void RemovePermaTraits()
        {
            allPermaTraits.ForEach(t=>t.EndTrait());    
        }

        public PermaTraitModel GetPermaTraitModel(PermaTraitName permaTraitName)
        {
            int index = allPermaModels.FindIndex(t => t.permaTraitName == permaTraitName); 
            if(index!= -1)
            {
                return allPermaModels[index];   
            }
            Debug.Log(" PERMA trait Model not found" + permaTraitName); 
            return null; 
        }
        public PermaTraitBase GetPermaTraitBase(PermaTraitName permaTraitName)
        {
            int index = allPermaTraits.FindIndex(t => t.permaTraitName == permaTraitName);
            if (index != -1)
            {
                return allPermaTraits[index];
            }
            Debug.Log(" PERMA trait BASE not found" + permaTraitName);
            return null;
        }

    }
}