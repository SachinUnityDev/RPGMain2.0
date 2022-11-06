using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum DayBtnState
{
    Blankday, 
    Passedday, 
    Currentday, 
    Upcomingday, 
}

public class DayBtnController: MonoBehaviour    // Calendar UI Day Btns controller
{

    public DayBtnState dayBtnState;
    [SerializeField] Sprite blankDaySprite; 
    [SerializeField]Sprite passedDaySprite;
    [SerializeField] Sprite upcomingDaySprite;
    [SerializeField] Sprite currentDaySprite;
    Image btnImg;
    // Start is called before the first frame update
    void Start()
    {
        dayBtnState = DayBtnState.Upcomingday;
        btnImg = GetComponent<Image>(); 
       
    }

    public void SetState(DayBtnState _dayBtnState, int _dayNo)
    {
        dayBtnState = _dayBtnState;
       // Debug.Log("STATE SET " + dayBtnState); 
        TextMeshProUGUI txt = gameObject.GetComponentInChildren<TextMeshProUGUI>(true);
        txt.text = _dayNo.ToString();
        if (dayBtnState == DayBtnState.Blankday)
            txt.text = ""; 
        ApplyBtnImage(dayBtnState);            

    }

     void ApplyBtnImage(DayBtnState daybtnState)
     {
        switch (daybtnState)
        {
            case DayBtnState.Upcomingday: btnImg.sprite = upcomingDaySprite; SetAlpha(1f); break;
            case DayBtnState.Currentday: btnImg.sprite = currentDaySprite; SetAlpha(1f); break;           
            case DayBtnState.Passedday: btnImg.sprite = passedDaySprite; SetAlpha(1f); break;
            case DayBtnState.Blankday: SetAlpha(0f); break;
            default: break; 
        }
     }

     void SetAlpha(float alpha)
     {
        var tempColor = btnImg.color;
        tempColor.a = alpha;
        btnImg.color = tempColor; 
     }   
  
}
