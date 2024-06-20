using Common;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Town
{


    public class BuildBarkPtrEvents : MonoBehaviour
    {
        [Header("TBS")]
        public BuildingNames buildName;
        [Header("Global Var")]
        [SerializeField] BuildingState buildState;
        [SerializeField] BarkLineData lineData;
        public void ShowBark(BuildingNames buildingName, BuildingState buildState)
        {
            BuildBarkSO buildBarkSO = BarkService.Instance.allBarkSO.buildBarkSO;
            TimeState timeState = CalendarService.Instance.calendarModel.currtimeState;
            lineData =
                buildBarkSO.GetBarkLineData(buildingName, buildState, timeState);
            if (lineData != null)
            {
                gameObject.SetActive(true);
                transform.GetComponentInChildren<TextMeshProUGUI>().text = lineData.barkline;

                ShowBarkAnim();
            }
            else
            {
                CloseBarkAnim();
            }
        }

        void ShowBarkAnim()
        {
            Sequence barkSeq = DOTween.Sequence();

            barkSeq
                .Append(transform.DOScale(1f, 0.2f))
                .AppendInterval(2.0f)
                .AppendCallback(()=>CloseBarkAnim())
                ;
        }
        void CloseBarkAnim()
        {            
            transform.DOScale(0, 0.1f);
        }
    }
}