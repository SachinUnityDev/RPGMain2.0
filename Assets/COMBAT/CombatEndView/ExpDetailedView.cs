using Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Combat
{
    public class ExpDetailedView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("TBR")]
        [SerializeField] TextMeshProUGUI expTxt;
        [SerializeField] Color colorN;
        [SerializeField] Color colorHL;
        [SerializeField] Transform OnHoverExp;

        int sharedExp = 0;
        int manualExp = 0;
        int netExp = 0;
        int firstBloodExp = 0;
        int killsNSavesExp = 0; 

        [Header("Global var")]        
        [SerializeField] CharModel charModel; 
        public void InitExp(CharModel charModel, int sharedExp, int firstBloodExp, int killsNSavesExp)
        {            
            this.charModel= charModel;
            this.sharedExp= sharedExp;
            this.firstBloodExp= firstBloodExp;
            this.killsNSavesExp= killsNSavesExp;
            manualExp = 0;
            netExp = sharedExp;
            AddNPrintExpTxtOnTop();
        }

        void AddNPrintExpTxtOnTop()
        {
            netExp = sharedExp + manualExp + firstBloodExp;
            string signStr = sharedExp > 0 ? "+" : "";
            expTxt.text = $"{signStr} {netExp} Exp";
            FillDetails();
        }
        public void AddManualExpDsply(int manualExp)
        {
            this.manualExp= manualExp;
            
            AddNPrintExpTxtOnTop(); 
          
        }

        void FillDetails()
        {

            string signStr =   firstBloodExp > 0 ? "+" : "";
            OnHoverExp.GetChild(0).GetComponent<TextMeshProUGUI>().text
                                                    = $"First Blood: {signStr}{firstBloodExp} Exp";
            signStr = sharedExp > 0 ? "+" : "";
            OnHoverExp.GetChild(1).GetComponent<TextMeshProUGUI>().text
                                                    = $"Kills/Saves: {signStr}{killsNSavesExp} Exp";

            signStr = sharedExp > 0 ? "+" : "";
            OnHoverExp.GetChild(2).GetComponent<TextMeshProUGUI>().text
                                                    = $"Shared: {signStr}{sharedExp} Exp";


            signStr = manualExp > 0 ? "+" : "";
            if(manualExp == 0)
            {
                OnHoverExp.GetChild(3).GetComponent<TextMeshProUGUI>().text
                                                    = "";
            }
            else
            {
                OnHoverExp.GetChild(3).GetComponent<TextMeshProUGUI>().text
                                                    = $"High Merit: {signStr}{manualExp} Exp";
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            expTxt.color= colorHL;
            OnHoverExp.gameObject.SetActive(true);  
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            expTxt.color = colorN;
            OnHoverExp.gameObject.SetActive(false);
        }

        void Start()
        {
            expTxt.color = colorN;
        }
    }
}