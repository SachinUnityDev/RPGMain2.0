using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class OverloadedView : MonoBehaviour
    {
        [SerializeField] int overloadQty;
        [SerializeField] OverloadedPtrEvents overloadedPtrEvents;

        private void Awake()
        {
            InvService.Instance.OnInvOverload += InitOnOverLoad; 
        }
        private void OnEnable()
        {            
            InvService.Instance.OnItemRemovedFrmComm -= UpdateTxt;
            InvService.Instance.OnItemRemovedFrmComm += UpdateTxt;
        }
        private void OnDisable()
        {
            InvService.Instance.OnItemRemovedFrmComm -= UpdateTxt;
        }
        void UpdateTxt(Iitems item)
        {
            overloadQty = InvService.Instance.overLoadCount;
            ShowOverloadView();
        }
        public void Init()
        {
            overloadQty = InvService.Instance.overLoadCount;
            ShowOverloadView();
        }
        void InitOnOverLoad(int slotOverload)
        {   
            gameObject.SetActive(true);
            overloadedPtrEvents.Init(this);
        }
        void ShowOverloadView()
        {
            if (overloadQty > 0)
                InitOnOverLoad(overloadQty);
            else
                RemoveOverloadView();
        }
        void RemoveOverloadView()
        {
            gameObject.SetActive(false);    
        }
    }
}