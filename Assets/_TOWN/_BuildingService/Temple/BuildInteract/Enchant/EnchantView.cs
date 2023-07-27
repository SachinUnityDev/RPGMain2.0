using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Town;
using System.Linq;

public class EnchantView : MonoBehaviour, IPanel
{
    [Header("left right panels")]
    [SerializeField] Button leftBtn;
    [SerializeField] Button rightBtn;

    [SerializeField] Button closeBtn;

    [Header("Center img and Slot")]
    [SerializeField] Transform centerTrans;
    [SerializeField] Transform enchantSlot;

    [Header("btm currency and status update btn")]
    [SerializeField] Transform statusBtn;
    [SerializeField] DisplayCurrencyWithToggle currency;

    [Header("Curr Selects")]
    [SerializeField] CharNames charSelect;

    [Header("char Scroll var")]
    [SerializeField] int index;
    [SerializeField] float prevLeftClick;
    [SerializeField] float prevRightClick;
    [SerializeField] List<CharModel> charModelInTown = new List<CharModel>();
    private void Start()
    {
        leftBtn.onClick.AddListener(OnLeftBtnPressed); 
        rightBtn.onClick.AddListener(OnRightBtnPressed);
        closeBtn.onClick.AddListener(closeBtnPressed);        
    }

    void OnLeftBtnPressed()
    {
        if (Time.time - prevLeftClick < 0.3f) return;   
        if (index == 0)
        {
            index = CharService.Instance.allCharModels.Count - 1;
            FillCharPlanks(); 
        }
        else if(index > 0)
        {
            --index; FillCharPlanks();
        }
        prevLeftClick = Time.time;
    }
    void OnRightBtnPressed()
    {
        if (Time.time - prevRightClick < 0.3f) return;
        if (index == CharService.Instance.allCharModels.Count - 1)
        {
            index = 0;
            FillCharPlanks();
        }
        else
        {
            ++index; FillCharPlanks();
        }
        prevRightClick = Time.time;
    }

    public void FillCharPlanks()
    {
        if(charModelInTown.Count == 0) return;    
        CharNames selectChar = charModelInTown[index].charName;
        BuildingIntService.Instance.selectChar = selectChar; 

        CharController charController = CharService.Instance.GetCharCtrlWithName(selectChar);
        WeaponController weaponController = charController.weaponController;
        WeaponModel weaponModel = weaponController.weaponModel;

        centerTrans.GetComponent<EnchantWeaponView>().InitWeaponPanel(selectChar, weaponModel, this);      
        statusBtn.GetComponent<EnchantStatusBtnPtrEvents>().InitBtnEvents(selectChar, weaponModel, this);
        currency.FillMoney();
    }

    void closeBtnPressed() 
    {
        UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);    
    }

    public void Init()
    {
        index = 0;
        
    }

    public void Load()
    {
        index = 0;
        charModelInTown = CharService.Instance.allCharModels
                        .Where(t=>t.currCharLoc  == LocationName.Nekkisari).ToList();
        currency.InitCurrencyToggle();
        FillCharPlanks();
    }
    
   
    public void DebitMoney(Currency curr)
    {
        currency.DisplayCurrency(curr); 
    }
    public void UnLoad()
    {
        UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);    
    }




    //void Populate()
    //{

    //    charSelect = BuildingIntService.Instance.selectChar;
    //    weaponSO = WeaponService.Instance.allWeaponSO.GetWeaponSO(charSelect);
    //    weaponModel = WeaponService.Instance.GetWeaponModel(charSelect);
    //    // get char sprite
    //    // get weapon state


    //}

    
}
