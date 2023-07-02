using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{



    [CreateAssetMenu(fileName = "HelpSO", menuName = "GuideService/HelpSO")]
    public class HelpSO : ScriptableObject
    {
        public HelpName helpName;
        [TextArea(2,5)]
        public string headingTxt;
        [Space(5)]
        [TextArea(5,10)]
        public List<string> helpStrs = new List<string>();
    }
}