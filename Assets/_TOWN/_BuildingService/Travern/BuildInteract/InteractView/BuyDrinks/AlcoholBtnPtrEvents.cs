using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Interactables;
using Town;
using TMPro;

namespace Common
{
    public class AlcoholBtnPtrEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] AlcoholNames alcoholName;
        [Header("TBR")]
        [SerializeField] TextMeshProUGUI buffTxt;
        [SerializeField] int MAX_DRINK_PER_TIMESTATE = 6;

        Iitems item; // ref to buydrinksView
        BuyDrinksView buyDrinksView;
        TavernModel tavernModel;

        private void Start()
        {
            CalendarService.Instance.OnChangeTimeState += ResetSelfDrinks;
          
        }
        public void InitAlcoholPtrEvents(BuyDrinksView buyDrinksView)
        { 
            this.buyDrinksView= buyDrinksView;
            if (alcoholName == AlcoholNames.Beer)
                item = buyDrinksView.itemBeer;
            if (alcoholName == AlcoholNames.Cider)
                item = buyDrinksView.itemCider;

            tavernModel = BuildingIntService.Instance.tavernController.tavernModel;
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            AlcoholBase alcoholBase = item as AlcoholBase;
            buffTxt.text = "";
            foreach (string displayStr in alcoholBase.allDisplayStr)
            {
                buffTxt.text += displayStr+"\n"; 
            }
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            if(HasReachedMaxDrinksLimit())
            buffTxt.text = ""; 
        }

        void ResetSelfDrinks(TimeState timeState)
        {
            tavernModel.selfDrinks = 0; 
        }

        bool HasReachedMaxDrinksLimit()
        {  
            if (tavernModel.selfDrinks >= MAX_DRINK_PER_TIMESTATE)
            {
                buffTxt.text = "You drank enough, don't you think?";
                return true;
            }   
            else 
                return false;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (HasReachedMaxDrinksLimit()) return; 
            CharController abbasCharController 
                = CharService.Instance.GetCharCtrlWithName(CharNames.Abbas);

            AlcoholBase alcoholBase = item as AlcoholBase;
            alcoholBase.charController= abbasCharController;
            alcoholBase.OnDrink();


            if (alcoholName == AlcoholNames.Beer)
            {
                EcoServices.Instance.DebitPlayerInvThenStash(new Currency(0, 4));
            }                
            if (alcoholName == AlcoholNames.Cider)
            {
                EcoServices.Instance.DebitPlayerInvThenStash(new Currency(0, 5));
            }

            tavernModel.selfDrinks++;
            HasReachedMaxDrinksLimit(); 
                
        }
    }
}