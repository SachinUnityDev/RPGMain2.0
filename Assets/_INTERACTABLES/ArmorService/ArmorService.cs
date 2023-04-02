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
        public GameObject armorPanel;

        public ArmorViewController armorViewController;
        public void Init()
        {
                        
        }
        public void OnArmorFortifyPressed(CharNames charSelect,ArmorModel armorModel)
        {
            //ArmorBase armorBase = Get


            // check if it can be fortified
            // create armor base if not already there
            // 


        }
        public bool CanArmorBeFortified(CharNames charSelect, ArmorModel armorModel)
        {


            return false; 
        }
        private void Start()
        {
            armorViewController = armorPanel.GetComponent<ArmorViewController>();      

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
        public bool IsArmorSocketable(CharNames charName, GemNames gemName)
        {
            CharModel charModel = CharService.Instance.GetAllyCharModel(charName);

            return false; 
        }

        public bool SocketArmor(GemNames gemName)
        {
            CharNames charName = InvService.Instance.charSelect;
            if (IsArmorSocketable(charName, gemName))
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
