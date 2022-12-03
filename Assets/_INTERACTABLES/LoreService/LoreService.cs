using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Common;

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


        public LoreNames currLoreActive;
        public SubLores currSubLoreActive; 
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
        public void UnLockRanLockedLore()
        {
            List<LoreData> UnlockedLoreData = new List<LoreData>(); 
            
            UnlockedLoreData = loreModel.allLoreData.Where(t => t.isLocked == false).ToList(); 
            int loreIndex = UnityEngine.Random.Range(0, UnlockedLoreData.Count);

            LoreNames loreName = UnlockedLoreData[loreIndex].loreName;
            UnLockLore(loreName);
        }

        public List<LoreSubData> GetUnLockedSubLores(LoreNames loreName)
        {
            //loop thru the model and find unlocked sublores 

            LoreData loreData = GetLoreData(loreName);
            List<LoreSubData> unlockedSubLore 
                = loreData.allSubLore.Where(t => t.isLocked == false).ToList();

            if (unlockedSubLore.Count > 0)
                return unlockedSubLore;
            else
                Debug.Log("Nothing Unlocked inside this lore"); 
            return null;
        }


        public LoreData GetLoreData(LoreNames loreName)
        {
            LoreData loreData = 
                    loreModel.allLoreData.Find(t => t.loreName == loreName);
            return loreData ;
        }



        public List<Sprite> GetLoreSprite(LoreNames LoreName, SubLores subloreName)
        {
            List<LoreSubData> allLoreSub = loreSO.allLoreData.Find(t => t.isLocked == false).allSubLore;
            List<Sprite> loreSprites =  allLoreSub.Find(t => t.isLocked == false).pics;
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

        public void UnLockLore(LoreNames loreName)
        {
            loreModel.allLoreData
                          .Find(t => t.loreName == loreName).isLocked = false; 
        }

        public bool IsLoreLocked(LoreNames loreName)
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

