using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Interactables;
using Town;
using TMPro;
using DG.Tweening;

namespace Common
{
    public class AlcoholBtnPtrEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] AlcoholNames alcoholName;
        [Header("TBR")]
        [SerializeField] TextMeshProUGUI buffTxt;
        [SerializeField] int MAX_DRINK_PER_TIMESTATE = 6;
        [SerializeField] TextMeshProUGUI onDrinkBuffTxt;

        Iitems item; // ref to buydrinksView
        
        BuyDrinksTavernView buyDrinksView;
        SelfPagePtrEvents selfPage;
        TavernModel tavernModel;

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
        public void InitAlcoholPtrEvents(BuyDrinksTavernView buyDrinksView, SelfPagePtrEvents selfPage)
        { 
            this.buyDrinksView= buyDrinksView;
            this.selfPage= selfPage;
            if (alcoholName == AlcoholNames.Beer)
                item = buyDrinksView.itemBeer;
            if (alcoholName == AlcoholNames.Cider)
                item = buyDrinksView.itemCider;

            tavernModel = BuildingIntService.Instance.tavernController.tavernModel;
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
                buffTxt.text += displayStr+"\n"; 
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
            tavernModel.selfDrinks = 0; 
        }

        bool HasReachedMaxDrinksLimit()
        {  
            if (tavernModel.selfDrinks >= MAX_DRINK_PER_TIMESTATE)
            {
                buffTxt.text = "You drank enough, don't you think?";
                selfPage.SetState(false); 
                return true;
            }   
            else
            {
                selfPage.SetState(true);   
                return false;
            }
              
        }
        public void OnPointerClick(PointerEventData eventData)
        {
           
            if (HasReachedMaxDrinksLimit()) return; 
            CharController abbasCharController 
                = CharService.Instance.GetAbbasController(CharNames.Abbas);

            AlcoholBase alcoholBase = item as AlcoholBase;
            alcoholBase.charController= abbasCharController;
            string str=  alcoholBase.OnDrink();
            onDrinkBuffTxt.text= str;

            Sequence seq = DOTween.Sequence();
            seq
               .AppendCallback(() => onDrinkBuffTxt.GetComponent<TextRevealer>().Reveal())
               .AppendInterval(2.0f)
               .AppendCallback(() => onDrinkBuffTxt.GetComponent<TextRevealer>().Unreveal())
               ; 
             seq.Play();

            if (alcoholName == AlcoholNames.Beer)
            { 
                EcoService.Instance.DebitMoneyFrmCurrentPocket(new Currency(0, 5));
            }                
            if (alcoholName == AlcoholNames.Cider)
            {
                EcoService.Instance.DebitMoneyFrmCurrentPocket(new Currency(0, 4));
            }

            tavernModel.selfDrinks++;
            HasReachedMaxDrinksLimit(); 
                
        }
    }
}