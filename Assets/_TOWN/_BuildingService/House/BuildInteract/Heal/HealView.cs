using Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Town
{
    public class HealView : MonoBehaviour, IPanel
    {
        [Header("to be ref")]

        [SerializeField] Transform sicknessSlotContainer; 
        [SerializeField] Transform charPortrait;
        [SerializeField] Transform nameScroll; 
        [SerializeField] Transform healContainerSlot;
        [SerializeField] Button healBtn;
        [SerializeField] TextMeshProUGUI healDescTxt; 


        private void Awake()
        {
           // healBtn.onClick.AddListener(OnHealBtnPressed);
        }

        void FillSicknessList()
        {
            // get temp trait Controller .. Get Sickness
            // fill in the slot 
            

        }

        void FillHerbSlots()
        {



        }
        void OnHealBtnPressed()
        {
            // consume the herbs
            // remove the sickness
        }



        public void Init()
        {
            Load(); 
        }

        public void Load()
        {
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
        }
    }
}