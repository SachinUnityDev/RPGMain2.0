using Common;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Town;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace Town
{
    public class BuildNPCPortPtrEvents : MonoBehaviour, IPointerClickHandler
    { 
                                        
        [Header("Char Port")]
        [SerializeField] Image charPortImg;
        [SerializeField] Image bgPortImg; 

        [Header("Interact Data")]
        [SerializeField] NPCIntData interactData;
        [SerializeField] BuildingModel buildModel;
        BuildView buildView;
        private void Start()
        {                
           bgPortImg= transform.GetChild(0).GetComponent<Image>();
            charPortImg = transform.GetChild(1).GetComponent<Image>();
        }

        public void InitPortPtrEvents(NPCIntData npcInteractData, BuildingModel buildModel
                                        , BuildView buildView)
        {
            this.interactData= npcInteractData;
            this.buildModel= buildModel;
            this.buildView= buildView;
            FillPort(); 
        }
        void FillPort()
        {
            bgPortImg = transform.GetChild(0).GetComponent<Image>();
            charPortImg = transform.GetChild(1).GetComponent<Image>();

            NPCSO npcSO = CharService.Instance.allNpcSO.GetNPCSO(interactData.nPCNames);
            charPortImg.sprite = npcSO.npcHexPortrait;
            bgPortImg.sprite = CharService.Instance.allNpcSO.HexPortBgN;
        }

        #region INTERACT BTN 
        public void OnPointerClick(PointerEventData eventData)
        {
           // open talk panel in build view
            if (interactData.allInteract.Count>0)
            {
                buildView.OnPortSelect(null,interactData); 
            }

            if (interactData.allInteract.Any(t=>t.nPCIntType == IntType.Talk))
            {
                Transform talkPanel =
                    buildView.GetNPCInteractPanel(IntType.Talk);
                talkPanel.gameObject.SetActive(true);
                if(interactData != null)
                {
                    Debug.Log(" dial " + interactData.nPCNames + talkPanel.GetComponent<DialogueView>().gameObject.name); 
                    talkPanel.GetComponent<DialogueView>().ShowDialogueList(CharNames.None, interactData.nPCNames);
                }
                
                
            }
        }

        #endregion
    }
}

