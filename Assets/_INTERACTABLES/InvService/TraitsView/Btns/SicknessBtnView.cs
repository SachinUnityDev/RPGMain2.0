using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace Interactables
{


    public class SicknessBtnView : MonoBehaviour
    {
        // master script for three btns .. physical sickness clicked by default 

        InvTraitsView invTraitsView;

        public void Init(InvTraitsView invTraitsView)
        {
            this.invTraitsView = invTraitsView;
            if (invTraitsView == null) return; 
            foreach (Transform child in transform)
            {
                child.GetComponent<SicknessTypeBtnView>().Init(invTraitsView, this); 
            }           
            HideBtns();
        }

        public void ShowBtns()
        {
            gameObject.SetActive(true);
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
            transform.GetChild(0).GetComponent<SicknessTypeBtnView>().OnClick();
        }
        public void HideBtns()
        { 
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
                child.GetComponent<SicknessTypeBtnView>().Init(invTraitsView, this);
            }
            gameObject.SetActive(false);
        }

        public void UnClickall()
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<SicknessTypeBtnView>().OnUnClick();
            }            
        }
    }
}