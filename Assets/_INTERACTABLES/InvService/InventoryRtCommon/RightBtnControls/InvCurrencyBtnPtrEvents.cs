
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

using Common;
using Town;

namespace Interactables
{
    public class InvCurrencyBtnPtrEvents : MonoBehaviour, IPointerClickHandler, IPanel
    {
        [SerializeField] bool isClicked = false;

        [SerializeField] GameObject currencyPanel;

        public void Init()
        {
          
        }

        public void Load()
        {
            UIControlServiceGeneral.Instance.TogglePanel(currencyPanel, true);
            PopulatePanel(); 
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (isClicked)            
                UnLoad();             
            else            
                Load();             
            isClicked = !isClicked;
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(currencyPanel, false);
        }

        void PopulatePanel()
        {
            Currency invMoney = EcoServices.Instance.econoModel.moneyInInv.RationaliseCurrency();
            
            currencyPanel.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text
                = invMoney.silver.ToString();
            currencyPanel.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text
                = invMoney.bronze.ToString();



        }

        void Start()
        {
            isClicked = false;
            currencyPanel = transform.GetChild(0).gameObject;
            UnLoad();
            EcoServices.Instance.OnInvMoneyChg += PopulatePanel;
        }





    }

}

