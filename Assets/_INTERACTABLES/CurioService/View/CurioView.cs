using Common;
using DG.Tweening;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{


    public class CurioView : MonoBehaviour, INotify
    {
        [Header(" TBR")]
        [SerializeField] CurioViewPg1PtrEvents curioPg1; 
        [SerializeField] CurioViewPg2PtrEvents curioPg2; 

        [Header("Curio Model, base and events")]
        public CurioModel curioModel;
        [SerializeField] int curioNo; // very important 
        public CurioBase curioBase; 
        CurioColEvents curioColEvents;

        [Header("Notify Box View")]
        NotifyBoxView notifyBoxView; 
        public NotifyName notifyName { get ; set; }
        public bool isDontShowItAgainTicked { get; set; }

        public void InitCurioView(CurioColEvents curioColEvents, CurioModel curioModel, int curioNo)
        {
           this.curioNo= curioNo;
           this.curioModel = curioModel;         
           this.curioColEvents= curioColEvents;
            curioBase = CurioService.Instance
                        .curioController.GetCurioBase(curioModel.curioName);           
            InitPage1();
            Load();
        }
        void InitPage1()
        {           
            curioPg1.InitPage1(this, curioModel, curioColEvents, curioNo);            
            curioPg1.gameObject.SetActive(true);
            curioPg2.gameObject.SetActive(false);
            notifyBoxView = transform.GetChild(2).GetComponent<NotifyBoxView>();
        }
        public void ShowPage2(Iitems item)
        {
            curioPg2.InitPage2(this, curioModel, curioColEvents, curioNo, item);
            curioPg1.gameObject.SetActive(false);
            curioPg2.gameObject.SetActive(true);
            // remove Item from player Comm inv
            InvService.Instance.invMainModel.RemoveItemFrmCommInv(item);
        }
        public void Load()
        {
           gameObject.SetActive(true);
        }
        public void UnLoad()
        {
            curioColEvents.OnContinue();
            gameObject.SetActive(false);            

        }

        public void OnNotifyAnsPressed()
        {
            LootService.Instance.lootView.UnLoad(); 
            UnLoad();
        }

        public void LootNotifyBoxChk()
        {          
            NotifyName notifyName = NotifyName.LootTaken;
            notifyBoxView.OnShowNotifyBox(this, notifyName);
            //notifyBoxView.transform.DOScale(0.5f, 0.01f);
            //notifyBoxView.transform.DOLocalMoveY(-100f, 0.01f);
        }

    }
}

