using Combat;
using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{

    public class LootController : MonoBehaviour
    {
        [Header("Loot View")]
        [SerializeField] Vector2 pos = new Vector2(-400, 0);
        public LootBase lootBase;     

        [Header("Loot Prefab TBR")]
        public GameObject lootPrefab;

        [Header(" loot view GO")]
        [SerializeField] GameObject lootViewGO; 

        public void ShowLootTableInLandscape(List<ItemType> allItemType, Transform parentTrans)
        {
            LandscapeNames landscapeNames = LandscapeNames.Sewers;
            lootBase = LootService.Instance.lootFactory.GetLootBase(landscapeNames);
            List<ItemDataWithQty> itemLS = lootBase.GetLootList(allItemType);

            InitLootView(itemLS, parentTrans); 
        }

        public void ShowLootTableInCombat(List<ItemType> allItemType ,EnemyPackName enemyPackName, Transform parentTrans)
        {
            LandscapeNames landscapeNames = LandscapeNames.Sewers;
            lootBase = LootService.Instance.lootFactory.GetLootBase(landscapeNames);

            EnemyPackBase enemyPackBase = CombatService.Instance.enemyPackController.GetEnemyPackBase(enemyPackName);             

            List<ItemDataWithQty> itemLS = lootBase.GetLootList(allItemType);
           // itemLS.AddRange(enemyPackBase.lootData); 
            InitLootView(itemLS, parentTrans);
        }

        public void ShowLootTable4MapE(List<ItemDataWithQty> itemLS, Transform parent)
        {
            InitLootView(itemLS, parent); 
        }

        void InitLootView(List<ItemDataWithQty> itemLS, Transform parent)
        {

            if (LootService.Instance.isLootDsplyed) return; // return multiple clicks
            lootViewGO = FindObjectOfType<LootView>()?.gameObject;
            if(lootViewGO == null) 
             lootViewGO = Instantiate(lootPrefab);

            LootService.Instance.lootView = lootViewGO.GetComponent<LootView>(); // update in current scene
            lootViewGO.transform.SetParent(parent);

            int index = lootViewGO.transform.parent.childCount - 2;
            lootViewGO.transform.SetSiblingIndex(index);
            RectTransform lootRect = lootViewGO.GetComponent<RectTransform>();
           
      
            lootRect.pivot = new Vector2(0.5f, 0.5f);
            lootRect.localScale = Vector3.one;
            if (GameService.Instance.currGameModel.gameState == GameScene.InMapInteraction)
            {
                //  float offset =  parent.GetComponent<RectTransform>().rect.width / 2 + 50f; 
                lootRect.localPosition = new Vector3(0, 0);
            }

            if (GameService.Instance.currGameModel.gameState == GameScene.InQuestRoom)
            {
              //  float offset =  parent.GetComponent<RectTransform>().rect.width / 2 + 50f; 
                lootRect.localPosition = new Vector3(-400,-150);
            }
            if (GameService.Instance.currGameModel.gameState == GameScene.InCombat)
            {
                //  float offset =  parent.GetComponent<RectTransform>().rect.width / 2 + 50f; 
                Canvas canvas = FindObjectOfType<Canvas>();
                Vector2 size = canvas.GetComponent<RectTransform>().sizeDelta; 
                lootRect.localPosition = new Vector3(0, 0);
            }
            LootService.Instance.lootView.InitLootList(itemLS, parent);
            LootService.Instance.On_LootDsplyToggle(true);
        }

        public void InitLootController(LandscapeNames landscapeName)
        {
            if (landscapeName == LandscapeNames.None) return;
            lootBase = LootService.Instance.lootFactory.GetLootBase(landscapeName);        
        }


    }
}
