using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;
using Common;

namespace Town
{
    public class BuildCharPortPtrEvents : MonoBehaviour
    {
        [Header("Char Port")]
        [SerializeField] Transform charPort;

        [Header("Interact Data")]
        [SerializeField] CharIntData interactData;
        [SerializeField] BuildingModel buildModel;
        BuildView buildView;
        private void Awake()
        {
            // isShown= false;
        }

        public void InitPortPtrEvents(CharIntData charIntdata, BuildingModel buildModel
                                        , BuildView buildView)
        {
            this.interactData = charIntdata;
            this.buildModel = buildModel;
            this.buildView = buildView;
            FillPort();
        }
        void FillPort()
        {
            CharacterSO charSO = CharService.Instance.allCharSO.GetCharSO(interactData.compName);
            charPort.GetComponent<Image>().sprite = charSO.charHexPortrait;
        }
        void InitBtn()
        {
            // on build View Init talkNTrade(Interact data ) 
            //close talk n trade
        }

        #region INTERACT BTN 
        public void OnPointerClick(PointerEventData eventData)
        {
            // open talk panel in build view
            if (interactData.allInteract.Count > 0)
            {
                buildView.OnPortSelect(interactData, null);
            }

            if (interactData.allInteract.Any(t => t.nPCIntType == IntType.Talk))
            {
                Transform talkPanel =
                    buildView.GetNPCInteractPanel(IntType.Talk);
                talkPanel.gameObject.SetActive(true);
                talkPanel.GetComponent<DialogueView>().ShowDialogueList(interactData.compName
                                                            , NPCNames.None);
            }
        }
        #endregion
    }
}
