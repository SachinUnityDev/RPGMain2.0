using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Town
{

    public class DisplayCurrencyWithToggle : MonoBehaviour
    {

        [SerializeField] Button toggleBtn;        
        [SerializeField] bool isInvMoneySelect;

        [SerializeField] TextMeshProUGUI moneyFrmTxt;

        string inv_Txt = "In Inventory";
        string stash_Txt = "In Stash"; 
        private void Awake()
        {
            //toggleBtn = transform.GetChild(2).GetComponent<Button>();
            //moneyFrmTxt= transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            toggleBtn.onClick.AddListener(OnToggleBtnPressed);

            isInvMoneySelect = true;
            moneyFrmTxt.text = inv_Txt;
            EcoServices.Instance.OnInvMoneyChg += FillInvMoney;
            EcoServices.Instance.OnStashMoneyChg += FillStashMoney;
        }
        public void DisplayCurrency(Currency amt)
        {
            transform.GetComponent<DisplayCurrency>().Display(amt); 
        }
        public void InitCurrencyToggle()
        {
           
            FillInvMoney(EcoServices.Instance.GetMoneyAmtInPlayerInv().DeepClone());
        }
        void OnToggleBtnPressed()
        {
            isInvMoneySelect = !isInvMoneySelect;
            if (isInvMoneySelect)
            {
                Currency amt = EcoServices.Instance.GetMoneyAmtInPlayerInv().DeepClone();
                FillInvMoney(amt);
            }
            else
            {
                Currency amt = EcoServices.Instance.GetMoneyAmtInPlayerStash().DeepClone();
                FillStashMoney(amt);
            }
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