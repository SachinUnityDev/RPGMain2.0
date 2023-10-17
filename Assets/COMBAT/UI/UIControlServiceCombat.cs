using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using DG.Tweening;
using System.Linq;
using TMPro;
using System;
using Common;

public enum UITransformState
{
    None,
    Open,
    Close,
}
namespace Combat
{

    public enum PerkSelectState
    {         
        Clickable, 
        Clicked,
        UnClickable, 
    }
    public enum SkillSelectState  // hover information for the skill state 
    {
        None,        
        Clickable,
        UnHoverable, 
        Unclickable_passiveSkills,  // applies always to active skills 
        UnClickable_InCd,
        Unclickable_notCharsTurn,
        Unclickable_notOnCastPos,// grey out 
        UnClickable_NoTargets,// grey out 
        UnClickable_NoStamina,
        UnClickable_InTactics,
        Clicked,
        UnClickable_NoActionPts,
        UnClickable_Locked, 
        UnClickable_Misc, 

    }

    public class UIControlServiceCombat : MonoSingletonGeneric<UIControlServiceCombat>
    {
        // Set UI State and control the functionalities 
        [SerializeField] List<GameObject> allOpenUI = new List<GameObject>();
        [SerializeField] UITransformState currentState;
        [SerializeField] List<UIElementsHLSO> allUIElementHLSO = new List<UIElementsHLSO>();

   

        #region SCALING
        public void ToggleUIStateScale(GameObject UIObject, UITransformState newState)
        {

            if (newState == UITransformState.Open)
            {
                if (allOpenUI.Any(t => t == UIObject))
                {
                    newState = UITransformState.Close; 
                }
            }         
            switch (newState)
            {
                case UITransformState.None:
                    UIObject.transform.DOScale(0f, 0.25f);
                    Debug.Log("None UI State set"); 
                    break;
                case UITransformState.Open:
                    allOpenUI.Add(UIObject);
                    UIObject.transform.DOScale(1f, 0.25f);
                    break;
                case UITransformState.Close:
                    allOpenUI.Remove(UIObject);
                    UIObject.transform.DOScale(0f, 0.25f);
                    break;
                default:
                    break;
            }   

        }
        #endregion


        #region SIBILING HIERARCHY

        public void SetMaxSibliingIndex(GameObject GO)
        {
            int childCount = GO.transform.parent.childCount - 1;
            GO.transform.SetSiblingIndex(childCount);

        }



        #endregion


        UIElementsHLSO GetUIElementSO(UIElementType _uiElementType)
        {
            return allUIElementHLSO.Find(t => t.UIElementType == _uiElementType); 
        }
        public void ToggleSkillBtnSelectState(UIElementType _uIElementType, GameObject _uiObject, SkillSelectState _newState)
        {
            UIElementsHLSO UIElementSO = GetUIElementSO(_uIElementType);

            switch (_newState)
            {
                case SkillSelectState.Clickable:
                    _uiObject.GetComponent<Image>().color = UIElementSO.clickableColor;
                    _uiObject.GetComponentInChildren<Image>().sprite = UIElementSO.clickedSprite;                 // change color and frame..                                        

                    break;
                case SkillSelectState.UnClickable_InCd:
                    _uiObject.GetComponent<Image>().color = UIElementSO.unClickableColor;
                    _uiObject.GetComponentInChildren<Image>().sprite = UIElementSO.UnclickableSprite;

                    break;
                case SkillSelectState.UnClickable_NoTargets:
                    _uiObject.GetComponent<Image>().color = UIElementSO.unClickableColor;
                    _uiObject.GetComponentInChildren<Image>().sprite = UIElementSO.UnclickableSprite;
                    break;
                case SkillSelectState.UnClickable_NoStamina:
                    _uiObject.GetComponent<Image>().color = UIElementSO.unClickableColor;
                    _uiObject.GetComponentInChildren<Image>().sprite = UIElementSO.UnclickableSprite;
                    break;
                case SkillSelectState.Clicked:
                    _uiObject.GetComponent<Image>().color = UIElementSO.clickedColor;
                    _uiObject.GetComponentInChildren<Image>().sprite = UIElementSO.clickedSprite;
                    break;
                //case SkillSelectState.DoubleStrike:
                //    _uiObject.GetComponent<Image>().color = UIElementSO.clickedColor;
                //    _uiObject.GetComponentInChildren<Image>().sprite = UIElementSO.clickedSprite;
                 //   break;
                default:
                    break;
            }




        }


        public void TogglePerkSelectState(UIElementType _UIElementType, GameObject UIObject, PerkSelectState _newState)
        {
            UIElementsHLSO UIElementSO = GetUIElementSO(_UIElementType); 

            switch (_newState)
            {
                case PerkSelectState.Clickable:
                    UIObject.GetComponentInChildren<Image>().sprite = UIElementSO.clickableSprite;
                    UIObject.GetComponentInChildren<TextMeshProUGUI>().color = UIElementSO.clickableColor; 
                    break;
                case PerkSelectState.Clicked:
                    UIObject.GetComponentInChildren<Image>().sprite = UIElementSO.clickedSprite;
                    UIObject.GetComponentInChildren<TextMeshProUGUI>().color = UIElementSO.clickedColor;
                    break;
                case PerkSelectState.UnClickable:
                    UIObject.GetComponentInChildren<Image>().sprite = UIElementSO.UnclickableSprite;
                    UIObject.GetComponentInChildren<TextMeshProUGUI>().color = UIElementSO.unClickableColor;

                    break;             
                default:
                    break;
            }
        }

        public void TurnOnOff(GameObject ImgGO, bool status)
        {
            Image Img = ImgGO.GetComponentInChildren<Image>();
            Img.enabled = status;          
        } 
    
        public void CloseAllOpenUI()
        {
            if (allOpenUI.Count <= 0) return;
            foreach (GameObject uiObject in allOpenUI.ToList())
            {
                ToggleUIStateScale(uiObject, UITransformState.Close);
            }
        }
      
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                CloseAllOpenUI();
            }
        }
    }


}

