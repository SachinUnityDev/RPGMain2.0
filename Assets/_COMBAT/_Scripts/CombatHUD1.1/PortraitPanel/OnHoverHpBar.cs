using Common;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Combat
{
    public class OnHoverHpBar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] TextMeshProUGUI hpTxt;
        [SerializeField] TextMeshProUGUI stmTxt;

        string charNameStr;
        [SerializeField] StatData hpStatData;
        [SerializeField] AttribData vigorData;

        [SerializeField] StatData stmStatData;
        [SerializeField] AttribData wpData;




        private void OnEnable()
        {
            hpTxt.transform.parent.gameObject.SetActive(false);
            SceneManager.activeSceneChanged += OnActiveSceneChg;

        }
        private void OnDisable()
        {
            SceneManager.activeSceneChanged -= OnActiveSceneChg;
        }
        void OnActiveSceneChg(Scene curr, Scene next)
        {
            if (next.name == "COMBAT")
            {
                GetRef();
            }
        }
        void GetRef()
        {
            hpTxt = FindObjectOfType<HpTxtView>(true).GetComponent<TextMeshProUGUI>();
            stmTxt = FindObjectOfType<StmTxtView>(true).GetComponent<TextMeshProUGUI>();
        }
        public void InitOnHoverTxt(CharController charController)
        {
            if (hpTxt == null || stmTxt == null)
            {
                GetRef(); 
            }
            hpStatData = charController.GetStat(StatName.stamina);
            vigorData = charController.GetAttrib(AttribName.vigor);

            stmStatData = charController.GetStat(StatName.stamina);
            wpData = charController.GetAttrib(AttribName.willpower);
            charNameStr = charController.charModel.charNameStr;
            hpTxt.transform.parent.gameObject.SetActive(false);
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (hpTxt == null || stmTxt == null || hpStatData == null || vigorData == null || stmStatData == null || wpData == null)
            {
                Debug.LogError("One or more required references are null.");
                return;
            }
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