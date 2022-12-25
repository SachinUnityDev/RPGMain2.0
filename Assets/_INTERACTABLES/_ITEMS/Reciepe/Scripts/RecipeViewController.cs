using Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 


namespace Interactables
{
    public class RecipeViewController : MonoBehaviour, IPanel
    {

        [Header("Recipe Buttons")]
        [SerializeField] Button cookingBtn; 
        [SerializeField] Button brewingBtn;
        [SerializeField] Button craftingBtn;
        [SerializeField] Button mergingBtn;
        [SerializeField] Button loreEnchantBtn;

        [Header("page Turn Btn")]
        [SerializeField] Button pageTurn; 

        [Header("Containers")]
        [SerializeField] Transform pageTrans;
        [SerializeField] GameObject recipePrefab; 

        [Header("Not to be ref")]
        [SerializeField] TextMeshProUGUI titleTxt; 

        void Start()
        {
            titleTxt = pageTrans.GetChild(0).GetComponent<TextMeshProUGUI>();   

        }
        #region Button responses
        void OnCookingBtnPressed()
        {
            // list allknown cooking recipes
            // get item data from item data get SO and SO => list data

        }
        void OnAlcoholBtnPressed()
        {

        }
        void OnCraftingBtnPressed()
        {

        }
        void OnMergingBtnPressed()
        {

        }
        void OnLoreEnchantBtnPressed()
        {

        }


        #endregion 


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