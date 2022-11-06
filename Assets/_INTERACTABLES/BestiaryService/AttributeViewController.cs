using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Interactables
{
    public class AttributeViewController : MonoBehaviour
    {

        
        [Header("TO BE REF.")]
        [SerializeField] Transform leftPanel;
        [SerializeField] Transform rightPanel; 
        void Start()
        {
            
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
            AttributePtrEvents[] attributes = gameObject
                                            .GetComponentsInChildren<AttributePtrEvents>();

            foreach (AttributePtrEvents attribute in attributes)
            {

                attribute.PopulateData(); 
            }


        }

   

    }

}

