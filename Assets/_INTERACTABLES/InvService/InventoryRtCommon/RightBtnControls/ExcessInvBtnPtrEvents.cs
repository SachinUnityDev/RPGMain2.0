using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Common;



namespace Interactables
{
    public class ExcessInvBtnPtrEvents : MonoBehaviour, IPointerClickHandler, IPanel
    {
        [SerializeField] bool isClicked;
        [SerializeField] GameObject excessInvPanel;
        public void Init()
        {
           
        }

        public void Load()
        {
           UIControlServiceGeneral.Instance.TogglePanel(excessInvPanel, true);
            excessInvPanel.GetComponent<IPanel>().Load();
        }
        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(excessInvPanel, false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (isClicked)
            {
                UnLoad(); 
            }
            else
            {
                Load(); 
            }
            isClicked = !isClicked;
        }

        

        // Start is called before the first frame update
        void Start()
        {
            excessInvPanel = InvService.Instance.excessInvViewController.gameObject; 
            isClicked = false;
            UnLoad();          
        }


    }



}
