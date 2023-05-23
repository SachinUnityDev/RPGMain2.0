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
        public void OnPointerClick(PointerEventData eventData)
        {
            GameObject mapPanel =
                            MapService.Instance.mapIntViewPanel;
            UIControlServiceGeneral.Instance.TogglePanelNCloseOthers(mapPanel, true);
            mapPanel.GetComponent<IPanel>().Init();
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
            heading.alignment = TextAlignmentOptions.Right; 
            heading.text = str;
        }
        void Hidetxt()
        {
            heading.text = "";
        }

        void Start()
        {
            heading = transform.parent.parent.GetChild(1).GetComponent<TextMeshProUGUI>();
        }

    }
}