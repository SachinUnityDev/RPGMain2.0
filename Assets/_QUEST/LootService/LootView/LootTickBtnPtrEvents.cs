using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace Quest
{



    public class LootTickBtnPtrEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {

        LootView lootView;

        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite spriteHL;
        [SerializeField] Sprite spriteDisabled;

        Image img;
        bool isClickable = false; 
     

        public void InitLootTick(LootView lootView)
        {
            img = GetComponent<Image>();
            this.lootView = lootView;
            img.sprite = spriteN;
            isClickable= true;
        }
        public void UpdateTickBtnState(bool isClickable)
        {
            this.isClickable= isClickable;
            if (isClickable)
            {
                img.sprite = spriteN;
            }
            else
            {
                img.sprite = spriteDisabled;
            }
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (isClickable)
                img.sprite = spriteHL; 
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(isClickable) 
                img.sprite = spriteN;    
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(isClickable)
            {
                lootView.AddLoot2Inv();
               
            }
        }
    }
}