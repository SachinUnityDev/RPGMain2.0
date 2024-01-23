using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Common
{

    [CreateAssetMenu(fileName = "AllPermTraitSO", menuName = "Common/AllPermatraitSO")]

    public class AllPermaTraitSO : ScriptableObject
    {
       public List<PermaTraitSO> allPermaTraits = new List<PermaTraitSO>();

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

        public List<PermaTraitName> GetAllPermaTraitNames(ClassType classType, CultureType cultType)
        {
            List<PermaTraitName> allPermaTraitNames = new List<PermaTraitName>();   
            foreach (PermaTraitSO traitSO in allPermaTraits)
            {
                if (traitSO.cultureType == CultureType.None)
                    if(traitSO.classType == classType)                    
                        allPermaTraitNames.Add(traitSO.permaTraitName);

                if (traitSO.classType == ClassType.None)
                    if (traitSO.cultureType == cultType)
                        allPermaTraitNames.Add(traitSO.permaTraitName);
            }
            return allPermaTraitNames;
        }

    }
}