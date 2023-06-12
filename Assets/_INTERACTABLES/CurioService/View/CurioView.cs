using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public class CurioView : MonoBehaviour, IPanel
    {
        [Header(" TBR")]
        [SerializeField] CurioViewPg1PtrEvents curioPg1; 
        [SerializeField] CurioViewPg2PtrEvents curioPg2; 

        [Header("Curio Model, base and events")]
        public CurioModel curioModel;
        [SerializeField] int curioNo; // very important 
        public CurioBase curioBase; 
        CurioColEvents curioColEvents;


        public void InitCurioView(CurioColEvents curioColEvents, CurioModel curioModel, int curioNo)
        {
           this.curioNo= curioNo;
           this.curioModel = curioModel;         
           this.curioColEvents= curioColEvents;
            curioBase = CurioService.Instance
                        .curioController.GetCurioBase(curioModel.curioName); 
           Load(); 
            InitPage1();
        }
        void InitPage1()
        {
            curioPg1.InitPage1(this, curioModel, curioColEvents, curioNo);
            curioPg2.InitPage2(this, curioModel, curioColEvents, curioNo);
            curioPg1.gameObject.SetActive(true);
            curioPg2.gameObject.SetActive(false);
        }
        public void ShowPage2()
        {
            curioPg1.gameObject.SetActive(false);
            curioPg2.gameObject.SetActive(true);
        }

        
        public void Load()
        {
            UIControlServiceGeneral.Instance.TogglePanel(this.gameObject,true); 
        }
        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(this.gameObject, false);
        }
        public void Init()
        {
            
        }
    }
}

