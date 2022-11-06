using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
using Common;
using System.Linq;
using UnityEngine.UI;


namespace Interactables
{
    public class CharPortraitPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] CharModel charModel;
        [SerializeField] Image charPort;
        [SerializeField] Image expBar;
        [SerializeField] TextMeshProUGUI classTxt;
        [SerializeField] Image raceImg;
        [SerializeField] TextMeshProUGUI lvlTxt;

        [SerializeField] BtmCharViewController btmCharViewController;
        [SerializeField] bool isRight;
        bool isClicked = false;
        int lastSibIndex; 
        public void Init(CharModel charModel, BtmCharViewController btmCharViewController, bool isRightPanel)
        {
            this.charModel = charModel;
            charPort = transform.GetChild(0).GetComponent<Image>();
            expBar = transform.GetChild(2).GetComponent<Image>();
            classTxt = transform.GetChild(3).GetComponentInChildren<TextMeshProUGUI>();
            raceImg = transform.GetChild(4).GetChild(0).GetComponent<Image>();
            lvlTxt = transform.GetChild(5).GetChild(0).GetComponent<TextMeshProUGUI>();
            CharacterSO charSO = 
                        CharService.Instance.GetAllySO(charModel.charName);

            charPort.sprite = charSO.bpPortraitUnLocked;
            expBar.fillAmount = charModel.GetDeltaRatio();
            classTxt.text = charModel.classType.ToString().CreateSpace();
            raceImg.sprite = CharService.Instance.charComplimentarySO
                                .GetRaceSpriteData(charModel.raceType).raceSpriteN;

            lvlTxt.text = charModel.charLvl.ToString();

            this.btmCharViewController = btmCharViewController;
            isRight = isRightPanel;
            Reset();
        }
        public void Reset()
        {
            isClicked = false;
        }
        public void SetPanel(bool isRight)
        {
            this.isRight = isRight; 
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            isClicked = true;
            btmCharViewController.CharSelected(charModel, gameObject, isRight);
            Debug.Log("CHAR CLICKED" + charModel.charName);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
           // transform.SetAsLastSibling(); 
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            //if(isClicked)
            //    transform.SetSiblingIndex(lastSibIndex);
        }

      
        void Start()
        {

        }


    }



}

