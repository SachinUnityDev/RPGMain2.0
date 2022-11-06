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
        [SerializeField] RaceBtnAnim raceBtnAnim;
        [SerializeField] TextMeshProUGUI raceText; 
        Image img;

        public bool isRight = false;
        private void Start()
        {
           
        }
        public void Init(RaceSpriteData raceData, RaceBtnAnim raceBtnAnim, bool isRight )
        {
            if (raceData == null) return;
            img = transform.GetChild(0).GetComponent<Image>();
            raceText =
                transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            raceText.text = raceData.raceType.ToString();
            spriteN = raceData.raceSpriteN;
            SpriteLit = raceData.raceSpriteLit;

           raceType = raceData.raceType;
           this.isRight = isRight;
           img.sprite = spriteN;
           this.raceBtnAnim = raceBtnAnim; 
        }
        public void OnPointerClick(PointerEventData eventData)
        {            
            img.sprite = SpriteLit;              
            raceBtnAnim.RaceSelected(raceType, gameObject, isRight);               
           
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(img != null)
                img.sprite = SpriteLit;
            if(raceBtnAnim.isSpread)
                raceText.gameObject.SetActive(true);

        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (img != null)
                img.sprite = spriteN;

            raceText.gameObject.SetActive(false);

        }
    }



}
