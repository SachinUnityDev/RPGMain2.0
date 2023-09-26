using Common;
using Interactables;
using TMPro;
using Town;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Quest
{
    public class MapBtnPtrEvents : MonoBehaviour, IPointerClickHandler
                                                , IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] TextMeshProUGUI heading;
        [SerializeField] GameObject mapGO; 

        public void OnPointerClick(PointerEventData eventData)
        {
           
            mapGO.GetComponent<IPanel>().Load(); 
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ShowTxt();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Hidetxt();
        }

        void ShowTxt()
        {
            string str = "Map";
           // heading.alignment = TextAlignmentOptions.Right; 
            heading.text = str;
        }
        void Hidetxt()
        {
            heading.text = "";
        }

        void OnEnable()
        {
            heading = transform.parent.parent.GetChild(1).GetComponent<TextMeshProUGUI>();
            heading.text = "";
            mapGO = FindObjectOfType<QRoomMapView>(true).gameObject;
        }

    


    }
}