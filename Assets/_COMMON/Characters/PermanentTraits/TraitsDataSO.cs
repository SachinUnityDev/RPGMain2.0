using UnityEngine;
using System.Collections.Generic;


namespace Common
{
    [CreateAssetMenu(fileName = "PermTraitsData", menuName = "Character Service/PermTraitsData")]
    public class TraitsDataSO : ScriptableObject
    {
        public CultureType cultType;
        public ClassType classType;
        public List<PermTraitsINChar> PermTraitsINCharList;

    }
    [System.Serializable]
    public class PermTraitsINChar
    {
        public PermTraitType permaTraitType;
        public PermanentTraitName permanentTraitName;

    }
}

//3 positive traits for each race
//1 negative trait for each reac

//1 positive trait for each specific class


