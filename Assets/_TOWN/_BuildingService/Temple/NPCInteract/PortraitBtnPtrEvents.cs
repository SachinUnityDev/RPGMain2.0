using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace Town
{
    public class PortraitBtnPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("NTBR")]
        [SerializeField] Button talkBtn;
        [SerializeField] Button tradeBtn;   
        TempleViewController templeView;
        [SerializeField] CharNames charName;
        [SerializeField] NPCNames npcNames;
        [SerializeField] bool isShown; 
        private void Awake()
        {                
            isShown= false;
        }

        public void InitPortraitPtrEvents(TempleViewController  templeView, CharNames charName, NPCNames npcName)
        {
            this.templeView = templeView;
            this.charName= charName;
            this.npcNames= npcName;

            // init talk view and trade view 

        }
        void ShowBtn()
        {
            talkBtn.transform.DOLocalMoveX(125, 0.1f);
            tradeBtn.transform.DOLocalMoveX(125, 0.1f);
            isShown= true;
        }
        void HideBtn()
        {            
            talkBtn.transform.DOLocalMoveX(0, 0.05f);
            tradeBtn.transform.DOLocalMoveX(0, 0.05f);
           
        }
        public void OnPointerClick(PointerEventData eventData)
        {
           
        }
        IEnumerator WaitForSec()
        {
            yield return new WaitForSeconds(0.25f);
            if(!isShown)
             HideBtn();
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            ShowBtn();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isShown = false;
            StartCoroutine(WaitForSec());
        }
    }
}