using Quest;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;


namespace Common
{
    public class BuildBarkController : MonoBehaviour
    {
        


        void Start()
        {

        }
        public void ShowCurioBark(BuildingNames buildName)
        {
            // get build model and build state and get timeState 

            TimeState timeState = CalendarService.Instance.currtimeState; 
           // BuildingModel buildModel = BuildingIntService.Instance.

            //CurioBarkData curioBarkData = BarkService.Instance.allBarkSO.GetCurioBarkData(curioName);

            //BarkCharData barkCharData = new BarkCharData(CharNames.Abbas_Skirmisher
            //                                , curioBarkData.barkline, curioBarkData.VOaudioClip);

            //qbarkView.InitCurioBark(barkCharData, curioColEvents, curioBarkData.UIaudioClip);
        }

    }
}