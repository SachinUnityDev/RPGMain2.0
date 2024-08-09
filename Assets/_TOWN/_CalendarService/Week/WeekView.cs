using Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Town
{
    public class WeekView : MonoBehaviour, iHelp
    {
        [SerializeField] HelpName helpName; 

        [Header("TBR")]
        [SerializeField] TextMeshProUGUI weekName;
        [SerializeField] TextMeshProUGUI descTxt;
        [SerializeField] Transform specsContainer;
        [SerializeField] WeekEventBtnView weekEventBtnView; 

        [Header("Global Var")]
        CalendarUIController calendarUIController; 
        [SerializeField] WeekModel weekModel;
        WeekSO weekSO;

        public HelpName GetHelpName()
        {
            return helpName; 
        }

        public void InitWeek(CalendarUIController calendarUIController, WeekModel weekModel)
        {
            this.calendarUIController = calendarUIController;
            this.weekModel = weekModel.DeepClone();
            weekSO = CalendarService.Instance.allWeekSO.GetWeekSO(weekModel.weekName);
            weekEventBtnView.Init(weekModel,weekSO); 
            FillWeekPanel();
        }

        void FillWeekPanel()
        {
            weekName.text = weekModel.weekNameStr;
            descTxt.text = weekModel.weekDesc;
            // find week base
     
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