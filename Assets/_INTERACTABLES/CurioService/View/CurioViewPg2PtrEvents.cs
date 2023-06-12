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
        [SerializeField] TextMeshProUGUI resultDesc;
        [SerializeField] TextMeshProUGUI resultTxt;
        [SerializeField] Transform currTrans;

        [Header("Continue Btn")]
        [SerializeField] Button continueBtn;


        [Header("global Var")]
        [SerializeField] CurioView curioView;
        [SerializeField] CurioModel curioModel;
        [SerializeField] CurioColEvents curioColEvents;

        [SerializeField] int curioNo;

        void Start()
        {
            continueBtn.onClick.AddListener(OnContinueBtnPressed); 
        }

        public void InitPage2(CurioView curioView, CurioModel curioModel
                                , CurioColEvents curioColEvents, int curioNo)
        {
            this.curioView = curioView;
            this.curioModel = curioModel;
            this.curioColEvents = curioColEvents;
            this.curioNo = curioNo;
        }

        void OnContinueBtnPressed()
        {
            curioColEvents.OnContinue();
        }
        
    }
}