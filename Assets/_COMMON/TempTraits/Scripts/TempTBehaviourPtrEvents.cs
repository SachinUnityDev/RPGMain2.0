using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace Common
{
    public class TempTBehaviourPtrEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        TempTBehaviourSpriteData tempTBehaviourSpriteData;
        TempTraitSO tempTraitSO;
        [SerializeField] TextMeshProUGUI behaviourTxt; 

        public void InitPtrEvents(TempTraitSO tempTraitSO, TempTBehaviourSpriteData tempTBehaviourSpriteData)
        {
            this.tempTraitSO= tempTraitSO;  
            this.tempTBehaviourSpriteData= tempTBehaviourSpriteData;    
            transform.GetComponent<Image>().sprite = tempTBehaviourSpriteData.iconSprite;
            behaviourTxt.text = tempTraitSO.tempTraitName.ToString().CreateSpace(); 
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            behaviourTxt.text = tempTraitSO.temptraitBehavior.ToString().CreateSpace();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            behaviourTxt.text = tempTraitSO.tempTraitName.ToString().CreateSpace();
        }
    }
}
