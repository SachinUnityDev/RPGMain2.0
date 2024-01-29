using Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

public class FleeView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI headingTxt; 

    [SerializeField] Transform container; 

    [SerializeField] Button tickBtn; 
    [SerializeField] Button closeBtn; 
    List<string> allResultStr = new List<string>();
    [SerializeField] List<CharController> charFleeing= new List<CharController>();  


    void Start()
    {
        closeBtn.onClick.AddListener(OnCloseBtnPressed);
    }
    public void InitFleeView()
    {
        allResultStr.Clear();   
        if(CharService.Instance.allCharInCombat.Count <= 1) // abbas is the only char
        {
            headingTxt.text = "Are you sure you want to flee combat?"; 
        }
        else
        {
            foreach (CharController charCtrl in CharService.Instance.allCharInCombat)
            {
                if (charCtrl.charModel.orgCharMode == CharMode.Enemy) continue; 
                if (charCtrl.charModel.charName == CharNames.Abbas) 
                {
                    charFleeing.Add(charCtrl);  
                    continue; 
                }
                string str = "";
                string charName = charCtrl.charModel.charNameStr;

                bool willFlee = charCtrl.fleeController.OnCombatFleeChk();
                if (willFlee)
                {
                    charFleeing.Add(charCtrl);  
                    str = charName + " will come with you";
                }   
                else
                    str = charName + " will stay and fight";
                allResultStr.Add(str);
            }
            headingTxt.text = "If you flee combat:"; 
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
    void OnFleeBtnPressed()
    {
        if (charFleeing.Count == 0) return; 
        

            // if only abbas and all fleeing =>close combat 
            // if few staying continue combat => rest flee and disappear
    }






    void OnCloseBtnPressed()
    {
        gameObject.SetActive(false);
    }
}
