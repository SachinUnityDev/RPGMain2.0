using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    [CreateAssetMenu(fileName = "CalendarSO", menuName = "Calendar Service/CalendarSO")]
    public class CalendarSO : ScriptableObject
    {
        [Header("End day btn")]
        public Sprite endDayBtnN;
        public Sprite endDayBtnNLit;
        public Sprite endNightBtnN; 
        public Sprite endNightBtnLit;

        [Header("day/ night hour glass")]
        public Sprite hourGlassDay; 
        public Sprite hourGlassNight;
        
        [Header(" Rest Panel day n night")]
        public Sprite restPanelDay;
        public Sprite restPanelNight; 


    }
}