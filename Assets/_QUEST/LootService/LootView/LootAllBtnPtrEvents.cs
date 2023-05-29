using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
namespace Quest
{
    public class LootAllBtnPtrEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite spriteHL;
        [SerializeField] Image img;

        [SerializeField] bool isClicked;

        LootView lootView; 
        private void Awake()
        {
            img = transform.GetComponent<Image>();
            

        }
        private void Start()
        {
            img.sprite = spriteN;
            isClicked = false;
        }

        public void InitLootAllBtn(LootView lootView)
        {
            this.lootView= lootView;
            // show all selected
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!isClicked || !lootView.IsAllSelected())
            {
                lootView.OnLootAllSelected();
                isClicked = true;
            }         
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            img.sprite = spriteHL;

        }

        public void OnPointerExit(PointerEventData eventData)
        {            
            img.sprite = spriteN;
        }
    }
}