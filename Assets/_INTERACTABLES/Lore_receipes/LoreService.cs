using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Common;

namespace Interactables
{
    public class LoreService : MonoSingletonGeneric<LoreService>
    {
        public LoreModel loreModel;
        public LoreSO loreSO;
        public LoreViewController loreViewController;
        public LoreController loreController;

        public GameObject lorePrefab;
        public GameObject lorePanel;



        public LoreNames currLoreActive;
        public LoreNames currSubLoreActive; 
        void Start()
        {
            loreController = GetComponent<LoreController>();

            Init(); 
        }

        public void Init()
        {
            loreModel = new LoreModel(loreSO);
            //OpenLoreView();

        }

        public LoreData GetLoreData(LoreNames loreName)
        {
            LoreData loreData = 
                    loreModel.allLoreData.Find(t => t.loreName == loreName);
            return loreData ;
        }

        public bool IsLoreUnLocked(LoreNames loreName)
        {
            bool status = loreModel.allLoreData
                           .Find(t => t.loreName == loreName).isLocked;

            return status;
        }

        public string GetLoreString(LoreNames loreName)
        {
            string str = loreModel.allLoreStrData.Find(t => t.loreName == loreName).loreNameStr; 

            return str; 
        }
        
        public void OpenLoreView()
        {
            if(lorePanel == null)
            {
                lorePanel = Instantiate(lorePrefab);
                RectTransform rect = lorePanel.GetComponent<RectTransform>();
                rect.anchoredPosition = Vector3.zero;
                rect.localScale = Vector3.one;

                // Rect settings for anchor and pivot to be done for UI Service
            }
            UIControlServiceGeneral.Instance.SetMaxSibling2Canvas(lorePanel);
            lorePanel.SetActive(true);
            loreViewController = lorePanel.GetComponent<LoreViewController>();
            loreViewController.GetComponent<IPanel>().Init(); 
        }
        public void CloseLoreView()
        {

            lorePanel.SetActive(false);
        }

    }


}

