using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Common;
using TMPro;


namespace Interactables
{
    public class RaceBtnBeastPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {

        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite SpriteLit;
        [SerializeField] RaceType raceType;
        [SerializeField] TextMeshProUGUI raceTxt;
        Image Img; 


        public bool isClicked = false; 
        public void Init(RaceSpriteData raceData)
        {
            if (raceData == null) return;
            spriteN = raceData.raceSpriteN;
            SpriteLit = raceData.raceSpriteLit;
            raceType = raceData.raceType;
            Img.sprite = spriteN;
            raceTxt = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            raceTxt.gameObject.SetActive(false);
            raceTxt.text = raceType.ToString();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            
            isClicked = !isClicked;
            if (isClicked)
            {
                RemoveOtherClicked();
                Img.sprite = SpriteLit;
                BestiaryService.Instance.OnRaceSelect(raceType);
            }
            else
            {
                DeSelect();
                BestiaryService.Instance.OnRaceSelect(RaceType.None);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Img.sprite = SpriteLit;
            raceTxt.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!isClicked)
            {
                raceTxt.gameObject.SetActive(false);
                Img.sprite = spriteN;
            }
        }
        
        public void DeSelect()
        {
            if (isClicked)
            {
                isClicked = false;
                Img.sprite = spriteN;
                raceTxt.gameObject.SetActive(false);
            }
        }
        void RemoveOtherClicked()
        {
            foreach (Transform child in transform.parent)
            {
                if( child.name != transform.name)
                    child.GetComponent<RaceBtnBeastPtrEvents>().DeSelect();  
            }
            BestiaryService.Instance.OnRaceSelect(RaceType.None);
        }

        void Start()
        {
            Img = gameObject.GetComponent<Image>(); 
        }

    }
}

