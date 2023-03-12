using Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

namespace Town
{
    public class HealView : MonoBehaviour, IPanel
    {
        [Header("to be ref")]

        [SerializeField] Transform healSlotTrans;
        [SerializeField] Transform charOnHeal;
        [SerializeField] Transform sicknessList; 
        [SerializeField] TextMeshProUGUI restTxt;
        [SerializeField] Transform healBtn; // update sprite depending on state 

        private void Awake()
        {
            
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
    }
}