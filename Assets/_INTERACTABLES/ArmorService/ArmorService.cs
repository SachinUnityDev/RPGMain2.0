using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Interactables
{
    public class ArmorService : MonoSingletonGeneric<ArmorService>
    {
        // socket rules two divine and one support 
        // Btn socket for the support gem
        
       
        
        
        
        
        public ArmorSO armorSO; 
        public ArmorModel armorModel;
        public GameObject armorPanel;

      
        public ArmorViewController armorViewController;
        [Header("Not TBR")]
        public ArmorController armorController;
        public void Init()
        {
                        
        }
        private void Start()
        {
            armorViewController = armorPanel.GetComponent<ArmorViewController>();
          //  armorController = GetComponent<ArmorController>();
        }

#region Button Controls

        public void OpenArmorPanel()
        {            
            armorViewController.GetComponent<IPanel>().Load(); 
        }

        public void CloseArmorPanel()
        {
            armorViewController.GetComponent<IPanel>().UnLoad();

        }
#endregion


    }

}
