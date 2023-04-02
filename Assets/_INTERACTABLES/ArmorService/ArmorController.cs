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
        CharController charController;
        private void Start()
        {
            charController = GetComponent<CharController>();
            armorModel = new ArmorModel(ArmorService.Instance.allArmorSO
                            .GetArmorSOWithCharName(charController.charModel.charName));
        }
    }
}