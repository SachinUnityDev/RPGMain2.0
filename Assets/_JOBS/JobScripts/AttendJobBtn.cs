using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace Town
{


    public class AttendJobBtn : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler  
    {
        [Header("Sprites")]

        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite spriteHL;
        [SerializeField] Sprite spriteClicked;

        [Header("Global Var")]
        Image img;
        JobView jobView; 


        private void OnEnable()
        {
            img= GetComponent<Image>();
            img.sprite = spriteN;   
        }
        
        public void Init(JobView jobView)
        {
            this.jobView= jobView;
            img.sprite = spriteN;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            img.sprite = spriteClicked;
            JobService.Instance.StartJob(JobNames.Woodcutter);
            jobView.GetComponent<IPanel>().UnLoad();
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