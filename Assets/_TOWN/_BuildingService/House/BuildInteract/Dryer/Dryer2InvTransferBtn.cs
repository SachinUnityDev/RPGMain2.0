using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
namespace Town
{
    public class Dryer2InvTransferBtn : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite spriteHL;
        [SerializeField] Sprite spriteNA;
        [SerializeField] TextMeshProUGUI onHoverTxt;

        [SerializeField] Image img;

        [SerializeField] bool isClickable;
        DryerView dryerView;
        HouseModel houseModel; 
        public void Init(DryerView dryerView)
        {
            this.dryerView = dryerView;
            houseModel = BuildingIntService.Instance.houseController.houseModel;
            SetState(); 
            onHoverTxt.gameObject.SetActive(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(isClickable)
            {
                dryerView.OnTransfer2InvPressed();
                SetState();
                onHoverTxt.gameObject.SetActive(false);
            }
        }
        void SetState()
        {
            if(houseModel.itemDried.Count> 0)
            {
                isClickable= true;
                img.sprite = spriteN;
            }
            else
            {
                isClickable= false;
                img.sprite = spriteNA;
            }
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (isClickable)
            {
                onHoverTxt.gameObject.SetActive(true);
                img.sprite = spriteHL;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (isClickable)
            {
                onHoverTxt.gameObject.SetActive(false);
                img.sprite = spriteN;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            img = GetComponent<Image>();
        }

        
    }
}