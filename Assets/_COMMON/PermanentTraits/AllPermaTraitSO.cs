using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Common
{

    [CreateAssetMenu(fileName = "AllPermTraitSO", menuName = "Common/AllPermatraitSO")]

    public class AllPermaTraitSO : ScriptableObject
    {
        List<PermaTraitSO> allPermaTraits = new List<PermaTraitSO>();

        [Header("Icon ")]
        public Sprite classIcon;
        public Sprite cultIcon;
        public Sprite racialIcon; 
        

        public PermaTraitSO GetPermaTraitSO(PermaTraitName permaTraitName)
        {
            int index = allPermaTraits.FindIndex(t=>t.permaTraitName== permaTraitName); 
            if(index != -1)
            {
                return allPermaTraits[index];
            }
            Debug.Log("Perma trait SO not FOUND" + permaTraitName); 
            return null;
        }
    }
}