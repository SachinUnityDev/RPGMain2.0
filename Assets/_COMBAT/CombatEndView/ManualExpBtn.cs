using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
namespace Combat
{
    public enum ManualExpState
    {
        None, 
        Clicked, 
        UnClicked, 
        NA, 
    }

    public class ManualExpBtn : MonoBehaviour, IPointerClickHandler
    {
        [Header("TBR")]

        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite spriteHL;
        [SerializeField] Sprite spriteNA;
        Image img;
        ManualExpState manualExpState; 
        
        CombatEndView combatEndView; 
        public void ManualExpBtnInit(CombatEndView combatEndView)
        {
            this.combatEndView= combatEndView;
            img = GetComponent<Image>();

            if (combatEndView.IsOnlyAbbas())
            {
                StateNA(); 
            }
            else
            {
                StateUnClicked(); 
            }
        }
        
        public void StateNA()
        {
            img.sprite = spriteNA;
            manualExpState = ManualExpState.NA;
            combatEndView.highMeritExpRewarded = true;
        }
        void StateClicked()
        {
            img.sprite = spriteHL;
            manualExpState = ManualExpState.Clicked;
            combatEndView.highMeritExpBtnPressed = true;
        }
        void StateUnClicked()
        {
            img.sprite = spriteN;
            manualExpState = ManualExpState.UnClicked; 
            combatEndView.highMeritExpBtnPressed = false;
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            if (combatEndView.highMeritExpRewarded) return;
            if (manualExpState == ManualExpState.NA) return;

            if (manualExpState == ManualExpState.Clicked) 
            {
                StateUnClicked();
            }
            else if( manualExpState== ManualExpState.UnClicked) 
            {
                StateClicked();                
            } 
        }

        

    }
}