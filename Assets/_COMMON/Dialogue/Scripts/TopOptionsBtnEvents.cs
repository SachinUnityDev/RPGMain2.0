using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Common
{
    public class TopOptionsBtnEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        DialogueView dialogueViewController1;
        int index;

        Image img; 

      [SerializeField]SpriteOpts spriteOpts; 

        private void OnEnable()
        {
            index = gameObject.transform.GetSiblingIndex();
            dialogueViewController1 = DialogueService.Instance.dialogueView;
            img = GetComponent<Image>();
        }

        public void Init(SpriteOpts spriteOpts)
        {
            img = GetComponent<Image>(); 
            this.spriteOpts = spriteOpts;
            if (spriteOpts.isUnlocked)
            {
                img.sprite = spriteOpts.spriteN;
            }
            else
            {
                img.sprite = spriteOpts.spriteNA;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            dialogueViewController1.OnChoicePtrEnter(index);
            if (spriteOpts.isUnlocked)
            {
                img.sprite = spriteOpts.spriteHL;
            }
            else
            {
                img.sprite = spriteOpts.spriteNA;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            dialogueViewController1.OnChoicePtrExit(index);
            if (spriteOpts.isUnlocked)
            {
                img.sprite = spriteOpts.spriteN;
            }
            else
            {
                img.sprite = spriteOpts.spriteNA;
            }
        }
    }

}

