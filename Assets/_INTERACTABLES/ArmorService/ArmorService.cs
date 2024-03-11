using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Town; 
namespace Interactables
{
    public class ArmorService : MonoSingletonGeneric<ArmorService>
    {
        // socket rules two divine and one support 
        // Btn socket for the support gem
        
        public AllArmorSO allArmorSO;

        [Header("Not TBR")]
        public ArmorModel armorModel;
        public List<ArmorModel> allArmorModels = new List<ArmorModel>();
        public List<ArmorController> allArmorController = new List<ArmorController>();
        public List<ArmorBase> allArmorBases = new List<ArmorBase>();
        // public GameObject armorPanel;
        ArmorFactory armorFactory; 
        public ArmorViewController armorViewController;

        public void Init()
        {
            // Add armor to all allies 
            armorFactory = GetComponent<ArmorFactory>();
            foreach (CharController charController in CharService.Instance.allyInPlayControllers)
            {
                ArmorController armorController =  charController.gameObject.AddComponent<ArmorController>(); 
                armorController.Init(); 
                allArmorController.Add(armorController);    
            }
        }
        public ArmorBase GetNewArmorBase(ArmorModel armorModel)
        {
            ArmorType armorType = armorModel.armorType;
            ArmorBase armorBase = armorFactory.GetArmorBase(armorType);         
            return armorBase;
        }

        public void OnArmorFortifyPressed(CharController charController,ArmorModel armorModel)
        {
            ArmorBase armorBase = charController.armorController.armorBase;
            armorBase.OnArmorFortify(); 
            
        }
   
        private void Start()
        {
            

        }

#region Button Controls

        public void OpenArmorPanel()
        {  
            armorViewController = InvService.Instance.invXLGO.GetComponentInChildren<ArmorViewController>();
            armorViewController.GetComponent<IPanel>().Load();
        }

        public void CloseArmorPanel()
        {
            armorViewController.GetComponent<IPanel>().UnLoad();
        }
        #endregion
        public ArmorModel GetArmorModel(CharNames charName)
        {
            int index = allArmorModels.FindIndex(t => t.charName == charName);
            if (index != -1)
            {
                return allArmorModels[index];
            }
            else
            {
                Debug.Log("armor model not found " + charName);
                return null;
            }
        }
        public bool IsArmorSocketable(CharController charController, GemNames gemName)
        {
            CharModel charModel = charController.charModel; 
                // CharService.Instance.GetAllyCharModel(charController.charModel.charName);

            return false; 
        }

        public bool SocketArmor(GemNames gemName)
        {
            //   CharNames charName = InvService.Instance.charSelect;
            CharController charController = InvService.Instance.charSelectController; 
            if (IsArmorSocketable(charController, gemName))
            {
                // get gembase enchant weapon 
                // Unlock the weapon skill
                return true;
            }
            else
            {
                // error message
                return false;
            }
        }

    }

}
