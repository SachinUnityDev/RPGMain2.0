using Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

public class FleeView : MonoBehaviour
{

    [SerializeField] Transform container; 

    [SerializeField] Transform tickBtn; 
    [SerializeField] Button closeBtn; 
    List<string> allResultStr = new List<string>(); 
    void Start()
    {
        closeBtn.onClick.AddListener(OnCloseBtnPressed); 
    }
    public void InitFleeView()
    {
        allResultStr.Clear();   
        foreach (CharController charCtrl in CharService.Instance.allyInPlayControllers)
        {
            if (charCtrl.charModel.charName == CharNames.Abbas) continue;
            string str = ""; 
            string charName = charCtrl.charModel.charNameStr;
            
            bool willFlee = charCtrl.fleeController.OnCombatFleeChk();
            if (willFlee)            
                str = charName + " will come with you";            
            else            
                str = charName + " will stay and fight"; 
            allResultStr.Add(str);
        }
        FillContainer();
    }
    void FillContainer()
    {
        for (int i = 0; i < container.childCount; i++)
        {
            if(i< allResultStr.Count)
            {
                container.GetChild(i).gameObject.SetActive(true);
                container.GetChild(i).GetComponent<TextMeshProUGUI>().text = allResultStr[i];
            }
            else
            {
                container.GetChild(i).gameObject.SetActive(false);  
            }
        }
    }

    void OnCloseBtnPressed()
    {
        gameObject.SetActive(false);
    }
}
