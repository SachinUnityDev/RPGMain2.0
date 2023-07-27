using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Town
{
    [RequireComponent(typeof(DisplayCurrency))]
    public class DisplayCurrencyWithToggle : MonoBehaviour
    {
        [SerializeField] Button toggleBtn;    
        [SerializeField] TextMeshProUGUI moneyFrmTxt;

        string inv_Txt = "In Inventory";
        string stash_Txt = "In Stash";
        public PocketType pocketType;
        private void Awake()
        {
            //toggleBtn = transform.GetChild(2).GetComponent<Button>();
            //moneyFrmTxt= transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            toggleBtn.onClick.AddListener(OnToggleBtnPressed);            
                 
        }
        private void Start()
        {
            EcoServices.Instance.OnInvMoneyChg += FillInvMoney;
            EcoServices.Instance.OnStashMoneyChg += FillStashMoney;
            
            InitCurrencyToggle();
        }
        public void DisplayCurrency(Currency amt)
        {
            transform.GetComponent<DisplayCurrency>().Display(amt); 
        }
        public void InitCurrencyToggle()
        {
            pocketType = PocketType.Inv;
            EcoServices.Instance.On_PocketSelected(pocketType);             
            FillMoney();
        }
        void OnToggleBtnPressed()
        {
            if (pocketType == PocketType.Stash)
                pocketType = PocketType.Inv; 
            else
                pocketType= PocketType.Stash;
            EcoServices.Instance.On_PocketSelected(pocketType);


            FillMoney();
        }
        public void FillMoney()
        {
            Currency amt = EcoServices.Instance.GetMoneyFrmCurrentPocket().DeepClone();
            if (pocketType == PocketType.Stash)
                FillStashMoney(amt); 
            else
                FillInvMoney(amt);  
        }
        void FillInvMoney(Currency amt)
        {
            moneyFrmTxt.text = inv_Txt;            
            DisplayCurrency(amt);     
        }
        void FillStashMoney(Currency amt)
        {
            moneyFrmTxt.text = stash_Txt;
            DisplayCurrency(amt);
        }
    }
}

