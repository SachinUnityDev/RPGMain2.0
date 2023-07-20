using Common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Town
{


    public class TalkNTradeBtnView : MonoBehaviour
    {
        public BuildView buildView;

        bool isJustTalk = false; 
        void Start() // this game obj wil toggle on and off should avoid multiple subscriptions
        {
            DialogueService.Instance.OnDialogueLsDsply += HideBtns; 
            DialogueService.Instance.OnDialogueStart += (DialogueNames d) => HideBtns();
           // DialogueService.Instance.OnDialogueEnd += ShowBtns;
            DialogueService.Instance.OnDialogueEnd += OnDeSelect;

            TradeService.Instance.OnTradeStart += HideBtns;
           // TradeService.Instance.OnTradeEnds += ShowBtns;
            TradeService.Instance.OnTradeEnds += OnDeSelect; 
        }
        public void InitTalkNTrade(NPCIntData nPCInteractData, BuildView buildView)
        {
            this.buildView = buildView;
            isJustTalk = !nPCInteractData.allInteract.Any(t => t.nPCIntType == IntType.Trade); 
            if (!isJustTalk)
            {   
                ShowBtns();
                transform.GetChild(0).GetComponent<TalkNTradeBtnPtrEvents>().InitTalkNTrade(nPCInteractData, this);
                transform.GetChild(1).GetComponent<TalkNTradeBtnPtrEvents>().InitTalkNTrade(nPCInteractData, this);
            }                
            else
            {   // talk panel
                HideBtns();
                transform.GetChild(0).GetComponent<TalkNTradeBtnPtrEvents>().InitTalkNTrade(nPCInteractData, this);
                OnSelect(IntType.Talk);
            }            
        }
        public void InitTalkNTrade(CharIntData charInteractData, BuildView buildView)
        {
            this.buildView = buildView;
            isJustTalk = !charInteractData.allInteract.Any(t => t.nPCIntType == IntType.Trade); 
            if (!isJustTalk)
            {
                ShowBtns();
                transform.GetChild(0).GetComponent<TalkNTradeBtnPtrEvents>().InitTalkNTrade(charInteractData, this);
                transform.GetChild(1).GetComponent<TalkNTradeBtnPtrEvents>().InitTalkNTrade(charInteractData, this);
            }
            else
            {   // talk panel
                HideBtns();
                transform.GetChild(0).GetComponent<TalkNTradeBtnPtrEvents>().InitTalkNTrade(charInteractData, this);
            }          
        }
        public void OnSelect(IntType intType)
        {
            for (int i = 0; i < 2; i++)
            {
                if(intType == IntType.Talk)
                {
                    transform.GetChild(0).GetComponent<TalkNTradeBtnPtrEvents>().OnSelect();
                    transform.GetChild(1).GetComponent<TalkNTradeBtnPtrEvents>().OnDeSelect();
                }
                else  // Trade  init 
                {
                    TradeService.Instance.On_TradeStart(); 
                    transform.GetChild(0).GetComponent<TalkNTradeBtnPtrEvents>().OnDeSelect();
                    transform.GetChild(1).GetComponent<TalkNTradeBtnPtrEvents>().OnSelect();
                }
            }
            HideBtns();
        }
        public void OnDeSelect()
        {
            transform.GetChild(0).GetComponent<TalkNTradeBtnPtrEvents>().OnDeSelect();
            transform.GetChild(1).GetComponent<TalkNTradeBtnPtrEvents>().OnDeSelect();
        }

        void ShowBtns()
        {
            if (isJustTalk) return;
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
        }
        public void HideBtns()
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}