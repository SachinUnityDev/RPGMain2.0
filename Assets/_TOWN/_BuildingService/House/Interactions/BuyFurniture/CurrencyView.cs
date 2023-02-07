using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Interactables
{

    public class CurrencyView : MonoBehaviour
    {
        [SerializeField] Transform silver;
        [SerializeField] Transform bronze;

        private void Awake()
        {
            silver = transform.GetChild(0);
            bronze = transform.GetChild(1);
        }
        public void Init(Currency currency)
        {
            silver.GetChild(1).GetComponent<TextMeshProUGUI>().text = currency.silver.ToString();
            bronze.GetChild(1).GetComponent<TextMeshProUGUI>().text = currency.bronze.ToString();
        }
    }
}