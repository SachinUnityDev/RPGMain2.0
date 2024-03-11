using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{
    public class ArmorController : MonoBehaviour
    {
        public ArmorModel armorModel;
        public ArmorBase armorBase; 
        CharController charController;
        private void Start()
        {
                        
        }

        public void Init()
        {
            charController = GetComponent<CharController>();
            CharNames charName = charController.charModel.charName; 
            armorModel = new ArmorModel(ArmorService.Instance.allArmorSO
                                        .GetArmorSOWithCharName(charName));
            armorModel.charName = charName;
            charController.armorController = this;
            armorBase = ArmorService.Instance.GetNewArmorBase(armorModel);
            armorBase.InitArmor(charController, armorModel); 
            ArmorService.Instance.allArmorBases.Add(armorBase); 
        }



    }
}