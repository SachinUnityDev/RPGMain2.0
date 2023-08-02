using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace Town
{
    public class SicknessView : MonoBehaviour
    {   
        public List<Image> ptrImgs = new List<Image>();
      
        [SerializeField] List<TempTraitBuffData> sickLs = new List<TempTraitBuffData>();  
        HealView healView;
        public void SelectPtr(int select)
        {
            for (int i = 0; i < ptrImgs.Count; i++)
            {
                if(i != select)
                     ptrImgs[i].gameObject.SetActive(false);
                else
                    ptrImgs[i].gameObject.SetActive(true);
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