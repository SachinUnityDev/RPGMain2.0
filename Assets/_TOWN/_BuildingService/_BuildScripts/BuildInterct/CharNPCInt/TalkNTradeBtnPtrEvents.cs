using Common;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace Town
{
    public class TalkNTradeBtnPtrEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] IntType intType;
        TalkNTradeBtnView talkNTradeBtnView;
        NPCIntData nPCIntData;
        CharIntData charIntData;

        [Header("Sprite Ref")]
        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite spriteHL;
        Image img; 
        private void Awake()
        {

            img= GetComponent<Image>();
            
        }
        private void Start()
        {
            img.sprite = spriteN;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            talkNTradeBtnView.OnSelect(intType);
            img.sprite = spriteN;
        }

        public void InitTalkNTrade(NPCIntData nPCIntData, TalkNTradeBtnView talkNTradeBtnView)
        {
            this.talkNTradeBtnView= talkNTradeBtnView;
            this.nPCIntData = nPCIntData;         
        }
        public void InitTalkNTrade(CharIntData charIntData, TalkNTradeBtnView talkNTradeBtnView)
        {
            this.talkNTradeBtnView = talkNTradeBtnView;
            if (charIntData.allInteract.Any(t => t.nPCIntType == IntType.Talk))
            {
                talkNTradeBtnView.OnSelect(IntType.Talk);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
           img.sprite = spriteHL;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
           img.sprite = spriteN; 
        }

        public void OnSelect()
        {
            transform.DOScale(1.25f, 0.4f);
            if (intType == IntType.Talk)
            {
                Transform buildViewTrans = talkNTradeBtnView.buildView.transform;
                if(nPCIntData != null)
                    DialogueService.Instance.ShowDialogueLs(CharNames.None, nPCIntData.nPCNames, buildViewTrans);
                if (charIntData != null)
                    DialogueService.Instance.ShowDialogueLs(charIntData.compName, NPCNames.None, buildViewTrans);
            }                
            if (intType == IntType.Trade)
            {
                BuildingNames buildName = talkNTradeBtnView.buildView.BuildingName;
                talkNTradeBtnView.buildView.TradePanel.GetComponent<TradeView>()
                    .InitTradeView(nPCIntData.nPCNames, buildName);
            }
        }

        public void OnDeSelect()
        {
            transform.DOScale(1f, 0.4f);
        }
    }
}