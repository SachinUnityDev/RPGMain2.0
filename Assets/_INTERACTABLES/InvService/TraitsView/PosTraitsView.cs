using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class PosTraitsView : MonoBehaviour
{
    public void InitPosTrait(List<PermaTraitModel> allPosPermaTraits)
    {
        List<PermaTraitModel> allPosTraits = allPosPermaTraits.Where(t => t.traitBehaviour == TraitBehaviour.Positive).ToList();
        
            for (int i = 0; i < transform.childCount; i++)
            {
                if (i < allPosTraits.Count)
                    transform.GetChild(i).GetComponent<TraitTxtPtrEvents>().InitTxt(allPosTraits[i]);
                else
                    transform.GetChild(i).GetComponent<TraitTxtPtrEvents>().FillBlank();

            }
    }
    public void InitPosTrait(List<TempTraitModel> allPosTempTrait)
    {
        List<TempTraitModel> allPosTraits = allPosTempTrait.Where(t => t.temptraitBehavior == TraitBehaviour.Positive).ToList();
        
            for (int i = 0; i < transform.childCount; i++)
            {
                if (i < allPosTraits.Count)
                    transform.GetChild(i).GetComponent<TraitTxtPtrEvents>().InitTxt(allPosTraits[i]);
                else
                    transform.GetChild(i).GetComponent<TraitTxtPtrEvents>().FillBlank();

            }
    }
}
