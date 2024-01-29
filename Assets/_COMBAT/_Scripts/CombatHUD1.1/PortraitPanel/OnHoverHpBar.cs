using Common;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Combat
{
    public class OnHoverHpBar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] TextMeshProUGUI hpTxt;
        [SerializeField] TextMeshProUGUI stmTxt;

        string charNameStr;
        StatData hpStatData;
        AttribData vigorData;

        StatData stmStatData;
        AttribData wpData;

        private void OnEnable()
        {
            hpTxt.transform.parent.gameObject.SetActive(false);
        }

        public void InitOnHoverTxt(CharController charController)
        {
            hpStatData = charController.GetStat(StatName.stamina);
            vigorData = charController.GetAttrib(AttribName.vigor);

            stmStatData = charController.GetStat(StatName.stamina);
            wpData = charController.GetAttrib(AttribName.willpower);
            charNameStr = charController.charModel.charNameStr;
            hpTxt.transform.parent.gameObject.SetActive(false);
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            hpTxt.transform.parent.gameObject.SetActive(true);
            hpTxt.text = $"{hpStatData.currValue}/{vigorData.currValue * 4}";
            stmTxt.text = $"{stmStatData.currValue}/{wpData.currValue * 3}";
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            hpTxt.transform.parent.gameObject.SetActive(false);
            stmTxt.text = charNameStr;
        }
    }
}