using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Town
{


    public class BuildBarkPtrEvents : MonoBehaviour
    {
        public BuildingNames buildName; 

        public void InitBark(BuildingNames buildModel)
        {
           //BuildingState buildState = BuildingState.None;
           // // get buildSO and from  SO 
           // BuildingSO buildSO = 
           //     BuildingIntService.Instance.allBuildSO.GetBuildSO(buildName); 
           // if(buildState == BuildingState.Locked)
           // {
           //     string str = buildSO.GetUnLockedStr(); 
           //     if(str != null)
           //     {
           //         FillBark(str);
           //     }
           // }else if (buildState == BuildingState.UnAvailable)
           // {
           //     string str = buildSO.GetUnAvailStr();
           //     if (str != null)
           //     {
           //         FillBark(str);
           //     }
           // }
        }

        void FillBark(string str)
        {
            TextMeshProUGUI text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            text.text = str; 
        }
    }
}