using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{


    public class TalkNTradeBtnView : MonoBehaviour
    {
        public BuildView buildView; 
        void Start()
        {
            DialogueService.Instance.OnDialogueStart += (DialogueNames d) => HideBtns();
            DialogueService.Instance.OnDialogueEnd += ShowBtns;
        }

        public void InitTalkNTrade(NPCIntData nPCInteractData, BuildView buildView)
        {
            this.buildView = buildView;
            transform.GetChild(0).GetComponent<TalkNTradeBtnPtrEvents>().InitTalkNTrade(nPCInteractData, this);
            transform.GetChild(1).GetComponent<TalkNTradeBtnPtrEvents>().InitTalkNTrade(nPCInteractData, this);
        }
        public void InitTalkNTrade(CharIntData charInteractData, BuildView buildView)
        {
            this.buildView = buildView;
            transform.GetChild(0).GetComponent<TalkNTradeBtnPtrEvents>().InitTalkNTrade(charInteractData, this);
            transform.GetChild(1).GetComponent<TalkNTradeBtnPtrEvents>().InitTalkNTrade(charInteractData, this);
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
                else
                {
                    transform.GetChild(0).GetComponent<TalkNTradeBtnPtrEvents>().OnDeSelect();
                    transform.GetChild(1).GetComponent<TalkNTradeBtnPtrEvents>().OnSelect();
                }
            }
        }

        void ShowBtns()
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
        }
        void HideBtns()
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}