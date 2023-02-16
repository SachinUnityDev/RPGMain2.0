using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;
using Town;

public class EnchantView : MonoBehaviour, IPanel
{
    /// <summary>
    /// get details from the weapon panels
    /// get weapon SO... for btns and weapon state update 
    /// 
    /// </summary>

    [SerializeField] WeaponSO weaponSO;
    [SerializeField] WeaponModel weaponModel;   
    [SerializeField] CharNames charSelect; 
    public void Init()
    {


    }

    public void Load()
    {
        //FillSlots();
        //FillStashMoney();
    }

    public void UnLoad()
    {
        
    }

    void Populate()
    {

        charSelect = BuildingIntService.Instance.selectChar;
        weaponSO = WeaponService.Instance.allWeaponSO.GetWeaponSO(charSelect);
        weaponModel = WeaponService.Instance.GetWeaponModel(charSelect);
        // get char sprite
        // get weapon state


    }

    
}
