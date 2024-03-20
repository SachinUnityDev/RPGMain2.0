using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Town;
using System.Linq;
using TMPro;

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

    [Header("List of Avail char")]
    List<CharController> availChars = new List<CharController>();

    [Header("Enchantment txt")]
    [SerializeField] TextMeshProUGUI noCharAvailTxt; 
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
    void ToggleDsply(bool isNotEmpty)
    {
    
        leftBtn.gameObject.SetActive(isNotEmpty);
        rightBtn.gameObject.SetActive(isNotEmpty);
        closeBtn.gameObject.SetActive(!isNotEmpty);

        centerTrans.gameObject.SetActive(isNotEmpty);
        enchantSlot.gameObject.SetActive(isNotEmpty); 
        statusBtn.gameObject.SetActive(isNotEmpty);
        currency.gameObject.SetActive (isNotEmpty);

        noCharAvailTxt.gameObject.SetActive(!isNotEmpty);
    }
    public void FillCharPlanks()
    {
        availChars.Clear();
        availChars = CharService.Instance.allyInPlayControllers.Where(t => (t.charModel.availOfChar == AvailOfChar.Available ||
                               t.charModel.availOfChar == AvailOfChar.UnAvailable_InParty ||
                               t.charModel.availOfChar == AvailOfChar.UnAvailable_Prereq)
                               && t.charModel.charLvl > 1 && t.charModel.stateOfChar == StateOfChar.UnLocked 
                               ).ToList();

        if (availChars.Count == 0)
        {
            ToggleDsply(false);
            return;
        }
        else
        {
            ToggleDsply(true);
        }

        if (index >= availChars.Count)
        {
            index = 0;
        }


        CharNames selectChar = availChars[index].charModel.charName;
        BuildingIntService.Instance.selectChar = selectChar;

        CharController charController = availChars[index]; 
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
}
