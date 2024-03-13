using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace Town
{
    public class SicknessView : MonoBehaviour
    {   
      
        [SerializeField] List<TempTraitBuffData> sickLs = new List<TempTraitBuffData>();  
        HealView healView;
        public void SelectPtr(int select)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if(i == select)
                    transform.GetChild(i).GetComponent<SicknessBtnPtrEvents>().ShowPtr(); 
                else
                    transform.GetChild(i).GetComponent<SicknessBtnPtrEvents>().HidePtr();
            }
        }
        public void InitSicknessNames(HealView healView, List<TempTraitBuffData> sickLs)
        {
            this.healView = healView;
            this.sickLs= sickLs;
            int i = 0;            
            foreach (TempTraitBuffData temp in sickLs)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                transform.GetChild(i).GetComponent<SicknessBtnPtrEvents>().InitPtrEvents(healView, this, temp);
                i++; 
            }
            for (int j = i; j < transform.childCount; j++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            if (i != 0)
            {
                SelectPtr(0);
            }
        }
    }
}