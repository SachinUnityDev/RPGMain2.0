using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 


namespace Quest
{
    public class CurioViewPg2PtrEvents : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI heading;
        [SerializeField] TextMeshProUGUI resultStr;
        [SerializeField] TextMeshProUGUI resultStr2;
        [SerializeField] Transform currTrans;

        [Header("Continue Btn")]
        [SerializeField] Button continueBtn;


        [Header("global Var")]
        [SerializeField] CurioView curioView;
        [SerializeField] CurioModel curioModel;
        [SerializeField] CurioColEvents curioColEvents;
        [SerializeField] CurioBase curioBase; 
        [SerializeField] int curioNo;

        void Start()
        {
            continueBtn.onClick.AddListener(OnContinueBtnPressed); 
        }

        public void InitPage2(CurioView curioView, CurioModel curioModel
                                , CurioColEvents curioColEvents, int curioNo, Iitems item)
        {
            this.curioView = curioView;
            this.curioModel = curioModel;
            this.curioColEvents = curioColEvents;
            this.curioNo = curioNo;
            curioBase = CurioService.Instance.curioController.GetCurioBase(curioModel.curioName);
            heading.text = curioModel.curioName.ToString().CreateSpace();
            InteractPressed(item); 
        }

        void InteractPressed(Iitems item)
        {
            if (item == null)
            {
                curioBase.CurioInteractWithoutTool();               
            }
            else
            {
                curioBase.CurioInteractWithTool(); 
            }
            resultStr.text = curioBase.resultStr;
            resultStr2.text = curioBase.resultStr2;
            FillNAddCurrency(); 
        }
        void FillNAddCurrency()
        {
            //FILL
            Currency curr =
                    curioModel.GetLootMoney().DeepClone();
            currTrans.GetComponent<DisplayCurrency>().Display(curr); 

            // ADD
            EcoServices.Instance.AddMoney2PlayerInv(curr);

        }
    
        void OnContinueBtnPressed()
        { 
            curioView.UnLoad();
        }
        
    }
}