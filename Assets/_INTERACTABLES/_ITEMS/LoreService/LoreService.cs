using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Common;
using System.Security.Cryptography.X509Certificates;

namespace Interactables
{
    public class LoreService : MonoSingletonGeneric<LoreService>
    {
       // public LoreModel loreModel;
        public LoreSO loreSO;
        public LoreViewController loreViewController;
        public LoreController loreController;

        public GameObject lorePrefab;
        public GameObject lorePanel;
        public LoreModel loreModel;


        //public LoreNames currLoreActive;
        //public SubLores currSubLoreActive; 
        void Start()
        {
            loreController = GetComponent<LoreController>();
            Init(); 
        }
        public void Init()
        {
            loreModel = new LoreModel(loreSO);
           // OpenLoreView();

        }

        public void UnLockTheCompleteLore(LoreBookNames loreName) // is done on reading "LORE BOOK 
        {
            LoreData loreData = GetLoreData(loreName);
            if (IsLoreLocked(loreName))
            {
                loreData.isLocked = false;
            }
        }
        public void UnLockRandomSubLore(LoreBookNames _loreName)
        {
            LoreData loreData = GetLoreData(_loreName);
            if (IsLoreLocked(_loreName))
            {
                loreData.isLocked = false;
                int i = UnityEngine.Random.Range(0, loreData.allSubLore.Count);
                loreData.allSubLore[i].isLocked = false; 
            }
            else
            {
                foreach (LoreSubData subData in loreData.allSubLore)
                {
                    if (subData.isLocked)
                    {
                        subData.isLocked = false;
                        break;
                    }                        
                }
            }
        }


        public List<LoreSubData> GetUnLockedSubLores(LoreBookNames loreName)
        {
            LoreData loreData = GetLoreData(loreName);
            List<LoreSubData> unlockedSubLore 
                = loreData.allSubLore.Where(t => t.isLocked == false).ToList();

            if (unlockedSubLore.Count > 0)
                return unlockedSubLore;
            else
                Debug.Log("Nothing Unlocked inside this lore"); 
            return null;
        }
        public LoreData GetLoreData(LoreBookNames loreName)
        {
            LoreData loreData = 
                    loreModel.allLoreData.Find(t => t.loreName == loreName);
            return loreData ;
        }

        public List<Sprite> GetLoreSprite(LoreBookNames _loreName, SubLores subloreName)
        {

            LoreData loreData = loreSO.allLoreData.Find(t => (t.loreName ==_loreName) && (t.isLocked == false));

            List<Sprite> loreSprites =  loreData.allSubLore.Find(t => t.isLocked == false 
                                                                        && t.subLoreNames == subloreName).pics;

            if(loreSprites.Count != 0)
            {
                return loreSprites;
            }
            else
            {
                Debug.Log("Lore Sprites null"); 
                return null;
            }
        }

        public bool IsLoreLocked(LoreBookNames loreName)
        {
            bool status = loreModel.allLoreData
                           .Find(t => t.loreName == loreName).isLocked;

            return status;
        }

        public string GetLoreString(LoreBookNames loreName)
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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                UnLockRandomSubLore(LoreBookNames.LandsOfShargad);
            }
        }

    }


}

