using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{

    [CreateAssetMenu(fileName = "RecipeSO", menuName = "Item Service/RecipeSO")]

    public class RecipeSO : ScriptableObject
    {
        [Header("Product Item")]
        public MealsNames mealName;
        public SoupNames soupName;
        public TeaNames teaName;

        [Header("Ingredient")]
        public List<RcpSlotData> slotData = new List<RcpSlotData>(); 

        [Header("Tool")]
        public ToolNames toolName;

        private void Awake()
        {
            toolName = ToolNames.CookingPot;
        }
        [Header("Img")]
        public Sprite pdtSprite;

    }

    [System.Serializable]
    public class RcpSlotData
    {
        public IngredNames IngredName;
        public FruitNames FruitName;
        public FoodNames FoodName;
        public HerbNames HerbName;
        public int quantity;

 
    }

}
