using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Common;
using System.Windows.Forms.DataVisualization.Charting;

namespace Town
{
    public class TavernController : MonoBehaviour, IPanel, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {

        public TavernModel tavernModel;

        BuildingSO tavernSO;

        void Start()
        {
            tavernSO = BuildingIntService.Instance.allBuildSO.GetBuildSO(BuildingNames.House);
            tavernModel = new TavernModel(tavernSO);
        }

        public void Init()
        {
               
        }

        public void Load()
        {
           
        }

        public void UnLoad()
        {
           
        }

       Image btnImg;
       

        public void OnPointerClick(PointerEventData eventData)
        {

        }

        // talk .. trade ...each interaction is unique ....


        public void OnPointerEnter(PointerEventData eventData)
        {
            //   namePlank.GetComponent<RectTransform>().DOScale(1, 0.25f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            //  namePlank.GetComponent<RectTransform>().DOScale(0, 0.25f);

        }

    }


}


