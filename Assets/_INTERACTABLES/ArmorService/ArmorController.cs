using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorController : MonoBehaviour
{
    public ArmorModel armorModel;
    CharController charController;
    private void Awake()
    {
        charController = GetComponent<CharController>();
        armorModel = new ArmorModel(ArmorService.Instance.allArmorSO
                        .GetArmorSOWithCharName(charController.charModel.charName));
    }
}
