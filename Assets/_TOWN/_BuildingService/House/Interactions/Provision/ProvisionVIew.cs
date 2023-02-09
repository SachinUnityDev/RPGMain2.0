using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;
using UnityEngine.UI;
using DG.Tweening; 
namespace Town
{
    public class ProvisionView : MonoBehaviour
    {
        public int selectIndex;
        public PotionNames potionName; 
        [SerializeField] Button tickBtn;
        [SerializeField] Transform optContainer;
        [SerializeField] Transform arrowTrans; 

        private void Awake()
        {
            tickBtn.onClick.AddListener(OnAdd2ProvisionSlot);                 
        }
        public void OnSelect(PotionNames _potionName, int index)
        {
            foreach (Transform child in optContainer)
            {
                ProvisionOptionsPtrEvents opts = child.GetComponent<ProvisionOptionsPtrEvents>(); 
                if(opts.potionName != _potionName)
                {
                    opts.OnDeSelect(true);
                }                
            }
            selectIndex = index; 
            potionName = _potionName;
            MoveArrow();
        }

        void MoveArrow()
        {
            Vector3 pos = 
            optContainer.GetChild(selectIndex).GetComponent<RectTransform>().position;
            arrowTrans.DOMoveY(pos.y, 0.1f); 
        }

        public void OnAdd2ProvisionSlot()
        {
            //add to abbas provision slot here
        }

    }
}