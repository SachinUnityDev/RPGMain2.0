
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using Common;
using Town;

namespace Interactables
{
    public class InvCurrencyBtnPtrEvents : MonoBehaviour, IPanel, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] bool isClicked = false;

        [SerializeField] Transform invCurrency;

        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite spriteHL;

        [SerializeField] Image img;


        [Header("Withdraw/Transfer to Stash")]
        [SerializeField] Button withdrawFrmStashBtn;
        [SerializeField] Button transfer2StashBtn;
        [SerializeField] Button transactBtn;

        [Header("Transact Money Display")]
        [SerializeField] Transform transactPanel;

        [Header("Global Var")]
        [SerializeField] CurrTransactState transactState;
        //[SerializeField] bool isWithdrawSelect = false;
        //[SerializeField] bool isTransferSelect =false;
        
        void Start()
        {
            img= GetComponent<Image>();
            isClicked = false;          
            UnLoad();
            EcoService.Instance.OnInvMoneyChg += FillPanel;
   
            withdrawFrmStashBtn.onClick.AddListener(OnWithFrmStashPressed); 
            transfer2StashBtn.onClick.AddListener(OnTransfer2StashPressed);
            transactBtn.onClick.AddListener(OnTransferBtnPressed);
            transactState = CurrTransactState.None;

        }

        void OnWithFrmStashPressed()
        {
            transactState = CurrTransactState.WithDrawFrmStash; 
            transactPanel.gameObject.SetActive(true);
            Currency stashCurr = EcoService.Instance.GetMoneyAmtInPlayerStash().DeepClone();
            transactPanel.GetComponent<CurrencyTransactView>().FillTransactBox(stashCurr, transactState);
            transfer2StashBtn.GetComponent<CurrTransactBtnPtrEvents>().OnUnClick();
        }
        void OnTransfer2StashPressed()
        {
            transactState = CurrTransactState.Transfer2Stash;
            transactPanel.gameObject.SetActive(true);
            Currency invCurr = EcoService.Instance.GetMoneyAmtInPlayerInv().DeepClone();
            transactPanel.GetComponent<CurrencyTransactView>().FillTransactBox(invCurr, transactState);
            withdrawFrmStashBtn.GetComponent<CurrTransactBtnPtrEvents>().OnUnClick();
        }
        void OnTransferBtnPressed()
        {
            if(transactState == CurrTransactState.Transfer2Stash || transactState == CurrTransactState.WithDrawFrmStash)
                transactPanel.GetComponent<CurrencyTransactView>().CompleteTransaction();  
            transactState = CurrTransactState.None;
            withdrawFrmStashBtn.GetComponent<CurrTransactBtnPtrEvents>().OnUnClick();
            transfer2StashBtn.GetComponent<CurrTransactBtnPtrEvents>().OnUnClick();
        }
      

        public void Init()
        {
          
        }

        public void Load()
        {
            UIControlServiceGeneral.Instance.TogglePanel(transform.GetChild(0).gameObject, true);
            FillPanel(EcoService.Instance.GetMoneyAmtInPlayerInv());
            transactPanel.gameObject.SetActive(false);
            img.sprite = spriteN; 
        }


        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(transform.GetChild(0).gameObject, false);
            transactPanel.gameObject.SetActive(false);
            img.sprite = spriteN;
        }

        void FillPanel(Currency invMoney)
        {               
            invCurrency.GetComponent<DisplayCurrency>().Display(invMoney);
            
        }

    

        public void OnPointerClick(PointerEventData eventData)
        {
            if (isClicked)
                UnLoad();
            else
                Load();
            isClicked = !isClicked;
            img.sprite = spriteN;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            img.sprite = spriteHL;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            img.sprite = spriteN;
        }
    }

    public enum CurrTransactState
    {
        None, 
        WithDrawFrmStash, 
        Transfer2Stash, 
    }

}

