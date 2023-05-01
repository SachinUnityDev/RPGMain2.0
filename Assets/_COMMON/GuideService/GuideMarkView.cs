using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

namespace Common
{    
    public class GuideMarkView : MonoBehaviour, IPointerEnterHandler
    {
        public GuideMarkLoc guideMarkLoc;
        GuideViewController guideView;

        Transform orgParent;
        public bool isHovered; 


        private void Awake()
        {
            orgParent= transform;
        }
        public void InitGuideView(GuideViewController guideView)
        {
            this.guideView= guideView;
        }
        public void Load()
        {
            isHovered= false;
            gameObject.SetActive(true);
            transform.SetParent(guideView.gameObject.transform);
            transform.SetAsLastSibling(); 
        }
        public void UnLoad()
        {
            isHovered = true;
            gameObject.SetActive(false);
            transform.SetParent(orgParent);
            transform.SetAsLastSibling();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            UnLoad();
        }
    }
}