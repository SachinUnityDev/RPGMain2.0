using Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Quest
{
    public class QLandDispPtrEvents : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI buffTxt; 
        public void InitQLandDisplay()
        {
            LandscapeNames landName = LandscapeService.Instance.currLandscape;
            LandscapeSO landSO = LandscapeService.Instance.allLandSO.GetLandSO(landName);

            buffTxt.text = landSO.hazardStr; 
            

        }
        private void Awake()
        {
            gameObject.SetActive(false);
        }

    }
}