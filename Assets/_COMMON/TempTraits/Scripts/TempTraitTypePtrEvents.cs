using Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
public class TempTraitTypePtrEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler  
{
    TempTraitTypeSpriteData tempTraitTypeSpriteData;
    TempTraitSO tempTraitSO;
    [SerializeField] TextMeshProUGUI typeTxt;

    public void InitPtrEvents(TempTraitSO tempTraitSO, TempTraitTypeSpriteData tempTraitTypeSprites)
    {
        this.tempTraitSO= tempTraitSO;  
        this.tempTraitTypeSpriteData= tempTraitTypeSprites; 
        transform.GetComponent<Image>().sprite = tempTraitTypeSprites.iconSprite;
        typeTxt.text = tempTraitSO.tempTraitName.ToString().CreateSpace();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        typeTxt.text = tempTraitSO.tempTraitType.ToString().CreateSpace();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        typeTxt.text = tempTraitSO.tempTraitName.ToString().CreateSpace();
    }
}
