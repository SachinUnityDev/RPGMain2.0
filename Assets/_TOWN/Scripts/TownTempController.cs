using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Town
{
    public class TownTempController : MonoBehaviour
    {
        [SerializeField] Button House;
        [SerializeField] GameObject housePanel;

        void Start()
        {
            House.onClick.AddListener(OnHouseBtnPressed); 
        }
        void OnHouseBtnPressed()
        {
            housePanel.SetActive(true);


        }

    }



}

