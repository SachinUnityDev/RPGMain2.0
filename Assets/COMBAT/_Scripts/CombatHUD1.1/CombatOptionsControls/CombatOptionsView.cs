using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI; 

namespace Combat
{
    public class CombatOptionsView : MonoBehaviour
    {
        [Header("Flee and Setting btns")]
        [SerializeField] GameObject fleeBtn;
        [SerializeField] Transform combatOptionsBtn;

        [Header(" global Opts")]
        [SerializeField] bool isOptOpen;
        [SerializeField] Button OptionsBtn;

        
        [SerializeField] float fleeBtnHt;
        [SerializeField] float combatOptsWidth;
        private void Awake()
        {
            OptionsBtn = GetComponent<Button>(); 
        }
        void Start()
        {
            isOptOpen = false;
            fleeBtnHt = fleeBtn.GetComponent<RectTransform>().sizeDelta.y - 45f;
            combatOptsWidth = combatOptionsBtn.GetComponent<RectTransform>().sizeDelta.x - 20f;

            fleeBtn.GetComponent<Transform>().DOLocalMoveY(fleeBtnHt, 0);
            combatOptionsBtn.GetComponent<Transform>().DOLocalMoveX(combatOptsWidth, 0);
            OptionsBtn.onClick.AddListener(OnButtonClick);

           
        }

        public void OnButtonClick()
        {
            if (isOptOpen)
            {
                fleeBtn.GetComponent<Transform>().DOLocalMoveY(fleeBtnHt, 0.4f);
                combatOptionsBtn.GetComponent<Transform>().DOLocalMoveX(combatOptsWidth, 0.4f);
                isOptOpen = false;
            }
            else
            {
                fleeBtn.GetComponent<Transform>().DOLocalMoveY(-fleeBtnHt, 0.4f);
                combatOptionsBtn.GetComponent<Transform>().DOLocalMoveX(-combatOptsWidth, 0.4f);
                isOptOpen = true;
            }
        }

    }



}

