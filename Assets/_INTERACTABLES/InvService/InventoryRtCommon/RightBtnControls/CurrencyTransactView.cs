using Interactables;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;
using UnityEngine.UI;

namespace Common
{
    public class CurrencyTransactView : MonoBehaviour
    {
        [SerializeField] Button silverTopBtn;
        [SerializeField] Button silverBtmBtn;

        [SerializeField] Button bronzeTopBtn;
        [SerializeField] Button bronzeBtmBtn;

        //[SerializeField] bool isWithDrawFrmStash = false;
        //[SerializeField] bool isTransfer2Stash = false;

        [SerializeField] CurrTransactState transactState; 
        [SerializeField] Currency currTransactBox;

        [SerializeField] Currency currMax;

        private void Start()
        {
            silverTopBtn.onClick.AddListener(OnSilverTopBtnPressed);
            silverBtmBtn.onClick.AddListener(OnSilverBottomBtnPressed);
            bronzeTopBtn.onClick.AddListener(OnBronzeTopBtnPressed); 
            bronzeBtmBtn.onClick.AddListener(OnBronzeBtmBtnPressed);
            EcoService.Instance.OnInvMoneyChg += 
                (Currency invMoney) => transform.GetComponent<DisplayCurrency>()
                .Display(invMoney);          
        }
        public void FillTransactBox(Currency currTransactBox, CurrTransactState transactState)
        {
            this.transactState = transactState; 
            if(transactState == CurrTransactState.WithDrawFrmStash)
            {
                currMax = EcoService.Instance.GetMoneyAmtInPlayerStash().DeepClone();
            }
            if(transactState == CurrTransactState.Transfer2Stash)
            {
                currMax = EcoService.Instance.GetMoneyAmtInPlayerInv().DeepClone();                
            }            
            this.currTransactBox = currTransactBox.DeepClone();
            transform.GetComponent<DisplayCurrency>().Display(currTransactBox); 
        }

        public Currency GetCurrencyInTransactBox()
        {

            return null; 
        }

        public void CompleteTransaction()
        {
            if (transactState == CurrTransactState.WithDrawFrmStash)
            {
               EcoService.Instance.DebitPlayerStash(this.currTransactBox);
               EcoService.Instance.AddMoney2PlayerInv(this.currTransactBox);
            }
            else if(transactState == CurrTransactState.Transfer2Stash)
            {
                EcoService.Instance.DebitPlayerInv(this.currTransactBox);
                EcoService.Instance.AddMoney2PlayerStash(this.currTransactBox);
            }
            transform.gameObject.SetActive(false);
            transactState = CurrTransactState.None; 
        }
        void OnSilverTopBtnPressed() // increase 
        {            
            if(currMax.silver > currTransactBox.silver) 
            {
                currTransactBox.AddMoney(new Currency(1, 0));                     
            }
            transform.GetComponent<DisplayCurrency>().Display(currTransactBox);
        }
        void OnSilverBottomBtnPressed() 
        {
            if (currTransactBox.silver > 0)
            {
                currTransactBox.SubMoney(new Currency(1, 0));
            }
            transform.GetComponent<DisplayCurrency>().Display(currTransactBox);
        }
        void OnBronzeTopBtnPressed()
        {
            if (currMax.BronzifyCurrency() > currTransactBox.BronzifyCurrency())
            {
                currTransactBox.AddMoney(new Currency(0, 1));
            }      
            transform.GetComponent<DisplayCurrency>().Display(currTransactBox);
        }
        void OnBronzeBtmBtnPressed()
        {
            if (currTransactBox.bronze > 0 || currTransactBox.silver > 0)
            {
                currTransactBox.SubMoney(new Currency(0, 1));
            }
            transform.GetComponent<DisplayCurrency>().Display(currTransactBox);
        }
        

    }
}