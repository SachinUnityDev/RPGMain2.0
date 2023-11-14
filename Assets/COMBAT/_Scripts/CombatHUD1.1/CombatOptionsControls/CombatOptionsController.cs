using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI; 

namespace Combat
{
    public class CombatOptionsController : MonoBehaviour
    {
        [SerializeField] GameObject fleeBtn;
        [SerializeField] Transform combatOptionsBtn;

        [SerializeField] bool isOptOpen;
        [SerializeField] Button OptionsBtn;

        private void Awake()
        {
            OptionsBtn = GetComponent<Button>(); 
        }
        void Start()
        {
            isOptOpen = false;
            fleeBtn.GetComponent<RectTransform>().DOScaleX(0, 0);
            combatOptionsBtn.GetComponent<RectTransform>().DOScaleY(0, 0);
            OptionsBtn.onClick.AddListener(OnButtonClick);

        }

        public void OnButtonClick()
        {
            if (isOptOpen)
            {
                fleeBtn.GetComponent<RectTransform>().DOScaleX(0, 0.4f);
                combatOptionsBtn.GetComponent<RectTransform>().DOScaleY(0, 0.4f);
                isOptOpen = false;
            }
            else
            {
                fleeBtn.GetComponent<RectTransform>().DOScaleX(1, 0.4f);
                combatOptionsBtn.GetComponent<RectTransform>().DOScaleY(1, 0.4f);
                isOptOpen = true;
            }

        }

    }



}

