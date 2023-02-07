using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


namespace Town
{
    public class RestInteractView : MonoBehaviour
    {
        [SerializeField] Button endDayBtn;


        // on press close the day event in calendar
        string buffStr = "60% chance for Well Rested";

        HouseModel houseModel; 
        public void OnEndDayPressed()
        {

        }
        void Init()
        {


        }

        void ApplyBuff()
        {
            // apply well rested trait to ABBAS 

        }

    }
}