using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Town;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening; 

namespace Interactables
{
    public class ShipAlcoholBtnPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] AlcoholNames alcoholName;
        [Header("TBR")]
        [SerializeField] TextMeshProUGUI buffTxt;
        [SerializeField] int MAX_DRINK_PER_TIMESTATE = 6;

        Iitems item; // ref to buydrinksView

        BuyDrinksShipView buyDrinksShipView;
        SelfDrinksView selfDrinksView;
        ShipModel shipModel;

        [Header("Bg Color")]
        [SerializeField] Color BGColorN;
        [SerializeField] Color BGColorNA;
        [SerializeField] Color spriteColorHL;
        [Header("N/NA/HL Sprite Color")]
        [SerializeField] Color spriteColorN;
        [SerializeField] Color spriteColorNA;
        private void Start()
        {
            CalendarService.Instance.OnChangeTimeState += ResetSelfDrinks;

        }
        public void InitAlcoholPtrEvents(BuyDrinksShipView buyDrinkShipView, SelfDrinksView selfDrinksView)
        {
            this.buyDrinksShipView = buyDrinkShipView;
            this.selfDrinksView = selfDrinksView;
            if (alcoholName == AlcoholNames.Beer)
                item = buyDrinkShipView.itemBeer;
            if (alcoholName == AlcoholNames.Rum)
                item = buyDrinkShipView.itemRum;

            shipModel = BuildingIntService.Instance.shipController.shipModel; 
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (HasReachedMaxDrinksLimit())
            {
                return;
            }
            transform.GetComponent<Image>().DOColor(spriteColorHL, 0.1f);
            AlcoholBase alcoholBase = item as AlcoholBase;
            buffTxt.text = "";
            foreach (string displayStr in alcoholBase.allDisplayStr)
            {
                buffTxt.text += displayStr + "\n";
            }
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            if (HasReachedMaxDrinksLimit())
            {
                buffTxt.text = "";
                return;
            }
            transform.GetComponent<Image>().DOColor(spriteColorN, 0.1f);

        }

        public void SetNSprite()
        {
            transform.GetComponent<Image>().DOColor(BGColorN, 0.1f);
            transform.GetChild(0).GetChild(0).GetComponent<Image>().DOColor(spriteColorN, 0.1f);
        }
        public void SetNASprite()
        {
            transform.GetComponent<Image>().DOColor(BGColorNA, 0.1f);
            transform.GetChild(0).GetChild(0).GetComponent<Image>().DOColor(spriteColorNA, 0.1f);
        }
        void ResetSelfDrinks(TimeState timeState)
        {
            shipModel.selfDrinks = 0;
        }

        bool HasReachedMaxDrinksLimit()
        {
            if (shipModel.selfDrinks >= MAX_DRINK_PER_TIMESTATE)
            {
                buffTxt.text = "You drank enough, don't you think?";
                selfDrinksView.SetState(false);
                return true;
            }
            else
            {
                selfDrinksView.SetState(true);
                return false;
            }

        }
        public void OnPointerClick(PointerEventData eventData)
        {

            if (HasReachedMaxDrinksLimit()) return;
            CharController abbasCharController
                = CharService.Instance.GetAllyController(CharNames.Abbas);

            AlcoholBase alcoholBase = item as AlcoholBase;
            alcoholBase.charController = abbasCharController;
            alcoholBase.OnDrink();
            if (alcoholName == AlcoholNames.Beer)
            {
                EcoService.Instance.DebitMoneyFrmCurrentPocket(new Currency(0, 4));
            }
            if (alcoholName == AlcoholNames.Rum)
            {
                EcoService.Instance.DebitMoneyFrmCurrentPocket(new Currency(0, 11));
            }

            shipModel.selfDrinks++;
            HasReachedMaxDrinksLimit();

        }
    }
}