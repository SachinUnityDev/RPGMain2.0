using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Common
{
    public class LoadSlotBtnPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Editor set for references")]
        public SaveSlot saveSlot;

        [Header(" Sprites")]
        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite spriteHL;
        [SerializeField] Sprite spriteClick;
        [SerializeField] Image bgImg; 

        LoadSlotsView LoadSlotsView;
        LoadView loadView;
        GameModel gameModel; 
        public void Init(GameModel gameModel, LoadSlotsView loadSlotsView, LoadView loadView)
        {
            this.loadView = loadView;
            this.LoadSlotsView = loadSlotsView;
            this.gameModel = gameModel;
            bgImg.sprite = spriteN;
            
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            bgImg.sprite = spriteClick;
            loadView.OnLoadSlotBtnClicked(gameModel);    
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            bgImg.sprite = spriteHL;    
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            bgImg.sprite = spriteN;
        }
    }
}