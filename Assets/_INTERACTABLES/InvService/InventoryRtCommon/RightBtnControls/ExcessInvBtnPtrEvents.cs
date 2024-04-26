using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Common;
using UnityEngine.UI; 


namespace Interactables
{
    public class ExcessInvBtnPtrEvents : MonoBehaviour, IPointerClickHandler, IPanel, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] bool isClicked;
        [SerializeField] GameObject excessInvPanel;

        [Header(" TBR sprites")]
        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite spriteHL;
        [SerializeField] Image img;
        public void Init()
        {
           
        }

        public void Load()
        {
           UIControlServiceGeneral.Instance.TogglePanel(excessInvPanel, true);
            excessInvPanel.GetComponent<IPanel>().Load();
            img.sprite = spriteN;
        }
        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(excessInvPanel, false);
            img.sprite = spriteN;
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
                InvService.Instance.invMainViewController.ToggleViewFwd(true);
            }
            isClicked = !isClicked;
        }

        

        // Start is called before the first frame update
        void Start()
        {
            img = GetComponent<Image>();
            excessInvPanel = InvService.Instance.excessInvViewController.gameObject; 
            isClicked = false;
            UnLoad();          
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            img.sprite = spriteHL; 
         }

        public void OnPointerExit(PointerEventData eventData)
        {
            img.sprite = spriteN;
        }
    }



}
