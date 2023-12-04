using Common;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Combat
{


    public class FortCircleView : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] Sprite fearfulSprite;
        [SerializeField] Sprite faithfulSprite;

        void Start()
        {
            CombatEventService.Instance.OnCharClicked += FillFort;
           
        }
        private void OnDisable()
        {
            CombatEventService.Instance.OnCharClicked -= FillFort;
        }

        public void FillFort(CharController charController)
        {
            
            StatData fortData = charController.GetStat(StatName.fortitude);

            float val = (fortData.currValue - fortData.minLimit)/ (fortData.maxLimit - fortData.minLimit); 

            transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = val;
            
            transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = fortData.currValue.ToString();            
        }
    }
}