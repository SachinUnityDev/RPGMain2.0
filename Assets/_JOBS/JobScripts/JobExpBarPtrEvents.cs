using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
namespace Town
{


    public class JobExpBarPtrEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        JobModel jobModel;

        private void Start()
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
        public void InitJobExp(JobModel jobModel)
        {
            this.jobModel = jobModel;
            FillBar();
        }

   

        void FillBar()
        {
            MinMaxExp minMaxExp = 
                JobService.Instance.woodGameSO.GetMinMaxExp(jobModel.currentGameSeq, jobModel.currJobRank);

            float expVal = (float)(jobModel.currGameJobExp - minMaxExp.minExp)/ minMaxExp.maxExp; 
            transform.GetChild(0).GetComponent<Image>().fillAmount= expVal;

            transform.GetComponentInChildren<TextMeshProUGUI>(true).text = jobModel.currGameJobExp.ToString() + "/"
                                                                   + minMaxExp.maxExp.ToString();
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}