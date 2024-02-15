using Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Interactables
{
    public class TraitTxtPtrEvents : MonoBehaviour
    {
        [SerializeField] TempTraitModel tempTraitModel;
        [SerializeField] PermaTraitModel permaTraitModel;

        [SerializeField] TextMeshProUGUI text; 

        public void InitTxt(TempTraitModel tempTraitModel)
        {
            text = transform.GetComponent<TextMeshProUGUI>();
            text.text = tempTraitModel.tempTraitName.ToString();          
        }

        public void InitTxt(PermaTraitModel permaTraitModel)
        {
            text = transform.GetComponent<TextMeshProUGUI>();
             text.text = permaTraitModel.permaTraitName.ToString();        
        }

        public void FillBlank()
        {
            text = transform.GetComponent<TextMeshProUGUI>();
            text.text = ""; 
        }

        private void OnEnable()
        {
            text = transform.GetComponent<TextMeshProUGUI>();
        }
    }
}