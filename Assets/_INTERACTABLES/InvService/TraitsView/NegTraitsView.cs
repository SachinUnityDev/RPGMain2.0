using Interactables;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Common; 
namespace Interactables
{


    public class NegTraitsView : MonoBehaviour
    {
        public void InitNegTrait(List<PermaTraitModel> allPermaModels)
        {
            // find negative init
            List<PermaTraitModel> allNegTraits = allPermaModels.Where(t=>t.traitBehaviour == TraitBehaviour.Negative).ToList();
           
                for (int i = 0; i < transform.childCount; i++)
                {
                        if (i < allNegTraits.Count ) 
                          transform.GetChild(i).GetComponent<TraitTxtPtrEvents>().InitTxt(allNegTraits[i]); 
                        else
                            transform.GetChild(i).GetComponent<TraitTxtPtrEvents>().FillBlank();
                }
        }
        public void InitNegTrait(List<TempTraitModel> allTempTrait)
        {
            List<TempTraitModel> allNegTraits = allTempTrait.Where(t => t.temptraitBehavior == TraitBehaviour.Negative).ToList();
          
                for (int i = 0; i < transform.childCount; i++)
                {
                    if (i < allNegTraits.Count)
                        transform.GetChild(i).GetComponent<TraitTxtPtrEvents>().InitTxt(allNegTraits[i]);
                    else
                        transform.GetChild(i).GetComponent<TraitTxtPtrEvents>().FillBlank();
                }
        }
    }
}