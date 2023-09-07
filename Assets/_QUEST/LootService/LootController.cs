using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{

    public class LootController : MonoBehaviour
    {

        public LootBase lootBase;     

        [Header("Loot Prefab TBR")]
        public GameObject lootPrefab;

        [Header(" loot view GO")]
        [SerializeField] GameObject lootViewGO; 
        void Start()
        {

        }
        public void ShowLootTable(List<ItemType> allItemType, Transform parentTrans)
        {
            LandscapeNames landscapeNames = LandscapeNames.Sewers;
            lootBase = LootService.Instance.lootFactory.GetLootBase(landscapeNames);
            List<ItemDataWithQty> itemLS = lootBase.GetLootList(allItemType);

            InitLootView(itemLS, parentTrans); 
        }

        void InitLootView(List<ItemDataWithQty> itemLS, Transform parent)
        {

            if (LootService.Instance.isLootDsplyed) return; // return multiple clicks
            lootViewGO = Instantiate(lootPrefab);

            LootService.Instance.lootView = lootViewGO.GetComponent<LootView>();
            lootViewGO.transform.SetParent(parent);

            //UIControlServiceGeneral.Instance.SetMaxSiblingIndex(diaGO);
            int index = lootViewGO.transform.parent.childCount - 2;
            lootViewGO.transform.SetSiblingIndex(index);
            RectTransform lootRect = lootViewGO.GetComponent<RectTransform>();

            lootRect.anchorMin = new Vector2(0, 0);
            lootRect.anchorMax = new Vector2(1, 1);
            lootRect.pivot = new Vector2(0.5f, 0.5f);
            lootRect.localScale = Vector3.one;
            lootRect.offsetMin = new Vector2(0, 0); // new Vector2(left, bottom);
            lootRect.offsetMax = new Vector2(0, 0); // new Vector2(-right, -top);
            

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
