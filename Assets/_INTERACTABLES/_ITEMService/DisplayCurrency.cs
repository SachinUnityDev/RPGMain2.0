using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Town;
using UnityEngine;

public class DisplayCurrency : MonoBehaviour
{
    [SerializeField] Currency currency;
    void Start()
    {
        
    }
    public void Display(Currency curr)
    {
        currency = curr.RationaliseCurrency(); 
        //Silver
        transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text =currency.silver.ToString();
        // bronze
        transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = currency.bronze.ToString();
    }

}
