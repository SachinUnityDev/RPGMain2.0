using Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Town
{
    public class WeekView : MonoBehaviour
    {
        [Header("TBR")]
        [SerializeField] TextMeshProUGUI weekName;
        [SerializeField] TextMeshProUGUI descTxt;
        [SerializeField] Transform specsContainer;

        [Header("Global Var")]
        CalendarUIController calendarUIController; 
        [SerializeField] WeekModel weekModel;

        public void InitWeeek(CalendarUIController calendarUIController, WeekModel weekModel)
        {
            this.calendarUIController = calendarUIController;
            this.weekModel = weekModel;
            FillWeekPanel();
        }

        void FillWeekPanel()
        {
            weekName.text = weekModel.weekNameStr;
            descTxt.text = weekModel.weekDesc;
            int i = 0; 
            foreach (Transform t in specsContainer) 
            {
                if (i < weekModel.WeekSpecs.Count)
                    t.GetComponent<TextMeshProUGUI>().text = weekModel.WeekSpecs[i];
                else
                    t.GetComponent<TextMeshProUGUI>().text = "";
                i++; 
            }
        }
    }
}