using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
namespace Common
{
    public class UPonHoverTextHL : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        [SerializeField] Color HLColor;
        [SerializeField] Color startColor;
        [SerializeField] string Line = "";
        [SerializeField] string Line1 = "";
        [SerializeField] string Line2 = "";
        [SerializeField] string Line3 = "";


        public void OnPointerEnter(PointerEventData eventData)
        {
            gameObject.transform.GetComponent<TextMeshProUGUI>().DOColor(HLColor, 0.25f);
            gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().DOFade(1.0f, 0.4f);

        }

        public void OnPointerExit(PointerEventData eventData)
        {
            gameObject.transform.GetComponent<TextMeshProUGUI>().color = startColor;
            gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().DOFade(0.0f, 0.4f);

        }

        // Start is called before the first frame update
        void Awake()
        {
            startColor = gameObject.transform.GetComponent<TextMeshProUGUI>().color;
            gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().DOFade(0.0f, 0.01f);
            gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text
                = Line + "\n" + Line1 + "\n\n"+ Line2 + "\n" + Line3; 
        }


    }



}