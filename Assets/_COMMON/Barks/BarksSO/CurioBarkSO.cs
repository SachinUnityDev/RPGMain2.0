using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quest;

namespace Common
{
    [System.Serializable]
    public class BarkCurioData
    {
        public CurioNames curioName;
        [TextArea(2, 10)]
        public string barkline;
    }

    [CreateAssetMenu(fileName = "Bark", menuName = "Common/BarkService/CurioBark")]
    public class CurioBarkSO : ScriptableObject
    {
        public CharNames CharName;
        public List<BarkCurioData> curioData = new List<BarkCurioData> ();
    }
}