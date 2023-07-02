using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{


    [CreateAssetMenu(fileName = "AllHelpSO", menuName = "GuideService/AllHelpSO")]
    public class AllHelpSO : ScriptableObject
    {
        public List<HelpSO> allHelpSO = new List<HelpSO>();


        public HelpSO GetHelpSO(HelpName helpName)
        {
            int  index = allHelpSO.FindIndex(t => t.helpName == helpName); 
            if(index != -1)
            {
                return allHelpSO[index];
            }
            Debug.Log("Help SO Not found" + helpName); 
            return null;
        }
    }
}