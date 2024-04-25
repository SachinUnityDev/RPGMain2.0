using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Common;
using TMPro;


namespace Interactables
{
    public class RaceBtnCompPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite SpriteLit;
        [SerializeField] RaceType raceType;    
        Image img;

        [SerializeField] CharComplimentarySO charComplimentarySO;

        CompanionViewController companionViewController; 
        private void Start()
        {
            img = transform.GetChild(0).GetComponent<Image>();
        }
        public void Init(RaceType raceType, CompanionViewController companionViewController)
        {
            
            img = transform.GetChild(0).GetComponent<Image>();

            this.raceType = raceType;     
            this.companionViewController= companionViewController;
            RaceSpriteData raceData = charComplimentarySO.GetRaceSpriteData(raceType);


            spriteN = raceData.raceSpriteN;
            SpriteLit = raceData.raceSpriteLit;

           raceType = raceData.raceType;
           
           img.sprite = spriteN;       
        }
        public void OnPointerClick(PointerEventData eventData)
        {            
            img.sprite = SpriteLit;
            companionViewController.OnRaceBtnPressed(); 
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(img != null)
                img.sprite = SpriteLit;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (img != null)
                img.sprite = spriteN;
        }
    }



}
