using UnityEngine;
using UnityEngine.UI;
using Common;


namespace Town
{
    public class ShipView : BuildView
    {
        public Transform smugglePanel;
        public Transform buyDrinksPanel; 

        public override Transform GetBuildInteractPanel(BuildInteractType buildInteract)
        {
            switch (buildInteract)
            {
                case BuildInteractType.None:
                    return null;
                case BuildInteractType.Smuggle:
                    return smugglePanel;
                case BuildInteractType.BuyDrink:
                    return buyDrinksPanel;
                default:
                    return null;
            }
        }
        
    }
}