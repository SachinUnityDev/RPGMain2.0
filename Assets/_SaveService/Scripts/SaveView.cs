using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Common;
using System.Linq;
using TMPro;
using UnityEngine.UI;

namespace Common
{
    public class SaveView : MonoBehaviour, IPanel
    {
        //public SaveSlot currSlotSelected;
        //[SerializeField] Button loadBtn;
        //[SerializeField] Button saveBtn; 

        //void Start()
        //{
        //    loadBtn.onClick.AddListener(OnLoadBtnPressed);
        //    saveBtn.onClick.AddListener(OnSaveBtnPressed);
        //}
        //public void OnLoadBtnPressed()
        //{
        //    // display load slots ..load game from save folders in the slots 

        //}

        //public void OnSaveBtnPressed()
        //{
        //    // for Manual save in the slots

        //}
        public void Init()
        {
           
        }

        public void Load()
        {
            UIControlServiceGeneral.Instance.TogglePanel(this.gameObject, true);
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(this.gameObject, false);
        }
    }
}


