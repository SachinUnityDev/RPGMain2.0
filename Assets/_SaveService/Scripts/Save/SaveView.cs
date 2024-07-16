using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Common;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Common
{
    public class SaveView : MonoBehaviour, IPanel
    {       
        public void Init()
        {
           
        }
     
        public void InitOnLoad()
        {
            // get curr game Model
            // populate the slots with the save data
            // no auto save and quick save only the slot will be there

            // on click each slot save the game and Continue with the current game

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


