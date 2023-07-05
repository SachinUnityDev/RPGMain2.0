using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quest;
using System; 

namespace Common
{
    [Serializable]
    public class CurioBarkData
    {
        public CurioNames curioName;
        [TextArea(2, 10)]
        public string barkline;
        [Header("Curio UI")]
        public AudioClip UIaudioClip;
        [Header("Curio Bar VO")]
        public AudioClip VOaudioClip;
    }

    [CreateAssetMenu(fileName = "Bark", menuName = "Common/BarkService/CurioBark")]
    public class CurioBarkSO : ScriptableObject
    {
        public CharNames CharName;
        public List<CurioBarkData> curioData = new List<CurioBarkData> ();
    }
}