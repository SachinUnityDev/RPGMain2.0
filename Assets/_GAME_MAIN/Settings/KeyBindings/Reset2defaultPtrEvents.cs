using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace Common
{


    public class Reset2defaultPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite spriteHL;
        [SerializeField] Sprite spriteClick;

        [SerializeField] Image img;
        [SerializeField] KeyBindingsController keyBindingsController;
        public void OnPointerClick(PointerEventData eventData)
        {
            img.sprite = spriteN;

            keyBindingsController.keyBindingSO.ResetTodefault();
            keyBindingsController.FillCurrentKeys();
        }
        public void Init(KeyBindingsController keyBindingController)
        {
            this.keyBindingsController = keyBindingController;
            if (keyBindingController.keyBindingSO.AreKeysDiffFrmDefault())            
                gameObject.SetActive(true);
            else
                gameObject.SetActive(false);
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            img.sprite = spriteHL;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            img.sprite = spriteN;
        }

        void Start()
        {
            img.sprite = spriteN;
        }
    }
}