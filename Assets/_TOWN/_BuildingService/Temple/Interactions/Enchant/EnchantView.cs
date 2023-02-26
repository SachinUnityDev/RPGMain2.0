using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Town;

public class EnchantView : MonoBehaviour, IPanel
{
    /// <summary>
    /// get details from the weapon panels
    /// get weapon SO... for btns and weapon state update 
    /// 
    /// </summary>
    /// 
    [Header("left right panels")]
    [SerializeField] Button leftBtn;
    [SerializeField] Button rightBtn;
    //public Transform leftCharPanel;
    //public Transform rightGemPanel;

    [SerializeField] Button closeBtn;

    [Header("Center img and Slot")]
    [SerializeField] Transform centerTrans;
    [SerializeField] Transform enchantSlot;


    [Header("btm currency and status update btn")]
    [SerializeField] Transform statusBtn;
    [SerializeField] Transform stashCurrency;


    [Header("Curr Selects")]
    [SerializeField] WeaponSO weaponSO;
    [SerializeField] WeaponModel weaponModel;   
    [SerializeField] CharNames charSelect;

    [Header("char Scroll var")]
    [SerializeField] int index;
    [SerializeField] float prevLeftClick;
    [SerializeField] float prevRightClick;  

    //public List<CharModel> allCharWithWeaponSkill = new List<CharModel>();
     
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
            index = CharService.Instance.allAvailCompModels.Count - 1;
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
        if (index == CharService.Instance.allAvailCompModels.Count - 1)
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

    void FillCharPlanks()
    {
        CharNames selectChar = CharService.Instance.allAvailCompModels[index].charName;
        BuildingIntService.Instance.selectChar = selectChar; 

        CharController charController = CharService.Instance.GetCharCtrlWithName(selectChar);
        WeaponController weaponController = charController.weaponController;
        WeaponModel weaponModel = weaponController.weaponModel;

        centerTrans.GetComponent<EnchantWeaponView>().InitWeaponPanel(selectChar, weaponModel, this);      
        statusBtn.GetComponent<EnchantStatusBtnPtrEvents>().InitBtnEvents(selectChar, weaponModel);
       
    }

    void closeBtnPressed() 
    {
        UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);    
    }

    public void Init()
    {
        index = 0;
        FillCharPlanks();
        FillStashMoney();

    }

    public void Load()
    {
       Init();
        
    }
    void FillStashMoney()
    {
        Currency curr = EcoServices.Instance.GetMoneyAmtInPlayerStash().DeepClone(); 
             
        stashCurrency.GetComponent<DisplayCurrency>().Display(curr); 
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
