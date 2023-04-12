using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class PermaTraitController : MonoBehaviour
    {
        CharController charController;
        public List<PermaTraitBase> allPermaTraits = new List<PermaTraitBase>();
        void Start()
        {
            charController = GetComponent<CharController>();
        }

        public void ApplyPermaTraits()
        {
            foreach (PermaTraitName permaName in charController.charModel.permanentTraitList)
            {
                PermaTraitBase traitBase = 
                PermaTraitsService.Instance.permaTraitsFactory.GetNewPermaTraitBase(permaName);
                allPermaTraits.Add(traitBase);
                traitBase.ApplyTrait(charController);
            }
        }
        public void RemovePermaTraits()
        {
            allPermaTraits.ForEach(t=>t.EndTrait());    
        }

    }
}