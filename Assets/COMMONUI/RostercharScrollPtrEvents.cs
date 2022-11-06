using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
namespace Common
{
    public class RostercharScrollPtrEvents : MonoBehaviour,  IPointerExitHandler, IPointerEnterHandler
    {
        [SerializeField] Sprite spriteHL;
        [SerializeField] Sprite spriteN;

        [SerializeField] Image img;
        [SerializeField]int index;
        string str;
        [SerializeField] GameObject toolTipPlank; 

        private void Start()
        {
            img = gameObject.GetComponent<Image>();
            img.sprite = spriteN;
            index = transform.parent.GetSiblingIndex();
            switch (index)
            {
                case 0: 
                    str = CharService.Instance.charComplimentarySO.PreReqToolTip;
                    break;
                case 1:
                    str = CharService.Instance.charComplimentarySO.ProvisionToolTip;
                    break;
                case 2:
                    str = CharService.Instance.charComplimentarySO.earningsToolTip;
                    break;
                default:
                    break;
            }           
            toolTipPlank.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            toolTipPlank.SetActive(true);
            toolTipPlank.GetComponentInChildren<TextMeshProUGUI>().text = str;
            img.sprite = spriteHL; 
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            toolTipPlank.SetActive(false);
            img.sprite = spriteN; 
        }

        
    }


}


