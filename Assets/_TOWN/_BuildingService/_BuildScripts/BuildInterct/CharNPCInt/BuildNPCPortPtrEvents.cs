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
        [SerializeField] Transform charPort; 

        [Header("Interact Data")]
        [SerializeField] NPCIntData interactData;
        [SerializeField] BuildingModel buildModel;
        BuildView buildView;
        private void Awake()
        {                
           // isShown= false;
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
            NPCSO npcSO = CharService.Instance.allNpcSO.GetNPCSO(interactData.nPCNames);
            charPort.GetComponent<Image>().sprite = npcSO.npcHexPortrait;
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
                talkPanel.GetComponent<DialogueViewController1>().ShowDialogueList(CharNames.None, interactData.nPCNames); 
                
            }
        }

     
        #endregion
    }
}

