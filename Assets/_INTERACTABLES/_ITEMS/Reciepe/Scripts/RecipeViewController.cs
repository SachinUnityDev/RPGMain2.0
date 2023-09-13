using Common;
using System;
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
        [SerializeField] Button pageBtn; 

        [Header("Containers")]
        [SerializeField] Transform pageContainer;
        [SerializeField] GameObject recipePrefab; 

  

        [Header("Recipe Type Selected")]
        public RecipeType recipeTypeSelect;
        [SerializeField] TextMeshProUGUI recipeTypeTxt; 
        public List<ItemData> recipeLs = new List<ItemData>();

        [Header("Recipe Pages")]
        [SerializeField] int pageIndex;
        [SerializeField]const int PANEL_PER_page = 5;
        [SerializeField] int netPages = -1;

        void OnEnable()
        {
            
            cookingBtn.onClick.AddListener(OnCookingBtnPressed); 
            craftingBtn.onClick.AddListener(OnCraftingBtnPressed);  
            mergingBtn.onClick.AddListener(OnMergingBtnPressed);
            brewingBtn.onClick.AddListener(OnBrewingBtnPressed); 

            pageBtn.onClick.AddListener(OnPageBtnPressed);
        }
        
        #region Button responses
        void OnCookingBtnPressed()
        {
            On_RecipeTypeSelect(RecipeType.Cooking); 
        }
     
        void OnBrewingBtnPressed()
        {
            On_RecipeTypeSelect(RecipeType.Brewing);
        }
        void OnCraftingBtnPressed()
        {
            On_RecipeTypeSelect(RecipeType.Crafting);
        }
        void OnMergingBtnPressed()
        {
            On_RecipeTypeSelect(RecipeType.Merging);
        }

        void OnLoreEnchantBtnPressed()
        {
            
        }
        void On_RecipeTypeSelect(RecipeType recipeType)
        {
            recipeTypeSelect = recipeType;
            recipeTypeTxt = transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
            recipeTypeTxt.text = recipeType.ToString();
            
            PopulateTheRecipe();
        }

        #endregion 

        void OnPageBtnPressed()
        {            
            PopulatePageScroll();  
        }
        void PopulateTheRecipe()
        {
            recipeLs.Clear();
            recipeLs = RecipeService.Instance.recipeModel.GetRecipeTypeKnown(recipeTypeSelect);
            int recipeCount = recipeLs.Count;

            if (recipeCount % 5 == 0)
                netPages = recipeCount / 5;
            else
                netPages = (recipeCount / 5) + 1;

            pageContainer = transform.GetChild(1);
            pageIndex = 1;
            int index = pageIndex * PANEL_PER_page;
            int j = 0;
           
            for (int i = index - 5; i < index; i++)
            {
                if (i < recipeLs.Count)
                {
                   // Debug.Log("PAGE..." + pageContainer.GetChild(j).name);
                    pageContainer.GetChild(j).gameObject.SetActive(true);
                    pageContainer.GetChild(j).GetComponent<RecipePanelView>().Init(recipeLs[i]);
                }
                else
                {
                    pageContainer.GetChild(j).gameObject.SetActive(false);
                }
                j++;
                if (j > 5)
                    j = 0;

            }
        }

        void PopulatePageScroll()
        {
            if (netPages > 0 && pageIndex < netPages)
            {
                pageIndex++;
            }
            else
            {
                pageIndex = 1;
            }
            PopulateTheRecipe();
        }
 
 
        
        public void Init( )
        {
            On_RecipeTypeSelect(RecipeType.Cooking);
        }

        public void Load()
        {
          
        }

        public void UnLoad()
        {
           
        }
    }
}