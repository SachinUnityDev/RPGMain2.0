using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;


namespace Interactables
{
    public class AttribPanelViewComp : MonoBehaviour
    {
        [Header("TO BE REF.")]
        [SerializeField] Transform leftPanel;
        [SerializeField] Transform rightPanel;

        BtmCharViewController btmCharViewController;

        void OnEnable()
        {
            btmCharViewController =
               transform.parent.parent
                               .GetChild(2).GetComponent<BtmCharViewController>();


        }
        
        public void ToggleLRPanel(bool isONLeft)
        {
            if (isONLeft)
            {
                rightPanel.SetSiblingIndex(0);
                leftPanel.SetSiblingIndex(1);
            }
            else
            {
                leftPanel.SetSiblingIndex(0);
                rightPanel.SetSiblingIndex(1);
            }

        }

        public void PopulateAttribPanel()
        {
            AttribPtrEventsComp[] attributes = gameObject
                                            .GetComponentsInChildren<AttribPtrEventsComp>();

            foreach (AttribPtrEventsComp attribute in attributes)
            {
                CharModel charModel = btmCharViewController.charSelect; 
                attribute.PopulateData(charModel);
            }


        }




    }
}

